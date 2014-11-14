using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace WpfApplication1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool turn = false;
        private double widthRatio = (65535.0 / System.Windows.SystemParameters.PrimaryScreenWidth);
        private double heightRatio = (65535.0 / System.Windows.SystemParameters.PrimaryScreenHeight);
        private ModifierKeys modifierKey = ModifierKeys.Alt;
        private Key key = Key.Q;
        private KeyGesture keyGesture;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public int X;
            public int Y;

            public Win32Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return (X + "," + Y);
            }

            public static implicit operator Point(Win32Point p)
            {
                return new Point(p.X, p.Y);
            }

            public static implicit operator Win32Point(Point p)
            {
                return new Win32Point((int)p.X, (int)p.Y);
            }
        }

        public static Point CorrectGetPosition(Visual relativeTo)
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return relativeTo.PointFromScreen(new Point(w32Mouse.X, w32Mouse.Y));
        }


        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public void SetPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN    = 0x00000002,
            LEFTUP      = 0x00000004,
            MIDDLEDOWN  = 0x00000020,
            MIDDLEUP    = 0x00000040,
            MOVE        = 0x00000001,
            ABSOLUTE    = 0x00008000,
            RIGHTDOWN   = 0x00000008,
            RIGHTUP     = 0x00000010,
            WHEEL       = 0x00000800,
            XDOWN       = 0x00000080,
            XUP         = 0x00000100
        }

        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;   // If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224).
        public const uint KEYEVENTF_KEYUP = 0x0002;         // If specified, the key is being released. If not specified, the key is being depressed.

        enum VirtualKey : int
        {
            LBUTTON = 0x01,     //Left mouse button
            RBUTTON = 0x02,     //Right mouse button
            CANCEL = 0x03,     //Control-break processing
            MBUTTON = 0x04,     //Middle mouse button (three-button mouse)
            XBUTTON1 = 0x05,     //Windows 2000/XP: X1 mouse button
            XBUTTON2 = 0x06,     //Windows 2000/XP: X2 mouse button
            BACK = 0x08,     //BACKSPACE key
            TAB = 0x09,     //TAB key
            CLEAR = 0x0C,     //CLEAR key
            RETURN = 0x0D,     //ENTER key
            SHIFT = 0x10,     //SHIFT key
            CONTROL = 0x11,     //CTRL key
            MENU = 0x12,     //ALT key
            PAUSE = 0x13,     //PAUSE key
            CAPITAL = 0x14,     //CAPS LOCK key
            KANA = 0x15,     //Input Method Editor (IME) Kana mode
            HANGUL = 0x15,     //IME Hangul mode
            JUNJA = 0x17,     //IME Junja mode
            FINAL = 0x18,     //IME final mode
            HANJA = 0x19,     //IME Hanja mode
            KANJI = 0x19,     //IME Kanji mode
            ESCAPE = 0x1B,     //ESC key
            CONVERT = 0x1C,     //IME convert
            NONCONVERT = 0x1D,     //IME nonconvert
            ACCEPT = 0x1E,     //IME accept
            MODECHANGE = 0x1F,     //IME mode change request
            SPACE = 0x20,     //SPACEBAR
            PRIOR = 0x21,     //PAGE UP key
            NEXT = 0x22,     //PAGE DOWN key
            END = 0x23,     //END key
            HOME = 0x24,     //HOME key
            LEFT = 0x25,     //LEFT ARROW key
            UP = 0x26,     //UP ARROW key
            RIGHT = 0x27,     //RIGHT ARROW key
            DOWN = 0x28,     //DOWN ARROW key
            SELECT = 0x29,     //SELECT key
            PRINT = 0x2A,     //PRINT key
            EXECUTE = 0x2B,     //EXECUTE key
            SNAPSHOT = 0x2C,     //PRINT SCREEN key
            INSERT = 0x2D,     //INS key
            DELETE = 0x2E,     //DEL key
            HELP = 0x2F,     //HELP key
            KEY_0 = 0x30,     //0 key
            KEY_1 = 0x31,     //1 key
            KEY_2 = 0x32,     //2 key
            KEY_3 = 0x33,     //3 key
            KEY_4 = 0x34,     //4 key
            KEY_5 = 0x35,     //5 key
            KEY_6 = 0x36,     //6 key
            KEY_7 = 0x37,     //7 key
            KEY_8 = 0x38,     //8 key
            KEY_9 = 0x39,     //9 key
            KEY_A = 0x41,     //A key
            KEY_B = 0x42,     //B key
            KEY_C = 0x43,     //C key
            KEY_D = 0x44,     //D key
            KEY_E = 0x45,     //E key
            KEY_F = 0x46,     //F key
            KEY_G = 0x47,     //G key
            KEY_H = 0x48,     //H key
            KEY_I = 0x49,     //I key
            KEY_J = 0x4A,     //J key
            KEY_K = 0x4B,     //K key
            KEY_L = 0x4C,     //L key
            KEY_M = 0x4D,     //M key
            KEY_N = 0x4E,     //N key
            KEY_O = 0x4F,     //O key
            KEY_P = 0x50,     //P key
            KEY_Q = 0x51,     //Q key
            KEY_R = 0x52,     //R key
            KEY_S = 0x53,     //S key
            KEY_T = 0x54,     //T key
            KEY_U = 0x55,     //U key
            KEY_V = 0x56,     //V key
            KEY_W = 0x57,     //W key
            KEY_X = 0x58,     //X key
            KEY_Y = 0x59,     //Y key
            KEY_Z = 0x5A,     //Z key
            LWIN = 0x5B,     //Left Windows key (Microsoft Natural keyboard) 
            RWIN = 0x5C,     //Right Windows key (Natural keyboard)
            APPS = 0x5D,     //Applications key (Natural keyboard)
            SLEEP = 0x5F,     //Computer Sleep key
            NUMPAD0 = 0x60,     //Numeric keypad 0 key
            NUMPAD1 = 0x61,     //Numeric keypad 1 key
            NUMPAD2 = 0x62,     //Numeric keypad 2 key
            NUMPAD3 = 0x63,     //Numeric keypad 3 key
            NUMPAD4 = 0x64,     //Numeric keypad 4 key
            NUMPAD5 = 0x65,     //Numeric keypad 5 key
            NUMPAD6 = 0x66,     //Numeric keypad 6 key
            NUMPAD7 = 0x67,     //Numeric keypad 7 key
            NUMPAD8 = 0x68,     //Numeric keypad 8 key
            NUMPAD9 = 0x69,     //Numeric keypad 9 key
            MULTIPLY = 0x6A,     //Multiply key
            ADD = 0x6B,     //Add key
            SEPARATOR = 0x6C,     //Separator key
            SUBTRACT = 0x6D,     //Subtract key
            DECIMAL = 0x6E,     //Decimal key
            DIVIDE = 0x6F,     //Divide key
            F1 = 0x70,     //F1 key
            F2 = 0x71,     //F2 key
            F3 = 0x72,     //F3 key
            F4 = 0x73,     //F4 key
            F5 = 0x74,     //F5 key
            F6 = 0x75,     //F6 key
            F7 = 0x76,     //F7 key
            F8 = 0x77,     //F8 key
            F9 = 0x78,     //F9 key
            F10 = 0x79,     //F10 key
            F11 = 0x7A,     //F11 key
            F12 = 0x7B,     //F12 key
            F13 = 0x7C,     //F13 key
            F14 = 0x7D,     //F14 key
            F15 = 0x7E,     //F15 key
            F16 = 0x7F,     //F16 key
            F17 = 0x80,     //F17 key  
            F18 = 0x81,     //F18 key  
            F19 = 0x82,     //F19 key  
            F20 = 0x83,     //F20 key  
            F21 = 0x84,     //F21 key  
            F22 = 0x85,     //F22 key, (PPC only) Key used to lock device.
            F23 = 0x86,     //F23 key  
            F24 = 0x87,     //F24 key  
            NUMLOCK = 0x90,     //NUM LOCK key
            SCROLL = 0x91,     //SCROLL LOCK key
            LSHIFT = 0xA0,     //Left SHIFT key
            RSHIFT = 0xA1,     //Right SHIFT key
            LCONTROL = 0xA2,     //Left CONTROL key
            RCONTROL = 0xA3,     //Right CONTROL key
            LMENU = 0xA4,     //Left MENU key
            RMENU = 0xA5,     //Right MENU key
            BROWSER_BACK = 0xA6,     //Windows 2000/XP: Browser Back key
            BROWSER_FORWARD = 0xA7,     //Windows 2000/XP: Browser Forward key
            BROWSER_REFRESH = 0xA8,     //Windows 2000/XP: Browser Refresh key
            BROWSER_STOP = 0xA9,     //Windows 2000/XP: Browser Stop key
            BROWSER_SEARCH = 0xAA,     //Windows 2000/XP: Browser Search key 
            BROWSER_FAVORITES = 0xAB,     //Windows 2000/XP: Browser Favorites key
            BROWSER_HOME = 0xAC,     //Windows 2000/XP: Browser Start and Home key
            VOLUME_MUTE = 0xAD,     //Windows 2000/XP: Volume Mute key
            VOLUME_DOWN = 0xAE,     //Windows 2000/XP: Volume Down key
            VOLUME_UP = 0xAF,     //Windows 2000/XP: Volume Up key
            MEDIA_NEXT_TRACK = 0xB0,     //Windows 2000/XP: Next Track key
            MEDIA_PREV_TRACK = 0xB1,     //Windows 2000/XP: Previous Track key
            MEDIA_STOP = 0xB2,     //Windows 2000/XP: Stop Media key
            MEDIA_PLAY_PAUSE = 0xB3,     //Windows 2000/XP: Play/Pause Media key
            LAUNCH_MAIL = 0xB4,     //Windows 2000/XP: Start Mail key
            LAUNCH_MEDIA_SELECT = 0xB5,     //Windows 2000/XP: Select Media key
            LAUNCH_APP1 = 0xB6,     //Windows 2000/XP: Start Application 1 key
            LAUNCH_APP2 = 0xB7,     //Windows 2000/XP: Start Application 2 key
            OEM_1 = 0xBA,     //Used for miscellaneous characters; it can vary by keyboard.
            OEM_PLUS = 0xBB,     //Windows 2000/XP: For any country/region, the '+' key
            OEM_COMMA = 0xBC,     //Windows 2000/XP: For any country/region, the ',' key
            OEM_MINUS = 0xBD,     //Windows 2000/XP: For any country/region, the '-' key
            OEM_PERIOD = 0xBE,     //Windows 2000/XP: For any country/region, the '.' key
            OEM_2 = 0xBF,     //Used for miscellaneous characters; it can vary by keyboard.
            OEM_3 = 0xC0,     //Used for miscellaneous characters; it can vary by keyboard. 
            OEM_4 = 0xDB,     //Used for miscellaneous characters; it can vary by keyboard. 
            OEM_5 = 0xDC,     //Used for miscellaneous characters; it can vary by keyboard. 
            OEM_6 = 0xDD,     //Used for miscellaneous characters; it can vary by keyboard. 
            OEM_7 = 0xDE,     //Used for miscellaneous characters; it can vary by keyboard. 
            OEM_8 = 0xDF,     //Used for miscellaneous characters; it can vary by keyboard.
            OEM_102 = 0xE2,     //Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
            PROCESSKEY = 0xE5,     //Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
            PACKET = 0xE7,     //Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
            ATTN = 0xF6,     //Attn key
            CRSEL = 0xF7,     //CrSel key
            EXSEL = 0xF8,     //ExSel key
            EREOF = 0xF9,     //Erase EOF key
            PLAY = 0xFA,     //Play key
            ZOOM = 0xFB,     //Zoom key
            NONAME = 0xFC,     //Reserved 
            PA1 = 0xFD,     //PA1 key
            OEM_CLEAR = 0xFE      //Clear key
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int MapVirtualKey(int uCode, int uMapType);

        public bool IsExtendedKey(KeyEventArgs e)
        {
            //bool isExtended = (bool)typeof(System.Windows.Input.KeyEventArgs).InvokeMember(
            //    "IsExtendedKey",
            //    BindingFlags.GetProperty |
            //    BindingFlags.NonPublic |
            //    BindingFlags.Instance,
            //    null, e, null);
            return (bool)typeof(System.Windows.Input.KeyEventArgs).InvokeMember("IsExtendedKey", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, null, e, null);
        }

        
        public MainWindow()
        {
            InitializeComponent();
            keyGesture = new KeyGesture(key, modifierKey);
        }

        private void ButtonProva_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = (turn) ? "Testo di prova..." : "Ciao Mamma!!";
            turn = !turn;
            // buttonOk.Content = "Chiudi";
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            /*
            SetCursorPos(130, 234);
            mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            */
            //mouse_event((int)MouseEventFlags.MOVE | (int)MouseEventFlags.ABSOLUTE, 32767, 32767, 0, 0);
            //MessageBox.Show(((int)VirtualKey.KEY_A).ToString("X"));
            TestTextBox1.Focus();
            TestTextBox1.CaretIndex = 0;
            keybd_event((byte)VirtualKey.SHIFT, 0, 0, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | (int)KEYEVENTF_KEYUP, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | (int)KEYEVENTF_KEYUP, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | 0, 0);
            keybd_event((byte)VirtualKey.RIGHT, 0, (int)KEYEVENTF_EXTENDEDKEY | (int)KEYEVENTF_KEYUP, 0);
            keybd_event((byte)VirtualKey.SHIFT, 0, (int)KEYEVENTF_KEYUP, 0);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Win32Point p = new Win32Point();
            GetCursorPos(ref p);
            // p = CorrectGetPosition(this); prende le coordinate a partire dall'angolo in alto a sinistra del Grid
            Ascissa.Text = p.X.ToString();
            Ordinata.Text = p.Y.ToString();
            //Ascissa.Text = ((int)(p.X * widthRatio)).ToString();
            //Ordinata.Text = ((int)(p.Y * heightRatio)).ToString();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                e.Handled = true;

            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            LogTextBox.AppendText("Character pressed: " + key.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(key) + "\n");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            LogTextBox.AppendText("Character released: " + key.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(key) + "\n");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();
        }

        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
            
        //    if ((Keyboard.Modifiers & ModifierKeys.Windows) == ModifierKeys.Windows)
        //    {
        //        if(e.Key == Key.Tab)
        //        e.Handled = true;
        //    }
        //    base.OnPreviewKeyDown(e);
        //}

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {            
            Key pressedKey = (e.Key == Key.System ? e.SystemKey : e.Key);
            
            //if (pressedKey == key && ((Keyboard.Modifiers & modifierKey) == modifierKey))
            if (keyGesture.Matches(this, e))
                this.Close();

            if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                e.Handled = true;
            
            if (IsExtendedKey(e))
                LogTextBox.AppendText("Character pressed: " + pressedKey.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(pressedKey) + ", is extended key\n");
            else
                LogTextBox.AppendText("Character pressed: " + pressedKey.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(pressedKey) + "\n");

        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            if (IsExtendedKey(e))
                LogTextBox.AppendText("Character released: " + key.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(key) + ", is extended key\n");
            else
                LogTextBox.AppendText("Character released: " + key.ToString() + ", code: " + KeyInterop.VirtualKeyFromKey(key) + "\n");
        }


    }
}
