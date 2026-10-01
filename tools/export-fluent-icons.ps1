#Requires -Version 5.1
<#
.SYNOPSIS
Exports the bundled MIT-licensed Fluent System Icons as SVG and/or PNG.
.EXAMPLE
powershell.exe -NoProfile -STA -File .\tools\export-fluent-icons.ps1 -OutputDirectory .\hub\apps\images\fluent -Format Both
.EXAMPLE
powershell.exe -NoProfile -STA -File .\tools\export-fluent-icons.ps1 -Icon design -Size 32 -Color '#57506B' -OutputDirectory .\artifacts
.NOTES
Uses local, pinned SVG sources, not the Segoe Fluent Icons font.
PNG export uses Windows WPF; run in Windows PowerShell with -STA.
Keep LICENSE.txt with redistributed output.
#>
[CmdletBinding()]
param(
    [ValidateSet('all', 'apps', 'design', 'feature', 'package', 'code', 'editor', 'terminal')]
    [string]$Icon = 'all',

    [Parameter(Mandatory = $true)]
    [string]$OutputDirectory,

    [ValidateSet(16, 20, 24, 32, 40, 48, 64)]
    [int]$Size = 24,

    [ValidatePattern('^#[0-9A-Fa-f]{6}$')]
    [string]$Color = '#737373',

    [ValidateSet('Svg', 'Png', 'Both')]
    [string]$Format = 'Both',

    [ValidateRange(1, 4)]
    [int]$PngScale = 2,

    [switch]$Force
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$sourceDirectory = Join-Path $PSScriptRoot 'fluent-icons'
$manifest = Get-Content -Raw -LiteralPath (Join-Path $sourceDirectory 'manifest.json') | ConvertFrom-Json
$entries = @($manifest.icons | Where-Object { $Icon -eq 'all' -or $_.name -eq $Icon })
if ($entries.Count -eq 0) {
    throw "No source is registered for icon '$Icon'."
}

$outputPath = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($OutputDirectory)
$licensePath = Join-Path $sourceDirectory 'LICENSE.txt'
$license = [System.IO.File]::ReadAllText($licensePath)
$utf8 = New-Object System.Text.UTF8Encoding($false)
$culture = [System.Globalization.CultureInfo]::InvariantCulture
$formats = if ($Format -eq 'Both') { @('svg', 'png') } else { @($Format.ToLowerInvariant()) }

if ($formats -contains 'png') {
    if ($env:OS -ne 'Windows_NT') {
        throw 'PNG export requires Windows. Use -Format Svg on other platforms.'
    }
    if ([System.Threading.Thread]::CurrentThread.ApartmentState -ne 'STA') {
        throw 'PNG export requires an STA thread. Run powershell.exe -NoProfile -STA -File tools\export-fluent-icons.ps1 with your arguments.'
    }
    Add-Type -AssemblyName WindowsBase
    Add-Type -AssemblyName PresentationCore
}

# Validate all inputs and destinations before producing any output.
$prepared = @(
    foreach ($entry in $entries) {
        $inputPath = Join-Path $sourceDirectory $entry.file
        if (-not (Test-Path -LiteralPath $inputPath -PathType Leaf)) {
            throw "Missing bundled source: $inputPath"
        }
        foreach ($extension in $formats) {
            $destination = Join-Path $outputPath "$($entry.name).$extension"
            if ((Test-Path -LiteralPath $destination) -and -not $Force) {
                throw "Output already exists: $destination. Use -Force to replace it."
            }
            if ([System.IO.Path]::GetFullPath($destination) -eq [System.IO.Path]::GetFullPath($inputPath)) {
                throw 'The output must not overwrite a bundled source.'
            }
        }

        $settings = New-Object System.Xml.XmlReaderSettings
        $settings.DtdProcessing = [System.Xml.DtdProcessing]::Prohibit
        $settings.XmlResolver = $null
        $reader = [System.Xml.XmlReader]::Create($inputPath, $settings)
        try {
            $document = New-Object System.Xml.XmlDocument
            $document.XmlResolver = $null
            $document.Load($reader)
        }
        finally {
            $reader.Dispose()
        }
        $svg = $document.DocumentElement
        if ($svg.LocalName -ne 'svg' -or $svg.NamespaceURI -ne 'http://www.w3.org/2000/svg') {
            throw "Source is not an SVG: $inputPath"
        }
        $viewBox = @($svg.GetAttribute('viewBox').Split(' ', [System.StringSplitOptions]::RemoveEmptyEntries) |
            ForEach-Object { [double]::Parse($_, $culture) })
        if ($viewBox.Count -ne 4 -or $viewBox[0] -ne 0 -or $viewBox[1] -ne 0 -or
            $viewBox[2] -le 0 -or [double]::IsInfinity($viewBox[2]) -or
            [double]::IsNaN($viewBox[2]) -or $viewBox[2] -ne $viewBox[3]) {
            throw "Expected an untransformed square SVG viewBox starting at 0,0: $inputPath"
        }
        foreach ($attribute in $svg.Attributes) {
            if ($attribute.Name -notin @('xmlns', 'width', 'height', 'viewBox', 'fill')) {
                throw "Unsupported SVG attribute '$($attribute.Name)' in $inputPath"
            }
        }
        $paths = @(
            foreach ($node in $svg.ChildNodes) {
                if ($node.NodeType -ne [System.Xml.XmlNodeType]::Element) { continue }
                if ($node.LocalName -ne 'path' -or $node.NamespaceURI -ne $svg.NamespaceURI) {
                    throw "Only filled path elements are supported: $inputPath"
                }
                foreach ($attribute in $node.Attributes) {
                    if ($attribute.Name -notin @('d', 'fill', 'fill-rule')) {
                        throw "Unsupported path attribute '$($attribute.Name)' in $inputPath"
                    }
                }
                $data = $node.GetAttribute('d')
                if ([string]::IsNullOrWhiteSpace($data) -or $node.GetAttribute('fill') -eq 'none') {
                    throw "Expected a nonempty filled path: $inputPath"
                }
                $rule = $node.GetAttribute('fill-rule')
                if (-not $rule) { $rule = 'nonzero' }
                if ($rule -notin @('nonzero', 'evenodd')) {
                    throw "Unsupported fill rule '$rule' in $inputPath"
                }
                $geometry = $null
                if ($formats -contains 'png') {
                    $prefix = if ($rule -eq 'nonzero') { 'F1 ' } else { 'F0 ' }
                    $geometry = [System.Windows.Media.Geometry]::Parse($prefix + $data)
                }
                [pscustomobject]@{ Data = $data; Rule = $rule; Geometry = $geometry }
            }
        )
        if ($paths.Count -eq 0) { throw "No paths in source: $inputPath" }
        [pscustomobject]@{ Entry = $entry; Paths = $paths; Extent = $viewBox[2] }
    }
)

[void][System.IO.Directory]::CreateDirectory($outputPath)
$outputLicense = Join-Path $outputPath 'LICENSE.txt'
if ((Test-Path -LiteralPath $outputLicense) -and [System.IO.File]::ReadAllText($outputLicense) -ne $license) {
    throw "A different LICENSE.txt already exists in $outputPath. Choose a dedicated output directory."
}
[System.IO.File]::WriteAllText($outputLicense, $license, $utf8)

foreach ($item in $prepared) {
    $basePath = Join-Path $outputPath $item.Entry.name
    if ($formats -contains 'svg') {
        $extent = $item.Extent.ToString($culture)
        $sourceUrl = "https://github.com/microsoft/fluentui-system-icons/blob/$($manifest.revision)/$($item.Entry.upstreamPath)"
        $lines = @(
            '<!--'
            "Source: $sourceUrl"
            'Modified: recolored and sized by tools/export-fluent-icons.ps1. License: LICENSE.txt.'
            '-->'
            "<svg xmlns=`"http://www.w3.org/2000/svg`" width=`"$Size`" height=`"$Size`" viewBox=`"0 0 $extent $extent`">"
        )
        foreach ($part in $item.Paths) {
            $data = [System.Security.SecurityElement]::Escape($part.Data)
            $lines += "  <path fill=`"$Color`" fill-rule=`"$($part.Rule)`" d=`"$data`"/>"
        }
        $lines += '</svg>'
        [System.IO.File]::WriteAllText("$basePath.svg", ($lines -join "`n") + "`n", $utf8)
    }
    if ($formats -contains 'png') {
        $pixels = $Size * $PngScale
        $visual = New-Object System.Windows.Media.DrawingVisual
        $drawing = $visual.RenderOpen()
        try {
            $factor = $pixels / $item.Extent
            $drawing.PushTransform((New-Object System.Windows.Media.ScaleTransform($factor, $factor)))
            $brush = New-Object System.Windows.Media.SolidColorBrush([System.Windows.Media.ColorConverter]::ConvertFromString($Color))
            foreach ($part in $item.Paths) { $drawing.DrawGeometry($brush, $null, $part.Geometry) }
            $drawing.Pop()
        }
        finally {
            $drawing.Close()
        }
        $bitmap = New-Object System.Windows.Media.Imaging.RenderTargetBitmap(
            $pixels, $pixels, 96, 96, [System.Windows.Media.PixelFormats]::Pbgra32)
        $bitmap.Render($visual)
        $encoder = New-Object System.Windows.Media.Imaging.PngBitmapEncoder
        $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($bitmap))
        $stream = [System.IO.File]::Create("$basePath.png")
        try { $encoder.Save($stream) }
        finally { $stream.Dispose() }
    }
    [pscustomobject]@{ Icon = $item.Entry.name; Size = $Size; Color = $Color; Format = $Format; Output = $basePath }
}
