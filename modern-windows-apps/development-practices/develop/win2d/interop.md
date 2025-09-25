---
title: Interop with Direct2D
description: An overview of how to manually interoperate Win2D and Direct2D in advanced scenarios.
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Interop with Direct2D

Win2D is implemented as a layer on top of [Direct2D](https://msdn.microsoft.com/library/windows/desktop/dd370990), and supports interop in both directions. If you have a Win2D object, you can access the native Direct2D object that is used to implement it. If you have a Direct2D object, you can look up the Win2D object that wraps it, or create a new wrapper if one did not already exist.

Interop allows you to mix and match Win2D with native DirectX APIs. You can write an app that mostly uses Win2D, but drop down to native DirectX at any point - perhaps to call into some other API or 3rd party component that requires native interfaces. Or your app can be mostly native DirectX, yet you can switch over to Win2D in specific places where you want its extra convenience or C# support.

## Interop APIs

C++/CX interop APIs are defined in the header Microsoft.Graphics.Canvas.native.h:

```cpp
#include <Microsoft.Graphics.Canvas.native.h>

using namespace Microsoft::Graphics::Canvas;
```

To get the native Direct2D object that is wrapped by a Win2D object:

```cpp
template<typename T, typename U>
Microsoft::WRL::ComPtr<T> GetWrappedResource(U^ wrapper);

template<typename T, typename U>
Microsoft::WRL::ComPtr<T> GetWrappedResource(CanvasDevice^ device, U^ wrapper);

template<typename T, typename U>
Microsoft::WRL::ComPtr<T> GetWrappedResource(CanvasDevice^ device, U^ wrapper, float dpi);
```

For most types `GetWrappedResource` can be called with only a Win2D wrapper object as parameter. For a few types (see below table) it must also be passed a device and/or DPI value. It is not an error to pass a device or DPI when using `GetWrappedResource` with types that do not require them.

To get a Win2D object wrapping a native Direct2D object:

```cpp
template<typename WRAPPER>
WRAPPER^ GetOrCreate(IUnknown* resource);

template<typename WRAPPER>
WRAPPER^ GetOrCreate(CanvasDevice^ device, IUnknown* resource);

template<typename WRAPPER>
WRAPPER^ GetOrCreate(ID2D1Device1* device, IUnknown* resource);

template<typename WRAPPER>
WRAPPER^ GetOrCreate(CanvasDevice^ device, IUnknown* resource, float dpi);

template<typename WRAPPER>
WRAPPER^ GetOrCreate(ID2D1Device1* device, IUnknown* resource, float dpi);
```

`GetOrCreate` returns an existing wrapper instance if one already exists, or creates a new wrapper if one does not. Calling it repeatedly on the same native object will return the same wrapper each time, as long as that wrapper instance continues to exist. If all references to the wrapper are released such that its reference count goes to zero and it is destroyed, any later call to `GetOrCreate` will have to create a new wrapper.

For some types `GetOrCreate` can be called with only a Direct2D resource object as parameter, while for other types (see below table) it must also be passed a device and DPI value. It is not an error to pass a device or DPI when using `GetOrCreate` with types that do not require them. If a Win2D wrapper already exists, it is ok to omit the device and DPI even for types that would normally need them: these parameters are only used when creating new wrapper instances.

`GetOrCreate` understands inheritance hierarchies and will always create the appropriate most-derived wrapper type. For instance if you call `GetOrCreate<CanvasBitmap>(ID2D1Bitmap1*)` with an `ID2D1Bitmap1` that has the `D2D1_BITMAP_OPTIONS_TARGET` flag, the returned wrapper instance will in fact be a `CanvasRenderTarget` (which derives from `CanvasBitmap`). The other way around, if you call `GetOrCreate<CanvasRenderTarget>(ID2D1Bitmap1*)` with an `ID2D1Bitmap1` that does not have `D2D1_BITMAP_OPTIONS_TARGET`, this will throw an invalid cast exception.

Taking this to the extreme, it is valid to call `GetOrCreate<Object>(IUnknown*)`, and also `GetWrappedResource<IUnknown>(Object^)`.

## Types that support interop

| Win2D type | Direct2D type | `GetOrCreate` parameters | `GetWrappedResource` parameters |
| -- | -- | -- | -- |
| [`CanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm) | `ID2D1Bitmap1` without `D2D1_BITMAP_OPTIONS_TARGET` | Device | - |
| [`CanvasCachedGeometry`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasCachedGeometry.htm) | `ID2D1GeometryRealization` | Device | - |
| [`CanvasCommandList`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasCommandList.htm) | `ID2D1CommandList` | Device | - |
| [`CanvasDevice`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDevice.htm) | `ID2D1Device1` | - | - |
| [`CanvasDrawingSession`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm) | `ID2D1DeviceContext1` | - | - |
| [`CanvasFontFace`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasFontFace.htm) | `IDWriteFontFaceReference` | - | - |
| [`CanvasFontSet`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasFontSet.htm) | `IDWriteFontSet` | - | - |
| [`CanvasGeometry`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasGeometry.htm) | `ID2D1Geometry`, or one of its derived interfaces `ID2D1PathGeometry`, `ID2D1RectangleGeometry`, `ID2D1RoundedRectangleGeometry`, `ID2D1EllipseGeometry`, `ID2D1TransformedGeometry`, or `ID2D1GeometryGroup` | Device | - |
| [`CanvasGradientMesh`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasGradientMesh.htm) | `ID2D1GradientMesh` | Device | - |
| [`CanvasImageBrush`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasImageBrush.htm) | `ID2D1BitmapBrush1` (if the image is a `CanvasBitmap` and `SourceRectangle` is `null`) or `ID2D1ImageBrush` (if it is any other type of `ICanvasImage`, or if `SourceRectangle is set`) | Device | Optional DPI^1 |
| [`CanvasLinearGradientBrush`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasLinearGradientBrush.htm) | `ID2D1LinearGradientBrush` | Device | - |
| [`CanvasNumberSubstitution`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasNumberSubstitution.htm) | `IDWriteNumberSubstitution` | - | - |
| [`CanvasRadialGradientBrush`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasRadialGradientBrush.htm) | `ID2D1RadialGradientBrush` | Device | - |
| [`CanvasRenderTarget`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm) | `ID2D1Bitmap1` with `D2D1_BITMAP_OPTIONS_TARGET` | Device | - |
| [`CanvasSolidColorBrush`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasSolidColorBrush.htm) | `ID2D1SolidColorBrush` | Device | - |
| [`CanvasStrokeStyle`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm) | `ID2D1StrokeStyle1` | - | Device |
| [`CanvasSvgDocument`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Svg_CanvasSvgDocument.htm) | `ID2D1SvgDocument`^2 | Device | - |
| [`CanvasSwapChain`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasSwapChain.htm) | `IDXGISwapChain1` | Device, DPI | - |
| [`CanvasTestFormat`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextFormat.htm) | `IDWriteTextFormat1` | - | - |
| [`CanvasTextLayout`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextLayout.htm) | `IDWriteTextLayout3	` | Device | - |
| [`CanvasTextRenderingParameters`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextRenderingParameters.htm) | `IDWriteRenderingParams3` | - | - |
| [`CanvasTypography`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasTypography.htm) | `IDWriteTypography` | - | - |
| [`CanvasVirtualBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasVirtualBitmap.htm) | `ID2D1ImageSource` or `ID2D1TransformedImageSource` | Device | - |
| [`ColorManagementProfile`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_ColorManagementProfile.htm) | `ID2D1ColorContext` | Device | Device |
| [`EffectTransferTable3D`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_EffectTransferTable3D.htm) | `ID2D1LookupTable3D` | Device | - |
| [`Microsoft.Graphics.Canvas.Effects.*`](https://microsoft.github.io/Win2D/WinUI3/html/N_Microsoft_Graphics_Canvas_Effects.htm) (multiple Win2D classes map to the same D2D type) | `ID2D1Effect` with the appropriate `D2D1_PROPERTY_TYPE_CLSID` | Device | Device, optional DPI^1 |

> [!NOTE]
> ^1: optional DPI means it is valid to call `GetWrappedResource` for this type without specifying a DPI value, but if you do specify DPI, Win2D may be able to more efficiently configure effect graphs by leaving out redundant DPI compensation nodes. This applies when calling `GetWrappedResource` on an effect, or on a `CanvasImageBrush` that has an effect as its source image.

> [!NOTE]
> ^2: when a `CanvasSvgDocument` is produced from an `ID2D1SvgDocument` using native C++ interop, the `ID2D1SvgDocument`'s viewport size is ignored.

## Interop using C++/CX

Here's an example of how Win2D can interoperate with Direct2D, using C++:

```cpp
#include <Microsoft.Graphics.Canvas.native.h>
#include <d2d1_2.h>

using namespace Microsoft::Graphics::Canvas;
using namespace Microsoft::WRL;

// Interop Win2D -> Direct2D.
CanvasDevice^ canvasDevice = ...;
CanvasBitmap^ canvasBitmap = ...;

ComPtr<ID2D1Device> nativeDevice = GetWrappedResource<ID2D1Device>(canvasDevice);
ComPtr<ID2D1Bitmap1> nativeBitmap = GetWrappedResource<ID2D1Bitmap1>(canvasBitmap);

// Interop Direct2D -> Win2D.
canvasDevice = GetOrCreate<CanvasDevice>(nativeDevice.Get());
bitmap = GetOrCreate<CanvasBitmap>(canvasDevice, nativeBitmap.Get());
```

> [!NOTE]
> Interop is also possible via C#, through various means (eg. using built-in COM/WinRT interop, via [CsWinRT](https://github.com/microsoft/CsWinRT)'s APIs, or using blittable bindings). For more info on manual interop in C#, refer to the docs about CsWinRT.

## Interop using C++/WinRT

You can also perform interop using C++/WinRT with some modification of the above. Note that the C++/WinRT headers for the Win2D Windows Runtime Components should be generated automatically when you add the Win2D NuGet package to your C++/WinRT project. However, for interop you will still need to use the header file Microsoft.Graphics.Canvas.native.h which contains the low-level ABI interface `ICanvasFactoryNative` in the namespace `ABI::Microsoft::Graphics::Canvas`. The interface has the following functions which you can use to perform interop.

```cpp
HRESULT GetOrCreate(ICanvasDevice* device, IUnknown* resource, float dpi, IInspectable** wrapper);
HRESULT GetNativeResource(ICanvasDevice* device, float dpi, REFIID iid, void** resource);
```

Here is an example showing how to create a `CanvasVirtualBitmap` from an `IWICBitmapSource` starting with the `IWICBitmapSource` and the shared `CanvasDevice`.

```cpp
#include "pch.h"
#include <wincodec.h>
#include <wincodecsdk.h>
#include <winrt/Microsoft.Graphics.Canvas.h> //This defines the C++/WinRT interfaces for the Win2D Windows Runtime Components
#include <Microsoft.Graphics.Canvas.h> //This defines the low-level ABI interfaces for the Win2D Windows Runtime Components
#include <Microsoft.Graphics.Canvas.native.h> //This is for interop
#include <d2d1_3.h>

using namespace winrt::Microsoft::Graphics::Canvas;
namespace abi {
  using namespace ABI::Microsoft::Graphics::Canvas;
}

namespace winrt::Win2DInteropTest::implementation {
  CanvasVirtualBitmap CreateVirtualBitmapFromBitmapSource(com_ptr<IWICBitmapSource> const& pBitmapSource){
    CanvasDevice sharedDevice = CanvasDevice::GetSharedDevice();

    //First we need to get an ID2D1Device1 pointer from the shared CanvasDevice
    com_ptr<abi::ICanvasResourceWrapperNative> nativeDeviceWrapper = sharedDevice.as<abi::ICanvasResourceWrapperNative>();
    com_ptr<ID2D1Device1> pDevice{ nullptr };
    check_hresult(nativeDeviceWrapper->GetNativeResource(nullptr, 0.0f, guid_of<ID2D1Device1>(), pDevice.put_void()));

    //Next we need to call some Direct2D functions to create the ID2D1ImageSourceFromWic object
    com_ptr<ID2D1DeviceContext1> pContext{ nullptr };
    check_hresult(pDevice->CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS_NONE, pContext.put()));
    com_ptr<ID2D1DeviceContext2> pContext2 = pContext.as<ID2D1DeviceContext2>();
    com_ptr<ID2D1ImageSourceFromWic> pImage{ nullptr };
    check_hresult(pContext2->CreateImageSourceFromWic(pBitmapSource.get(), D2D1_IMAGE_SOURCE_LOADING_OPTIONS_RELEASE_SOURCE, pImage.put()));

    //Finally we need to wrap the ID2D1ImageSourceFromWic object inside 
    com_ptr<::IInspectable> pInspectable{ nullptr };
    auto factory = winrt::get_activation_factory<CanvasDevice, abi::ICanvasFactoryNative>(); //abi::ICanvasFactoryNative is the activation factory for the CanvasDevice class
    check_hresult(factory->GetOrCreate(sharedDevice.as<abi::ICanvasDevice>().get(), pImage.as<::IUnknown>().get(), 0.0f, pInspectable.put())); //Note abi::ICanvasDevice is defined in the header Microsoft.Graphics.Canvas.h
    CanvasVirtualBitmap cvb = pInspectable.as<CanvasVirtualBitmap>();
    return cvb;
  }
}
```

Remember to include the header `<unknwn.h>` in pch.h before any WinRT headers (required in SDK 17763 and later).