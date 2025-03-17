---
title: Microsoft Copilot hardware key PressAndHoldAndRelease
description: Learn how to register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows + C is pressed. 
ms.topic: article
ms.date: 10/25/2024
ms.localizationpriority: medium
---



# Microsoft Copilot hardware key PressAndHoldAndRelease

This article describes how apps can register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows + C is pressed. This feature enables the following user scenario.

1. The user presses the Microsoft Copilot Hardware key or Windows + C and holds it for the system-defined 700 ms time window.
1. The system launches the Copilot Key provider app, letting the app know that the user is pressing and holding the key.
1. The app begins recording audio and shows a window indicating to the user that audio is being recorded.
1. The user speaks and then releases the key.
1. The app is notified that the key has been released, prompting it to process the recorded speech and taking additional actions based on the user's words.

This feature extends the features of a basic Microsoft Copilot hardware key provider, which simply registers to be launched when the hardware key is pressed. For more information, see [Microsoft Copilot hardware key providers](microsoft-copliot-key-provider.md).

## Register for URI activation

The system launches Microsoft Copilot hardware key providers that implement PressAndHoldAndRelease using URI activation. Register a launch protocol by adding the [uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol) element to your app manifest. For more information about how to register as the default handler for a URI scheme, see [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation).

The following example shows the **uap:Extension** registering the URI scheme "myapp-copilothotkey".

```xml
<!-- Package.appxmanifest -->
...
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
...

<Extensions> 
  ...
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="myapp-copilothotkey"> <!-- app-defined protocol name -->
      <uap:DisplayName>SDK Sample URI Scheme</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>
  ...
```
 
## Microsoft Copilot hardware key app extension

An app must be packaged in order to register as a Microsoft Copilot hardware key provider. For information on app packaging, see [An overview of Package Identity in Windows app](/windows/apps/desktop/modernize/package-identity-overview). Microsoft Copilot hardware key providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.copilotkeyprovider". To support the PressAndHoldAndRelease feature, apps must provide some additional entries to their **uap3:AppExtension** declaration.

Inside of the **uap3:AppExtension** element, add a [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) element with child elements **PressAndHoldStart** and **PressAndHoldStart**. The contents of these elements should be the URI of the protocol scheme registered in the manifest in the previous step. The query string arguments specify whether the URI is being launched because the user pressed and held the hot key or because the user released the hot key. The app uses these query string values during app activation to determine the correct action to take.

The following example shows a Copilot hot key provider registration with support for PressAndHoldAndRelease.

```xml
<!-- Package.appxmanifest -->

<Extensions> 
  ...
  <uap3:Extension Category="windows.appExtension"> 
    <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider"  
      Id="MyAppId" 
      DisplayName="App display name" 
      Description="App description" 
      PublicFolder="Public"> 
      <uap3:Properties> 
        <PressAndHoldStart>myapp-copilothotkey:?state=Down</PressAndHoldStart> 
        <PressAndHoldStop>myapp-copilothotkey:?state=Up</PressAndHoldStop> 
      </uap3:Properties> 
    </ uap3:AppExtension> 
  </uap3:Extension> 
  ...
```

## Handle URI activation

```csharp
// App.xaml.cs

protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    var eventargs = AppInstance.GetCurrent().GetActivatedEventArgs();
    string state = "";
    if ((eventargs != null) && (eventargs.Kind == ExtendedActivationKind.Protocol))
    {
        var protocolArgs = (Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs)eventargs.Data;
        WwwFormUrlDecoder decoderEntries = new WwwFormUrlDecoder(protocolArgs.Uri.Query);
        state = Uri.UnescapeDataString(decoderEntries.GetFirstValueByName("state"));
    }
    state = (state == "") ? "Launched" : state;

    m_window = new MainWindow(state);
    m_window.Activate();
}
```

## Handle fast path invocation

```xml
<!-- Package.appxmanifest -->

<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider"
    Id="MyAppId"
    DisplayName="App display name"
    Description="App description"
    PublicFolder="Public">
    <uap3:Properties>
      <SingleTap FastPathValue="0"/>
      <PressAndHoldStart FastPathValue="1">myapp-copilothotkey://?state=Down</PressAndHoldStart>
      <PressAndHoldStop FastPathValue="2">myapp-copilothotkey://?state=Up</PressAndHoldStop>
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

### Use DllImport to access win32 APIs

```csharp
// MainWindow.xaml.cs
public delegate int SUBCLASSPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData);
[DllImport("Comctl32.dll", SetLastError = true)]
public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, uint uIdSubclass, uint dwRefData);

[DllImport("Comctl32.dll", SetLastError = true)]
public static extern int DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

[DllImport("kernel32.dll")]
public static extern bool QueryPerformanceCounter(out System.Int64 lpPerformanceCount);

[DllImport("kernel32.dll")]
public static extern bool QueryPerformanceFrequency(out System.Int64 lpFrequency);

// Borrowed from https://github.com/devMashHub/GetDuration/blob/master/PropVariant.cs
// Renamed to avoid collision with the PropVariant class in the WindowsAPICodePack
[StructLayout(LayoutKind.Sequential)]
public struct MyPropVariant
{

    #region Struct fields

    // The layout of these elements needs to be maintained.
    //
    // NOTE: We could use LayoutKind.Explicit, but we want
    //       to maintain that the IntPtr may be 8 bytes on
    //       64-bit architectures, so we'll let the CLR keep
    //       us aligned.
    //
    // NOTE: In order to allow x64 compat, we need to allow for
    //       expansion of the IntPtr. However, the BLOB struct
    //       uses a 4-byte int, followed by an IntPtr, so
    //       although the p field catches most pointer values,
    //       we need an additional 4-bytes to get the BLOB
    //       pointer. The p2 field provides this, as well as
    //       the last 4-bytes of an 8-byte value on 32-bit
    //       architectures.

    // This is actually a VarEnum value, but the VarEnum type
    // shifts the layout of the struct by 4 bytes instead of the
    // expected 2.
    ushort vt;

    ushort wReserved1;
    ushort wReserved2;
    ushort wReserved3;

    IntPtr p;
    int p2;

    #endregion

    #region union members

    sbyte cVal // CHAR cVal;
    {
        get { return (sbyte)GetDataBytes()[0]; }
    }

    byte bVal // UCHAR bVal;
    {
        get { return GetDataBytes()[0]; }
    }

    short iVal // SHORT iVal;
    {
        get { return BitConverter.ToInt16(GetDataBytes(), 0); }
    }

    ushort uiVal // USHORT uiVal;
    {
        get { return BitConverter.ToUInt16(GetDataBytes(), 0); }
    }

    int lVal // LONG lVal;
    {
        get { return BitConverter.ToInt32(GetDataBytes(), 0); }
    }

    uint ulVal // ULONG ulVal;
    {
        get { return BitConverter.ToUInt32(GetDataBytes(), 0); }
    }

    long hVal // LARGE_INTEGER hVal;
    {
        get { return BitConverter.ToInt64(GetDataBytes(), 0); }
    }

    ulong uhVal // ULARGE_INTEGER uhVal;
    {
        get { return BitConverter.ToUInt64(GetDataBytes(), 0); }
    }

    float fltVal // FLOAT fltVal;
    {
        get { return BitConverter.ToSingle(GetDataBytes(), 0); }
    }

    double dblVal // DOUBLE dblVal;
    {
        get { return BitConverter.ToDouble(GetDataBytes(), 0); }
    }

    bool boolVal // VARIANT_BOOL boolVal;
    {
        get { return (iVal == 0 ? false : true); }
    }

    int scode // SCODE scode;
    {
        get { return lVal; }
    }

    decimal cyVal // CY cyVal;
    {
        get { return decimal.FromOACurrency(hVal); }
    }

    DateTime date // DATE date;
    {
        get { return DateTime.FromOADate(dblVal); }
    }

    #endregion

    /// <summary>
    /// Gets a byte array containing the data bits of the struct.
    /// </summary>
    /// <returns>A byte array that is the combined size of the data bits.</returns>
    private byte[] GetDataBytes()
    {
        byte[] ret = new byte[IntPtr.Size + sizeof(int)];
        if (IntPtr.Size == 4)
            BitConverter.GetBytes(p.ToInt32()).CopyTo(ret, 0);
        else if (IntPtr.Size == 8)
            BitConverter.GetBytes(p.ToInt64()).CopyTo(ret, 0);
        BitConverter.GetBytes(p2).CopyTo(ret, IntPtr.Size);
        return ret;
    }

    /// <summary>
    /// Called to properly clean up the memory referenced by a PropVariant instance.
    /// </summary>
    [DllImport("ole32.dll")]
    private extern static int PropVariantClear(ref MyPropVariant pvar);

    /// <summary>
    /// Called to clear the PropVariant's referenced and local memory.
    /// </summary>
    /// <remarks>
    /// You must call Clear to avoid memory leaks.
    /// </remarks>
    public void Clear()
    {
        // Can't pass "this" by ref, so make a copy to call PropVariantClear with
        MyPropVariant var = this;
        PropVariantClear(ref var);

        // Since we couldn't pass "this" by ref, we need to clear the member fields manually
        // NOTE: PropVariantClear already freed heap data for us, so we are just setting
        //       our references to null.
        vt = (ushort)VarEnum.VT_EMPTY;
        wReserved1 = wReserved2 = wReserved3 = 0;
        p = IntPtr.Zero;
        p2 = 0;
    }

    /// <summary>
    /// Gets the variant type.
    /// </summary>
    public VarEnum Type
    {
        get { return (VarEnum)vt; }
    }

    public void Init(VarEnum t, uint val)
    {
        vt = (ushort)t;
        p = (IntPtr)val;
    }

    /// <summary>
    /// Gets the variant value.
    /// </summary>
    public object Value
    {
        get
        {
            // TODO: Add support for reference types (ie. VT_REF | VT_I1)
            // TODO: Add support for safe arrays

            switch ((VarEnum)vt)
            {
                case VarEnum.VT_I1:
                    return cVal;
                case VarEnum.VT_UI1:
                    return bVal;
                case VarEnum.VT_I2:
                    return iVal;
                case VarEnum.VT_UI2:
                    return uiVal;
                case VarEnum.VT_I4:
                case VarEnum.VT_INT:
                    return lVal;
                case VarEnum.VT_UI4:
                case VarEnum.VT_UINT:
                    return ulVal;
                case VarEnum.VT_I8:
                    return hVal;
                case VarEnum.VT_UI8:
                    return uhVal;
                case VarEnum.VT_R4:
                    return fltVal;
                case VarEnum.VT_R8:
                    return dblVal;
                case VarEnum.VT_BOOL:
                    return boolVal;
                case VarEnum.VT_ERROR:
                    return scode;
                case VarEnum.VT_CY:
                    return cyVal;
                case VarEnum.VT_DATE:
                    return date;
                case VarEnum.VT_FILETIME:
                    return DateTime.FromFileTime(hVal);
                case VarEnum.VT_BSTR:
                    return Marshal.PtrToStringBSTR(p);
                case VarEnum.VT_BLOB:
                    byte[] blobData = new byte[lVal];
                    IntPtr pBlobData;
                    if (IntPtr.Size == 4)
                    {
                        pBlobData = new IntPtr(p2);
                    }
                    else if (IntPtr.Size == 8)
                    {
                        // In this case, we need to derive a pointer at offset 12,
                        // because the size of the blob is represented as a 4-byte int
                        // but the pointer is immediately after that.
                        pBlobData = new IntPtr(BitConverter.ToInt64(GetDataBytes(), sizeof(int)));
                    }
                    else
                        throw new NotSupportedException();
                    Marshal.Copy(pBlobData, blobData, 0, lVal);
                    return blobData;
                case VarEnum.VT_LPSTR:
                    return Marshal.PtrToStringAnsi(p);
                case VarEnum.VT_LPWSTR:
                    return Marshal.PtrToStringUni(p);
                case VarEnum.VT_UNKNOWN:
                    return Marshal.GetObjectForIUnknown(p);
                case VarEnum.VT_DISPATCH:
                    return p;
                default:
                    throw new NotSupportedException("The type of this variable is not support ('" + vt.ToString() + "')");
            }
        }
    }
}

[ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPropertyStore
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetCount([Out] out uint cProps);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetAt([In] uint iProp, out PropertyKey pkey);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetValue([In] ref PropertyKey key, out object pv);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int SetValue([In] ref PropertyKey key, [In] ref MyPropVariant pv);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Commit();
}


[DllImport("shell32.dll", SetLastError = true)]
static extern int SHGetPropertyStoreForWindow(
IntPtr handle,
ref Guid riid,
 [Out(), MarshalAs(UnmanagedType.Interface)] out IPropertyStore propertyStore);
```

### Implement a helper property for tracking the Copilot Key pressed state

```csharp
public event PropertyChangedEventHandler? PropertyChanged;

private void OnPropertyChanged([CallerMemberName] string propertyName = "State")
{
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


public void SetState(string state)
{
    State = state;
}

private string _state;
public string State
{
    get => _state;
    set
    {
        if (_state != value)
        {
            _state = value;
            OnPropertyChanged();
        }
    }
}
```

```xaml
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
  <TextBlock Name="KeyStateText" Text="{x:Bind State, Mode=OneWay}" />
</StackPanel>
```

### Register a Window to receive callbacks

```csharp
// MainWindow.xaml.cs
private SUBCLASSPROC SubClassDelegate;
private nint hWndMain;

public MainWindow(string state)
{
    ...
    hWndMain = (nint)WinRT.Interop.WindowNative.GetWindowHandle(this);
    Microsoft.UI.Windowing.AppWindow appWindow = AppWindow;
    
    var key = new PropertyKey(new Guid("38652BCA-4329-4E74-86F9-39CF29345EEA"), 0x00000002);
    var value = new MyPropVariant();
    value.Init(VarEnum.VT_UINT, WM_COPILOT);
    propertyStore.SetValue(ref key, ref value);
    
    SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
    bool bRet = SetWindowSubclass((int)appWindow.Id.Value, SubClassDelegate, 0, 0);

    _state = state;
    ...
}
```

```csharp
private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
{
    switch (uMsg)
    {
        case WM_COPILOT:
            {
                switch (wParam)
                {
                    case 0:
                        // Show window
                        SetState("SingleTap");
                        break;
                    case 1:
                        // Start recording audio and update UI to indicate recording is occurring.
                        SetState("PressAndHold START");
                        break;
                    case 2:
                        // End recording audio, process recorded audio
                        SetState("PressAndHold END");
                        break;
                }
            }
            break;

    }
    return DefSubclassProc(hWnd, uMsg, wParam, lParam);
}
```

