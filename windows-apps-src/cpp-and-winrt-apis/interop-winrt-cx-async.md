---
description: This is an advanced topic related to gradually porting from [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx) to [C++/WinRT](./intro-to-using-cpp-with-winrt.md). It shows how Parallel Patterns Library (PPL) tasks and coroutines can exist side by side in the same project.
title: Asynchrony, and interop between C++/WinRT and C++/CX
ms.date: 08/06/2020
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, port, migrate, interop, C++/CX, PPL, task, coroutine
ms.localizationpriority: medium
---

# Asynchrony, and interop between C++/WinRT and C++/CX

> [!TIP]
> Although we recommend that you read this topic from the beginning, you can jump straight to a summary of interop techniques in the [Overview of porting C++/CX async to C++/WinRT](#overview-of-porting-ccx-async-to-cwinrt) section.

This is an advanced topic related to gradually porting to [C++/WinRT](./intro-to-using-cpp-with-winrt.md) from [C++/CX](/cpp/cppcx/visual-c-language-reference-c-cx). This topic picks up where the topic [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md) leaves off.

If the size or complexity of your codebase makes it necessary to port your project gradually, then you'll need a porting process in which for a time C++/CX and C++/WinRT code exists side by side in the same project. If you have asynchronous code, then you might need to have Parallel Patterns Library (PPL) task chains and coroutines exist side by side in your project as you gradually port your source code. This topic focuses on techniques for interoperating between asynchronous C++/CX code and asynchronous C++/WinRT code. You can use these techniques individually, or together. The techniques allow you to make gradual, controlled, local changes along the path toward porting your entire project, without having each change cascade uncontrollably throughout the project.

Before reading this topic, it's a good idea to read [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md). That topic shows you how to prepare your project for gradual porting. It also introduces two helper functions that you can use to convert a C++/CX object into a C++/WinRT object (and vice versa). This topic about asynchrony builds on that info, and it uses those helper functions.

> [!NOTE]
> There are some limitations to porting gradually from C++/CX to C++/WinRT. If you have a [Windows Runtime component](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md) project, then porting gradually is not possible, and you'll need to port the project in one pass. And for a XAML project, at any given time your XAML page types must be *either* all C++/WinRT *or* all C++/CX. For more info, see the topic [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md).

## The reason an entire topic is dedicated to asynchronous code interop

Porting from C++/CX to C++/WinRT is generally straightforward, with the one exception of moving from [Parallel Patterns Library (PPL)](/cpp/parallel/concrt/parallel-patterns-library-ppl) tasks to coroutines. The models are different. There isn't a natural one-to-one mapping from PPL tasks to coroutines, and there's no simple way (that works for all cases) to mechanically port the code.

The good news is that conversion from tasks to coroutines results in significant simplifications. And development teams routinely report that once they're over the hurdle of porting their asynchronous code, the remainder of the porting work is largely mechanical.

Often, an algorithm was originally written to suit synchronous APIs. And then that was translated into tasks and explicit continuations&mdash;the result often being an inadvertent obfuscation of the underlying logic. For example, loops become recursion; if-else branches turn into a nested tree (a chain) of tasks; shared variables become **shared_ptr**. To deconstruct the often unnatural structure of PPL source code, we recommend that you first step back and understand the intent of the original code (that is, discover the original synchronous version). And then insert `co_await` (cooperatively await) into the appropriate places.

For that reason, if you have a C# (rather than C++/CX) version of the asynchronous code from which to begin your port, then that can give you an easier time, and a cleaner port. C# code uses `await`. So C# code already essentially follows a philosophy of beginning with a synchronous version and then inserting `await` into the appropriate places.

If you *don't* have a C# version of your project, then you can use the techniques described in this topic. And once you've ported to C++/WinRT, the structure of your async code will then be easier to port to C#, should you wish to.

## Some background in asynchronous programming

So that we have a common frame of reference for asynchronous programming concepts and terminology, let's briefly set the scene regarding Windows Runtime asynchronous programming in general, and also how the two C++ language projections are each, in their different ways, layered on top of that.

Your project has methods that do work asynchronously, and there are two main kinds.

- It's common to want to wait on the completion of asynchronous work before you do something else. A method that returns an asynchronous operation object is one that you can wait on.
- But sometimes you don't want or need to wait on the completion of work done asynchronously. In that case it's more efficient for the asynchronous method *not* to return an asynchronous operation object. An asynchronous method such as that&mdash;one that you don't wait on&mdash;is known as a *fire-and-forget* method.

### Windows Runtime async objects (**IAsyncXxx**)

The **Windows::Foundation** Windows Runtime namespace contains four types of asynchronous operation object.

- [**IAsyncAction**](/uwp/api/windows.foundation.iasyncaction),
- [**IAsyncActionWithProgress&lt;TProgress&gt;**](/uwp/api/windows.foundation.iasyncactionwithprogress-1),
- [**IAsyncOperation&lt;TResult&gt;**](/uwp/api/windows.foundation.iasyncoperation-1), and
- [**IAsyncOperationWithProgress&lt;TResult, TProgress&gt;**](/uwp/api/windows.foundation.iasyncoperationwithprogress-2).

In this topic, when we use the convenient shorthand of **IAsyncXxx**, we're referring either to these types collectively; or we're talking about one of the four types without needing to specify which one.

### C++/CX async

Asynchronous C++/CX code makes use of [Parallel Patterns Library (PPL)](/cpp/parallel/concrt/parallel-patterns-library-ppl) tasks. A PPL task is represented by the [**concurrency::task**](/cpp/parallel/concrt/reference/task-class) class.

Typically, an asynchronous C++/CX method chains PPL tasks together by using lambda functions with [**concurrency::create_task**](/cpp/parallel/concrt/reference/concurrency-namespace-functions#create_task) and [**concurrency::task::then**](/cpp/parallel/concrt/reference/task-class#then). Each lambda function returns a task which, when it completes, produces a value that is then passed into the lambda of the task's *continuation*.

Alternatively, instead of calling **create_task** to create a task, an asynchronous C++/CX method can call [**concurrency::create_async**](/cpp/parallel/concrt/reference/concurrency-namespace-functions#create_async) to create an **IAsyncXxx**\^.

So the return type of an asynchronous C++/CX method can be a PPL task, or an **IAsyncXxx**\^.

In either case, the method itself uses the `return` keyword to return an asynchronous object which, when it completes, produces the value that the caller actually wants (perhaps a file, an array of bytes, or a Boolean).

> [!NOTE]
> If an asynchronous C++/CX method returns an **IAsyncXxx**\^, then the **TResult** (if any) is limited to being a Windows Runtime type. A Boolean value, for example, is a Windows Runtime type; but a C++/CX projected type (for example, **Platform::Array<byte>**^) is not.

### C++/WinRT async

C++/WinRT integrates C++ coroutines into the programming model. Coroutines and the `co_await` statement provide a natural way to cooperatively wait for a result.

Each of the **IAsyncXxx** types is projected into a corresponding type in the **winrt::Windows::Foundation** C++/WinRT namespace. Let's refer to those as **winrt::IAsyncXxx** (as compared to the **IAsyncXxx**\^ of C++/CX).

The return type of a C++/WinRT coroutine is either a **winrt::IAsyncXxx**, or [**winrt::fire_and_forget**](/uwp/cpp-ref-for-winrt/fire-and-forget). And instead of using the `return` keyword to return an asynchronous object, a coroutine uses the `co_return` keyword to cooperatively return the value that the caller actually wants (perhaps a file, an array of bytes, or a Boolean).

If a method contains at least one `co_await` statement (or at least one `co_return` or `co_yield`), then the method is a coroutine for that reason.

For more info, and code examples, see [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md).

## The Direct3D game sample (**Simple3DGameDX**)

This topic contains walkthroughs of several specific programming techniques that illustrate how to gradually port asynchronous code. To serve as a case study, we'll be using the C++/CX version of the [Direct3D game sample](/samples/microsoft/windows-universal-samples/simple3dgamedx/) (which is called **Simple3DGameDX**). We'll show some examples of how you can take the original C++/CX source code in that project and gradually port its asynchronous code to C++/WinRT.

- Download the ZIP from the link above, and unzip it.
- Open the C++/CX project (it's in the folder named `cpp`) in Visual Studio.
- You'll then need to add C++/WinRT support to the project. The steps that you follow to do that are described in [Taking a C++/CX project and adding C++/WinRT support](./interop-winrt-cx.md#taking-a-ccx-project-and-adding-cwinrt-support). In that section, the step about adding the `interop_helpers.h` header file to your project is particularly important because we'll be depending on those helper functions in this topic.
- Finally, add `#include <pplawait.h>` to `pch.h`. That gives you coroutine support for PPL (there's more about that support in the following section).

Don't build yet, otherwise you'll get errors about **byte** being ambiguous. Here's how to resolve that.

- Open `BasicLoader.cpp`, and comment out `using namespace std;`.
- In that same source code file, you'll then need to qualify **shared_ptr** as **std::shared_ptr**. You can do that with a search-and-replace within that file.
- Then qualify **vector** as **std::vector**, and **string** as **std::string**.

The project now builds again, has C++/WinRT support, and contains the **from_cx** and **to_cx** interop helper functions.

You now have the **Simple3DGameDX** project ready to follow along with the code walkthroughs in this topic.

## Overview of porting C++/CX async to C++/WinRT

In a nutshell, as we port, we'll be changing PPL task chains into calls to `co_await`. We'll be changing the return value of a method from a PPL task to a C++/WinRT **winrt::IAsyncXxx** object. And we'll also be changing any **IAsyncXxx**\^ to a C++/WinRT **winrt::IAsyncXxx**.

You'll recall that a coroutine is any method that calls `co_xxx`. A C++/WinRT coroutine uses `co_return` to cooperatively return its value. Thanks to the coroutine support for PPL (courtesy of `pplawait.h`), you can also use `co_return` to return a PPL task from a coroutine. And you can also `co_await` both tasks and **IAsyncXxx**. But you can't use `co_return` to return an **IAsyncXxx**\^. The table below describes support for interop between the various asynchronous techniques with `pplawait.h` in the picture.

|Method|Can you `co_await` it?|Can you `co_return` from it?|
|-|-|-|
|Method returns **task\<void\>**|Yes|Yes|
|Method returns **task\<T\>**|No|Yes|
|Method returns **IAsyncXxx**^|Yes|No. But you wrap **create_async** around a task that uses `co_return`.|
|Method returns **winrt::IAsyncXxx**|Yes|Yes|

Use this next table to jump straight to the section in this topic that describes an interop technique of interest, or just continue reading from here.

|Async interop technique|Section in this topic|
|-|-|
|Use `co_await` to await a **task\<void\>** method from within a fire-and-forget method, or within a constructor.|[Await **task\<void\>** within a fire-and-forget method](#await-taskvoid-within-a-fire-and-forget-method)|
|Use `co_await` to await a **task\<void\>** method from within a **task\<void\>** method.|[Await **task\<void\>** within a **task\<void\>** method](#await-taskvoid-within-a-taskvoid-method)|
|Use `co_await` to await a **task\<void\>** method from within a **task\<T\>** method.|[Await **task\<void\>** within a **task\<T\>** method](#await-taskvoid-within-a-taskt-method)|
|Use `co_await` to await an **IAsyncXxx**^ method.|[Await an **IAsyncXxx**^ in a **task** method, leaving the rest of the project unchanged](#await-an-iasyncxxx-in-a-task-method-leaving-the-rest-of-the-project-unchanged)|
|Use `co_return` within a **task\<void\>** method.|[Await **task\<void\>** within a **task\<void\>** method](#await-taskvoid-within-a-taskvoid-method)|
|Use `co_return` within a **task\<T\>** method.|[Await an **IAsyncXxx**^ in a **task** method, leaving the rest of the project unchanged](#await-an-iasyncxxx-in-a-task-method-leaving-the-rest-of-the-project-unchanged)|
|Wrap **create_async** around a task that uses `co_return`.|[Wrap **create_async** around a task that uses `co_return`](#wrap-create_async-around-a-task-that-uses-co_return)|
|Port **concurrency::wait**.|[Port **concurrency::wait** to `co_await winrt::resume_after`](#port-concurrencywait-to-co_await-winrtresume_after)|
|Return **winrt::IAsyncXxx** instead of **task\<void\>**.|[Port a **task\<void\>** return type to **winrt::IAsyncXxx**](#port-a-taskvoid-return-type-to-winrtiasyncxxx)|
|Convert a **winrt::IAsyncXxx\<T\>** (T is primitive) to a **task\<T\>**.|[Convert a **winrt::IAsyncXxx\<T\>** (T is primitive) to a **task\<T\>**](#convert-a-winrtiasyncxxxt-t-is-primitive-to-a-taskt)|
|Convert a **winrt::IAsyncXxx\<T\>** (T is a Windows Runtime type) to a **task\<T^\>**.|[Convert a **winrt::IAsyncXxx\<T\>** (T is a Windows Runtime type) to a **task\<T^\>**](#convert-a-winrtiasyncxxxt-t-is-a-windows-runtime-type-to-a-taskt)|

And here's a short code example illustrating some of the support.

```cppcx
#include <ppltasks.h>
#include <pplawait.h>
#include <winrt/Windows.Foundation.h>

concurrency::task<bool> TaskAsync()
{
    co_return true;
}

Windows::Foundation::IAsyncOperation<bool>^ IAsyncXxxCppCXAsync()
{
    // co_return true; // Error! Can't do that. But you can do
    // the following.
    return concurrency::create_async([=]() -> concurrency::task<bool> {
        co_return true;
        });
}

winrt::Windows::Foundation::IAsyncOperation<bool> IAsyncXxxCppWinRTAsync()
{
    co_return true;
}

concurrency::task<bool> CppCXAsync()
{
    bool b1 = co_await TaskAsync();
    bool b2 = co_await IAsyncXxxCppCXAsync();
    co_return co_await IAsyncXxxCppWinRTAsync();
}

winrt::fire_and_forget CppWinRTAsync()
{
    bool b1 = co_await TaskAsync();
    bool b2 = co_await IAsyncXxxCppCXAsync();
    bool b3 = co_await IAsyncXxxCppWinRTAsync();
}
```

> [!IMPORTANT]
> Even with these great interop options, porting gradually depends on choosing changes that we can make surgically that don't affect the rest of the project. We want to avoid tugging at an arbitrary loose end, and thereby unraveling the structure of the whole project. For that, we have to do things in a particular order. Next we'll look closely at some examples of making these kinds of async-related porting/interop changes.

## Await a **task\<void\>** method, leaving the rest of the project unchanged

A method that returns **task\<void\>** performs work asynchronously, and it returns an asynchronous operation object, but it doesn't ultimately produce a value. We can `co_await` a method like that.

So a good place to start porting async code gradually is to find places where you call such methods. Those places will involve creating and/or returning a task. They might also involve the kind of task chain where no value is passed from each task to its continuation. In places like that, you can just replace the async code with `co_await` statements, as we'll see.

> [!NOTE]
> As this topic progresses, you'll see the benefit to this strategy. Once a particular **task\<void\>** method is being called exclusively via `co_await`, you're then free to port that method to C++/WinRT, and have it return a **winrt::IAsyncXxx**.

Let's find some examples. Open the **Simple3DGameDX** project (see [The Direct3D game sample](#the-direct3d-game-sample-simple3dgamedx)).

> [!IMPORTANT]
> In the examples that follow, as you see the implementations of methods being changed, bear in mind that we don't need to change the *callers* of the methods we're changing. These changes are localized, and they don't cascade through the project.

### Await **task\<void\>** within a fire-and-forget method

Let's start with awaiting **task\<void\>** within *fire-and-forget* methods, since that's the simplest case. These are methods that do work asynchronously, but the caller of the method doesn't wait for that work to complete. You just call the method and forget it, despite the fact that it completes asynchronously.

Look toward the root of your project's dependency graph for `void` methods that contain **create_task** and/or task chains where only **task\<void\>** methods are called.

In **Simple3DGameDX**, you'll find code like that in the implementation of the method **GameMain::Update**. It's in the source code file `GameMain.cpp`.

#### **GameMain::Update**

Here's an extract from the C++/CX version of the method, showing the two parts of the method that complete asynchronously.

```cppcx
void GameMain::Update()
{
    ...
    case UpdateEngineState::WaitingForPress:
        ...
        m_game->LoadLevelAsync().then([this]()
        {
            m_game->FinalizeLoadLevel();
            m_updateState = UpdateEngineState::ResourcesLoaded;
        }, task_continuation_context::use_current());
        ...
    case UpdateEngineState::Dynamics:
        ...
        m_game->LoadLevelAsync().then([this]()
        {
            m_game->FinalizeLoadLevel();
            m_updateState = UpdateEngineState::ResourcesLoaded;
        }, task_continuation_context::use_current());
        ...
    ...
}
```

You can see a call to the **Simple3DGame::LoadLevelAsync** method (which returns a PPL **task\<void\>**). After that is a *continuation* that does some synchronous work. **LoadLevelAsync** is asynchronous, but it doesn't return a value. So no value is being passed from the task to the continuation.

We can make the same kind of change to the code in these two places. The code is explained after the listing below. We could have a discussion here about the safe way to access the *this* pointer in a class-member coroutine. But let's defer that for a later section ([The deferred discussion about `co_await` and the *this* pointer](#the-deferred-discussion-about-co_await-and-the-this-pointer))&mdash;for now, this code works.

```cppcx
winrt::fire_and_forget GameMain::Update()
{
    ...
    case UpdateEngineState::WaitingForPress:
        ...
        co_await m_game->LoadLevelAsync();
        m_game->FinalizeLoadLevel();
        m_updateState = UpdateEngineState::ResourcesLoaded;
        ...
    case UpdateEngineState::Dynamics:
        ...
        co_await m_game->LoadLevelAsync();
        m_game->FinalizeLoadLevel();
        m_updateState = UpdateEngineState::ResourcesLoaded;
        ...
    ...
}
```

As you can see, because **LoadLevelAsync** returns a task, we can `co_await` it. And we don't need an explicit continuation&mdash;the code that follows a `co_await` executes only when **LoadLevelAsync** completes.

Introducing the `co_await` turns the method into a coroutine, so we couldn't leave it returning `void`. It's a fire-and-forget method, so we changed it to return [**winrt::fire_and_forget**](/uwp/cpp-ref-for-winrt/fire-and-forget).

You'll also need to edit `GameMain.h`. Change the return type of **GameMain::Update** from `void` to **winrt::fire_and_forget** in the declaration there, too.

You can make this change to your copy of the project, and the game still builds and runs the same. The source code is still fundamentally C++/CX, but it's now using the same patterns as C++/WinRT, so that has moved us a little closer to being able to port the rest of the code mechanically.

#### **GameMain::ResetGame**

**GameMain::ResetGame** is another fire-and-forget method; it calls **LoadLevelAsync**, too. So you can make the same code change there if you want the practice.

#### **GameMain::OnDeviceRestored**

Things get a little more interesting in **GameMain::OnDeviceRestored** because of its deeper nesting of async code, including a no-op task. Here's an outline of the asynchronous parts of the method (with the less-interesting synchronous code represented by ellipses).

```cppcx
void GameMain::OnDeviceRestored()
{
    ...
    create_task([this]()
    {
        return m_renderer->CreateGameDeviceResourcesAsync(m_game);
    }).then([this]()
    {
        ...
        if (m_updateState == UpdateEngineState::WaitingForResources)
        {
            ...
            return m_game->LoadLevelAsync().then([this]()
            {
                ...
            }, task_continuation_context::use_current());
        }
        else
        {
            return create_task([]()
            {
                // Return a no-op task.
            });
        }
    }, task_continuation_context::use_current()).then([this]()
    {
        ...
    }, task_continuation_context::use_current());
}
```

First, change the return type of **GameMain::OnDeviceRestored** from `void` to **winrt::fire_and_forget** in `GameMain.h` and `.cpp`. You'll also need to open `DeviceResources.h` and make the same change to the return type of **IDeviceNotify::OnDeviceRestored**.

To port the async code, remove all of the **create_task** and **then** calls and their curly brackets, and simplify the method into a flat series of statements.

Change any `return` that returns a task into a `co_await`. You'll be left with one `return` that returns nothing, so just delete that. When you're done, the no-op task will have disappeared, and the outline of the asynchronous parts of the method will look like this. Again, the less-interesting synchronous code is elided.

```cppcx
winrt::fire_and_forget GameMain::OnDeviceRestored()
{
    ...
    co_await m_renderer->CreateGameDeviceResourcesAsync(m_game);
    ...
    if (m_updateState == UpdateEngineState::WaitingForResources)
    {
        co_await m_game->LoadLevelAsync();
        ...
    }
    ...
}
```

As you can see, this form of async structure is significantly simpler, and easier to read.

#### **GameMain::GameMain**

The **GameMain::GameMain** constructor performs work asynchronously, and no part of the project waits for that work to complete. Again, this listing outlines the asynchronous parts.

```cppcx
GameMain::GameMain(...) : ...
{
    ...
    create_task([this]()
    {
        ...
        return m_renderer->CreateGameDeviceResourcesAsync(m_game);
    }).then([this]()
    {
        ...
        if (m_updateState == UpdateEngineState::WaitingForResources)
        {
            return m_game->LoadLevelAsync().then([this]()
            {
                ...
            }, task_continuation_context::use_current());
        }
        else
        {
            return create_task([]()
            {
                // Return a no-op task.
            });
        }
    }, task_continuation_context::use_current()).then([this]()
    {
        ....
    }, task_continuation_context::use_current());
}
```

But a constructor can't return **winrt::fire_and_forget**, so we'll move the asynchronous code into a new **GameMain::ConstructInBackground** fire-and-forget method, flatten the code into `co_await` statements, and call the new method from the constructor. Here's the result.

```cppcx
GameMain::GameMain(...) : ...
{
    ...
    ConstructInBackground();
}

winrt::fire_and_forget GameMain::ConstructInBackground()
{
    ...
    co_await m_renderer->CreateGameDeviceResourcesAsync(m_game);
    ...
    if (m_updateState == UpdateEngineState::WaitingForResources)
    {
        ...
        co_await m_game->LoadLevelAsync();
        ...
    }
    ...
}
```

Now all of the fire-and-forget methods&mdash;in fact, all of the async code&mdash; in **GameMain** has been turned into coroutines. If you feel so inclined, perhaps you could look for fire-and-forget methods in other classes, and make similar changes.

### The deferred discussion about `co_await` and the *this* pointer

When we were making changes to **GameMain::Update**, I deferred a discussion about the *this* pointer. Let's have that discussion here.

This applies to all of the methods we've changed so far; and it applies to *all* coroutines, not just fire-and-forget ones. Introducing a `co_await` into a method introduces a *suspension point*. And because of that, we have to be careful with the *this* pointer, which of course we make use of *after* the suspension point each time we access a class member.

The short story is that the solution is to call [**implements::get_strong**](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function). But for a complete discussion of the issue and the solution, see [Safely accessing the *this* pointer in a class-member coroutine](./weak-references.md#safely-accessing-the-this-pointer-in-a-class-member-coroutine).

You can call **implements::get_strong** only in a class that derives from [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements).

#### Derive **GameMain** from **winrt::implements**

The first change we need to make is in `GameMain.h`.

```cppcx
class GameMain :
    public DX::IDeviceNotify
```

**GameMain** will continue to implement **DX::IDeviceNotify**, but we'll change it to derive from [**winrt::implements**](/uwp/cpp-ref-for-winrt/implements).

```cppwinrt
class GameMain : 
    public winrt::implements<GameMain, winrt::Windows::Foundation::IInspectable>,
    DX::IDeviceNotify
```

Next, in `App.cpp`, you'll find this method.

```cppcx
void App::Load(Platform::String^)
{
    if (!m_main)
    {
        m_main = std::unique_ptr<GameMain>(new GameMain(m_deviceResources));
    }
}
```

But now that **GameMain** derives from **winrt::implements**, we need to construct it in a different way. In this case, we'll use the [**winrt::make_self**](/uwp/cpp-ref-for-winrt/make-self) function template. For more info, see [Instantiating and returning implementation types and interfaces](./author-apis.md#instantiating-and-returning-implementation-types-and-interfaces).

Replace that line of code with this.

```cppwinrt
    ...
    m_main = winrt::make_self<GameMain>(m_deviceResources);
    ...
```

To close the loop on that change, we'll also need to change the type of *m_main*. In `App.h`, you'll find this code.

```cppcx
ref class App sealed :
    public Windows::ApplicationModel::Core::IFrameworkView
{
    ...
private:
    ...
    std::unique_ptr<GameMain> m_main;
};
```

Change that declaration of *m_main* to this.

```cppwinrt
    ...
    winrt::com_ptr<GameMain> m_main;
    ...
```

#### We can now call **implements::get_strong**

For **GameMain::Update**, and for any of the other methods we added a `co_await` to, here's how you can call **get_strong** at the beginning of a coroutine to ensure that a strong reference survives until the coroutine completes.

```cppcx
winrt::fire_and_forget GameMain::Update()
{
    auto strong_this{ get_strong() }; // Keep *this* alive.
    ...
        co_await ...
    ...
}
```

### Await **task\<void\>** within a **task\<void\>** method

The next simplest case is awaiting **task\<void\>** within a method that itself returns **task\<void\>**. That's because we can `co_await` a **task\<void\>**, and we can `co_return` from one.

You'll find a very simple example in the implementation of the method **Simple3DGame::LoadLevelAsync**. It's in the source code file `Simple3DGame.cpp`.

```cppcx
task<void> Simple3DGame::LoadLevelAsync()
{
    m_level[m_currentLevel]->Initialize(m_objects);
    m_levelDuration = m_level[m_currentLevel]->TimeLimit() + m_levelBonusTime;
    return m_renderer->LoadLevelResourcesAsync();
}
```

There's just some synchronous code, followed by returning the task that's created by **GameRenderer::LoadLevelResourcesAsync**.

Instead of returning that task, we `co_await` it, and then `co_return` the resulting `void`.

```cppcx
task<void> Simple3DGame::LoadLevelAsync()
{
    m_level[m_currentLevel]->Initialize(m_objects);
    m_levelDuration = m_level[m_currentLevel]->TimeLimit() + m_levelBonusTime;
    co_return co_await m_renderer->LoadLevelResourcesAsync();
}
```

That doesn't look like a profound change. But now that we're calling **GameRenderer::LoadLevelResourcesAsync** via `co_await`, we're free to port it to return a **winrt::IAsyncXxx** instead of a task. We'll do that later in the section [Port a **task\<void\>** return type to **winrt::IAsyncXxx**](#port-a-taskvoid-return-type-to-winrtiasyncxxx).

### Await **task\<void\>** within a **task\<T\>** method

Although there are no suitable examples to be found in **Simple3DGameDX**, we can contrive a hypothetical example just to show the pattern.

The first line in the code example below demonstrates the simple `co_await` of a **task\<void\>**. Then, in order to satisfy the **task\<T\>** return type, we need to asynchronously return a **StorageFile\^**. To do that, we `co_await` a Windows Runtime API, and `co_return` the resulting file.

```cppcx
task<StorageFile^> Simple3DGame::LoadLevelAndRetrieveFileAsync(
    StorageFolder^ location,
    Platform::String^ filename)
{
    co_await m_renderer->LoadLevelResourcesAsync();
    co_return co_await location->GetFileAsync(filename);
}
```

We could even port more of the method to C++/WinRT like this.

```cppcx
winrt::Windows::Foundation::IAsyncOperation<winrt::Windows::Storage::StorageFile>
Simple3DGame::LoadLevelAndRetrieveFileAsync(
    StorageFolder location,
    std::wstring filename)
{
    co_await m_renderer->LoadLevelResourcesAsync();
    co_return co_await location.GetFileAsync(filename);
}
```

The *m_renderer* data member is still C++/CX in that example.

## Await an **IAsyncXxx**^ in a **task** method, leaving the rest of the project unchanged

We've seen how you can `co_await` **task\<void\>**. You can also `co_await` a method that returns an **IAsyncXxx**, whether that's a method in your project, or an asynchronous Windows API (for example, [**StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync), which we cooperatively awaited in the previous section).

For an example of where we can make this kind of code change, let's look at **BasicReaderWriter::ReadDataAsync** (you'll find it implemented in `BasicReaderWriter.cpp`).

Here's the original C++/CX version.

```cppcx
task<Platform::Array<byte>^> BasicReaderWriter::ReadDataAsync(
    _In_ Platform::String^ filename
    )
{
    return task<StorageFile^>(m_location->GetFileAsync(filename)).then([=](StorageFile^ file)
    {
        return FileIO::ReadBufferAsync(file);
    }).then([=](IBuffer^ buffer)
    {
        auto fileData = ref new Platform::Array<byte>(buffer->Length);
        DataReader::FromBuffer(buffer)->ReadBytes(fileData);
        return fileData;
    });
}
```

The code listing below shows that we can `co_await` Windows APIs that return **IAsyncXxx**^. Not only that, we can also `co_return` the value that **BasicReaderWriter::ReadDataAsync** returns asynchronously (in this case, an array of bytes). This first step shows how to make just those changes; we'll actually port the C++/CX code to C++/WinRT in the next section.

```cppcx
task<Platform::Array<byte>^> BasicReaderWriter::ReadDataAsync(
    _In_ Platform::String^ filename
)
{
    StorageFile^ file = co_await m_location->GetFileAsync(filename);
    IBuffer^ buffer = co_await FileIO::ReadBufferAsync(file);
    auto fileData = ref new Platform::Array<byte>(buffer->Length);
    DataReader::FromBuffer(buffer)->ReadBytes(fileData);
    co_return fileData;
}
```

Again, we don't need to change the *callers* of the methods we're changing, because we didn't change the return type.

### Port **ReadDataAsync** (mostly) to C++/WinRT, leaving the rest of the project unchanged

We can go a step further and port the method *almost entirely* to C++/WinRT without needing to change any other part of the project.

The only dependency that this method has on the rest of the project is the **BasicReaderWriter::m_location** data member, which is a C++/CX **StorageFolder**^. To leave that data member unchanged, and to leave the parameter type and return type unchanged, we need only perform a couple of conversions&mdash;one at the beginning of the method, and one at the end. For that, we can use the **from_cx** and **to_cx** interop helper functions.

Here's how **BasicReaderWriter::ReadDataAsync** looks after porting its implementation predominantly to C++/WinRT. This is a good example of *porting gradually*. And this method is at the stage where we can move away from thinking of it as *a C++/CX method that uses some C++/WinRT techniques*, and see it as *a C++/WinRT method that interoperates with C++/CX*.

```cppwinrt
#include <winrt/Windows.Storage.h>
#include <winrt/Windows.Storage.Streams.h>
#include <robuffer.h>
...
task<Platform::Array<byte>^> BasicReaderWriter::ReadDataAsync(
    _In_ Platform::String^ filename)
{
    auto location_from_cx = from_cx<winrt::Windows::Storage::StorageFolder>(m_location);

    auto file = co_await location_from_cx.GetFileAsync(filename->Data());
    auto buffer = co_await winrt::Windows::Storage::FileIO::ReadBufferAsync(file);
    byte* bytes;
    auto byteAccess = buffer.as<Windows::Storage::Streams::IBufferByteAccess>();
    winrt::check_hresult(byteAccess->Buffer(&bytes));

    co_return ref new Platform::Array<byte>(bytes, buffer.Length());
}
```

> [!NOTE]
> In **ReadDataAsync** above, we construct and return a new C++/CX array. And of course we do that to satisfy the method's return type (so that we don't have to change the rest of the project).
>
> You may come across other examples in your own project where, after porting, you reach the end of the method and all you have is a C++/WinRT object. To `co_return` that, just call **to_cx** to convert it. There's more info about that, and an example, the next section.

## Convert a **winrt::IAsyncXxx\<T\>** to a **task\<T\>**

This section deals with the situation where you've ported an asynchronous method to C++/WinRT (so that it returns a **winrt::IAsyncXxx\<T\>**), but you still have C++/CX code calling that method as if it's still returning a task.

- One case is where **T** is primitive, which needs no conversion.
- The other case is where **T** is a Windows Runtime type, in which case you'll need to convert that to a **T**^.

### Convert a **winrt::IAsyncXxx\<T\>** (T is primitive) to a **task\<T\>**

The pattern in this section applies when you're asynchronously returning a primitive value (we'll use a Boolean value to illustrate). Consider an example where a method that you've already ported to C++/WinRT has this signature.

```cppwinrt
winrt::Windows::Foundation::IAsyncOperation<bool>
MyClass::GetBoolMemberFunctionAsync()
{
    bool value = ...
    co_return value;
}
```

You can convert a call to that method into a task like this.

```cppcx
task<bool> MyClass::RetrieveBoolTask()
{
    co_return co_await GetBoolMemberFunctionAsync();
}
```

Or like this.

```cppcx
task<bool> MyClass::RetrieveBoolTask()
{
    return concurrency::create_task(
        [this]() -> concurrency::task<bool> {
            auto result = co_await GetBoolMemberFunctionAsync();
            co_return result;
        });
}
```

Notice that the **task** return type of the lambda function is explicit, because the compiler can't deduce it.

We could also call the method from within an arbitrary task chain like this. Again, with an explicit lambda return type.

```cppcx
...
.then([this]() -> concurrency::task<bool> {
    co_return co_await GetBoolMemberFunctionAsync();
}).then([this](bool result) {
    ...
});
...
```

### Convert a **winrt::IAsyncXxx\<T\>** (T is a Windows Runtime type) to a **task\<T^\>**

The pattern in this section applies when you're asynchronously returning a Windows Runtime value (we'll use a **StorageFile** value to illustrate). Consider an example where a method that you've already ported to C++/WinRT has this signature.

```cppwinrt
winrt::Windows::Foundation::IAsyncOperation<winrt::Windows::Storage::StorageFile>
MyClass::GetStorageFileMemberFunctionAsync()
{
    co_return co_await winrt::Windows::Storage::StorageFile::GetFileFromPathAsync
    (L"MyFile.txt");
}
```

This next listing shows how to convert a call to that method into a task. Notice that we need to call the **to_cx** interop helper function to convert the returned C++/WinRT object into a C++/CX handle (also known as a *hat*) object.

```cppcx
task<Windows::Storage::StorageFile^> RetrieveStorageFileTask()
{
    winrt::Windows::Storage::StorageFile storageFile =
        co_await GetStorageFileMemberFunctionAsync();
    co_return to_cx<Windows::Storage::StorageFile>(storageFile);
}
```

Here's a more succinct version of that.

```cppcx
task<Windows::Storage::StorageFile^> RetrieveStorageFileTask()
{
    co_return to_cx<Windows::Storage::StorageFile>(GetStorageFileMemberFunctionAsync());
}
```

And you can even choose to wrap that pattern up into a reusable function template, and `return` it just like you'd normally return a task.

```cppcx
template<typename ResultTypeCX, typename Awaitable>
concurrency::task<ResultTypeCX^> to_task(Awaitable awaitable)
{
    co_return to_cx<ResultTypeCX>(co_await awaitable);
}

task<Windows::Storage::StorageFile^> RetrieveStorageFileTask()
{
    return to_task<Windows::Storage::StorageFile>(GetStorageFileMemberFunctionAsync());
}
```

If you like that idea, you might want to add **to_task** to `interop_helpers.h`.

## Wrap **create_async** around a task that uses `co_return`

You can't `co_return` an **IAsyncXxx**\^ directly, but you can achieve something similar. If you have a task that cooperatively returns a value, then you can wrap that inside a call to [**concurrency::create_async**](/cpp/parallel/concrt/reference/concurrency-namespace-functions#create_async).

Here's a hypothetical example, since there isn't an example we can lift from **Simple3DGameDX**.

```cppcx
Windows::Foundation::IAsyncOperation<bool>^ MyClass::RetrieveBoolAsync()
{
    return concurrency::create_async(
        [this]() -> concurrency::task<bool> {
            bool result = co_await GetBoolMemberFunctionAsync();
            co_return result;
        });
}
```

As you can see, you could obtain the return value from any method that you can `co_await`.

## Port **concurrency::wait** to `co_await winrt::resume_after`

There are a couple of places where **Simple3DGameDX** uses [**concurrency::wait**](/cpp/parallel/concrt/reference/concurrency-namespace-functions#wait) to pause the thread for a short amount of time. Here's an example.

```cppcx
// GameConstants.h
namespace GameConstants
{
    ...
    static const int InitialLoadingDelay = 2000;
    ...
}

// GameRenderer.cpp
task<void> GameRenderer::CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game)
{
    std::vector<task<void>> tasks;
    ...
    tasks.push_back(create_task([]()
    {
        wait(GameConstants::InitialLoadingDelay);
    }));
    ...
}
```

The C++/WinRT version of **concurrency::wait** is the **winrt::resume_after** struct. We can `co_await` that struct inside a PPL task. Here's a code example.

```
// GameConstants.h
namespace GameConstants
{
    using namespace std::literals::chrono_literals;
    ...
    static const auto InitialLoadingDelay = 2000ms;
    ...
}

// GameRenderer.cpp
task<void> GameRenderer::CreateGameDeviceResourcesAsync(_In_ Simple3DGame^ game)
{
    std::vector<task<void>> tasks;
    ...
    tasks.push_back(create_task([]() -> task<void>
    {
        co_await winrt::resume_after(GameConstants::InitialLoadingDelay);
    }));
    ...
}
```

Notice the two other changes that we had to make. We changed the type of **GameConstants::InitialLoadingDelay** to **std::chrono::duration**, and we made the return type of the lambda function explicit, because the compiler is no longer able to deduce it.

## Port a **task\<void\>** return type to **winrt::IAsyncXxx**

### **Simple3DGame::LoadLevelAsync**

At this stage in our work with **Simple3DGameDX**, all of the places in the project that call **Simple3DGame::LoadLevelAsync** use `co_await` to call it.

That means that we can simply change that method's return type from **task\<void\>** to **winrt::Windows::Foundation::IAsyncAction** (leaving the rest of it unchanged).

```cppcx
winrt::Windows::Foundation::IAsyncAction Simple3DGame::LoadLevelAsync()
{
    m_level[m_currentLevel]->Initialize(m_objects);
    m_levelDuration = m_level[m_currentLevel]->TimeLimit() + m_levelBonusTime;
    co_return co_await m_renderer->LoadLevelResourcesAsync();
}
```

It should now be fairly mechanical to port the rest of that method, and its dependencies (such as *m_level*, and so on), to C++/WinRT.

### **GameRenderer::LoadLevelResourcesAsync**

Here's the original C++/CX version of **GameRenderer::LoadLevelResourcesAsync**.

```cppcx
// GameConstants.h
namespace GameConstants
{
    ...
    static const int LevelLoadingDelay = 500;
    ...
}

// GameRenderer.cpp
task<void> GameRenderer::LoadLevelResourcesAsync()
{
    m_levelResourcesLoaded = false;

    return create_task([this]()
    {
        wait(GameConstants::LevelLoadingDelay);
    });
}
```

**Simple3DGame::LoadLevelAsync** is the only place in the project that calls **GameRenderer::LoadLevelResourcesAsync**, and it already uses `co_await` to call it.

So there's no longer any need for **GameRenderer::LoadLevelResourcesAsync** to return a task&mdash;it can return a **winrt::Windows::Foundation::IAsyncAction** instead. And the implementation itself is simple enough to port completely to C++/WinRT. That involves making the same change we made in [Port **concurrency::wait** to `co_await winrt::resume_after`](#port-concurrencywait-to-co_await-winrtresume_after). And there are no significant dependencies on the rest of the project to worry about.

So here's how the method looks after porting it completely to C++/WinRT.

```cppwinrt
// GameConstants.h
namespace GameConstants
{
    using namespace std::literals::chrono_literals;
    ...
    static const auto LevelLoadingDelay = 500ms;
    ...
}

// GameRenderer.cpp
winrt::Windows::Foundation::IAsyncAction GameRenderer::LoadLevelResourcesAsync()
{
    m_levelResourcesLoaded = false;
    co_return co_await winrt::resume_after(GameConstants::LevelLoadingDelay);
}
```

### The goal&mdash;fully port a method to C++/WinRT

Let's wrap up this walkthrough with an example of the end goal, by fully porting the method **BasicReaderWriter::ReadDataAsync** to C++/WinRT.

Last time we looked at this method (in the section [Port **ReadDataAsync** (mostly) to C++/WinRT, leaving the rest of the project unchanged](#port-readdataasync-mostly-to-cwinrt-leaving-the-rest-of-the-project-unchanged)), it was *mostly* ported to C++/WinRT. But it still returned a task of **Platform::Array\<byte\>**^.

```cppwinrt
task<Platform::Array<byte>^> BasicReaderWriter::ReadDataAsync(
    _In_ Platform::String^ filename)
{
    auto location_from_cx = from_cx<winrt::Windows::Storage::StorageFolder>(m_location);

    auto file = co_await location_from_cx.GetFileAsync(filename->Data());
    auto buffer = co_await winrt::Windows::Storage::FileIO::ReadBufferAsync(file);
    byte* bytes;
    auto byteAccess = buffer.as<Windows::Storage::Streams::IBufferByteAccess>();
    winrt::check_hresult(byteAccess->Buffer(&bytes));

    co_return ref new Platform::Array<byte>(bytes, buffer.Length());
}
```

Instead of returning a task, we'll change it to return an **IAsyncOperation**. And instead of returning an array of bytes via that **IAsyncOperation**, we'll instead return a C++/WinRT [**IBuffer**](/uwp/api/windows.storage.streams.ibuffer) object. That will also require a minor change to the code at the call sites, as we'll see.

Here's how the method looks after porting its implementation, its parameter, and the *m_location* data member to use C++/WinRT syntax and objects.

```cppwinrt
winrt::Windows::Foundation::IAsyncOperation<winrt::Windows::Storage::Streams::IBuffer>
BasicReaderWriter::ReadDataAsync(
    _In_ winrt::hstring const& filename)
{
    StorageFile file{ co_await m_location.GetFileAsync(filename) };
    co_return co_await FileIO::ReadBufferAsync(file);
}

winrt::array_view<byte> BasicLoader::GetBufferView(
    winrt::Windows::Storage::Streams::IBuffer const& buffer)
{
    byte* bytes;
    auto byteAccess = buffer.as<Windows::Storage::Streams::IBufferByteAccess>();
    winrt::check_hresult(byteAccess->Buffer(&bytes));
    return { bytes, bytes + buffer.Length() };
}
```

As you can see, **BasicReaderWriter::ReadDataAsync** itself is much simpler, because we've factored into its own method the synchronous logic that retrieves bytes from the buffer.

But now we need to port the call sites from this kind of structure in C++/CX.

```cppcx
task<void> BasicLoader::LoadTextureAsync(...)
{
    return m_basicReaderWriter->ReadDataAsync(filename).then(
        [=](const Platform::Array<byte>^ textureData)
    {
        CreateTexture(...);
    });
}
```

To this pattern in C++/WinRT.

```cppwinrt
winrt::Windows::Foundation::IAsyncAction BasicLoader::LoadTextureAsync(...)
{
    auto textureBuffer = co_await m_basicReaderWriter.ReadDataAsync(filename);
    auto textureData = GetBufferView(textureBuffer);
    CreateTexture(...);
}
```

## Important APIs

* [IAsyncAction](/uwp/api/windows.foundation.iasyncaction)
* [IAsyncActionWithProgress&lt;TProgress&gt;](/uwp/api/windows.foundation.iasyncactionwithprogress-1)
* [IAsyncOperation&lt;TResult&gt;](/uwp/api/windows.foundation.iasyncoperation-1)
* [IAsyncOperationWithProgress&lt;TResult, TProgress&gt;](/uwp/api/windows.foundation.iasyncoperationwithprogress-2)
* [implements::get_strong](/uwp/cpp-ref-for-winrt/implements#implementsget_strong-function)
* [concurrency::create_async](/cpp/parallel/concrt/reference/concurrency-namespace-functions#create_async)
* [concurrency::create_task](/cpp/parallel/concrt/reference/concurrency-namespace-functions#create_task)
* [concurrency::task](/cpp/parallel/concrt/reference/task-class)
* [concurrency::task::then](/cpp/parallel/concrt/reference/task-class#then)
* [concurrency::wait](/cpp/parallel/concrt/reference/concurrency-namespace-functions#wait)
* [winrt::fire_and_forget](/uwp/cpp-ref-for-winrt/fire-and-forget)
* [winrt::make_self](/uwp/cpp-ref-for-winrt/make-self)

## Related topics

* [Move to C++/WinRT from C++/CX](./move-to-winrt-from-cx.md)
* [Interop between C++/WinRT and C++/CX](./interop-winrt-cx.md)
* [Concurrency and asynchronous operations with C++/WinRT](./concurrency.md)
* [Strong and weak references in C++/WinRT](./weak-references.md)
* [Author APIs with C++/WinRT](./author-apis.md)