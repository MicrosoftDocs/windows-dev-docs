---
title: Using winapp CLI with Flutter
description: Learn how to use the winapp CLI with Flutter desktop applications to add package identity, access Windows APIs, and package as MSIX.
ms.date: 02/20/2026
ms.topic: how-to
---

# Using winapp CLI with Flutter

This guide demonstrates how to use the winapp CLI with a Flutter application to add package identity and package your app as an MSIX.

Package identity is a core concept in the Windows app model. It allows your application to access specific Windows APIs (like Notifications, Security, AI APIs, etc.), have a clean install/uninstall experience, and more.

## Prerequisites

1. **Flutter SDK**: Install Flutter following the [official guide](https://docs.flutter.dev/install/quick).

2. **winapp CLI**: Install the `winapp` CLI via winget:

    ```powershell
    winget install Microsoft.winappcli --source winget
    ```

## 1. Create a new Flutter app

Follow the guide at the official Flutter docs to create a new application and run it.

## 2. Update code to check identity

Add the `ffi` package:

```powershell
flutter pub add ffi
```

Replace the contents of `lib/main.dart` with the following code that checks for package identity using the Windows `GetCurrentPackageFamilyName` API via Dart FFI:

```dart
import 'dart:ffi';
import 'dart:io' show Platform;

import 'package:ffi/ffi.dart';
import 'package:flutter/material.dart';

String? getPackageFamilyName() {
  if (!Platform.isWindows) return null;

  final kernel32 = DynamicLibrary.open('kernel32.dll');
  final getCurrentPackageFamilyName = kernel32.lookupFunction<
      Int32 Function(Pointer<Uint32>, Pointer<Uint16>),
      int Function(
          Pointer<Uint32>, Pointer<Uint16>)>('GetCurrentPackageFamilyName');

  final length = calloc<Uint32>();
  try {
    final result =
        getCurrentPackageFamilyName(length, Pointer<Uint16>.fromAddress(0));
    if (result != 122) return null; // ERROR_INSUFFICIENT_BUFFER = 122

    final namePtr = calloc<Uint16>(length.value);
    try {
      final result2 = getCurrentPackageFamilyName(length, namePtr);
      if (result2 == 0) {
        return namePtr.cast<Utf16>().toDartString();
      }
      return null;
    } finally {
      calloc.free(namePtr);
    }
  } finally {
    calloc.free(length);
  }
}

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;
  late final String? _packageFamilyName;

  @override
  void initState() {
    super.initState();
    _packageFamilyName = getPackageFamilyName();
  }

  void _incrementCounter() {
    setState(() { _counter++; });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Container(
              padding: const EdgeInsets.all(16),
              margin: const EdgeInsets.only(bottom: 24),
              decoration: BoxDecoration(
                color: _packageFamilyName != null
                    ? Colors.green.shade50
                    : Colors.orange.shade50,
                borderRadius: BorderRadius.circular(8),
                border: Border.all(
                  color: _packageFamilyName != null
                      ? Colors.green
                      : Colors.orange,
                ),
              ),
              child: Text(
                _packageFamilyName != null
                    ? 'Package Family Name:\n$_packageFamilyName'
                    : 'Not packaged',
                textAlign: TextAlign.center,
                style: Theme.of(context).textTheme.bodyLarge,
              ),
            ),
            const Text('You have pushed the button this many times:'),
            Text('$_counter',
                style: Theme.of(context).textTheme.headlineMedium),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ),
    );
  }
}
```

## 3. Run without identity

Build and run the app:

```powershell
flutter build windows
.\build\windows\x64\runner\Release\flutter_app.exe
```

You should see the app with an orange "Not packaged" indicator.

## 4. Initialize project with winapp CLI

```powershell
winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default
- **Publisher name**: Press Enter to accept the default or enter your name
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (flutter_app.exe)
- **Setup SDKs**: Select "Stable SDKs" to download Windows App SDK and generate C++ headers

## 5. Debug with identity

1. **Build the app**:

    ```powershell
    flutter build windows
    ```

2. **Apply debug identity**:

    ```powershell
    winapp create-debug-identity .\build\windows\x64\runner\Release\flutter_app.exe
    ```

3. **Run the executable**:

    ```powershell
    .\build\windows\x64\runner\Release\flutter_app.exe
    ```

You should see the app with a green indicator showing the Package Family Name.

> [!NOTE]
> After running `flutter clean` or rebuilding, you'll need to re-run `create-debug-identity` since the executable is replaced.

## 6. Package with MSIX

1. **Build for release**:

    ```powershell
    flutter build windows
    ```

2. **Prepare package directory**:

    ```powershell
    mkdir dist
    copy .\build\windows\x64\runner\Release\* .\dist\ -Recurse
    ```

3. **Generate a development certificate**:

    ```powershell
    winapp cert generate --if-exists skip
    ```

4. **Package and sign**:

    ```powershell
    winapp pack .\dist --cert .\devcert.pfx
    ```

5. **Install the certificate** (run as administrator):

    ```powershell
    winapp cert install .\devcert.pfx
    ```

6. **Install the package**:

    ```powershell
    Add-AppxPackage .\flutter-app.msix
    ```

> [!TIP]
> - The Microsoft Store signs the MSIX for you, no need to sign before submission.
> - [Azure Trusted Signing](https://azure.microsoft.com/products/trusted-signing) is a great way to manage certificates securely for CI/CD pipelines.

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
- [Windows App SDK documentation](/windows/apps/windows-app-sdk/)
