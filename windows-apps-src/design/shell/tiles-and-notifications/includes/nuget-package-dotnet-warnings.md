> [!IMPORTANT]
> .NET Framework desktop apps that still use packages.config must migrate to PackageReference, otherwise the Windows 10 SDKs won't be referenced correctly. In your project, right-click on "References", and click "Migrate packages.config to PackageReference".
> 
> .NET Core 3.0 WPF apps must update to .NET Core 3.1, otherwise the APIs will be absent.