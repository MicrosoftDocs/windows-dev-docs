using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_3_basic_win32_interop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        // <OnLaunched>
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(m_window);

            SetWindowDetails(hwnd, 800, 600);

            m_window.Activate();
        }
        // </OnLaunched>

        // <SetWindowDetails>
        private static void SetWindowDetails(IntPtr hwnd, int width, int height)
        {
            var dpi = Windows.Win32.PInvoke.GetDpiForWindow((Windows.Win32.Foundation.HWND)hwnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            _ = Windows.Win32.PInvoke.SetWindowPos((Windows.Win32.Foundation.HWND)hwnd,
                                        Windows.Win32.Foundation.HWND.HWND_TOP,
                                        0, 0, width, height,
                                        Windows.Win32.UI.WindowsAndMessaging.SET_WINDOW_POS_FLAGS.SWP_NOMOVE);

            var nIndex = Windows.Win32.PInvoke.GetWindowLong((Windows.Win32.Foundation.HWND)hwnd,
                      Windows.Win32.UI.WindowsAndMessaging.WINDOW_LONG_PTR_INDEX.GWL_STYLE) &
                      ~(int)Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE.WS_MINIMIZEBOX &
                      ~(int)Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE.WS_MAXIMIZEBOX;

            _ = Windows.Win32.PInvoke.SetWindowLong((Windows.Win32.Foundation.HWND)hwnd,
                   Windows.Win32.UI.WindowsAndMessaging.WINDOW_LONG_PTR_INDEX.GWL_STYLE,
                   nIndex);
        }
        // </SetWindowDetails>

        private Window? m_window;
    }
}
