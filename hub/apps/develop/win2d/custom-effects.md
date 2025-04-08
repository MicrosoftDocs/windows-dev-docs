---
title: Implementing custom effects
description: An in-depth guide on implementing custom D2D effects with Win2D.
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Implementing custom effects

Win2D provides several APIs to represent objects that can be drawn, which are divided into two categories: images and effects. Images, represented by the `ICanvasImage` interface, have no inputs and can be directly drawn on a given surface. For example, `CanvasBitmap`, `VirtualizedCanvasBitmap` and `CanvasRenderTarget` are examples of image types. Effects, on the other hand, are represented by the `ICanvasEffect` interface. They can have inputs as well as additional resources, and can apply arbitrary logic to produce their outputs (as an effect is also an image). Win2D includes effects wrapping most [D2D effects](/windows/win32/direct2d/effects-overview), such as `GaussianBlurEffect`, `TintEffect` and `LuminanceToAlphaEffect`.

Images and effects can also be chained together, to create arbitrary graphs which can then be displayed in your application (also refer to the D2D docs on [Direct2D effects](/windows/win32/direct2d/effects-overview)). Together, they provide an extremely flexible system to author complex graphics in an efficient manner. However, there are cases where the built-in effects are not sufficient, and you might want to build your very own Win2D effect. To support this, Win2D includes a set of powerful interop APIs that allows defining custom images and effects that can seamlessly integrate with Win2D.

> [!TIP]
> If you are using C# and want to implement a custom effect or effect graph, it is recommended to use [ComputeSharp](https://github.com/Sergio0694/ComputeSharp) rather than trying to implement an effect from scratch. [See the paragraph below](#custom-effects-in-c-using-computesharp) for a detailed explanation of how to use this library to implement custom effects that integrate seamlessly with Win2D.

> **Platform APIs:** [`ICanvasImage`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`CanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`VirtualizedCanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`CanvasRenderTarget`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`CanvasEffect`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`GaussianBlurEffect`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`TintEffect`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`ICanvasLuminanceToAlphaEffectImage`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasImage.htm), [`IGraphicsEffectSource`](/uwp/api/windows.graphics.effects.igraphicseffectsource), [`ID2D21Image`](/windows/win32/api/d2d1/nn-d2d1-id2d1image), [`ID2D1Factory1`](/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1factory1), [`ID2D1Effect`](/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1effect)

## Implementing a custom `ICanvasImage`

The simplest scenario to support is creating a custom `ICanvasImage`. As we mentioned, this is the WinRT interface defined by Win2D which represents all kinds of images that Win2D can interop with. This interface only exposes two `GetBounds` methods, and extends `IGraphicsEffectSource`, which is a marker interface representing "some effect source".

As you can see, there are no "functional" APIs exposed by this interface to actually perform any drawing. In order to implement your own `ICanvasImage` object, you'll need to also implement the `ICanvasImageInterop` interface, which exposes all the necessary logic for Win2D to draw the image. This is a COM interface defined in the public `Microsoft.Graphics.Canvas.native.h` header, that ships with Win2D.

The interface is defined as follows:

```cpp
[uuid("E042D1F7-F9AD-4479-A713-67627EA31863")]
class ICanvasImageInterop : IUnknown
{
    HRESULT GetDevice(
        ICanvasDevice** device,
        WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type);

    HRESULT GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage);
}
```

And it also relies on these two enumeration types, from the same header:

```cpp
enum WIN2D_GET_DEVICE_ASSOCIATION_TYPE
{
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_UNSPECIFIED,
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_REALIZATION_DEVICE,
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_CREATION_DEVICE
}

enum WIN2D_GET_D2D_IMAGE_FLAGS
{
    WIN2D_GET_D2D_IMAGE_FLAGS_NONE,
    WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT,
    WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION,
    WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION,
    WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION,
    WIN2D_GET_D2D_IMAGE_FLAGS_ALLOW_NULL_EFFECT_INPUTS,
    WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE
}
```

The two `GetDevice` and `GetD2DImage` methods are all that's needed to implement custom images (or effects), as they provide Win2D with the extensibility points to initialize them on a given device and retrieve the underlying D2D image to draw. Implementing these methods correctly is critical to ensure things will work properly in all supported scenarios.

Let's go over them to see how each method works.

### Implementing `GetDevice`

The `GetDevice` method is the simplest of the two. What it does is it retrieves the canvas device associated with the effect, so that Win2D can inspect it if necessary (for instance, to ensure it matches the device in use). The `type` parameter indicates the "association type" for the returned device.

There are two main possible cases:
- If the image is an effect, it should support being "realized" and "unrealized" on multiple devices. What this means is: a given effect is created in an uninitialized state, then it can be realized when a device is passed while drawing, and after that it can keep being used with that device, or it can be moved to a different device. In that case, the effect will reset its internal state and then realize itself again on the new device. This means that the associated canvas device can change over time, and it can also be `null`. Because of this, `type` should be set to `WIN2D_GET_DEVICE_ASSOCIATION_TYPE_REALIZATION_DEVICE`, and the returned device should be set to the current realization device, if one is available.
- Some images have a single "owning device" which is assigned at creation time and can never change. For instance, this would be the case for an image representing a texture, as that is allocated on a specific device and cannot be moved. When `GetDevice` is called, it should return the creation device and set `type` to `WIN2D_GET_DEVICE_ASSOCIATION_TYPE_CREATION_DEVICE`. Note that when this type is specified, the returned device should not be `null`.

> [!NOTE]
> Win2D can call `GetDevice` while recursively traversing an effect graph, meaning there might be multiple active calls to `GetD2DImage` in the stack. Because of this, `GetDevice` should not take a blocking lock on the current image, as that could potentially deadlock. Rather, it should use a re-entrant lock in a non-blocking manner, and return an error if it cannot be acquired. This ensures that the same thread recursively calling it will successfully acquire it, whereas concurrent threads doing the same will fail gracefully.

### Implementing `GetD2DImage`

`GetD2DImage` is where most of the work takes place. This method is responsible for retrieving the `ID2D1Image` object that Win2D can draw, optionally realizing the current effect if needed. This also includes recursively traversing and realizing the effect graph for all sources, if any, as well as initializing any state that the image might need (eg. constant buffers and other properties, resource textures, etc.).

The exact implementation of this method is highly dependent on the image type and it can vary a lot, but generally speaking for an arbitrary effect you can expect the method to perform the following steps:
- Check whether the call was recursive on the same instance, and fail if so. This is needed to detect cycles in an effect graph (eg. effect `A` has effect `B` as source, and effect `B` has effect `A` as source).
- Acquire a lock on the image instance to protect against concurrent access.
- Handle the target DPIs according to the input flags
- Validate whether the input device matches the one in use, if any. If it does not match and the current effect supports realization, unrealize the effect.
- Realize the effect on the input device. This can include registering the D2D effect on the `ID2D1Factory1` object retrieved from the input device or device context, if needed. Additionally, all necessary state should be set on the D2D effect instance being created.
- Recursively traverse any sources and bind them to the D2D effect.

With respect to the input flags, there are several possible cases that custom effects should properly handle, to ensure compatibility with all other Win2D effects. Excluding `WIN2D_GET_D2D_IMAGE_FLAGS_NONE`, the flags to handle are the following:
- `WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT`: in this case, `device` is guaranteed to not be `null`. The effect should check whether the device context target is an `ID2D1CommandList`, and if so, add the `WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION` flag. Otherwise, it should set `targetDpi` (which is also guaranteed to not be `null`) to the DPIs retrieved from the input context. Then, it should remove `WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT` from the flags.
- `WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION` and `WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION`: used when setting effect sources (see notes below).
- `WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION`: if set, skips recursively realizing the sources of the effect, and just returns the realized effect with no other changes.
- `WIN2D_GET_D2D_IMAGE_FLAGS_ALLOW_NULL_EFFECT_INPUTS`: if set, effect sources being realized are allowed to be `null`, if the user has not set them to an existing source yet.
- `WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE`: if set, and an effect source being set is not valid, the effect should unrealize before failing. That is, if the error occurred while resolving the effect sources after realizing the effect, the effect should unrealize itself before returning the error to the caller.

With respect to the DPI-related flags, these control how effect sources are set. To ensure compatibility with Win2D, effects should automatically add DPI compensation effects to their inputs when needed. They can control whether that is the case like so:
- If `WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION` is set, a DPI compensation effect is needed whenever the `inputDpi` parameter is not `0`.
- Otherwise, DPI compensation is needed if `inputDpi` is not `0`, `WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION` is not set, and either `WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION` is set, or the input DPI and the target DPI values don't match.

This logic should be applied whenever a source is being realized and bound to an input of the current effect. Note that if a DPI compensation effect is added, that should be the input set to the underlying D2D image. But, if the user tries to retrieve the WinRT wrapper for that source, the effect should take care to detect whether a DPI effect was used, and return a wrapper for the original source object instead. That is, DPI compensation effects should be transparent to users of the effect.

After all initialization logic is done, the resulting `ID2D1Image` (just like with Win2D objects, a D2D effect is also an image) should be ready to be drawn by Win2D on the target context, which is not yet known by the callee at this time.

> [!NOTE]
> Correctly implementing this method (and `ICanvasImageInterop` in general) is **extremely** complicated, and it's only meant to be done by advanced users that absolutely need the extra flexibility. A solid understanding of D2D, Win2D, COM, WinRT and C++ is recommended before attempting to write an `ICanvasImageInterop` implementation. If your custom Win2D effect also has to wrap a custom D2D effect, you'll need to implement your own `ID2D1Effect` object as well (refer to the [D2D docs](/windows/win32/direct2d/custom-effects) on custom effects for more info on this). These docs are not an exhaustive description of all necessary logic (for instance, they don't cover how effect sources should be marshalled and managed across the D2D/Win2D boundary), so it is recommended to also use the `CanvasEffect` implementation in Win2D's codebase as a reference point for a custom effect, and modify it as needed.

### Implementing `GetBounds`

The last missing component to fully implement a custom `ICanvasImage` effect is to support the two `GetBounds` overloads. To make this easy, Win2D exposes a C export which can be used to leverage the existing logic for this from Win2D on any custom image. The export is as follows:

```cpp
HRESULT GetBoundsForICanvasImageInterop(
    ICanvasResourceCreator* resourceCreator,
    ICanvasImageInterop* image,
    Numerics::Matrix3x2 const* transform,
    Rect* rect);
```

Custom images can invoke this API and pass themselves as the `image` parameter, and then simply return the result to their callers. The `transform` parameter can be `null`, if no transform is available.

## Optimizing device context accesses

The `deviceContext` parameter in `ICanvasImageInterop::GetD2DImage` can sometimes be `null`, if a context is not immediately available before the invocation. This is done on purpose, so that a context is only created lazily when it's actually needed. That is, if a context is available, Win2D will pass it to the `GetD2DImage` invocation, otherwise it will let callees retrieve one on their own if necessary.

Creating a device context is relatively expensive, so to make retrieving one faster Win2D exposes APIs to access its internal device context pool. This allows custom effects to rent and return device contexts associated with a given canvas device in an efficient manner.

The device context lease APIs are defined as follows:

```cpp
[uuid("A0928F38-F7D5-44DD-A5C9-E23D94734BBB")]
interface ID2D1DeviceContextLease : IUnknown
{
    HRESULT GetD2DDeviceContext(ID2D1DeviceContext** deviceContext);
}

[uuid("454A82A1-F024-40DB-BD5B-8F527FD58AD0")]
interface ID2D1DeviceContextPool : IUnknown
{
    HRESULT GetDeviceContextLease(ID2D1DeviceContextLease** lease);
}
```

The `ID2D1DeviceContextPool` interface is implemented by `CanvasDevice`, which is the Win2D type implementing the `ICanvasDevice` interface. To use the pool, use `QueryInterface` on the device interface to obtain an `ID2D1DeviceContextPool` reference, and then call `ID2D1DeviceContextPool::GetDeviceContextLease` to obtain an `ID2D1DeviceContextLease` object to access the device context. Once that's no longer needed, release the lease. Make sure to not touch the device context after the lease has been released, as it might be used concurrently by other threads.

## Enabling WinRT wrappers lookup

As seen in the [Win2D interop docs](https://microsoft.github.io/Win2D/WinUI2/html/Interop.htm), the Win2D public header also exposes a `GetOrCreate` method (accessible from the `ICanvasFactoryNative` activation factory, or through the `GetOrCreate` C++/CX helpers defined in the same header). This allows retrieving a WinRT wrapper from a given native resource. For instance, it lets you retrieve or create a `CanvasDevice` instance from an `ID2D1Device1` object, a `CanvasBitmap` from an `ID2D1Bitmap`, etc.

This method also works for all built-in Win2D effects: retrieving the native resource for a given effect and then using that to retrieve the corresponding Win2D wrapper will correctly return the owning Win2D effect for it. In order for custom effects to also benefit from the same mapping system, Win2D exposes several APIs in the interop interface for the activation factory for `CanvasDevice`, which is the `ICanvasFactoryNative` type, as well as an additional effect factory interface, `ICanvasEffectFactoryNative`:

```cpp
[uuid("29BA1A1F-1CFE-44C3-984D-426D61B51427")]
class ICanvasEffectFactoryNative : IUnknown
{
    HRESULT CreateWrapper(
        ICanvasDevice* device,
        ID2D1Effect* resource,
        float dpi,
        IInspectable** wrapper);
};

[uuid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
class ICanvasFactoryNative : IInspectable
{
    HRESULT GetOrCreate(
        ICanvasDevice* device,
        IUnknown* resource,
        float dpi,
        IInspectable** wrapper);

    HRESULT RegisterWrapper(IUnknown* resource, IInspectable* wrapper);

    HRESULT UnregisterWrapper(IUnknown* resource);

    HRESULT RegisterEffectFactory(
        REFIID effectId,
        ICanvasEffectFactoryNative* factory);

    HRESULT UnregisterEffectFactory(REFIID effectId);
};
```

There are several APIs to consider here, as they're needed to support all the various scenarios where Win2D effects can be used, as well as how developers could do interop with the D2D layer and then try to resolve wrappers for them. Let's go over each of these APIs.

The `RegisterWrapper` and `UnregisterWrapper` methods are meant to be invoked by custom effects to add themselves into the internal Win2D cache:
- `RegisterWrapper`: registers a native resource and its owning WinRT wrapper. The `wrapper` parameter is required to also implemement [`IWeakReferenceSource`](/windows/win32/api/weakreference/nn-weakreference-iweakreferencesource), so that it can be cached correctly without causing reference cycles which would lead to memory leaks. The method returns `S_OK` if the native resource could be added to the cache, `S_FALSE` if there was already a registered wrapper for `resource`, and an error code if an error occurs.
- `UnregisterWrapper`: unregisters a native resource and its wrapper. Returns `S_OK` if the resource could be removed, `S_FALSE` if `resource` was not already registered, and an erro code if another error occurrs.

Custom effects should call `RegisterWrapper` and `UnregisterWrapper` whenever they are realized and unrealized, ie. when a new native resource is created and associated with them. Custom effects that do not support realization (eg. those having a fixed associated device) can call `RegisterWrapper` and `UnregisterWrapper` when they are created and destroyed. Custom effects should make sure to correctly unregister themselves from all possible code paths that would cause the wrapper to become invalid (eg. including when the object is finalized, in case it's implemented in a managed language).

The `RegisterEffectFactory` and `UnregisterEffectFactory` methods are also meant to be used by custom effects, so that they can also register a callback to create a new wrapper in case a developer tries to resolve one for an "orphaned" D2D resource:
- `RegisterEffectFactory`: register a callback that takes in input the same parameters that a developer passed to `GetOrCreate`, and creates a new inspectable wrapper for the input effect. The effect id is used as key, so that each custom effect can register a factory for it when it's first loaded. Of course, this should only be done once per effect type, and not every time the effect is realized. The `device`, `resource` and `wrapper` parameters are checked by Win2D before invoking any registered callback, so they are guaranteed to not be `null` when `CreateWrapper` is invoked. The `dpi` is considered optional, and can be ignored in case the effect type doesn't have a specific use for it. Note that when a new wrapper is created from a registered factory, that factory should also make sure that the new wrapper is registered in the cache (Win2D will not automatically add wrappers produced by external factories to the cache).
- `UnregisterEffectFactory`: removes a previously register callback. For instance, this could be used if an effect wrapper is implemented in a managed assembly which is being unloaded.

> [!NOTE]
> `ICanvasFactoryNative` is implemented by the activation factory for `CanvasDevice`, which you can retrieve by either manually calling [`RoGetActivationFactory`](/windows/win32/api/roapi/nf-roapi-rogetactivationfactory), or using helper APIs from the language extensions you're using (eg. [`winrt::get_activation_factory`](/uwp/cpp-ref-for-winrt/get-activation-factory) in C++/WinRT). For more info, see [WinRT type system](/uwp/winrt-cref/winrt-type-system) for more information on how this works.

For a practical example of where this mapping comes into play, consider how built-in Win2D effects work. If they are not realized, all state (eg. properties, sources, etc.) is stored in an internal cache in each effect instance. When they are realized, all state is transferred to the native resource (eg. properties are set on the D2D effect, all sources are resolved and mapped to effect inputs, etc.), and as long as the effect is realized it will act as the authority on the state of the wrapper. That is, if the value of any property is fetched from the wrapper, it will retrieve the updated value for it from the native D2D resource associated with it.

This ensures that if any changes are made directly to the D2D resource, those will be visible on the outer wrapper as well, and the two will never be "out of sync". When the effect is unrealized, all state is transferred back from the native resource to the wrapper state, before the resource is released. It will be kept and updated there until the next time the effect is realized. Now, consider this sequence of events:
- You have some Win2D effect (either built-in, or custom).
- You get the `ID2D1Image` from it (which is an `ID2D1Effect`).
- You create an instance of a custom effect.
- You also get the `ID2D1Image` from that.
- You manually set this image as input for the previous effect (via [`ID2D1Effect::SetInput`](/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1effect-setinput)).
- You then ask that first effect for the WinRT wrapper for that input.

Since the effect is realized (it was realized when the native resource was requested), it will use the native resource as the source of truth. As such, it will get the `ID2D1Image` corresponding to the requested source, and try to retrieve the WinRT wrapper for it. If the effect this input was retrieved from has correctly added its own pair of native resource and WinRT wrapper to Win2D's cache, the wrapper will be resolved and returned to callers. If not, that property access will fail, as Win2D can't resolve WinRT wrappers for effects it does not own, as it doesn't know how to instantiate them.

This is where `RegisterWrapper` and `UnregisterWrapper` help, as they allow custom effects to seamlessly participate in Win2D's wrapper resolution logic, so that the correct wrapper can always be retrieved for any effect source, regardless of whether it was set from WinRT APIs, or directly from the underlying D2D layer.

To explain how the effect factories also come into play, consider this scenario:
- A user creates an instance of a custom wrapper and realizes it
- They then gets a reference to the underlying D2D effect and keeps it.
- Then, the effect is realized on a different device. The effect will unrealize and re-realize, and in doing so it will create a new D2D effect. The previous D2D effect no longer as an associated inspectable wrapper at this point.
- The user then calls `GetOrCreate` on the first D2D effect.

Without a callback, Win2D would just fail to resolve a wrapper, as there's no registered wrapper for it. If a factory is registered instead, a new wrapper for that D2D effect can be created and returned, so the scenario just keeps working seamlessly for the user.

## Implementing a custom `ICanvasEffect`

The Win2D `ICanvasEffect` interface extends `ICanvasImage`, so all the previous points apply to custom effects as well. The only difference is the fact that `ICanvasEffect` also implements additional methods specific to effects, such as invalidating a source rectangle, getting the required rectangles and so on.

To support this, Win2D exposes C exports that authors of custom effects can use, so that they won't have to reimplement all this extra logic from scratch. This works in the same way as the C export for `GetBounds`. Here are the available exports for effects:

```cpp
HRESULT InvalidateSourceRectangleForICanvasImageInterop(
    ICanvasResourceCreatorWithDpi* resourceCreator,
    ICanvasImageInterop* image,
    uint32_t sourceIndex,
    Rect const* invalidRectangle);

HRESULT GetInvalidRectanglesForICanvasImageInterop(
    ICanvasResourceCreatorWithDpi* resourceCreator,
    ICanvasImageInterop* image,
    uint32_t* valueCount,
    Rect** valueElements);

HRESULT GetRequiredSourceRectanglesForICanvasImageInterop(
    ICanvasResourceCreatorWithDpi* resourceCreator,
    ICanvasImageInterop* image,
    Rect const* outputRectangle,
    uint32_t sourceEffectCount,
    ICanvasEffect* const* sourceEffects,
    uint32_t sourceIndexCount,
    uint32_t const* sourceIndices,
    uint32_t sourceBoundsCount,
    Rect const* sourceBounds,
    uint32_t valueCount,
    Rect* valueElements);
```

Let's go over how they can be used:
- `InvalidateSourceRectangleForICanvasImageInterop` is meant to support `InvalidateSourceRectangle`. Simply marshal the input parameters and invoke it directly, and it'll take care of all the necessary work. Note that the `image` parameter is the current effect instance being implemented.
- `GetInvalidRectanglesForICanvasImageInterop` supports `GetInvalidRectangles`. This also requires no special consideration, other than needing to dispose the returned COM array once it's no longer needed.
- `GetRequiredSourceRectanglesForICanvasImageInterop` is a shared method that can support both `GetRequiredSourceRectangle` and `GetRequiredSourceRectangles`. That is, it takes a pointer to an existing array of values to populate, so callers can either pass a pointer to a single value (which can also be on the stack, to avoid one allocation), or to an array of values. The implementation is the same in both cases, so a single C export is enough to power both of them.

## Custom effects in C# using ComputeSharp

As we mentioned, if you're using C# and want to implement a custom effect, the recommended approach is to use the [ComputeSharp](https://github.com/Sergio0694/ComputeSharp) library. It enables you to both implement custom D2D1 pixel shaders entirely in C#, as well as to easily define custom effects graphs that are compatible with Win2D. The same library is also used in the Microsoft Store to power several graphics components in the application.

You can add a reference to ComputeSharp in your project through NuGet:
  * On UWP, select the [**ComputeSharp.D2D1.Uwp**](https://www.nuget.org/packages/ComputeSharp.D2D1.Uwp/) package.
  * On WinAppSDK, select the [**ComputeSharp.D2D1.WinUI**](https://www.nuget.org/packages/ComputeSharp.D2D1.WinUI/) package.

> [!NOTE]
> Many APIs in ComputeSharp.D2D1.\* are identical across the UWP and WinAppSDK targets, the only difference being the namespace (ending in either `.Uwp` or `.WinUI`). However, the UWP target is in sustained maintenance and not receiving new features. As such, some code changes might be needed compared to the samples shown here for WinUI. The snippets in this document reflect the API surface as of ComputeSharp.D2D1.WinUI 3.0.0 (the last release for the UWP target is instead 2.1.0).

There are two main components in ComputeSharp to interop with Win2D:
- `PixelShaderEffect<T>`: a Win2D effect that is powered by a D2D1 pixel shader. The shader itself is written in C# using the APIs provided by ComputeSharp. This class also provides properties to set effect sources, constant values, and more.
- `CanvasEffect`: a base class for custom Win2D effects that wraps an arbitrary effect graph. It can be used to "package" complex effects into an easy to use object that can be reused in several parts of an application.

Here is an example of a custom pixel shader (ported from [this shadertoy shader](https://www.shadertoy.com/view/tlVGDt)), used with `PixelShaderEffect<T>` and then draw onto a Win2D `CanvasControl` (note that `PixelShaderEffect<T>` implements `ICanvasImage`):

![a sample pixel shader displaying infinite colored hexagons, being drawn onto a Win2D control, and displayed running in an app window](https://user-images.githubusercontent.com/10199417/224403057-162c9bcf-f974-4275-b0ec-eb68ca8879b8.png)

You can see how in just two lines of code you can create an effect and draw it via Win2D. ComputeSharp takes care of all the work necessary to compile the shader, register it, and manage the complex lifetime of a Win2D-compatible effect.

Next, let's see a step by step guide on how to create a custom Win2D effect that also uses a custom D2D1 pixel shader. We'll go over how to author a shader with ComputeSharp and setup its properties, and then how to create a custom effect graph packaged into a `CanvasEffect` type that can easily be reused in your application.

### Designing the effect

For this demo, we want to create a simple frosted glass effect.

This will include the following components:
- Gaussian blur
- Tint effect
- Noise (which we can procedurally generate with a shader)

We'll also want to expose properties to control the blur and noise amount. The final effect will contain a "packaged" version of this effect graph and be easy to use by just creating an instance, setting those properties, connecting a source image, and then drawing it. Let's get started!

### Creating a custom D2D1 pixel shader

For the noise on top of the effect, we can use a simple D2D1 pixel shader. The shader will compute a random value based on its coordinates (which will act as a "seed" for the random number), and then it will use that noise value to compute the RGB amount for that pixel. We can then blend this noise on top of the resulting image.

To write the shader with ComputeSharp, we just need to define a `partial struct` type implementing the `ID2D1PixelShader` interface, and then write our logic in the `Execute` method. For this noise shader, we can write something like this:

```csharp
using ComputeSharp;
using ComputeSharp.D2D1;

[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader40)]
[D2DGeneratedPixelShaderDescriptor]
public readonly partial struct NoiseShader(float amount) : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        // Get the current pixel coordinate (in pixels)
        int2 position = (int2)D2D.GetScenePosition().XY;

        // Compute a random value in the [0, 1] range for each target pixel. This line just
        // calculates a hash from the current position and maps it into the [0, 1] range.
        // This effectively provides a "random looking" value for each pixel.
        float hash = Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(position, new float2(41, 289))) * 45758.5453f);

        // Map the random value in the [0, amount] range, to control the strength of the noise
        float alpha = Hlsl.Lerp(0, amount, hash);

        // Return a white pixel with the random value modulating the opacity
        return new(1, 1, 1, alpha);
    }
}
```

> [!NOTE]
> While the shader is written entirely in C#, basic knowledge of [HLSL](/windows/win32/direct3dhlsl/dx-graphics-hlsl) (the programming language for DirectX shaders, which ComputeSharp transpiles C# to) is recommended.

Let's go over this shader in detail:
- The shader has no inputs, it just produces an infinite image with random grayscale noise.
- The shader requires access to the current pixel coordinate.
- The shader is precompiled at build time (using the `PixelShader40` profile, which is guaranteed to be available on any GPU where the application could be running).
- The `[D2DGeneratedPixelShaderDescriptor]` attribute is needed to trigger the source generator bundled with ComputeSharp, which will analyze the C# code, transpile it to HLSL, compile the shader to bytecode, etc.
- The shader captures a `float amount` parameter, via its [primary constructor](/dotnet/csharp/whats-new/tutorials/primary-constructors). The source generator in ComputeSharp will automatically take care of extracting all captured values in a shader and preparing the constant buffer that D2D needs to initialize the shader state.

And this part is done! This shader will generate our custom noise texture whenever needed. Next, we need to create our packaged effect with the effect graph connecting all our effects together.

### Creating a custom effect

For our easy to use, packaged effect, we can use the `CanvasEffect` type from ComputeSharp. This type provides a straightforward way to setup all the necessary logic to create an effect graph and update it via public properties that users of the effect can interact with. There are two main methods we'll need to implement:
- `BuildEffectGraph`: this method is responsible for building the effect graph that we want to draw. That is, it needs to create all effects we need, and register the output node for the graph. For effects that can be updated at a later time, the registration is done with an associated `CanvasEffectNode<T>` value, which acts as lookup key to retrieve the effects from the graph when needed.
- `ConfigureEffectGraph`: this method refreshes the effect graph by applying the settings that the user has configured. This method is automatically invoked when needed, right before drawing the effect, and only if at least one effect property has been modified since the last time the effect was used.

Our custom effect can be defined as follows:

```csharp
using ComputeSharp.D2D1.WinUI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

public sealed class FrostedGlassEffect : CanvasEffect
{
    private static readonly CanvasEffectNode<GaussianBlurEffect> BlurNode = new();
    private static readonly CanvasEffectNode<PixelShaderEffect<NoiseShader>> NoiseNode = new();

    private ICanvasImage? _source;
    private double _blurAmount;
    private double _noiseAmount;

    public ICanvasImage? Source
    {
        get => _source;
        set => SetAndInvalidateEffectGraph(ref _source, value);
    }

    public double BlurAmount
    {
        get => _blurAmount;
        set => SetAndInvalidateEffectGraph(ref _blurAmount, value);
    }

    public double NoiseAmount
    {
        get => _noiseAmount;
        set => SetAndInvalidateEffectGraph(ref _noiseAmount, value);
    }

    /// <inheritdoc/>
    protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
    {
        // Create the effect graph as follows:
        //
        // ┌────────┐   ┌──────┐
        // │ source ├──►│ blur ├─────┐
        // └────────┘   └──────┘     ▼
        //                       ┌───────┐   ┌────────┐
        //                       │ blend ├──►│ output │
        //                       └───────┘   └────────┘
        //    ┌───────┐              ▲   
        //    │ noise ├──────────────┘
        //    └───────┘
        //
        GaussianBlurEffect gaussianBlurEffect = new();
        BlendEffect blendEffect = new() { Mode = BlendEffectMode.Overlay };
        PixelShaderEffect<NoiseShader> noiseEffect = new();
        PremultiplyEffect premultiplyEffect = new();

        // Connect the effect graph
        premultiplyEffect.Source = noiseEffect;
        blendEffect.Background = gaussianBlurEffect;
        blendEffect.Foreground = premultiplyEffect;

        // Register all effects. For those that need to be referenced later (ie. the ones with
        // properties that can change), we use a node as a key, so we can perform lookup on
        // them later. For others, we register them anonymously. This allows the effect
        // to autommatically and correctly handle disposal for all effects in the graph.
        effectGraph.RegisterNode(BlurNode, gaussianBlurEffect);
        effectGraph.RegisterNode(NoiseNode, noiseEffect);
        effectGraph.RegisterNode(premultiplyEffect);
        effectGraph.RegisterOutputNode(blendEffect);
    }

    /// <inheritdoc/>
    protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
    {
        // Set the effect source
        effectGraph.GetNode(BlurNode).Source = Source;

        // Configure the blur amount
        effectGraph.GetNode(BlurNode).BlurAmount = (float)BlurAmount;

        // Set the constant buffer of the shader
        effectGraph.GetNode(NoiseNode).ConstantBuffer = new NoiseShader((float)NoiseAmount);
    }
}
```

You can see there are four sections in this class:
- First, we have fields to track all mutable state, such as the effects that can be updated as well as the backing fields for all the effect properties that we want to expose to users of the effect.
- Next, we have properties to configure the effect. The setter of each property uses the `SetAndInvalidateEffectGraph` method exposed by `CanvasEffect`, which will automatically invalidate the effect if the value being set is different than the current one. This ensures the effect is only configured again when really necessary.
- Lastly, we have the `BuildEffectGraph` and `ConfigureEffectGraph` methods we mentioned above.

> [!NOTE]
> The `PremultiplyEffect` node after the noise effect is very important: this is because Win2D effects assume that the output is premultiplied, whereas pixel shaders generally work with unpremultiplied pixels. As such, remember to manually insert premultiply/unpremultiply nodes before and after custom shaders, to ensure colors are correctly preserved.

> [!NOTE]
> This sample effect is using WinUI 3 namespaces, but the same code can be used on UWP as well. In that case, the namespace for ComputeSharp will be `ComputeSharp.Uwp`, matching the package name.

### Ready to draw!

And with this, our custom frosted glass effect is ready! We can easily draw it as follows:

```csharp
private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
{
    FrostedGlassEffect effect = new()
    {
        Source = _canvasBitmap,
        BlurAmount = 12,
        NoiseAmount = 0.1
    };

    args.DrawingSession.DrawImage(effect);
}
```

In this example, we're drawing the effect from the `Draw` handler of a `CanvasControl`, using a `CanvasBitmap` which we previously loaded as source. This is the input image we'll use to test the effect:

![a picture of some mountains under a cloudy sky](images/win2d_mountains_before.png)

And here is the result:

![a blurred version of the picture above](images/win2d_mountains_after.png)

> [!NOTE]
> Credits to [Dominic Lange](https://unsplash.com/it/foto/OWB4VB7Gx9s) for the picture.

## Additional resources

- Check out the [Win2D source code](https://github.com/microsoft/Win2D) for more information.
- For more information on ComputeSharp, check out the [sample apps](https://github.com/Sergio0694/ComputeSharp/tree/main/samples) and the [unit tests](https://github.com/Sergio0694/ComputeSharp/tree/main/tests).
