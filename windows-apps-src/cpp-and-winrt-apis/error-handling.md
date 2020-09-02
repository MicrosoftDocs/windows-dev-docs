---
description: This topic discusses strategies for handling errors when programming with C++/WinRT.
title: Error handling with C++/WinRT
ms.date: 04/23/2019
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, error, handling, exception
ms.localizationpriority: medium
---

# Error handling with C++/WinRT

This topic discusses strategies for handling errors when programming with [C++/WinRT](./intro-to-using-cpp-with-winrt.md). For more general info, and background, see [Errors and Exception Handling (Modern C++)](/cpp/cpp/errors-and-exception-handling-modern-cpp).

## Avoid catching and throwing exceptions
We recommend that you continue to write [exception-safe code](/cpp/cpp/how-to-design-for-exception-safety), but that you prefer to avoid catching and throwing exceptions whenever possible. If there's no handler for an exception, then Windows automatically generates an error report (including a minidump of the crash), which will help you track down where the problem is.

Don't throw an exception that you expect to catch. And don't use exceptions for expected failures. Throw an exception *only when an unexpected runtime error occurs*, and handle everything else with error/result codes&mdash;directly, and close to the source of the failure. That way, when an exception *is* thrown, you know that the cause is either a bug in your code, or an exceptional error state in the system.

Consider the scenario of accessing the Windows Registry. If your app fails to read a value from the Registry, then that's to be expected, and you should handle it gracefully. Don't throw an exception; rather return a `bool` or `enum` value indicating that, and perhaps why, the value wasn't read. Failing to *write* a value to the Registry, on the other hand, is likely to indicate that there's a bigger problem than you can handle sensibly in your application. In a case like that, you don't want your application to continue, so an exception that results in an error report is the fastest way to keep your application from causing any harm.

For another example, consider retrieving a thumbnail image from a call to [**StorageFile.GetThumbnailAsync**](/uwp/api/windows.storage.storagefile.getthumbnailasync#Windows_Storage_StorageFile_GetThumbnailAsync_Windows_Storage_FileProperties_ThumbnailMode_), and then passing that thumbnail to [**BitmapSource.SetSourceAsync**](/uwp/api/windows.ui.xaml.media.imaging.bitmapsource.setsourceasync#Windows_UI_Xaml_Media_Imaging_BitmapSource_SetSourceAsync_Windows_Storage_Streams_IRandomAccessStream_). If that sequence of calls causes you to pass `nullptr` to **SetSourceAsync** (the image file can't be read; perhaps its file extension makes it look like it contains image data, but it actually doesn't), then you'll cause an invalid pointer exception to be thrown. If you discover a case like that in your code, rather than catching and handling the case as an exception, instead check for `nullptr` returned from **GetThumbnailAsync**.

Throwing exceptions tends to be slower than using error codes. If you only throw an exception when a fatal error occurs, then if all goes well you'll never pay the performance price.

But a more likely performance hit involves the runtime overhead of ensuring that the appropriate destructors are called in the unlikely event that an exception is thrown. The cost of this assurance comes whether an exception is actually thrown or not. So, you should ensure that the compiler has a good idea of what functions can potentially throw exceptions. If the compiler can prove that there won't be any exceptions from certain functions (the `noexcept` specification), then it can optimize the code it generates.

## Catching exceptions
An error condition that arises at the [Windows Runtime ABI](interop-winrt-abi.md#what-is-the-windows-runtime-abi-and-what-are-abi-types) layer is returned in the form of a HRESULT value. But you don't need to handle HRESULTs in your code. The C++/WinRT projection code that's generated for an API on the consuming side detects an error HRESULT code at the ABI layer and converts the code into a [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) exception, which you can catch and handle. If you *do* wish to handle HRESULTS, then use the **winrt::hresult** type.

For example, if the user happens to delete an image from the Pictures Library while your application is iterating over that collection, then the projection throws an exception. And this is a case where you'll have to catch and handle that exception. Here's a code example showing this case.

```cppwinrt
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.Storage.h>
#include <winrt/Windows.UI.Xaml.Media.Imaging.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage;
using namespace Windows::UI::Xaml::Media::Imaging;

IAsyncAction MakeThumbnailsAsync()
{
    auto imageFiles{ co_await KnownFolders::PicturesLibrary().GetFilesAsync() };

    for (StorageFile const& imageFile : imageFiles)
    {
        BitmapImage bitmapImage;
        try
        {
            auto thumbnail{ co_await imageFile.GetThumbnailAsync(FileProperties::ThumbnailMode::PicturesView) };
            if (thumbnail) bitmapImage.SetSource(thumbnail);
        }
        catch (winrt::hresult_error const& ex)
        {
            winrt::hresult hr = ex.code(); // HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND).
            winrt::hstring message = ex.message(); // The system cannot find the file specified.
        }
    }
}
```

Use this same pattern in a coroutine when calling a `co_await`-ed function. Another example of this HRESULT-to-exception conversion is that when a component API returns E_OUTOFMEMORY, that causes a **std::bad_alloc** to be thrown.

Prefer [**winrt::hresult_error::code**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errorcode-function) when you're just peeking at a HRESULT code. The [**winrt::hresult_error::to_abi**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errorto_abi-function) function on the other hand converts to a COM error object, and pushes state into the COM thread-local storage.

## Throwing exceptions
There will be cases where you decide that, should your call to a given function fail, your application won't be able to recover (you'll no longer be able to rely on it to function predictably). The code example below uses a [**winrt::handle**](/uwp/cpp-ref-for-winrt/handle) value as a wrapper around the HANDLE returned from [**CreateEvent**](/windows/desktop/api/synchapi/nf-synchapi-createeventa). It then passes the handle (creating a `bool` value from it) to the [**winrt::check_bool**](/uwp/cpp-ref-for-winrt/error-handling/check-bool) function template. **winrt::check_bool** works with a `bool`, or with any value that's convertible to `false` (an error condition), or `true` (a success condition).

```cppwinrt
winrt::handle h{ ::CreateEvent(nullptr, false, false, nullptr) };
winrt::check_bool(bool{ h });
winrt::check_bool(::SetEvent(h.get()));
```

If the value that you pass to [**winrt::check_bool**](/uwp/cpp-ref-for-winrt/error-handling/check-bool) is false, then the following sequence of actions take place.

- **winrt::check_bool** calls the [**winrt::throw_last_error**](/uwp/cpp-ref-for-winrt/error-handling/throw-last-error) function.
- **winrt::throw_last_error** calls [**GetLastError**](/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror) to retrieve the calling thread's last-error code value, and then calls the [**winrt::throw_hresult**](/uwp/cpp-ref-for-winrt/error-handling/throw-hresult) function.
- **winrt::throw_hresult** throws an exception using a [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) object (or a standard object) that represents that error code.

Because Windows APIs report run-time errors using various return-value types, there are in addition to **winrt::check_bool** a handful of other useful helper functions for checking values and throwing exceptions.

- [**winrt::check_hresult**](/uwp/cpp-ref-for-winrt/error-handling/check-hresult). Checks whether the HRESULT code represents an error and, if so, calls **winrt::throw_hresult**.
- [**winrt::check_nt**](/uwp/cpp-ref-for-winrt/error-handling/check-nt). Checks whether a code represents an error and, if so, calls **winrt::throw_hresult**.
- [**winrt::check_pointer**](/uwp/cpp-ref-for-winrt/error-handling/check-pointer). Checks whether a pointer is null and, if so, calls **winrt::throw_last_error**.
- [**winrt::check_win32**](/uwp/cpp-ref-for-winrt/error-handling/check-win32). Checks whether a code represents an error and, if so, calls **winrt::throw_hresult**.

You can use these helper functions for common return code types, or you can respond to any error condition and call either [**winrt::throw_last_error**](/uwp/cpp-ref-for-winrt/error-handling/throw-last-error) or [**winrt::throw_hresult**](/uwp/cpp-ref-for-winrt/error-handling/throw-hresult). 

## Throwing exceptions when authoring an API
All [Windows Runtime Application Binary Interface](interop-winrt-abi.md#what-is-the-windows-runtime-abi-and-what-are-abi-types) boundaries (or ABI boundaries) must be *noexcept*&mdash;meaning that exceptions must never escape there. When you author an API, you should always mark the ABI boundary with the C++ `noexcept` keyword. `noexcept` has specific behavior in C++. If a C++ exception hits a `noexcept` boundary, then the process will fail fast with **std::terminate**. That behavior is generally desirable, because an unhandled exception almost always implies unknown state in the process.

Since exceptions mustn't cross the ABI boundary, an error condition that arises in an implementation is returned across the ABI layer in the form of an HRESULT error code. When you're authoring an API using C++/WinRT, code is generated for you to convert any exception that you *do* throw in your implementation into an HRESULT. The [**winrt::to_hresult**](/uwp/cpp-ref-for-winrt/error-handling/to-hresult) function is used in that generated code in a pattern like this.

```cppwinrt
HRESULT DoWork() noexcept
{
    try
    {
        // Shim through to your C++/WinRT implementation.
        return S_OK;
    }
    catch (...)
    {
        return winrt::to_hresult(); // Convert any exception to an HRESULT.
    }
}
```

[**winrt::to_hresult**](/uwp/cpp-ref-for-winrt/error-handling/to-hresult) handles exceptions derived from **std::exception**, and [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) and its derived types. In your implementation, you should prefer **winrt::hresult_error**, or a derived type, so that consumers of your API receive rich error information. **std::exception** (which maps to E_FAIL) is supported in case exceptions arise from your use of the Standard Template Library.

### Debuggability with noexcept
As we mentioned above, a C++ exception hitting a `noexcept` boundary fails fast with **std::terminate**. That's not ideal for debugging, because **std::terminate** often loses much or all of the error or the exception context thrown, especially when coroutines are involved.

So, this section deals with the case where your ABI method (which you've properly annotated with `noexcept`) uses `co_await` to call asynchronous C++/WinRT projection code. We recommend that you wrap the calls to the C++/WinRT projection code within a **winrt::fire_and_forget**. Doing so provides a proper place for an unhandled exception to be properly recorded as a stowed exception, which greatly increases debuggability.

```cppwinrt
HRESULT MyWinRTObject::MyABI_Method() noexcept
{
    winrt::com_ptr<Foo> foo{ get_a_foo() };

    [/*no captures*/](winrt::com_ptr<Foo> foo) -> winrt::fire_and_forget
    {
        co_await winrt::resume_background();

        foo->ABICall();

        AnotherMethodWithLotsOfProjectionCalls();
    }(foo);

    return S_OK;
}
```

**winrt::fire_and_forget** has a built-in `unhandled_exception` method helper, which calls **winrt::terminate**, which in turn calls **RoFailFastWithErrorContext**. This guarantees that any context (stowed exception, error code, error message, stack backtrace, and so on) is preserved either for live debugging or for a post-mortem dump. For convenience, you can factor the fire-and-forget portion into a separate function that returns a **winrt::fire_and_forget**, and then call that.

### Synchronous code
In some cases, your ABI method (which, again, you've properly annotated with `noexcept`) calls only synchronous code. In other words, it never uses `co_await`, either to call an asynchronous Windows Runtime method, or to switch between foreground and background threads. In that case, the fire_and_forget technique will still work, but it's not efficient. Instead, you can do something like this.

```cppwinrt
HRESULT abi() noexcept try
{
    // ABI code goes here.
} catch (...) { winrt::terminate(); }
```

### Fail fast
The code in the previous section still fails fast. As written, that code doesn't handle any exceptions. Any unhandled exception results in program termination.

But that form is superior, because it ensures debuggability. In rare cases, you might want to `try/catch`, and handle certain exceptions. But that should be rare because, as this topic explains, we discourage using exceptions as a flow-control mechanism for conditions that you expect.

Remember that it's a bad idea to let an unhandled exception escape a naked `noexcept` context. Under that condition, the C++ runtime will **std::terminate** the process, thereby losing any stowed exception information that C++/WinRT carefully recorded.

## Assertions
For internal assumptions in your application, there are assertions. Prefer **static_assert** for compile-time validation, wherever possible. For run-time conditions, use `WINRT_ASSERT` with a Boolean expression. `WINRT_ASSERT` is a macro definition, and it expands to [_ASSERTE](/cpp/c-runtime-library/reference/assert-asserte-assert-expr-macros).

```cppwinrt
WINRT_ASSERT(pos < size());
```

WINRT_ASSERT is compiled away in release builds; in a debug build, it stops the application in the debugger on the line of code where the assertion is.

You shouldn't use exceptions in your destructors. So, at least in debug builds, you can assert the result of calling a function from a destructor with WINRT_VERIFY (with a Boolean expression) and WINRT_VERIFY_ (with an expected result and a Boolean expression).

```cppwinrt
WINRT_VERIFY(::CloseHandle(value));
WINRT_VERIFY_(TRUE, ::CloseHandle(value));
```

## Important APIs
* [winrt::check_bool function template](/uwp/cpp-ref-for-winrt/error-handling/check-bool)
* [winrt::check_hresult function](/uwp/cpp-ref-for-winrt/error-handling/check-hresult)
* [winrt::check_nt function template](/uwp/cpp-ref-for-winrt/error-handling/check-nt)
* [winrt::check_pointer function template](/uwp/cpp-ref-for-winrt/error-handling/check-pointer)
* [winrt::check_win32 function template](/uwp/cpp-ref-for-winrt/error-handling/check-win32)
* [winrt::handle struct](/uwp/cpp-ref-for-winrt/handle)
* [winrt::hresult_error struct](/uwp/cpp-ref-for-winrt/error-handling/hresult-error)
* [winrt::throw_hresult function](/uwp/cpp-ref-for-winrt/error-handling/throw-hresult)
* [winrt::throw_last_error function](/uwp/cpp-ref-for-winrt/error-handling/throw-last-error)
* [winrt::to_hresult function](/uwp/cpp-ref-for-winrt/error-handling/to-hresult)

## Related topics
* [Errors and Exception Handling (Modern C++)](/cpp/cpp/errors-and-exception-handling-modern-cpp)
* [How to: Design for Exception Safety](/cpp/cpp/how-to-design-for-exception-safety)