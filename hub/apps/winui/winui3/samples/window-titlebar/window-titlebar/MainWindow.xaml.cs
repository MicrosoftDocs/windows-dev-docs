using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace window_titlebar
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        // <basicButton_Click>
        private void basicButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the custom title bar content is not displayed.
            customTitleBarPanel.Visibility = Visibility.Collapsed;

            // Disable custom title bar content.
            ExtendsContentIntoTitleBar = false;

            //Get the Window's HWND
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            var hIcon = Windows.Win32.PInvoke.LoadImage(
                null,
                "Images/windowIcon.ico",
                Windows.Win32.UI.WindowsAndMessaging.GDI_IMAGE_TYPE.IMAGE_ICON,
                20, 20,
                Windows.Win32.UI.WindowsAndMessaging.IMAGE_FLAGS.LR_LOADFROMFILE);

            Windows.Win32.PInvoke.SendMessage(
                (Windows.Win32.Foundation.HWND)hwnd,
                Windows.Win32.PInvoke.WM_SETICON,
                (Windows.Win32.Foundation.WPARAM)0,
                (Windows.Win32.Foundation.LPARAM)hIcon.DangerousGetHandle());

            Windows.Win32.PInvoke.SetWindowText((Windows.Win32.Foundation.HWND)hwnd, "Basic customization of title bar");
        }
        // </basicButton_Click>

        // <customButton_Click>
        private void customButton_Click(object sender, RoutedEventArgs e)
        {
            customTitleBarPanel.Visibility = Visibility.Visible;

            // Enable custom title bar content.
            ExtendsContentIntoTitleBar = true;
            // Set the content of the custom title bar.
            SetTitleBar(customTitleBarPanel);
        }
        // </customButton_Click>
    }
}
