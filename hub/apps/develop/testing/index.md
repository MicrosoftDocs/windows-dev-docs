---
title: Test apps built with the Windows App SDK and WinUI 3
description: Learn how to test and validate functionality in apps created with the Windows App SDK and WinUI 3, including unit testing with MSTest and the WinUI 3 unit test project templates.
ms.topic: how-to
ms.date: 10/28/2025
ms.localizationpriority: medium
---

# Test WinUI apps built with the Windows App SDK

In this topic we provide some recommendations for how to test and validate functionality in apps created with the [Windows App SDK](/windows/apps/windows-app-sdk/) using [WinUI 3](/windows/apps/winui/winui3/) user interface (UI) features. Testing is an essential part of the app development process—it helps you catch bugs early, maintain code quality, and ensure a reliable user experience as your app evolves. By incorporating unit tests into your workflow, you can confidently refactor code, add new features, and ship updates knowing that existing functionality continues to work as expected.

## Tutorial: Create a WinUI 3 unit test project.

Most object types under the Microsoft.UI.Xaml namespaces must be used from a UI thread in a XAML application process. (For details on testing apps created with Windows App SDK that don't use WinUI 3, see the following section, [Testing non-WinUI functionality](#testing-non-winui-functionality).)

> [!NOTE]
> We recommend that you refactor any code to be tested by pulling it out of the main app project and placing it into a library project. Both your app project and your unit test project can then reference that library project.
This section describes how to create unit tests for WinUI 3 apps in Visual Studio using the built-in unit test project templates.

> [!NOTE]
> The unit test app described here is written in the context of a WinUI 3 application. This is required for any tests that execute code requiring the XAML runtime. This project will create a XAML UI Thread and execute the tests.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create a **WinUI Unit Test App** project in Visual Studio.
> * Use the Visual Studio **Test Explorer**.
> * Add a **WinUI Class Library** project for testing.
> * Run tests with the Visual Studio Test Explorer.

### Prerequisites

You must have Visual Studio installed and setup for WinUI development. See [Quick start: Set up your environment and create a WinUI 3 project](../../get-started/start-here.md).

### Create a WinUI Unit Test App project

To start, create a unit test project. The project type comes with all the template files you need.

1. Open Visual Studio and select **Create a new project** in the Start window.

   :::image type="content" source="images/visual-studio-start-window.png" alt-text="Screenshot of the Visual Studio start window.":::

2. In the **Create a new project** window, filter projects to **C#**, **Windows**, and **WinUI**, select the **WinUI Unit Test App** template, and then select **Next**

   :::image type="content" source="images/visual-studio-new-project-window.png" alt-text="Screenshot of the Visual Studio 'Create a new project' window.":::

3. [Optional] On the **Configure your new project** window, change the **Project name**, **Solution name** (uncheck **Place solution and project in the same directory**) and the **Location** of your project.

4. Select **Create**.

### Run tests with Test Explorer

When you create the test project, your tests appear in **Test Explorer**, which is used to run your unit tests. You can also group tests into categories, filter the test list, create, save, and run playlists of tests, debug unit tests and (in Visual Studio Enterprise) analyze code coverage.

The UnitTests.cs file contains the source code for the unit tests used by **Test Explorer**. By default, the basic sample tests shown here are created automatically:

```csharp
namespace WinUITest1
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void TestMethod1()
      {
         Assert.AreEqual(0, 0);
      }

      // Use the UITestMethod attribute for tests that need to run on the UI thread.
      [UITestMethod]
      public void TestMethod2()
      {
         var grid = new Grid();
         Assert.AreEqual(0, grid.MinWidth);
      }
   }
}
```

1. If you haven't done so already, build your solution. This will allow Visual Studio to "discover" all available tests.

2. Open Test Explorer. If it's not visible, open the **Test** menu, and then choose **Test Explorer** (or press **Ctrl + E, T**).

   :::image type="content" source="images/visual-studio-unittest-test-menu.png" alt-text="Screenshot of the Test menu in Visual Studio.":::

3. View tests. In the **Test Explorer** window, expand all nodes (only the sample tests will be present at this point).

   :::image type="content" source="images/visual-studio-unittest-test-explorer-tests.png" alt-text="Screenshot of the Test Explorer window in Visual Studio showing the default sample tests." :::

4. Run tests.

   - Right click on individual test nodes and select **Run**.
   - Select a test and either press the **Play** button or press **Ctrl + R, T**.
   - Press the **Run All Tests In View** button or press **Ctrl + R, V**.

   :::image type="content" source="images/visual-studio-unittest-tests-run.png" alt-text="Screenshot of the Test Explorer window in Visual Studio showing the test context menu with Run command highlighted." :::

5. Review results. After tests are complete, results are shown in the **Test Explorer** window.

   :::image type="content" source="images/visual-studio-unittest-test-explorer-test-results.png" alt-text="Screenshot of the Test Explorer window in Visual Studio showing the test run results." :::

### Add a Class Library project for testing

6. Add a new project to the unit test solution. In the **Solution Explorer**, right click the solution and select **Add -> New Project...**.

   :::image type="content" source="images/visual-studio-unittest-add-new-project.png" alt-text="Screenshot of the Solution context menu with Add\/New Project highlighted in Visual Studio." :::

7. For this example, add a WinUI 3 class library project. From the New Project window, filter on C#/Windows/WinUI and select **WinUI Class Library**.

   :::image type="content" source="images/visual-studio-unittest-add-winui-class-library-project.png" alt-text="Screenshot of the New Project window with WinUI Class Library highlighted in Visual Studio." :::

8. Select **Next** and enter a name for the project (for this example we use `WinUIClassLibrary1`) and press **Create**.

   :::image type="content" source="images/visual-studio-unittest-winui-class-library.png" alt-text="Screenshot of the new 'WinUI Class Library' project highlighted in the Solution Explorer and the Class1.cs file open in the code editor." :::

9. Add a new `UserControl` to the project. In the **Solution Explorer**, right click on the WinUI 3 class library project you just added and select **Add -> New Item** from the context menu.

   :::image type="content" source="images/visual-studio-unittest-add-new-item.png" alt-text="Screenshot of the Solution context menu with Add\/New Item highlighted in Visual Studio." :::

10. In the **Add New Item** window, select the **WinUI** node in the **Installed** items list and then choose **User Control** from the results. Name the control `UserControl1`.

    :::image type="content" source="images/visual-studio-unittest-add-winui-user-control.png" alt-text="Screenshot of the Add New Item window with WinUI\/User Control (WinUI) highlighted in Visual Studio." :::

11. Open the UserControl1.xaml.cs code-behind file. For this example, we add a new public method called `GetSeven` that simply returns an integer.

    ```csharp
    namespace WinUIClassLibrary1
    {
      public sealed partial class UserControl1 : UserControl
      {
         public UserControl1()
         {
             this.InitializeComponent();
         }

         public int GetSeven()
         {
             return 7;
         }
      }
    }
    ```

12. Set the WinUI 3 Class Library project as a dependency of the unit test project to enable the use of types from the WinUI 3 class library project. In **Solution Explorer**, under the class library project, right click on **Dependencies** and select **Add Project Reference**.

    :::image type="content" source="images/visual-studio-unittest-add-project-reference.png" alt-text="Screenshot of the Dependencies context menu with Add Project Reference highlighted in Visual Studio.":::

    Select the `WinUIClassLibrary1` item from the **Projects** list.

    :::image type="content" source="images/visual-studio-unittest-add-project-reference-ref-manager.png" alt-text="Screenshot of the Reference Manager dialog with the 'WinUIClassLibrary1' project selected.":::

13. Create a new test method in UnitTests.cs. As this test case requires a XAML UI Thread to run, mark it with the `[UITestMethod]` attribute instead of the standard `[TestMethod]` attribute.

    ```csharp
    [UITestMethod]
    public void TestUserControl1()
    {
       WinUIClassLibrary1.UserControl1 userControl1 = new WinUIClassLibrary1.UserControl1();
       Assert.AreEqual(7, userControl1.GetSeven());
    }
    ```

    This new test method now appears in **Test Explorer** as one of your unit tests.

    :::image type="content" source="images/visual-studio-unittest-test-explorer-with-new-test-not-run.png" alt-text="Screenshot of the Test Explorer window in Visual Studio showing the default sample tests with the new unit test.":::

14. Run tests.

   - Right click the new test node and select **Run**.
   - Select the new test and either press the **Play** button or press **Ctrl + R, T**.
   - Press the **Run All Tests In View** button or press **Ctrl + R, V**.

   :::image type="content" source="images/visual-studio-unittest-test-explorer-with-new-test.png" alt-text="Screenshot of the Test Explorer window in Visual Studio showing a completed test run of the default sample tests and the new unit test.":::

## Testing non-WinUI functionality

In many cases, an app includes functionality that does not depend on Microsoft.UI.Xaml types but still needs testing. Various tools are available for testing .NET code, including [MSTest](/dotnet/core/testing/unit-testing-with-mstest), [NUnit](/dotnet/core/testing/unit-testing-with-nunit) and [xUnit](/dotnet/core/testing/unit-testing-with-dotnet-test). For more details on testing .NET apps, see [Testing in .NET](/dotnet/core/testing/).

In Visual Studio, you can create a new project for any of these testing tools by right clicking your solution in Solution Explorer, selecting **Add -> New Project** from the context menu, choosing **C#** from the **All languages** selector/**Windows** from the **All languages** selector/**Test** from the **All project types** selector, and then picking the appropriate testing tool from the list (**MSTest Test Project**, **NUnit Test Project** or **xUnit Test Project**).

When creating a new MSTest, NUnit or xUnit project that references a WinUI 3 project, you must:

1. Update the `TargetFramework` in the .csproj file of your testing project. This value must match the `TargetFramework` in the WinUI 3 project. By default MSTest, NUnit and xUnit projects target the full range of platforms supported by .NET, but a WinUI 3 project only supports Windows and has a more specific TargetFramework.

   For example, if targeting .NET 8, update the TargetFramework of the unit test project from
`<TargetFramework>net8.0</TargetFramework>` to `<TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>`.

2. Update the RuntimeIdentifiers in your test project.

   `<RuntimeIdentifiers Condition="$([MSBuild]::GetTargetFrameworkVersion('$(TargetFramework)')) &gt;= 8">win-x86;win-x64;win-arm64</RuntimeIdentifiers>`

   `<RuntimeIdentifiers Condition="$([MSBuild]::GetTargetFrameworkVersion('$(TargetFramework)')) &lt; 8">win10-x86;win10-x64;win10-arm64</RuntimeIdentifiers>`

3. Add the following property to the `PropertyGroup` in .csproj file of your test project to ensure that the test loads the Windows App SDK runtime:
   `<WindowsAppSdkBootstrapInitialize>true</WindowsAppSdkBootstrapInitialize>`

4. Ensure that the Windows App SDK runtime is installed on the machine running the test. For more information on Windows App SDK deployment, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location (or unpackaged)](../../windows-app-sdk/deploy-unpackaged-apps.md).

## Additional resources

- [Unit test basics](/visualstudio/test/unit-test-basics)
- [Testing tools in Visual Studio](/visualstudio/test/)
