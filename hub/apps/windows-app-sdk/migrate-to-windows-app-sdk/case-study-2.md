---
title: A Windows App SDK migration of the UWP Photo Editor sample app (C++/WinRT)
description: A case study of taking the C++/WinRT [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/), and migrating it to the Windows App SDK.
ms.topic: article
ms.date: 11/17/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, C++/WinRT, Photo, Editor, UWP
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# A Windows App SDK migration of the UWP Photo Editor sample app (C++/WinRT)

This topic is a case study of taking the C++/WinRT [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/), and migrating it to the Windows App SDK.

* Begin by [cloning the UWP sample app's repo](https://github.com/microsoft/windows-appsample-photo-editor/tree/master/), and opening the solution in [Visual Studio](https://visualstudio.microsoft.com/downloads/).

> [!IMPORTANT]
> For considerations and strategies for approaching the migration process, and how to set up your development environment for migrating, see [Overall migration strategy](overall-migration-strategy.md).

## Install tools for the Windows App SDK

To set up your development computer, see [Install tools for the Windows App SDK](../set-up-your-development-environment.md).

> [!IMPORTANT]
> You'll find release notes topics along with the [Windows App SDK release channels](../release-channels.md) topic. There are release notes for each channel. Be sure to check any *limitations and known issues* in those release notes, since those might affect the results of following along with this case study and/or running the migrated app.

## Create a new project

* In Visual Studio, create a new C++/WinRT project from the **Blank App, Packaged (WinUI 3 in Desktop)** project template. Name the project *PhotoEditor*, uncheck **Place solution and project in the same directory**. You can target the most recent release (not preview) of the client operating system.

> [!NOTE]
> We'll be referring to the UWP version of the sample project (the one that you cloned from its [repo](https://github.com/microsoft/windows-appsample-photo-editor/tree/master/)) as the *source* solution/project. We'll be referring to the Windows App SDK version as the *target* solution/project.

## The order in which we'll migrate the code

**MainPage** is an important and prominent piece of the app. But if we were to begin by migrating that, then we'd soon realize that **MainPage** has a dependency on the **DetailPage** view; and then that **DetailPage** has a dependency on the **Photo** model. So for this walkthrough we'll take this approach.

* We'll begin by copying over the asset files.
* Then we'll migrate the **Photo** model.
* Next we'll migrate the **App** class (because that needs some members adding to it that **DetailPage** and **MainPage** will depend on).
* Then we'll begin migrating the views, starting with **DetailPage** first.
* And we'll finish up by migrating the **MainPage** view.

### We'll be copying entire source code files

In this walkthrough we'll be copying over source code files using **File Explorer**. If you prefer to copy file *contents* over, then see the [Appendix: copying the contents of the **Photo** model's files](#appendix-copying-the-contents-of-the-photo-models-files) section at the end of this topic for an example of how you could do that for **Photo** (and you could then apply a similar procedure to other types in the project). That option does involve a lot more steps, though.

## Copy asset files

1. In your clone of the source project, in **File Explorer**, locate the folder **Windows-appsample-photo-editor** > **PhotoEditor** > **Assets**. You'll find eight asset files in that folder. Select those eight files, and copy them to the clipboard.

2. Also in **File Explorer**, now locate the corresponding folder in the target project that you created. The path to that folder is **PhotoEditor** > **PhotoEditor** > **Assets**. Paste into that folder the asset files that you just copied, and accept the prompt to replace the seven files that already exist in the destination.

3. In your target project in Visual Studio, in **Solution Explorer**, expand the **Assets** folder. Add to that folder the existing `bg1.png` asset file that you just pasted. You can hover the mouse pointer over the asset files. A thumbnail preview will appear for each, confirming that you've replaced/added the asset files correctly.

## Migrate the Photo model

**Photo** is a runtime class that represents a photo. It's a *model* (in the sense of models, views, and viewmodels).

### Copy Photo source code files

1. In your clone of the source project, in **File Explorer**, locate the folder **Windows-appsample-photo-editor** > **PhotoEditor**. In that folder you'll find the three source code files `Photo.idl`, `Photo.h`, and `Photo.cpp`; those files together implement the **Photo** runtime class. Select those three files, and copy them to the clipboard.

2. In Visual Studio, right-click the target project node, and click **Open Folder in File Explorer**. This opens the target project folder in **File Explorer**. Paste into that folder the three files that you just copied.

3. Back in **Solution Explorer**, with the target project node selected, make sure that **Show All Files** is toggled on. Right-click the three files that you just pasted, and click **Include In Project**. Toggle **Show All Files** off.

4. In the source project, in **Solution Explorer**, `Photo.h` and `.cpp` are nested under `Photo.idl` to indicate that they're generated from (dependent upon) it. If you like that arrangement, then you can do the same thing in the target project by manually editing `\PhotoEditor\PhotoEditor\PhotoEditor\PhotoEditor.vcxproj` (you'll first need to **Save All** in Visual Studio). Find the following:

   ```xml
   <ClInclude Include="Photo.h" />
   ```

   And replace it with this:

   ```xml
   <ClInclude Include="Photo.h">
     <DependentUpon>Photo.idl</DependentUpon>
   </ClInclude>
   ```

   Repeat that for `Photo.cpp`, and save and close the project file. When you set focus back to Visual Studio, click **Reload**.

### Migrate Photo source code

1. In `Photo.idl`, search for the namespace name `Windows.UI.Xaml` (which is the namespace for UWP XAML), and change that to `Microsoft.UI.Xaml` (which is the namespace for WinUI XAML).

> [!NOTE]
> The [Mapping UWP APIs to the Windows App SDK](api-mapping-table.md) topic provides a mapping of UWP APIs to their Windows App SDK equivalents. The change we made above is an example of a namespace name change necessary during the migration process.

2. In `Photo.cpp`, add `#include "Photo.g.cpp"` to the existing include directives, immediately after `#include "Photo.h"`. This is one of the [Folder and file name differences (C++/WinRT)](overall-migration-strategy.md#folder-and-file-name-differences-cwinrt) to be aware of between UWP and Windows App SDK projects.

3. Make the following find/replacement (match case and whole word) in the contents of all of the source code in the files that you just copied and pasted.

   * `Windows::UI::Xaml` => `Microsoft::UI::Xaml`

4. From `pch.h` in the source project, copy the following includes, and paste them into `pch.h` in the target project. This is a subset of the header files included in the source project; these are just the headers we need to support the code we've migrated so far.

   ```cppwinrt
   #include <winrt/Microsoft.UI.Xaml.Media.Imaging.h>
   #include <winrt/Windows.Storage.h>
   #include <winrt/Windows.Storage.FileProperties.h>
   #include <winrt/Windows.Storage.Streams.h>
   ```

5. Now confirm that you can build the target solution (but don't run yet).

## Migrate the App class

No changes are necessary to the target project's `App.idl` and `App.xaml`. But we do need to edit `App.xaml.h` and App`.xaml.cpp` to add some new members to the **App** class. We'll do so in a way that lets us build after each section (with the exception of the last section, which is about **App::OnLaunched**).

### Making the main window object available

In this step we'll make the change that's explained in [Change Windows.UI.Xaml.Window.Current to App.Window](guides/winui3.md#change-windowsuixamlwindowcurrent-to-appwindow).

In the target project, **App** stores the main window object in its private data member *window*. Later in the migration process (when we migrate the source project's use of **Window.Current**), it'll be convenient if that *window* data member is static; and is also made available via an accessor function. So we'll make those changes next.

* Since we're making *window* static, we'll need to initialize it in `App.xaml.cpp` instead of via the default member initializer that the code is currently using. Here are what those changes look like in `App.xaml.h` and `App.xaml.cpp`.

   ```cppwinrt
   // App.xaml.h
   ...
   struct App : AppT<App>
   {
       ...
       static winrt::Microsoft::UI::Xaml::Window Window(){ return window; };

   private:
       static winrt::Microsoft::UI::Xaml::Window window;
   };
   ...

   // App.xaml.cpp
   ...
   winrt::Microsoft::UI::Xaml::Window App::window{ nullptr };
   ...
   ```

### App::OnNavigationFailed

The *Photo Editor* sample app uses navigation logic to navigate between **MainPage** and **DetailPage**. For more info about Windows App SDK apps that need navigation (and those that don't), see [Do I need to implement page navigation?](guides/winui3.md#do-i-need-to-implement-page-navigation).

So the members we'll migrate in the next few sections all exist to support navigation within the app.

1. Let's begin by migrating the **OnNavigationFailed** event handler. Copy the declaration and the definition of that member function from the source project, and paste it into the target project (in `App.xaml.h` and `App.xaml.cpp`).

2. In the code you pasted into `App.xaml.h`, change `Windows::UI::Xaml` to `Microsoft::UI::Xaml`.

### App::CreateRootFrame

1. The source project contains a helper function named **App::CreateRootFrame**. Copy the declaration and the definition of that helper function from the source project, and paste it into the target project (in `App.xaml.h` and `App.xaml.cpp`).

2. In the code you pasted into `App.xaml.h`, change `Windows::UI::Xaml` to `Microsoft::UI::Xaml`.

3. In the code you pasted into `App.xaml.cpp`, change the two occurrences of `Window::Current()` to `window` (which is the name of the data member of the **App** class that we saw earlier).

### App::OnLaunched

The target project already contains an implementation of the **OnLaunched** event handler. Its parameter is a constant reference to a **Microsoft::UI::Xaml::LaunchActivatedEventArgs**, which is correct for the Windows App SDK (contrast that to the source project, which uses **Windows::ApplicationModel::Activation::LaunchActivatedEventArgs**, which is correct for UWP).

* We just need to merge the two definitions (source and target) of **OnLaunched** so that **App::OnLaunched** in `App.xaml.cpp` in the target project looks like the listing below. Note that it uses `window` (instead of `Window::Current()`, like the UWP version does).

   ```cppwinrt
   void App::OnLaunched(LaunchActivatedEventArgs const&)
   {
       window = make<MainWindow>();

       Frame rootFrame = CreateRootFrame();
       if (!rootFrame.Content())
       {
           rootFrame.Navigate(xaml_typename<PhotoEditor::MainPage>());
       }

       window.Activate();
   }
   ```

The code above gives **App** a dependency on **MainPage**, so we won't be able to build from this point until we've migrated **DetailPage** and then **MainPage**. When we're able to build again, we'll say so.

## Migrate the DetailPage view

**DetailPage** is the class that represents the photo editor page, where Win2D effects are toggled, set, and chained together. You get to the photo editor page by selecting a photo thumbnail on **MainPage**. **DetailPage** is a *view* (in the sense of models, views, and viewmodels).

### Reference the Win2D NuGet package

To support code in **DetailPage**, the source project has a dependency on [Microsoft.Graphics.Win2D](https://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm). So we'll also need a dependency on Win2D in our target project.

* In the target solution in Visual Studio, click **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution...** > **Browse**. Make sure that **Include prerelease** is unchecked, and type or paste *Microsoft.Graphics.Win2D* into the search box. Select the correct item in search results, check the *PhotoEditor* project, and click **Install** to install the package.

### Copy DetailPage source code files

1. In your clone of the source project, in **File Explorer**, locate the folder **Windows-appsample-photo-editor** > **PhotoEditor**. In that folder you'll find the four source code files `DetailPage.idl`, `DetailPage.xaml`, `DetailPage.h`, and `DetailPage.cpp`; those files together implement the **DetailPage** view. Select those four files, and copy them to the clipboard.

2. In Visual Studio, right-click the target project node, and click **Open Folder in File Explorer**. This opens the target project folder in **File Explorer**. Paste into that folder the four files that you just copied.

3. Still in **File Explorer**, change the names of `DetailPage.h` and `DetailPage.cpp` to `DetailPage.xaml.h` and `DetailPage.xaml.cpp`, respectively. This is one of the [Folder and file name differences (C++/WinRT)](overall-migration-strategy.md#folder-and-file-name-differences-cwinrt) to be aware of between UWP and Windows App SDK projects.

4. Back in **Solution Explorer**, with the target project node selected, make sure that **Show All Files** is toggled on. Right-click the four files that you just pasted (and renamed), and click **Include In Project**. Toggle **Show All Files** off.

5. In the source project, in **Solution Explorer**, `DetailPage.idl` is nested under `DetailPage.xaml`. If you like that arrangement, then you can do the same thing in the target project by manually editing `\PhotoEditor\PhotoEditor\PhotoEditor\PhotoEditor.vcxproj` (you'll first need to **Save All** in Visual Studio). Find the following:

   ```xml
   <Midl Include="DetailPage.idl" />
   ```

   And replace it with this:

   ```xml
   <Midl Include="DetailPage.idl">
     <DependentUpon>DetailPage.xaml</DependentUpon>
   </Midl>
   ```

Save and close the project file. When you set focus back to Visual Studio, click **Reload**.

### Migrate DetailPage source code

1. In `DetailPage.idl`, search for `Windows.UI.Xaml`, and change that to `Microsoft.UI.Xaml`.

2. In `DetailPage.xaml.cpp`, change `#include "DetailPage.h"` to `#include "DetailPage.xaml.h"`.

3. Immediately below that, add `#include "DetailPage.g.cpp"`.

4. For the call to the static **App::Window** method (which we're about to add) to compile, still in `DetailPage.xaml.cpp`, add `#include "App.xaml.h"` immediately before `#include "Photo.h"`.

5. Make the following find/replacements (match case and whole word) in the contents of the source code in the files that you just copied and pasted.

   * In `DetailPage.xaml.h` and `.xaml.cpp`, `Windows::UI::Composition` => `Microsoft::UI::Composition`
   * In `DetailPage.xaml.h` and `.xaml.cpp`, `Windows::UI::Xaml` => `Microsoft::UI::Xaml`
   * In `DetailPage.xaml.cpp`, `Window::Current()` => `App::Window()`

6. From `pch.h` in the source project, copy the following includes, and paste them into `pch.h` in the target project.

   ```cppwinrt
   #include <winrt/Windows.Graphics.Effects.h>
   #include <winrt/Microsoft.Graphics.Canvas.Effects.h>
   #include <winrt/Microsoft.Graphics.Canvas.UI.Xaml.h>
   #include <winrt/Microsoft.UI.Composition.h>
   #include <winrt/Microsoft.UI.Xaml.Input.h>
   #include <winrt/Windows.Graphics.Imaging.h>
   #include <winrt/Windows.Storage.Pickers.h>
   ```

7. Also, at the top of `pch.h`, immediately after `#pragma once`, add this:

   ```cppwinrt
   // This is required because we are using std::min and std::max, otherwise 
   // we have a collision with min and max macros being defined elsewhere.
   #define NOMINMAX
   ```

We can't build yet, but we will be able to after migrating **MainPage** (which is next).

## Migrate the MainPage view

The main page of the app represents the view that you see first when you run the app. It's the page that loads the photos from the **Pictures Library**, and displays a tiled thumbnail view.

### Copy MainPage source code files

1. Similar to what you did with **DetailPage**, now copy over `MainPage.idl`, `MainPage.xaml`, `MainPage.h`, and `MainPage.cpp`.

2. Rename the `.h` and `.cpp` files to `.xaml.h` and `.xaml.cpp`, respectively. 

3. Include all four files in the target project like before.

4. In the source project, in **Solution Explorer**, `MainPage.idl` is nested under `MainPage.xaml`. If you like that arrangement, then you can do the same thing in the target project by manually editing `\PhotoEditor\PhotoEditor\PhotoEditor\PhotoEditor.vcxproj`. Find the following:

   ```xml
   <Midl Include="MainPage.idl" />
   ```

   And replace it with:

   ```xml
   <Midl Include="MainPage.idl">
     <DependentUpon>MainPage.xaml</DependentUpon>
   </Midl>
   ```

### Migrate MainPage source code

1. In `MainPage.idl`, search for `Windows.UI.Xaml`, and change both occurrences to `Microsoft.UI.Xaml`.

2. In `MainPage.xaml.cpp`, change `#include "MainPage.h"` to `#include "MainPage.xaml.h"`.

3. Immediately below that, add `#include "MainPage.g.cpp"`.

4. For the call to the static **App::Window** method (which we're about to add) to compile, still in `MainPage.xaml.cpp`, add `#include "App.xaml.h"` immediately before `#include "Photo.h"`.

For the next step, we'll make the change that's explained in [ContentDialog, and Popup](guides/winui3.md#contentdialog-and-popup).

5. So, still in `MainPage.xaml.cpp`, in the **MainPage::GetItemsAsync** method, immediately after the line `ContentDialog unsupportedFilesDialog{};`, add this line of code.

   ```cppwinrt
   unsupportedFilesDialog.XamlRoot(this->Content().XamlRoot());
   ```

6. Make the following find/replacements (match case and whole word) in the contents of the source code in the files that you just copied and pasted.

   * In `MainPage.xaml.h` and `.xaml.cpp`, `Windows::UI::Composition` => `Microsoft::UI::Composition`
   * In `MainPage.xaml.h` and `.xaml.cpp`, `Windows::UI::Xaml` => `Microsoft::UI::Xaml`
   * In `MainPage.xaml.cpp`, `Window::Current()` => `App::Window()`

7. From `pch.h` in the source project, copy the following includes, and paste them into `pch.h` in the target project.

   ```cppwinrt
   #include <winrt/Microsoft.UI.Xaml.Hosting.h>
   #include <winrt/Microsoft.UI.Xaml.Media.Animation.h>
   #include <winrt/Windows.Storage.Search.h>
   ```

Confirm that you can build the target solution (but don't run yet).

## Update MainWindow

1. In `MainWindow.xaml`, delete the **StackPanel** and its contents, since we don't need any UI in **MainWindow**. That leaves only the empty **Window** element.

2. In `MainWindow.idl`, delete the placeholder `Int32 MyProperty;`, leaving only the constructor.

3. In `MainWindow.xaml.h` and `MainWindow.xaml.cpp`, delete the declarations and definitions of the placeholder **MyProperty** and **myButton_Click**, leaving only the constructor.

## Migration changes needed for threading model difference

The two changes in this section are necessary due to a threading model difference between UWP and the Windows App SDK, as described in [ASTA to STA threading model](guides/threading.md#asta-to-sta-threading-model). Here are brief descriptions of the causes of the issues, and then a way to resolve each.

### MainPage

**MainPage** loads image files from your **Pictures** folder, calls [**StorageItemContentProperties.GetImagePropertiesAsync**](/uwp/api/windows.storage.fileproperties.storageitemcontentproperties.getimagepropertiesasync) to get the image file's properties, creates a **Photo** model object for each image file (saving those same properties in a data member), and adds that **Photo** object to a collection. The collection of **Photo** objects is data-bound to a **GridView** in the UI. On behalf of that **GridView**, **MainPage** handles the [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event, and for phase 1 that handler calls into a coroutine that calls [**StorageFile.GetThumbnailAsync**](/uwp/api/windows.storage.storagefile.getthumbnailasync). This call to **GetThumbnailAsync** results in messages being pumped (it doesn't return immediately, and do *all* of its work async), and that causes reentrancy. The result is that the **GridView** has its **Items** collection changed while layout is taking place, and that causes a crash.

If we comment out the call to **StorageItemContentProperties::GetImagePropertiesAsync**, then we don't get the crash. But the real fix is to make the **StorageFile.GetThumbnailAsync** call be explicitly async by cooperatively awaiting **wil::resume_foreground** immediately before calling **GetThumbnailAsync**. This works because **wil::resume_foreground** schedules the code that follows it to be a task on the **DispatcherQueue**.

Here's the code to change:

```cppwinrt
// MainPage.xaml.cpp
IAsyncAction MainPage::OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
{
    ...
    if (args.Phase() == 1)
    {
        ...
        try
        {
            co_await wil::resume_foreground(this->DispatcherQueue());
            auto thumbnail = co_await impleType->GetImageThumbnailAsync(this->DispatcherQueue());
            image.Source(thumbnail);
        }
        ...
    }
}
```

### Photo

The **Photo::ImageTitle** property is data-bound to the UI, so the UI calls into the accessor function for that property whenever it needs its value. But when we try to access [**ImageProperties.Title**](/uwp/api/windows.storage.fileproperties.imageproperties.title) from that accessor function on the UI thread, we get an access violation.

So instead, we can just access that **Title** one-time, from the constructor of **Photo**, and store it in the *m_imageName* data member if it's not empty. Then in the **Photo::ImageTitle** accessor function we need only to access the *m_imageName* data member.

Here's the code to change:

```
// Photo.h
...
Photo(Photo(Windows::Storage::FileProperties::ImageProperties const& props,
    ...
    ) : ...
{
    if (m_imageProperties.Title() != L"")
    {
        m_imageName = m_imageProperties.Title();
    }
}
...
hstring ImageTitle() const
{
    return m_imageName;
}
...
```

Those are the last of the changes we need to make to migrate the *Photo Editor* sample app. In the **Test the migrated app** section we'll confirm that we've correctly followed the steps.

## Known issues

### App type issue (affects only Preview 3)

If you followed along with this case study using the project template from the VSIX for Windows App SDK [version 1.0 Preview 3](../release-notes-archive/preview-channel-1.0.md#winui-3-100-preview3), then you'll need to make a small correction to `PhotoEditor.vcxproj`. Here's how to do that.

In Visual Studio, in **Solution Explorer**, right-click the project node, and click **Unload Project**. Now `PhotoEditor.vcxproj` is open for editing. As the first child of **Project**, add a **PropertyGroup** element like this:

```xml
<Project ... >
    <PropertyGroup>
        <EnableWin32Codegen>true</EnableWin32Codegen>
    </PropertyGroup>
    <Import ... />
...
```

Save and close `PhotoEditor.vcxproj`. Right-click the project node, and click **Reload Project**. Now rebuild the project.

## Test the migrated app

Now build the project, and run the app to test it. Select an image, set a zoom level, choose effects, and configure them.

## Appendix: copying the contents of the **Photo** model's files

As we discussed earlier, you have the option to copy over source code *files* themselves, or the *contents* of source code files. We've already shown how to copy over source code *files* themselves. So this section gives an example of copying file *contents*.

In the source project in Visual Studio, locate the folder **PhotoEditor (Universal Windows)** > **Models**. That folder contains the files `Photo.idl`, `Photo.h`, and `Photo.cpp`, which together implement the **Photo** runtime class.

### Add the IDL, and generate stubs

In your target project in Visual Studio, add a new **Midl File (.idl)** item to the project. Name the new item `Photo.idl`. Delete the default contents of `Photo.idl`.

From the source project in Visual Studio, copy the contents of **Models** > `Photo.idl`, and paste them into the `Photo.idl` file that you just added to your target project. In the code you pasted, search for `Windows.UI.Xaml`, and change that to `Microsoft.UI.Xaml`.

Save the file.

> [!IMPORTANT]
> We're about to perform a build of your target solution. The build won't run to completion at this point, but it will get far enough to do necessary work for us.

Now build the target solution. Even though it won't complete, building is necessary now because it'll generate the source code files (stubs) that we need to get started implementing the **Photo** model.

In Visual Studio, right-click the target project node, and click **Open Folder in File Explorer**. This opens the target project folder in **File Explorer**. There, navigate into the `Generated Files\sources` folder (so you'll be in `\PhotoEditor\PhotoEditor\PhotoEditor\Generated Files\sources`). Copy the stub files `Photo.h` and `.cpp`, and paste them into the project folder, which is now up two folder levels in `\PhotoEditor\PhotoEditor\PhotoEditor`.

Back in **Solution Explorer**, with the target project node selected, make sure that **Show All Files** is toggled on. Right-click the stub files that you just pasted (`Photo.h` and `.cpp`), and click **Include In Project**. Toggle **Show All Files** off.

You'll see a `static_assert` at the top of the contents of `Photo.h` and `.cpp`, which you'll need to delete.

Confirm that you can build again (but don't run yet).

### Migrate code into the stubs

Copy the contents of `Photo.h` and `.cpp` from the source project into the target project.

From here, the remaining steps to migrate the code that you copied are the same as those given in the [Migrate **Photo** source code](#migrate-photo-source-code) section.

## Related topics

* [Windows App SDK and suppported Windows releases](../support.md)
* [UWP Photo Editor sample app](/samples/microsoft/windows-appsample-photo-editor/photo-editor-cwinrt-sample-application/)
* [Overall migration strategy](overall-migration-strategy.md)
* [Mapping UWP APIs to the Windows App SDK](api-mapping-table.md)
* [WinUI migration](guides/winui3.md)
