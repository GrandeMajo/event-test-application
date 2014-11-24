using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1.Border
{
    /// <summary>
    /// Logica di interazione per BorderWindow.xaml
    /// </summary>
    public partial class BorderWindow : Window
    {
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public const int SWP_DEFERERASE     = 0x2000;
        public const int SWP_DRAWFRAME      = 0x0020;
        public const int SWP_FRAMECHANGED   = 0x0020;
        public const int SWP_HIDEWINDOW     = 0x0080;
        public const int SWP_NOACTIVATE     = 0x0010;
        public const int SWP_NOCOPYBITS     = 0x0100;
        public const int SWP_NOMOVE         = 0x0002;
        public const int SWP_NOOWNERZORDER  = 0x0200;
        public const int SWP_NOREDRAW       = 0x0008;
        public const int SWP_NOREPOSITION   = 0x0200;
        public const int SWP_NOSENDCHANGING = 0x0400;
        public const int SWP_NOSIZE         = 0x0001;
        public const int SWP_NOZORDER       = 0x0004;
        public const int SWP_SHOWWINDOW     = 0x0040;

        public const int HWND_TOP       =  0;
        public const int HWND_BOTTOM    =  1;
        public const int HWND_TOPMOST   = -1;
        public const int HWND_NOTOPMOST = -2;


        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        
        public BorderWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                SetWindowPos((IntPtr)hwnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_ASYNCWINDOWPOS | SWP_NOMOVE | SWP_NOSIZE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eccezione");
            }
        }
    }
}
