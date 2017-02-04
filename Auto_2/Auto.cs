using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowScrape;
namespace Auto_2
{
    class Auto
    {
        private IntPtr _Handle = FindWindow("RCLIENT", "League of Legends");
        
        private static Color[,] _Xac_nhan = new Color[155, 25];
        private static Color[,] _Tim_tran = new Color[155, 25];
        private static Color[,] _Trong_hang_cho = new Color[155, 25];
        private static Color[,] _Dong_y = new Color[95, 25];
        private static Color[,] _Chon_tuong = new Color[100, 20];
        private static Color[,] _Doi_che_do_choi = new Color[170,30];
        private static Color[,] _Dang_tim_tran = new Color[110, 10];

        
        public Auto()
        {
            Bitmap Xac_nhan = Image.FromFile(@"Source/xacnhan.png") as Bitmap;
            Bitmap Tim_tran = Image.FromFile(@"Source/timtran.png") as Bitmap;
            Bitmap Trong_hang_cho = Image.FromFile(@"Source/tronghangcho.png") as Bitmap;
            Bitmap Dong_y = Image.FromFile(@"Source/dongy.png") as Bitmap;
            Bitmap Chon_tuong = Image.FromFile(@"Source/chontuong.png") as Bitmap;
            Bitmap Doi_che_do_choi = Image.FromFile(@"Source/doichedochoi.png") as Bitmap;
            for (int i = 0; i < 155; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    _Xac_nhan[i, j] = Xac_nhan.GetPixel(i, j);
                    _Tim_tran[i, j] = Tim_tran.GetPixel(i, j);
                    _Trong_hang_cho[i, j] = Trong_hang_cho.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 95; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    _Dong_y[i, j] = Dong_y.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    _Chon_tuong[i, j] = Chon_tuong.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 170; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    _Doi_che_do_choi[i, j] = Doi_che_do_choi.GetPixel(i, j);
                }
            }
        }
        public bool Check_Minimize()
        {

            return true;
        }

        public static Color[,] Xacnhan()
        {
            return _Tim_tran;
        }
        

       

        private enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized 
            /// or maximized, the system restores it to its original size and 
            /// position. An application should specify this flag when displaying 
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position. 
            /// This value is similar to "ShowNormal", except the window is not 
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size 
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next 
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is 
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This 
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is 
            /// minimized or maximized, the system restores it to its original size 
            /// and position. An application should specify this flag when restoring 
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
            /// that owns the window is hung. This flag should only be used when 
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }
        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }
        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        public Bitmap PrintWindow1(IntPtr hwnd)
        {
            RECT rc = new RECT();
            GetWindowRect(hwnd, out rc);
            int w = rc.right - rc.left;
            int h = rc.bottom - rc.top;

            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            bool succeeded = PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp.Size));
            }
            IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);
            if (hRgn != IntPtr.Zero)
            {
                GetWindowRgn(hwnd, hRgn);
                Region region = Region.FromHrgn(hRgn);
                if (!region.IsEmpty(gfxBmp))
                {
                    gfxBmp.ExcludeClip(region);
                    gfxBmp.Clear(Color.Transparent);
                }
                gfxBmp.Dispose();
                return bmp;
            }
            else
            {
                gfxBmp.Dispose();
                return bmp;
            }
        }

      

        public static void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            var oldPos = Cursor.Position;

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            /// return mouse 
            Cursor.Position = oldPos;
        }

        public bool Check(Color[,] _Color, int size_x, int size_y, int x, int y) // x, y là tọa độ bottom-left
        {
            Bitmap Screenshot = PrintWindow1(this._Handle);
            Bitmap tmp = new Bitmap(size_x, size_y);
            Bitmap tmp1 = new Bitmap(size_x, size_y);

            for (int i = 0; i < size_x; i++)
            {
                for (int j = 0; j < size_y; j++)
                {
                    tmp.SetPixel(i, j, Screenshot.GetPixel(x + i, y + j));
                }
            }


            int dem = 0;
            for (int i = 0; i < size_x; i++)
            {
                for (int j = 0; j < size_y; j++)
                {
                    if (_Color[i, j] == tmp.GetPixel(i, j))
                    {
                        dem++;
                    }
                    else
                    {
                        tmp1.SetPixel(i, j, tmp.GetPixel(i, j));
                    }
                }
            }
            //return dem;
            Screenshot.Dispose();
            tmp.Dispose();
            tmp1.Dispose();
            if (dem > 1000)
            {
                return true;
            }
            else
                return false;
        }

        public void Click_vao_game()
        {
            if (!_Handle.Equals(IntPtr.Zero))
            {
                SetForegroundWindow(_Handle);
                Thread.Sleep(500);
                ClickOnPoint(_Handle, new Point(128, 35));
                Thread.Sleep(500);
                ClickOnPoint(_Handle, new Point(139, 96));
                Thread.Sleep(500);
                ClickOnPoint(_Handle, new Point(400, 546));
                Thread.Sleep(500);
            }
            while (true)
            {
                if (Check(_Xac_nhan, 155, 25, 535, 675) == true)
                {
                    //SetForegroundWindow(_Handle);
                    Thread.Sleep(500);
                    ClickOnPoint(_Handle, new Point(623, 687));
                }
                //while (Check(_Doi_che_do_choi, 170, 30, 680, 105) == true)
                //{
                if (Check(_Tim_tran, 155, 25, 535, 675) == true)
                {

                    //SetForegroundWindow(_Handle);
                    Thread.Sleep(500);
                    ClickOnPoint(_Handle, new Point(618, 685));
                    //while (Check(_Dang_tim_tran, 110, 10, 1060, 90) == true)
                    // {

                    //}
                }
                if (Check(_Dong_y, 95, 25, 585, 535) == true)
                {
                    SetForegroundWindow(_Handle);
                    Thread.Sleep(500);
                    ClickOnPoint(_Handle, new Point(640, 555));
                }
                //}
                //}
            }
        }

        public bool Is_handle_exist()
        {
            if (FindWindow("RCLIENT", "League of Legends") != IntPtr.Zero)
            {
                return true;
            }
            else return false;
        }

        





        public void Doi_kich_thuoc_cua_so(int w, int h)
        {
            SetWindowPos(_Handle, IntPtr.Zero, 0, 0, w, h, 6);
        }



        #region DLL
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("user32.dll")]
        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, bool wParam, int lParam);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);
        #endregion
    }  
}
