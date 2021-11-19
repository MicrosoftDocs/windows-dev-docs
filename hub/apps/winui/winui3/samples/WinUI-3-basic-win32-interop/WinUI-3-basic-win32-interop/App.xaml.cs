using Microsoft.UI.Xaml;
using System;
using static PInvoke.User32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_3_basic_win32_interop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window m_window;

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
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
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
            var dpi = GetDpiForWindow(hwnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            _ = SetWindowPos(hwnd, SpecialWindowHandles.HWND_TOP,
                                        0, 0, width, height,
                                        SetWindowPosFlags.SWP_NOMOVE);
            _ = SetWindowLong(hwnd, 
                   WindowLongIndexFlags.GWL_STYLE,
                   (SetWindowLongFlags)(GetWindowLong(hwnd,
                      WindowLongIndexFlags.GWL_STYLE) &
                      ~(int)SetWindowLongFlags.WS_MINIMIZEBOX &
                      ~(int)SetWindowLongFlags.WS_MAXIMIZEBOX));
        }
        // </SetWindowDetails>
    }
}
