using Auto_2.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace Auto_2
{
    class Auto_new
    {
        private IntPtr _Handle = FindWindow("RCLIENT", "League of Legends");


        private static bool Pause_Auto;
        private static bool Stop_Auto;
        private int xx, yy;
        private int so_tran;

        public Auto_new()
        {
            string path = @"SoTran\sotran.txt";
            if (!File.Exists(path))
            {
                //File.Create(path);
                StreamWriter sw = File.CreateText(path);
                sw.WriteLine("0");
                sw.Close();
            }
        }

        private static void SetPause()
        {
            if (Form1.GetPauseState() == true)
            {
                Pause_Auto = true;
            }
            else Pause_Auto = false;
        }
        private static void SetStop()
        {
            if (Form1.GetStopState() == true)
            {
                Stop_Auto = true;
            }
            else Stop_Auto = false;
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

        //public Bitmap PrintWindow1(IntPtr hwnd)
        //{
        //    RECT rc = new RECT();
        //    GetWindowRect(hwnd, out rc);
        //    int w = rc.right - rc.left;
        //    int h = rc.bottom - rc.top;

        //    if ((w != 160 && h != 27) && (w != 0 && h != 0) && (w != 132 && h != 38))
        //    {

        //        Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        //        //{
        //            Graphics gfxBmp = Graphics.FromImage(bmp);
        //            //try
        //            {
        //                IntPtr hdcBitmap = gfxBmp.GetHdc();
        //                bool succeeded = PrintWindow(hwnd, hdcBitmap, 0);

        //                gfxBmp.ReleaseHdc(hdcBitmap);
        //                if (!succeeded)
        //                {
        //                    gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp.Size));
        //                }
        //                IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);
        //                if (hRgn != IntPtr.Zero)
        //                {
        //                    GetWindowRgn(hwnd, hRgn);
        //                    Region region = Region.FromHrgn(hRgn);
        //                    if (!region.IsEmpty(gfxBmp))
        //                    {
        //                        gfxBmp.ExcludeClip(region);
        //                        gfxBmp.Clear(Color.Transparent);
        //                    }
        //                    //bmp.Dispose();
        //                    gfxBmp.Dispose();
        //                    return bmp;
        //                }
        //                else
        //                {
        //                    //bmp.Dispose();
        //                    gfxBmp.Dispose();
        //                    return bmp;
        //                }
        //            }
        //        //}
        //        //catch (Exception e)
        //        //{
        //        //    MessageBox.Show(e.ToString());
        //        //}

        //    }
        //    else
        //    {
        //        Bitmap bmp1 = new Bitmap(1280, 720, PixelFormat.Format32bppArgb);
        //        Graphics gfxBmp = Graphics.FromImage(bmp1);
        //        // IntPtr hdcBitmap = gfxBmp.GetHdc();
        //        gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp1.Size));
        //        //bmp1.Dispose();
        //        gfxBmp.Dispose();
        //        return bmp1;
        //    }
        //}

        public Bitmap PrintWindow1(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img as Bitmap;
        }
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
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

            //MessageBox.Show(clientPoint.X.ToString() + " " + clientPoint.Y.ToString());
        }


        private void Update_log_textbox(string text)
        {
            string date = DateTime.Now.ToString("dd.MM HH:mm:ss tt");
            TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;

            if (lb.InvokeRequired)
            {
                lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["textBox3"].Text += date + " " + text + "\r\n"; }));
            }
        }

        public void Feed_bot()
        {
            SetPause();
            SetStop();
            while (Pause_Auto == false && Stop_Auto == false)
            {
                while (Form1.SotrandungAuto() > 0 && Pause_Auto == false && Stop_Auto == false)
                {
                    #region Auto

                    #region label3
                    TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl; //.Controls["TabPages[0]"].Controls["label4"]
                    if (lb.InvokeRequired)
                    {
                        lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["label3"].Text = "Auto dừng sau " + Form1.SotrandungAuto().ToString() + " trận"; }));
                    }
                    #endregion

                    #region Đấu
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 123, 40, 67, 20), Resources.dau) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(139, 96));
                        Update_log_textbox("Tạo phòng đấu với máy");
                        SetPause(); SetStop();
                    }
                    #endregion
                    // Thread.Sleep(300);
                    #region Xác nhận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.xacnhan) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        if (Compare(CropBitmap(PrintWindow1(_Handle), 150, 25, 315, 535), Resources.Check_danhmay) == true && Pause_Auto == false && Stop_Auto == false)
                        {
                            ClickOnPoint(_Handle, new Point(400, 546));

                        }
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Update_log_textbox("Click xác nhận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //  Thread.Sleep(300);
                    #region Tìm trận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.timtran) == true && Pause_Auto == false && Stop_Auto == false)
                    {

                        SetForegroundWindow(_Handle);
                        Thread.Sleep(200);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //  Thread.Sleep(300);
                    #region Đồng ý
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 95, 25, 585, 535), Resources.dongy) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(640, 555));
                        Update_log_textbox("Click đồng ý");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //   Thread.Sleep(300);
                    #region Chọn tướng
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 100, 20, 590, 10), Resources.chontuong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        //while (Check(_Tim_tran, 155, 25, 535, 675) == false && Pause_Auto == false && Stop_Auto == false)
                        {
                            while (Compare(CropBitmap(PrintWindow1(_Handle), 140, 20, 570, 570), Resources.khoa) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                SetForegroundWindow(_Handle);
                                Thread.Sleep(2000);
                                ClickOnPoint(_Handle, new Point(x, y));
                                x += 100;
                                if (x > 880)
                                {
                                    x = 380;
                                    y += 100;
                                }
                                if (y > 645)
                                {
                                    y = 145;
                                }
                                Thread.Sleep(2000);
                                ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                                Update_log_textbox("Đã chọn tướng");
                                SetPause(); SetStop();
                            }
                        }
                    }
                    #endregion

                    #region vào game
                    if (check_vao_game() == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        #region kiểm tra đội xanh hay đỏ
                        Check_team();
                        #endregion
                        SetPause(); SetStop();

                        int m = 0;
                        while (m == 0 && Pause_Auto == false && Stop_Auto == false)
                        {
                            #region auto click
                            #region click
                            Thread.Sleep(Form1.ClickDl()); //milisecond
                            DoMouseRightClick(xx, yy);
                            #endregion

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");

                            so_tran = Int32.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion

                            if (Check_endgame() == true)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                Update_log_textbox("Xong trận");
                            }

                            SetPause(); SetStop();
                            #endregion
                        }
                        SetPause(); SetStop();

                        #region chờ chụp màn hình xong trận
                        int n = 0;
                        while (n == 0)
                        {
                            if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.choilai) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                SetForegroundWindow(_Handle);
                                Thread.Sleep(2000);
                                Taskbar.Show();
                                take_screen_shot("Tran " + so_tran);
                                Taskbar.Hide();
                                write_log_file(so_tran.ToString());
                                ClickOnPoint(_Handle, new Point(616, 687));
                                Form1.GiamSotrandungAuto();
                                n = 1;
                                Update_log_textbox("Chơi lại");
                                SetPause(); SetStop();
                            }
                        }
                        #endregion
                    }


                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.choilai) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }

                    Thread.Sleep(300);
                    SetPause(); SetStop();
                    #endregion
                    #endregion
                }
                SetPause(); SetStop();
                while (Pause_Auto == true)
                {
                    SetPause(); SetStop();
                    Thread.Sleep(500);
                }
                if (Form1.SotrandungAuto() == 0)
                {
                    MessageBox.Show("Đã hoàn thành số trận");
                    Stop_Auto = false;
                }
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

        private void write_log_file(string text)
        {
            string path = @"SoTran\SoTran.txt";
            StreamWriter sw = File.CreateText(path);
            sw.Write(text);
            sw.Dispose();
        }

        private void take_screen_shot(string file_name_to_save)
        {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            string tpm = file_name_to_save + ".jpeg";

            string subPath = @"SoTran\";

            System.IO.Directory.CreateDirectory(subPath);

            bmpScreenshot.Save(@"SoTran\" + file_name_to_save + ".jpeg", ImageFormat.Jpeg);
            bmpScreenshot.Dispose();
        }

        #region func_Click_ingame
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        public void DoMouseRightClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        public void DoMouseClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        #endregion

        public void Doi_kich_thuoc_cua_so(int w, int h)
        {
            SetWindowPos(_Handle, IntPtr.Zero, 0, 0, w, h, 6);
        }
        private bool check_vao_game()
        {
            //Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Bitmap bmpScreenshot = new Bitmap(1440, 900, PixelFormat.Format32bppArgb);
            try
            {
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                if (Compare2(CropBitmap(bmpScreenshot, 21, 21, 1220, 679), Resources.questionmark1) == true)
                {
                    Update_log_textbox("Đã vào game");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                //CropBitmap(bmpScreenshot, 21, 21, 1220, 679).Save("vg.png", ImageFormat.Png);
                bmpScreenshot.Dispose();
            }
        }
        private void Check_team()
        {
            Color team_red = System.Drawing.ColorTranslator.FromHtml("#172634");
            Color team_blue = System.Drawing.ColorTranslator.FromHtml("#2d2125");
            var inGame_Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var inGame_gfxScreenshot = Graphics.FromImage(inGame_Screenshot);
            inGame_gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            //  inGame_Screenshot.Dispose();

            if (inGame_Screenshot.GetPixel(1227, 881) == team_red)
            {
                //đội đỏ
                xx = 1227;
                yy = 881;
                Update_log_textbox("Xác định đội đỏ");
            }
            if (inGame_Screenshot.GetPixel(1423, 685) == team_blue)
            {
                //đội xanh
                xx = 1421;
                yy = 691;
                Update_log_textbox("Xác định đội xanh");
            }
        }
        private bool Check_endgame()
        {
            #region chụp màn hình
            Bitmap check_endgame_Screenshot_1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            
            var check_endgame_gfxScreenshot_1 = Graphics.FromImage(check_endgame_Screenshot_1);
            check_endgame_gfxScreenshot_1.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            //check_endgame_Screenshot_1.Save("CCC.png", ImageFormat.Png);
            #endregion
            try
            {
                if (Compare(CropBitmap(check_endgame_Screenshot_1, 80, 25, 680, 535), Resources.end) == true)
                    return true;
                else return false;
            }
            finally
            {
                //CropBitmap(check_endgame_Screenshot_1, 80, 25, 680, 535).Save("end.png", ImageFormat.Png);
                check_endgame_Screenshot_1.Dispose();
            }
        }
        #region New ver

        private bool Compare(Bitmap b1, Bitmap b2)
        {
            //var watch = Stopwatch.StartNew();
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            BitmapData bmd1 = b1.LockBits(new Rectangle(0, 0, b1.Width - 1, b1.Height - 1), ImageLockMode.ReadOnly, b1.PixelFormat);
            BitmapData bmd2 = b2.LockBits(new Rectangle(0, 0, b2.Width - 1, b2.Height - 1), ImageLockMode.ReadOnly, b2.PixelFormat);

            try
            {
                int bytes = b1.Width * b1.Height; //* (Image.GetPixelFormatSize(b1.PixelFormat) / 8);

                byte[] b1bytes = new byte[bytes];
                byte[] b2bytes = new byte[bytes];

                Marshal.Copy(bmd1.Scan0, b1bytes, 0, bytes);
                Marshal.Copy(bmd2.Scan0, b2bytes, 0, bytes);

                int dem = 0;
                for (int n = 0; n <= bytes - 1; n++)
                {
                    if (Math.Abs(b1bytes[n] - b2bytes[n]) > 1) 
                    {
                        dem++;
                    }
                }

                //watch.Stop();
                //var elapsedMs = watch.ElapsedMilliseconds;
                //TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;

                //if (lb.InvokeRequired)
                //{
                //    lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["textBox11"].Text += elapsedMs*1000 + "\r\n"; }));
                //}
                if (dem < bytes / 3)
                    return true;
                else
                    return false;
            }
            finally
            {
                b1.UnlockBits(bmd1);
                b2.UnlockBits(bmd2);
            }
        }
        private bool Compare2(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, (IntPtr)len) == 0;

            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
        public Bitmap CropBitmap(Bitmap bitmap, int w, int h, int x, int y)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            if ((bitmap.Width != 160 && bitmap.Height != 27) && (bitmap.Width != 0 && bitmap.Height != 0) && (bitmap.Width != 132 && bitmap.Height != 38))
            {
                Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
                return cropped;
            }
            else return bitmap;
        }
        public void Feed_rank_2()
        {
            //ShowWindow(_Handle, WindowShowStyle.ShowNoActivate);
            SetPause();
            SetStop();
            while (Pause_Auto == false && Stop_Auto == false)
            {
                while (Form1.SotrandungAuto() > 0 && Pause_Auto == false && Stop_Auto == false)
                {
                    #region Auto

                    #region label3
                    TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl; //.Controls["TabPages[0]"].Controls["label4"]
                    if (lb.InvokeRequired)
                    {
                        lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["label3"].Text = "Auto dừng sau " + Form1.SotrandungAuto().ToString() + " trận"; }));
                    }
                    #endregion

                    #region Đấu
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 123, 40, 67, 20), Resources.dau) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        //Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Update_log_textbox("Tạo phòng đấu với máy");
                        ClickOnPoint(_Handle, new Point(46, 97));
                        bool done = false;
                        while (done == false)
                        {
                            //vị trí có hàng chờ luân phiên
                            if (Compare(CropBitmap(PrintWindow1(_Handle), 195, 25, 73, 544), Resources.Check_xephang_HCLP) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                //SetForegroundWindow(_Handle);
                                ClickOnPoint(_Handle, new Point(185, 555));
                                Thread.Sleep(200);
                                SetPause(); SetStop();
                                done = true;
                            }
                            //if (Compare(CropBitmap(PrintWindow1(_Handle), 195, 25, 199, 543), Resources.check_xephang) == true && Pause_Auto == false && Stop_Auto == false)
                            //{
                            //    //SetForegroundWindow(_Handle);
                            //    ClickOnPoint(_Handle, new Point(310, 555));
                            //    Thread.Sleep(200);
                            //    SetPause(); SetStop();
                            //    done = true;
                            //}
                        }
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Xác nhận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.xacnhan) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        
                        //CropBitmap(PrintWindow1(_Handle), 195, 25, 199, 543).Save("bbbb.png", ImageFormat.Png);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Update_log_textbox("Click xác nhận");
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Xếp hạng
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 451, 28, 94, 94), Resources.xephangdong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 450));
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Chọn roll
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 149, 101, 410, 410), Resources.chonroll) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 550));
                        Update_log_textbox("Đã chọn roll support");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Tìm trận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.timtran) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(100);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Đồng ý
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 95, 25, 585, 535), Resources.dongy) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(100);
                        ClickOnPoint(_Handle, new Point(640, 555));
                        Update_log_textbox("Click đồng ý");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Cấm
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 125, 40, 580, 1), Resources.cam) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        //Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        SetForegroundWindow(_Handle);
                        //Thread.Sleep(1500);

                        while (Compare(CropBitmap(PrintWindow1(_Handle), 172, 42, 554, 562), Resources.cam_button) == true && Pause_Auto == false && Stop_Auto == false)
                        {
                            //SetForegroundWindow(_Handle);
                            Thread.Sleep(2000);
                            ClickOnPoint(_Handle, new Point(x, y));
                            x += 100;
                            if (x > 880)
                            {
                                x = 380;
                                y += 100;
                            }
                            if (y > 645)
                            {
                                y = 145;
                            }
                            Thread.Sleep(500);
                            ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                            SetPause(); SetStop();
                        }
                        Update_log_textbox("Đã cấm tướng");
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Chọn tướng
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 100, 20, 590, 10), Resources.chontuong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 1280; //tọa độ atroxx
                        int y = 345;

                        //while (Check(_Tim_tran, 155, 25, 535, 675) == false && Pause_Auto == false && Stop_Auto == false)
                        while (Compare(CropBitmap(PrintWindow1(_Handle), 140, 20, 570, 570), Resources.khoa) == true && Pause_Auto == false && Stop_Auto == false)
                        {
                            SetForegroundWindow(_Handle);
                            Thread.Sleep(2000);
                            ClickOnPoint(_Handle, new Point(x, y));
                            x += 100;
                            if (x > 880)
                            {
                                x = 380;
                                y += 100;
                            }
                            if (y > 645)
                            {
                                y = 145;
                            }
                            Thread.Sleep(500);
                            ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                            Update_log_textbox("Đã chọn tướng");
                            SetPause(); SetStop();
                        }
                       
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region vào game
                    if (check_vao_game() == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        #region kiểm tra đội xanh hay đỏ
                        Check_team();
                        #endregion
                        SetPause(); SetStop();

                        int m = 0;
                        while (m == 0 && Pause_Auto == false && Stop_Auto == false)
                        {
                            #region auto click

                            #region click
                            Thread.Sleep(Form1.ClickDl()); //milisecond
                            DoMouseRightClick(xx, yy);
                            #endregion

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");
                            so_tran = int.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion

                            #region check xong trận
                            //bool end = false;
                            while (Check_endgame() == true)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                //end = true;
                            }
                            //if (end == true)
                            //{
                            //    Update_log_textbox("Xong trận");
                            //}
                            #endregion

                            #endregion
                            SetPause(); SetStop();
                        }
                        SetPause(); SetStop();

                        int n = 0;
                        #region chờ chụp màn hình xong trận
                        while (n == 0)
                        {
                            //SetForegroundWindow(_Handle);
                            if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.cl) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                SetForegroundWindow(_Handle);
                                Thread.Sleep(2000);
                                Taskbar.Show();
                                take_screen_shot("Tran " + so_tran);
                                Taskbar.Hide();
                                write_log_file(so_tran.ToString());
                                ClickOnPoint(_Handle, new Point(616, 687));
                                Form1.GiamSotrandungAuto();
                                n = 1;
                                Update_log_textbox("Chơi lại");
                                SetPause(); SetStop();
                            }

                        }
                        #endregion
                    }
                    #endregion
                    Thread.Sleep(100);
                    #region Chơi lại
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.cl) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }
                    //CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675).Save("cl.png", ImageFormat.Png);
                    #endregion
                    Thread.Sleep(100);
                    if (Check_endgame() == true)
                    {
                        Update_log_textbox("Xong trận");
                    }
                    #endregion
                }
                SetPause(); SetStop();
                while (Pause_Auto == true)
                {
                    SetPause(); SetStop();
                    //Thread.Sleep(500);
                }
                if (Form1.SotrandungAuto() == 0)
                {
                    MessageBox.Show("Đã hoàn thành số trận");
                    Stop_Auto = true;
                }
            }
        }
        public void Feed_rank_3()
        {
            //ShowWindow(_Handle, WindowShowStyle.ShowNoActivate);
            SetPause();
            SetStop();
            while (Pause_Auto == false && Stop_Auto == false)
            {
                bool Rank_Ticked = false;
                while (Form1.SotrandungAuto() > 0 && Pause_Auto == false && Stop_Auto == false)
                {
                    #region Auto

                    #region label3
                    TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl; //.Controls["TabPages[0]"].Controls["label4"]
                    if (lb.InvokeRequired)
                    {
                        lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["label3"].Text = "Auto dừng sau " + Form1.SotrandungAuto().ToString() + " trận"; }));
                    }
                    #endregion

                    //var watch = System.Diagnostics.Stopwatch.StartNew();

                    Bitmap Screen = PrintWindow1(_Handle);
                    Bitmap Dau = CropBitmap(Screen, 123, 40, 67, 20);
                    Bitmap Xac_nhan = CropBitmap(Screen, 155, 25, 535, 675);
                    Bitmap Xep_hang = CropBitmap(Screen, 451, 28, 94, 94);
                    Bitmap Chon_roll = CropBitmap(Screen, 149, 101, 410, 410);
                    Bitmap Tim_tran = CropBitmap(Screen, 155, 25, 535, 675);
                    Bitmap Dong_y = CropBitmap(Screen, 95, 25, 585, 535);
                    Bitmap Cam = CropBitmap(Screen, 125, 40, 580, 1);
                    Bitmap Chon_tuong = CropBitmap(Screen, 100, 20, 590, 10);
                    Bitmap Choi_lai = CropBitmap(Screen, 155, 25, 535, 675);                  
                    //Choi_lai.Save(@"C:\Users\The_Flash\Desktop\ccccccd.png", ImageFormat.Png);                  
                    Bitmap Xep_hang_dong_HCLP = CropBitmap(Screen, 195, 25, 73, 544);
                    //watch.Stop();
                    //MessageBox.Show(watch.ElapsedMilliseconds.ToString());


                    #region Đấu
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 123, 40, 67, 20), Resources.dau) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        //Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Update_log_textbox("Tạo phòng đấu với máy");
                        ClickOnPoint(_Handle, new Point(46, 97));
                        bool done = false;
                        while (done == false)
                        {
                            //vị trí có hàng chờ luân phiên
                            if (Compare(CropBitmap(PrintWindow1(_Handle), 195, 25, 73, 544), Resources.Check_xephang_HCLP) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                //SetForegroundWindow(_Handle);
                                ClickOnPoint(_Handle, new Point(185, 555));
                                Thread.Sleep(200);
                                SetPause(); SetStop();
                                done = true;
                            }
                            //if (Compare(CropBitmap(PrintWindow1(_Handle), 195, 25, 199, 543), Resources.check_xephang) == true && Pause_Auto == false && Stop_Auto == false)
                            //{
                            //    //SetForegroundWindow(_Handle);
                            //    ClickOnPoint(_Handle, new Point(310, 555));
                            //    Thread.Sleep(200);
                            //    SetPause(); SetStop();
                            //    done = true;
                            //}
                        }
                        SetPause(); SetStop();
                    }
                    //Update_log_textbox("đấu");
                    //Thread.Sleep(100);
                    #endregion

                    #region chọn xếp hạng
                    //vị trí có hàng chờ luân phiên
                    if (Compare(Xep_hang_dong_HCLP, Resources.Check_xephang_HCLP) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        //SetForegroundWindow(_Handle);
                        Thread.Sleep(200);
                        ClickOnPoint(_Handle, new Point(185, 555));
                        Rank_Ticked = true;
                        SetPause(); SetStop();
                    }

                    //if (Compare(CropBitmap(PrintWindow1(_Handle), 195, 25, 199, 543), Resources.check_xephang) == true && Pause_Auto == false && Stop_Auto == false)
                    //{
                    //    //SetForegroundWindow(_Handle);
                    //    ClickOnPoint(_Handle, new Point(310, 555));
                    //    Thread.Sleep(200);
                    //    SetPause(); SetStop();
                      
                    //}
                    #endregion

                    #region Xác nhận
                    if (Compare(Xac_nhan, Resources.xacnhan) == true && Pause_Auto == false && Stop_Auto == false && Rank_Ticked == true)
                    {
                        SetForegroundWindow(_Handle);

                        //CropBitmap(PrintWindow1(_Handle), 195, 25, 199, 543).Save("bbbb.png", ImageFormat.Png);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Rank_Ticked = false;
                        Update_log_textbox("Click xác nhận");
                    }
                    #endregion
                    //Update_log_textbox("xác nhận");
                    Thread.Sleep(100);
                    #region Xếp hạng
                    if (Compare(Xep_hang, Resources.xephangdong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 450));
                        SetPause(); SetStop();
                    }
                    #endregion
                    //Update_log_textbox("xếp hạng");
                   // Thread.Sleep(100);
                    #region Chọn roll
                    if (Compare(Chon_roll, Resources.chonroll) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 550));
                        Update_log_textbox("Đã chọn roll support");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //Update_log_textbox("chọn roll");
                    Thread.Sleep(100);
                    #region Tìm trận
                    if (Compare(Tim_tran, Resources.timtran) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(100);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //Update_log_textbox("tìm trận");
                  //  Thread.Sleep(100);
                    #region Đồng ý
                    if (Compare(Dong_y, Resources.dongy) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(100);
                        ClickOnPoint(_Handle, new Point(640, 555));
                        Update_log_textbox("Click đồng ý");
                        SetPause(); SetStop();
                    }
                    #endregion
                    //Update_log_textbox("đồng ý");
                    Thread.Sleep(100);
                    #region Cấm
                    if (Compare(Cam, Resources.cam) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        //Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        SetForegroundWindow(_Handle);
                        //Thread.Sleep(1500);

                        while (Compare(CropBitmap(PrintWindow1(_Handle), 172, 42, 554, 562), Resources.cam_button) == true && Pause_Auto == false && Stop_Auto == false)
                        {
                            //SetForegroundWindow(_Handle);
                            Thread.Sleep(2000);
                            ClickOnPoint(_Handle, new Point(x, y));
                            x += 100;
                            if (x > 880)
                            {
                                x = 380;
                                y += 100;
                            }
                            if (y > 645)
                            {
                                y = 145;
                            }
                            Thread.Sleep(500);
                            ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                            SetPause(); SetStop();
                        }
                        Update_log_textbox("Đã cấm tướng");
                    }
                    #endregion
                    //Update_log_textbox("Cấm");
                  //  Thread.Sleep(100);
                    #region Chọn tướng
                    if (Compare(Chon_tuong, Resources.chontuong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 1280; //tọa độ atroxx
                        int y = 345;

                        //while (Check(_Tim_tran, 155, 25, 535, 675) == false && Pause_Auto == false && Stop_Auto == false)
                        while (Compare(CropBitmap(PrintWindow1(_Handle), 140, 20, 570, 570), Resources.khoa) == true && Pause_Auto == false && Stop_Auto == false)
                        {
                            SetForegroundWindow(_Handle);
                            Thread.Sleep(2000);
                            ClickOnPoint(_Handle, new Point(x, y));
                            x += 100;
                            if (x > 880)
                            {
                                x = 380;
                                y += 100;
                            }
                            if (y > 645)
                            {
                                y = 145;
                            }
                            Thread.Sleep(500);
                            ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                            Update_log_textbox("Đã chọn tướng");
                            SetPause(); SetStop();
                        }

                    }
                    #endregion
                    //Update_log_textbox("chọn tướng");
                    Thread.Sleep(100);
                    #region vào game
                    if (check_vao_game() == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        #region kiểm tra đội xanh hay đỏ
                        Check_team();
                        #endregion
                        SetPause(); SetStop();

                        int m = 0;
                        while (m == 0 && Pause_Auto == false && Stop_Auto == false)
                        {
                            #region auto click

                            #region click
                            Thread.Sleep(Form1.ClickDl()); //milisecond
                            DoMouseRightClick(xx, yy);
                            #endregion

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");
                            so_tran = int.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion

                            #region check xong trận
                            //bool end = false;
                            while (Check_endgame() == true)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                //end = true;
                            }
                            //if (end == true)
                            //{
                            //    Update_log_textbox("Xong trận");
                            //}
                            #endregion

                            #endregion
                            SetPause(); SetStop();
                        }
                        SetPause(); SetStop();

                        int n = 0;
                        #region chờ chụp màn hình xong trận
                        while (n == 0)
                        {
                            //SetForegroundWindow(_Handle);
                            if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675), Resources.choilai) == true && Pause_Auto == false && Stop_Auto == false)
                            {
                                SetForegroundWindow(_Handle);
                                Thread.Sleep(2000);
                                Taskbar.Show();
                                take_screen_shot("Tran " + so_tran);
                                Taskbar.Hide();
                                write_log_file(so_tran.ToString());
                                ClickOnPoint(_Handle, new Point(616, 687));
                                Form1.GiamSotrandungAuto();
                                n = 1;
                                Update_log_textbox("Chơi lại");
                                SetPause(); SetStop();
                            }

                        }
                        #endregion
                    }
                    #endregion
                    //Update_log_textbox("vào game");
                 //   Thread.Sleep(100);
                    #region Chơi lại
                    if (Compare(Choi_lai, Resources.choilai) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }
                    //CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675).Save("cl.png", ImageFormat.Png);
                    #endregion
                    //Update_log_textbox("chơi lại");
                    Thread.Sleep(100);
                    if (Check_endgame() == true)
                    {
                        Update_log_textbox("Xong trận");
                    }
                    #endregion
                }
                SetPause(); SetStop();
                while (Pause_Auto == true)
                {
                    SetPause(); SetStop();
                    //Thread.Sleep(500);
                }
                if (Form1.SotrandungAuto() == 0)
                {
                    MessageBox.Show("Đã hoàn thành số trận");
                    Stop_Auto = true;
                }
            }
        }
        #endregion




        #region DLL
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
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
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(IntPtr b1, IntPtr b2, IntPtr count);
        #endregion

    }
}

