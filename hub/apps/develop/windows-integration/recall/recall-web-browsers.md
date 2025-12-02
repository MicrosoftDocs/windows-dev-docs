---
title: Recall guidance for developers of web browsers
description: Learn how you should optimize your web browser for Recall.
ms.date: 05/21/2025
ms.topic: article
no-loc: [Recall]
---

# Recall guidance for developers of web browsers

Many web browsers support a concept of "InPrivate" browsing, where the user's history does not get saved.

To make sure that Recall doesn't save your user's browsing history while in modes like this, your app can use the [`SetInputScope`](/windows/win32/api/inputscope/nf-inputscope-setinputscope) function, setting the input scope to `IS_PASSWORD`.

> [!IMPORTANT]
> Your app must also have a `http` or `https` protocol handler registered before `SetInputScope` will support the behavior described in this article.

```csharp
[DllImport("msctf.dll", SetLastError = true)]
private static extern int SetInputScope(IntPtr hwnd, InputScope inputScope);

private new enum InputScope : int
{
    IS_DEFAULT = 0,
    IS_URL = 1,
    IS_FILE_FULLFILEPATH = 2,
    IS_PRIVATE = 0x1f // Input is treated as private (e.g. passwords)
}

private void EnterInPrivateMode()
{
    // Get your HWND. This will vary based on your UI Framework. WPF can use WindowInteropHelper, passing in your current Window.
    IntPtr hwnd = new WindowInteropHelper(this).Handle;

    // Then, set the input scope on the HWND to private
    SetInputScope(hwnd, InputScope.IS_PRIVATE);
}

private void ExitInPrivateMode()
{
    // Get your HWND. This will vary based on your UI Framework. WPF can use WindowInteropHelper, passing in your current Window.
    IntPtr hwnd = new WindowInteropHelper(this).Handle;

    // Then, set the input scope on the HWND to default
    SetInputScope(hwnd, InputScope.IS_DEFAULT);
}
```

Your app should suspend providing user activities while the user is in "private" browsing mode.
