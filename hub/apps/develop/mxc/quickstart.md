---
title: "Quickstart: Run a workload with the MXC SDK"
description: Use the .NET, TypeScript, or Rust SDK to run a Windows workload with read-only inputs, a writable output folder, and denied network access.
author: GrantMeStrength
ms.author: jebishop
ms.topic: quickstart
ms.date: 10/27/2026
---

# Quickstart: Run a workload with the MXC SDK

Use the .NET, TypeScript, or Rust SDK to run a command in Windows process isolation. Each example creates a sample input, allow the workload to read it, and grant write access to a separate output folder. Network and UI permissions remain denied.

Your host app creates the policy and handles the result. The command runs inside MXC; the rest of the host app does not.

The examples use the MXC 1.0 V1 SDK APIs. The V1 SDKs use the stable `1.0.0` schema internally, so you don't specify a schema version in your request. See [Choose a compatible schema](policy.md#choose-a-compatible-schema).

## Prerequisites

You need:

- Windows 11, version 24H2 or later, with a supported ProcessContainer environment.
- For C#, the .NET 8 SDK or later and the [`Microsoft.Mxc.Sdk`](https://www.nuget.org/packages/Microsoft.Mxc.Sdk) NuGet package.
- For TypeScript, npm, the [`@microsoft/mxc-sdk`](https://www.npmjs.com/package/@microsoft/mxc-sdk) package, and Node.js 24.21.0 or later in the Node.js 24 release line, or Node.js 26.8.0 or later.
- For Rust, a stable Rust toolchain with the MSVC build tools, and the [`mxc-sdk`](https://crates.io/crates/mxc-sdk) crate. See [Set up your Rust development environment on Windows](../../../dev-environment/rust/setup.md).
- A writable working directory and PowerShell for the setup commands.

The instructions use the MXC 1.0 packages from NuGet.org, npm, and crates.io. No MXC source build is required.

Check [platform support](platform-support.md) before you run the example. A supported Windows version doesn't guarantee that every optional feature is available.

On some hosts, MXC runs process isolation in an AppContainer-based fallback tier. That tier needs one-time preparation of the system drive and the null device, and the null-device setting resets at every restart. When preparation is missing, MXC's host probe returns a warning that names the [`wxc-host-prep.exe`](releases.md#command-line-tools) subcommand to run. The TypeScript example prints these warnings from `getPlatformSupport()`, and you can also check a host by running `wxc-exec.exe --probe`. Because `wxc-host-prep.exe` requires administrator approval, have an administrator run the recommended command rather than running it from your app. For details, see [Host preparation](https://github.com/microsoft/mxc/blob/main/docs/backends/process-container/host-prep.md).

## 1. Create a project

# [.NET](#tab/dotnet)

Create a console app and add the MXC package from NuGet.org:

```powershell
dotnet new console -n MxcQuickstart
Set-Location MxcQuickstart
dotnet add package Microsoft.Mxc.Sdk
```

# [TypeScript](#tab/typescript)

Create a Node.js project, install MXC, and add `tsx` to run the TypeScript example:

```powershell
New-Item -ItemType Directory MxcQuickstart
Set-Location MxcQuickstart
npm init -y
npm pkg set type=module
npm install @microsoft/mxc-sdk
npm install --save-dev tsx
```

# [Rust](#tab/rust)

Create a binary crate and add the MXC crate from crates.io:

```powershell
cargo new mxc-quickstart
Set-Location mxc-quickstart
cargo add mxc-sdk
```

---

## 2. Define and run the workload

Process isolation fits this short-lived command because it needs selected host files and no retained environment. Other workloads can require a different [containment type](architecture.md#choose-a-containment-type).

The host creates `workspace\input` and `workspace\output` beneath the project directory. The contained command reads `input.txt` and writes `result.txt`. It doesn't receive write access to its input directory or access to other project files.

The host supplies a small, explicit environment instead of inheriting its own variables. When you supply an environment for a Windows ProcessContainer workload, include both `SystemRoot` and `LOCALAPPDATA`. MXC rejects the request before launch if either one is missing. The example points `LOCALAPPDATA`, `TEMP`, and `TMP` at the granted output directory so that temporary and per-user files stay within the policy.

# [.NET](#tab/dotnet)

Replace `Program.cs` with:

```csharp
using Microsoft.Mxc.Sdk.V1;

if (!OperatingSystem.IsWindows())
{
    Console.Error.WriteLine("This example requires Windows.");
    return 1;
}

try
{
    var support = MxcPlatform.GetPlatformSupport();
    if (!support.IsSupported ||
        !support.AvailableMethods.Contains(ContainmentBackend.ProcessContainer))
    {
        Console.Error.WriteLine(support.Reason ?? "Windows ProcessContainer is unavailable.");
        return 1;
    }

    string inputDirectory = Path.GetFullPath(@"workspace\input");
    string outputDirectory = Path.GetFullPath(@"workspace\output");
    string outputFile = Path.Combine(outputDirectory, "result.txt");
    string commandInterpreter = Path.Combine(Environment.SystemDirectory, "cmd.exe");

    Directory.CreateDirectory(inputDirectory);
    Directory.CreateDirectory(outputDirectory);
    File.WriteAllText(Path.Combine(inputDirectory, "input.txt"), "Hello from MXC!\n");

    var request = new ContainerRequest(
        $"\"{commandInterpreter}\" /d /c type input.txt > \"{outputFile}\"")
    {
        Containment = new Containment.Process(),
        Filesystem = new FilesystemPolicy
        {
            ReadonlyPaths = { inputDirectory, commandInterpreter },
            ReadwritePaths = { outputDirectory },
        },
        // Omitted network and UI permissions remain denied.
        WorkingDirectory = inputDirectory,
        Environment = new()
        {
            ["SystemRoot"] = Environment.GetFolderPath(Environment.SpecialFolder.Windows),
            ["LOCALAPPDATA"] = outputDirectory,
            ["TEMP"] = outputDirectory,
            ["TMP"] = outputDirectory,
        },
        TimeoutMs = 30_000,
    };

    ExecutionResult result = await MxcContainer.RunAsync(request);
    Console.Write(result.Stdout);
    Console.Error.Write(result.Stderr);
    foreach (string warning in result.Warnings)
    {
        Console.Error.WriteLine(warning);
    }

    if (result.TimedOut)
    {
        Console.Error.WriteLine("The workload exceeded its timeout.");
        return 1;
    }

    Console.WriteLine($"Sandbox exited with code {result.ExitCode}");
    if (result.ExitCode == 0)
    {
        Console.Write(File.ReadAllText(outputFile));
    }
    return result.ExitCode;
}
catch (MxcException ex)
{
    Console.Error.WriteLine(ex);
    if (ex.Remediation is not null)
    {
        Console.Error.WriteLine(ex.Remediation);
    }
    return 1;
}
```

Run the app:

```powershell
dotnet run
```

# [TypeScript](#tab/typescript)

Create `sandbox.ts` with:

```typescript
import {
  getPlatformSupport,
  MxcError,
  run,
  type ContainerRequest,
} from '@microsoft/mxc-sdk/v1';
import { mkdirSync, readFileSync, writeFileSync } from 'node:fs';
import { join, resolve } from 'node:path';

try {
  const systemRoot = process.env.SystemRoot;
  if (process.platform !== 'win32' || !systemRoot) {
    throw new Error('This example requires Windows and SystemRoot.');
  }

  const support = getPlatformSupport();
  if (!support.isSupported || !support.availableMethods.includes('processcontainer')) {
    throw new Error(support.reason ?? 'Windows ProcessContainer is unavailable.');
  }
  for (const warning of support.isolationWarnings ?? []) {
    console.error(warning);
  }

  const inputDirectory = resolve('workspace', 'input');
  const outputDirectory = resolve('workspace', 'output');
  const outputFile = join(outputDirectory, 'result.txt');
  const commandInterpreter = join(systemRoot, 'System32', 'cmd.exe');

  mkdirSync(inputDirectory, { recursive: true });
  mkdirSync(outputDirectory, { recursive: true });
  writeFileSync(join(inputDirectory, 'input.txt'), 'Hello from MXC!\n');

  const request: ContainerRequest = {
    command: `"${commandInterpreter}" /d /c type input.txt > "${outputFile}"`,
    containment: { type: 'process' },
    filesystem: {
      readonlyPaths: [inputDirectory, commandInterpreter],
      readwritePaths: [outputDirectory],
    },
    // Omitted network and UI permissions remain denied.
    workingDirectory: inputDirectory,
    environment: {
      SystemRoot: systemRoot,
      LOCALAPPDATA: outputDirectory,
      TEMP: outputDirectory,
      TMP: outputDirectory,
    },
    timeoutMs: 30_000,
  };

  const result = await run(request);
  process.stdout.write(result.stdout);
  process.stderr.write(result.stderr);
  for (const warning of result.warnings) {
    console.error(warning);
  }

  if (result.timedOut) {
    throw new Error('The workload exceeded its timeout.');
  }

  console.log(`Sandbox exited with code ${result.exitCode}`);
  if (result.exitCode === 0) {
    process.stdout.write(readFileSync(outputFile, 'utf8'));
  }
  process.exitCode = result.exitCode;
} catch (error) {
  console.error(error);
  if (error instanceof MxcError && error.remediation) {
    console.error(error.remediation);
  }
  process.exitCode = 1;
}
```

Run the app:

```powershell
npx tsx sandbox.ts
```

# [Rust](#tab/rust)

Replace `src\main.rs` with:

```rust
use std::env;
use std::error::Error;
use std::fs;
use std::io::{self, Write};
use std::path::Path;

use mxc_sdk::v1::{self, ContainerRequest, Containment, FilesystemPolicy, WaitResult};

fn main() {
    match run() {
        Ok(code) => std::process::exit(code),
        Err(error) => {
            eprintln!("{error}");
            std::process::exit(1);
        }
    }
}

fn run() -> Result<i32, Box<dyn Error>> {
    if !cfg!(windows) {
        return Err("This example requires Windows.".into());
    }

    let support = v1::platform_support();
    if !support.is_supported
        || !support.available_methods.iter().any(|method| method == "processcontainer")
    {
        let reason = support
            .reason
            .unwrap_or_else(|| "Windows ProcessContainer is unavailable.".into());
        return Err(reason.into());
    }

    let system_root = env::var("SystemRoot")?;
    let project_directory = env::current_dir()?;
    let input_directory = project_directory.join("workspace").join("input");
    let output_directory = project_directory.join("workspace").join("output");
    let output_file = output_directory.join("result.txt");
    let command_interpreter = Path::new(&system_root).join("System32").join("cmd.exe");

    fs::create_dir_all(&input_directory)?;
    fs::create_dir_all(&output_directory)?;
    fs::write(input_directory.join("input.txt"), "Hello from MXC!\n")?;

    let input_path = input_directory.display().to_string();
    let output_path = output_directory.display().to_string();
    let request = ContainerRequest {
        containment: Containment::Process,
        filesystem: Some(FilesystemPolicy {
            readonly_paths: vec![input_path.clone(), command_interpreter.display().to_string()],
            readwrite_paths: vec![output_path.clone()],
            ..Default::default()
        }),
        // Omitted network and UI permissions remain denied.
        working_directory: Some(input_path),
        environment: Some(vec![
            ("SystemRoot".into(), system_root),
            ("LOCALAPPDATA".into(), output_path.clone()),
            ("TEMP".into(), output_path.clone()),
            ("TMP".into(), output_path),
        ]),
        timeout_ms: Some(30_000),
        ..ContainerRequest::new(format!(
            "\"{}\" /d /c type input.txt > \"{}\"",
            command_interpreter.display(),
            output_file.display()
        ))
    };

    let result = match v1::run(request, Default::default()) {
        Ok(result) => result,
        Err(error) => {
            eprintln!("{error}");
            if let Some(remediation) = &error.remediation {
                eprintln!("{remediation}");
            }
            return Ok(1);
        }
    };
    io::stdout().write_all(&result.stdout)?;
    io::stderr().write_all(&result.stderr)?;
    for warning in &result.warnings {
        eprintln!("{warning}");
    }

    let exit_code = match result.outcome {
        WaitResult::Exited(code) => code,
        WaitResult::TimedOut => return Err("The workload exceeded its timeout.".into()),
    };

    println!("Sandbox exited with code {exit_code}");
    if exit_code == 0 {
        print!("{}", fs::read_to_string(&output_file)?);
    }
    Ok(exit_code)
}
```

Run the app:

```powershell
cargo run
```

---

Each version prints:

```console
Sandbox exited with code 0
Hello from MXC!
```

You can also inspect `workspace\output\result.txt`.

## 3. Verify a denied operation

Change only the output file declaration so the command attempts to write into the read-only input directory:

# [.NET](#tab/dotnet)

```csharp
string outputFile = Path.Combine(inputDirectory, "blocked.txt");
```

# [TypeScript](#tab/typescript)

```typescript
const outputFile = join(inputDirectory, 'blocked.txt');
```

# [Rust](#tab/rust)

```rust
let output_file = input_directory.join("blocked.txt");
```

---

Run the app again without changing the policy. Expect an access-denied error and a nonzero exit code, with no new `workspace\input\blocked.txt` file. Restore the original output file declaration and confirm that the permitted operation still succeeds.

If the permitted run fails, distinguish host setup or configuration errors from a resource denial. Use [Learning Mode](learning-mode.md) to investigate the latter; don't broaden the policy automatically.

## 4. Adapt the example

Replace the sample command and inputs with a trusted representative workload. Review required tool binaries, libraries, temporary files, and outputs. Add network or UI access only when the task requires it and the selected backend can enforce the intended restrictions.

Test permitted and denied operations on every target host. When you use Learning Mode to refine the policy, review each observation and finish with an enforcing run without learning or audit settings.

### Optional: use a coding agent

Give your agent the target platforms, SDK language and version, command, required resources, lifecycle, and isolation goals. You can adapt this prompt:

```text
Use the MXC Learn quickstart and the documentation for my installed SDK release.
Integrate this workload through the .NET, TypeScript, or Rust SDK used by this project.

First identify the command, target platforms, input and output paths, network
and UI requirements, and whether state must persist between commands.
Explain the containment choice and create the narrowest policy that fits.
Run both permitted and denied cases and report the effective permissions.

Do not execute the workload outside MXC to get around a failure. Ask for my
approval before expanding access, changing host settings, or using permissive
Learning Mode. Treat observed resource access as evidence, not automatic grants.
```

Review the agent's assumptions and resulting policy. An agent can assist with integration, but trusted host code or a person must approve changes to the workload's authority.

## Next steps

- Review [MXC architecture and containment models](architecture.md) before using persistent or specialized backends.
- Learn how to [author and validate an MXC policy](policy.md).
- Compare [MXC containment options and platform support](platform-support.md).
- Review the complete [Learning Mode workflow](learning-mode.md).
- Adapt the [MXC policy examples](examples.md) to other workloads.
- Find SDK packages and release notes in [MXC releases](releases.md).
