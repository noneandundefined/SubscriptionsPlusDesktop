using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubscriptionPlusDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region OnSourceInitialized

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            var source = System.Windows.Interop.HwndSource.FromHwnd(hwnd);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            const int BORDER_WIDTH = 8;

            if (msg == WM_NCHITTEST)
            {
                int x = (short)(lParam.ToInt64() & 0xFFFF);
                int y = (short)((lParam.ToInt64() >> 16) & 0xFFFF);

                Point pos = PointFromScreen(new Point(x, y));

                double width = ActualWidth;
                double height = ActualHeight;

                IntPtr result = IntPtr.Zero;

  
                if (pos.Y <= BORDER_WIDTH)
                {
                    if (pos.X <= BORDER_WIDTH) result = (IntPtr)HTTOPLEFT;
                    else if (pos.X >= width - BORDER_WIDTH) result = (IntPtr)HTTOPRIGHT;
                    else result = (IntPtr)HTTOP;
                }

                else if (pos.Y >= height - BORDER_WIDTH)
                {
                    if (pos.X <= BORDER_WIDTH) result = (IntPtr)HTBOTTOMLEFT;
                    else if (pos.X >= width - BORDER_WIDTH) result = (IntPtr)HTBOTTOMRIGHT;
                    else result = (IntPtr)HTBOTTOM;
                }

                else if (pos.X <= BORDER_WIDTH) result = (IntPtr)HTLEFT;
                else if (pos.X >= width - BORDER_WIDTH) result = (IntPtr)HTRIGHT;

                if (result != IntPtr.Zero)
                {
                    handled = true;
                    return result;
                }
            }

            return IntPtr.Zero;
        }


        #endregion

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
