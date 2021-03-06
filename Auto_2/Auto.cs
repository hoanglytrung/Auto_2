﻿using Auto_2.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowScrape;
using WindowsFormsApplication1;
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
        private static Color[,] _Doi_che_do_choi = new Color[170, 30];
        private static Color[,] _Dang_tim_tran = new Color[110, 10];
        private static Color[,] _Khoa = new Color[140, 20];
        private static Color[,] _Dau = new Color[123, 40];
        private static Color[,] _Choi_lai = new Color[155, 25];
        private static Color[,] _Xep_hang = new Color[451, 28];
        private static Color[,] _Chon_roll = new Color[149, 101];
        private static Color[,] _Cam = new Color[125, 40];
        private static Color[,] _Khoa_cam = new Color[120, 40];
        private static Color[,] _Cam_button = new Color[172, 42];


        private static bool Pause_Auto;
        private static bool Stop_Auto;
        private int xx, yy;
        private int so_tran;

        public Auto()
        {
            Bitmap Xac_nhan = new Bitmap(Resources.xacnhan);
            Bitmap Tim_tran = new Bitmap(Resources.timtran);
            Bitmap Trong_hang_cho = new Bitmap(Resources.tronghangcho);
            Bitmap Dong_y = new Bitmap(Resources.dongy);
            Bitmap Chon_tuong = new Bitmap(Resources.chontuong);
            Bitmap Doi_che_do_choi = new Bitmap(Resources.doichedochoi);
            Bitmap Khoa = new Bitmap(Resources.khoa);
            Bitmap Dau = new Bitmap(Resources.dau);
            Bitmap Choi_lai = new Bitmap(Resources.choilai);
            Bitmap Xep_hang = new Bitmap(Resources.xephangdong);
            Bitmap Chon_roll = new Bitmap(Resources.chonroll);
            Bitmap Cam = new Bitmap(Resources.cam);
            Bitmap Khoa_cam = new Bitmap(Resources.khoacam);
            Bitmap Cam_button = new Bitmap(Resources.cam_button);

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

            for (int i = 0; i < 140; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    _Khoa[i, j] = Khoa.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 123; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    _Dau[i, j] = Dau.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 155; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    _Choi_lai[i, j] = Choi_lai.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 451; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    _Xep_hang[i, j] = Xep_hang.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 149; i++)
            {
                for (int j = 0; j < 101; j++)
                {
                    _Chon_roll[i, j] = Chon_roll.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 125; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    _Cam[i, j] = Cam.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    _Khoa_cam[i, j] = Khoa_cam.GetPixel(i, j);
                }
            }

            for (int i = 0; i < 172; i++)
            {
                for (int j = 0; j < 42; j++)
                {
                    _Cam_button[i, j] = Cam_button.GetPixel(i, j);
                }
            }

            string path = @"SoTran\sotran.txt";
            if (!File.Exists(path))
            {
                //File.Create(path);
                StreamWriter sw = File.CreateText(path);
                sw.WriteLine("0");
                sw.Close();
            }
            //else if (File.Exists(path))
            //{
            //    using (var tw = new StreamWriter(path, true))
            //    {
            //        tw.WriteLine("The next line!");
            //        tw.Close();
            //    }
            //}
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

        public Bitmap PrintWindow1(IntPtr hwnd)
        {
            RECT rc = new RECT();
            GetWindowRect(hwnd, out rc);
            int w = rc.right - rc.left;
            int h = rc.bottom - rc.top;

            if ((w != 160 && h != 27) && (w != 0 && h != 0) && (w != 132 && h != 38))
            {
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
            else
            {
                Bitmap bmp1 = new Bitmap(1280, 720, PixelFormat.Format32bppArgb);
                Graphics gfxBmp = Graphics.FromImage(bmp1);
                // IntPtr hdcBitmap = gfxBmp.GetHdc();
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Point.Empty, bmp1.Size));
                return bmp1;
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
            var watch = Stopwatch.StartNew();
            Bitmap Screenshot = PrintWindow1(this._Handle);
            Bitmap tmp = new Bitmap(size_x, size_y);
            Bitmap tmp1 = new Bitmap(size_x, size_y);

            int dem = 0;

            if (Screenshot.Width != 160 && Screenshot.Width != 132)// Screenshot.Width != 132)
            {
                for (int i = 0; i < size_x; i++)
                {
                    for (int j = 0; j < size_y; j++)
                    {
                        tmp.SetPixel(i, j, Screenshot.GetPixel(x + i, y + j));
                    }
                }

                //tmp.Save("check.png", ImageFormat.Png);
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
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;

            if (lb.InvokeRequired)
            {
                lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["textBox11"].Text += elapsedMs + "\r\n"; }));
            }
            if (_Color == _Dau) //trường hợp riêng cho button Đấu
            {
                if (dem > (4000))
                {
                    //Update_log_textbox(_Color + " " + dem.ToString());
                    return true;
                }
                else
                    return false;
            }

            else if (dem > ((size_x * size_y) / 2))
            {
                //Update_log_textbox(_Color + " " + dem.ToString());
                return true;
            }
            else
                return false;

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

        public void Click_vao_game()
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
                    if (Check(_Dau, 123, 40, 67, 20) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(139, 96));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(400, 546));
                        Thread.Sleep(700);
                        Update_log_textbox("Tạo phòng đấu với máy");
                        SetPause(); SetStop();
                    }
                    #endregion

                    Thread.Sleep(300);
                    #region Xác nhận
                    if (Check(_Xac_nhan, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Update_log_textbox("Click xác nhận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Tìm trận
                    if (Check(_Tim_tran, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {

                        SetForegroundWindow(_Handle);
                        Thread.Sleep(200);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Đồng ý
                    if (Check(_Dong_y, 95, 25, 585, 535) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(640, 555));
                        Update_log_textbox("Click đồng ý");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Chọn tướng
                    if (Check(_Chon_tuong, 100, 20, 590, 10) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        //while (Check(_Tim_tran, 155, 25, 535, 675) == false && Pause_Auto == false && Stop_Auto == false)
                        {
                            while (Check(_Khoa, 140, 20, 570, 570) == true && Pause_Auto == false && Stop_Auto == false)
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
                        //1270 883 mã (163,44,44) = #A32C2C
                        Color team_red = ColorTranslator.FromHtml("#172634");
                        Color team_blue = ColorTranslator.FromHtml("#2d2125");
                        var inGame_Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                        var inGame_gfxScreenshot = Graphics.FromImage(inGame_Screenshot);
                        inGame_gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        inGame_Screenshot.Save(@"Source\Screenshot_ingame.png", ImageFormat.Png);
                        //inGame_Screenshot.Dispose();
                        Bitmap src_2 = Image.FromFile(@"Source\Screenshot_ingame.png") as Bitmap;

                        if (src_2.GetPixel(1227, 881) == team_red)
                        {
                            //đội đỏ
                            xx = 1227;
                            yy = 881;
                            Update_log_textbox("Xác định đội đỏ");
                        }
                        if (src_2.GetPixel(1423, 685) == team_blue)
                        {
                            //đội xanh
                            xx = 1421;
                            yy = 691;
                            Update_log_textbox("Xác định đội xanh");
                        }

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
                            #region kiểm tra xem kết thúc game chưa
                            #region chụp màn hình
                            var check_endgame_Screenshot_1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                            var check_endgame_gfxScreenshot_1 = Graphics.FromImage(check_endgame_Screenshot_1);
                            check_endgame_gfxScreenshot_1.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                            check_endgame_Screenshot_1.Save(@"Source\Screenshot_1.png", ImageFormat.Png);


                            #endregion
                            #region kiểm tra

                            Bitmap test = Image.FromFile(@"Source\Screenshot_1.png") as Bitmap;
                            Color[,] pic = new Color[test.Width, test.Height];
                            for (int i = 680; i < 760; i++)
                            {
                                for (int j = 535; j < 560; j++)
                                {
                                    pic[i, j] = test.GetPixel(i, j);
                                }
                            }

                            Bitmap b = new Bitmap(80, 25);
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    b.SetPixel(i, j, pic[i + 680, j + 535]);
                                }
                            }
                            b.Save(@"Source\b.png", ImageFormat.Png);


                            Bitmap endgame = Image.FromFile(@"Source\b.png") as Bitmap; //endgame là hình chụp màn hình đã được cắt nhỏ ở phần chữ "Tiếp tục"
                            Bitmap sample_endgame = Image.FromFile(@"Source\endgame.png") as Bitmap;

                            int endgame_pixel = 0;
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    if (endgame.GetPixel(i, j) == sample_endgame.GetPixel(i + 680, j + 535))
                                    {
                                        endgame_pixel += 1;
                                    }
                                }
                            }

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");

                            so_tran = Int32.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion



                            if (endgame_pixel > 1800)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                Update_log_textbox("Xong trận");
                            }
                            #endregion
                            #endregion

                            sample_endgame.Dispose();
                            check_endgame_Screenshot_1.Dispose();
                            test.Dispose();
                            endgame.Dispose();
                            #endregion
                            SetPause(); SetStop();
                        }
                        src_2.Dispose();
                        SetPause(); SetStop();

                        #region chờ chụp màn hình xong trận
                        int n = 0;
                        while (n == 0)
                        {
                            if (Check(_Choi_lai, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
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

                    if (Check(_Choi_lai, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }

                    Thread.Sleep(300);
                    SetPause(); SetStop();
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
            int so_pixel = 0;

            using (var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb))
            {

                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                bmpScreenshot.Save(@"Source\Screenshot_vaogame.png", ImageFormat.Png);

                Color[,] pic = new Color[21, 21];
                Bitmap sample_question_mark = Image.FromFile(@"Source\vaogame.png") as Bitmap;
                Bitmap src = Image.FromFile(@"Source\Screenshot_vaogame.png") as Bitmap;
                for (int i = 1220; i < 1241; i++)
                {
                    for (int j = 679; j < 700; j++)
                    {
                        try
                        {
                            pic[i - 1220, j - 679] = src.GetPixel(i, j);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                            MessageBox.Show(i + " " + j);
                        }

                    }
                }
                for (int i = 0; i < sample_question_mark.Width; i++)
                {
                    for (int j = 0; j < sample_question_mark.Height; j++)
                    {
                        if (pic[i, j] == sample_question_mark.GetPixel(i, j))
                        {
                            so_pixel += 1;
                        }
                    }
                }
                Bitmap _pic = new Bitmap(21, 21);
                for (int i = 0; i < 21; i++)
                {
                    for (int j = 0; j < 21; j++)
                    {
                        _pic.SetPixel(i, j, pic[i, j]);
                    }
                }

                src.Dispose();
                bmpScreenshot.Dispose();
                //Thread.Sleep(10000);
                if (so_pixel == 21 * 21)
                {
                    return true;
                }
                else return false;
            }
        }

        public void c()
        {
            MessageBox.Show(Form1.ClickDl().ToString());
        }

        #region Feed_Rank
        public void Feed_rank()
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
                    if (Check(_Dau, 123, 40, 67, 20) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(46, 97));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(177, 555));
                        Thread.Sleep(700);
                        Update_log_textbox("Tạo phòng đấu với máy");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Xác nhận
                    if (Check(_Xac_nhan, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Update_log_textbox("Click xác nhận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Xếp hạng
                    if (Check(_Xep_hang, 451, 28, 94, 94) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 450));
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Chọn roll
                    if (Check(_Chon_roll, 149, 101, 410, 410) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 550));
                        Update_log_textbox("Đã chọn roll support");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Tìm trận
                    if (Check(_Tim_tran, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {

                        SetForegroundWindow(_Handle);
                        Thread.Sleep(200);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Đồng ý
                    if (Check(_Dong_y, 95, 25, 585, 535) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(640, 555));
                        Update_log_textbox("Click đồng ý");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Cấm
                    if (Check(_Cam, 125, 40, 580, 1) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        //Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(1500);

                        while (Check(_Cam_button, 172, 42, 554, 562) == true && Pause_Auto == false && Stop_Auto == false)
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
                            Update_log_textbox("Đã cấm tướng");
                            SetPause(); SetStop();
                        }
                    }
                    #endregion

                    //if (Check(_Khoa_cam, 120, 40, 580, 1) == true && Pause_Auto == false && Stop_Auto == false)
                    //{
                    //    Thread.Sleep(1500);
                    //    ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                    //    SetPause(); SetStop();
                    //}
                    Thread.Sleep(300);
                    #region Chọn tướng
                    if (Check(_Chon_tuong, 100, 20, 590, 10) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 1280; //tọa độ atroxx
                        int y = 345;

                        //while (Check(_Tim_tran, 155, 25, 535, 675) == false && Pause_Auto == false && Stop_Auto == false)
                        {
                            while (Check(_Khoa, 140, 20, 570, 570) == true && Pause_Auto == false && Stop_Auto == false)
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
                    Thread.Sleep(300);
                    #region vào game
                    if (check_vao_game() == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        #region kiểm tra đội xanh hay đỏ
                        //1270 883 mã (163,44,44) = #A32C2C
                        Color team_red = System.Drawing.ColorTranslator.FromHtml("#172634");
                        Color team_blue = System.Drawing.ColorTranslator.FromHtml("#2d2125");
                        var inGame_Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                        var inGame_gfxScreenshot = Graphics.FromImage(inGame_Screenshot);
                        inGame_gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        inGame_Screenshot.Save(@"Source\Screenshot_ingame.png", ImageFormat.Png);
                        inGame_Screenshot.Dispose();
                        Bitmap src_2 = Image.FromFile(@"Source\Screenshot_ingame.png") as Bitmap;

                        if (src_2.GetPixel(1227, 881) == team_red)
                        {
                            //đội đỏ
                            xx = 1227;
                            yy = 881;
                            Update_log_textbox("Xác định đội đỏ");
                        }
                        if (src_2.GetPixel(1423, 685) == team_blue)
                        {
                            //đội xanh
                            xx = 1421;
                            yy = 691;
                            Update_log_textbox("Xác định đội xanh");
                        }

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

                            #region kiểm tra xem kết thúc game chưa
                            #region chụp màn hình
                            var check_endgame_Screenshot_1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                            var check_endgame_gfxScreenshot_1 = Graphics.FromImage(check_endgame_Screenshot_1);
                            check_endgame_gfxScreenshot_1.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                            check_endgame_Screenshot_1.Save(@"Source\Screenshot_1.png", ImageFormat.Png);


                            #endregion
                            #region kiểm tra

                            Bitmap test = Image.FromFile(@"Source\Screenshot_1.png") as Bitmap;
                            Color[,] pic = new Color[test.Width, test.Height];
                            for (int i = 680; i < 760; i++)
                            {
                                for (int j = 535; j < 560; j++)
                                {
                                    pic[i, j] = test.GetPixel(i, j);
                                }
                            }

                            Bitmap b = new Bitmap(80, 25);
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    b.SetPixel(i, j, pic[i + 680, j + 535]);
                                }
                            }
                            b.Save(@"Source\b.png", ImageFormat.Png);


                            Bitmap endgame = Image.FromFile(@"Source\b.png") as Bitmap; //endgame là hình chụp màn hình đã được cắt nhỏ ở phần chữ "Tiếp tục"
                            Bitmap sample_endgame = Image.FromFile(@"Source\endgame.png") as Bitmap;

                            int endgame_pixel = 0;
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    if (endgame.GetPixel(i, j) == sample_endgame.GetPixel(i + 680, j + 535))
                                    {
                                        endgame_pixel += 1;
                                    }
                                }
                            }

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");

                            so_tran = Int32.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion



                            if (endgame_pixel > 1800)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                Update_log_textbox("Xong trận");
                            }
                            #endregion
                            #endregion

                            sample_endgame.Dispose();
                            check_endgame_Screenshot_1.Dispose();
                            test.Dispose();
                            endgame.Dispose();
                            #endregion
                            SetPause(); SetStop();
                        }
                        src_2.Dispose();
                        SetPause(); SetStop();

                        #region chờ chụp màn hình xong trận
                        int n = 0;
                        while (n == 0)
                        {
                            if (Check(_Choi_lai, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
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
                    Thread.Sleep(300);
                    if (Check(_Choi_lai, 155, 25, 535, 675) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }

                    Thread.Sleep(300);
                    SetPause(); SetStop();
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
        #endregion

        #region New ver

        private bool Compare(Bitmap b1, Bitmap b2)
        {
            var watch = Stopwatch.StartNew();
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            BitmapData bmd1 = b1.LockBits(new Rectangle(0, 0, b1.Width - 1, b1.Height - 1), ImageLockMode.ReadOnly, b1.PixelFormat);
            BitmapData bmd2 = b2.LockBits(new Rectangle(0, 0, b2.Width - 1, b2.Height - 1), ImageLockMode.ReadOnly, b2.PixelFormat);

            try
            {
                int bytes = b1.Width * b1.Height;// * (Image.GetPixelFormatSize(b1.PixelFormat) / 8);

                byte[] b1bytes = new byte[bytes];
                byte[] b2bytes = new byte[bytes];

                Marshal.Copy(bmd1.Scan0, b1bytes, 0, bytes);
                Marshal.Copy(bmd2.Scan0, b2bytes, 0, bytes);

                int dem = 0;
                for (int n = 0; n <= bytes - 1; n++)
                {
                    if (b1bytes[n] != b2bytes[n])
                    {
                        dem++;
                    }
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                TabControl lb = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;

                if (lb.InvokeRequired)
                {
                    lb.Invoke(new MethodInvoker(delegate { lb.SelectedTab.Controls["textBox11"].Text += elapsedMs + "\r\n"; }));
                }
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
        public Bitmap CropBitmap(Bitmap bitmap, int w, int h, int x, int y)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }
        public void Feed_rank_2()
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
                    if (Compare(CropBitmap(PrintWindow1(_Handle),123, 40, 67, 20),Resources.dau) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(128, 35));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(46, 97));
                        Thread.Sleep(700);
                        ClickOnPoint(_Handle, new Point(177, 555));
                        Thread.Sleep(700);
                        Update_log_textbox("Tạo phòng đấu với máy");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Xác nhận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675),Resources.xacnhan) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(623, 687));
                        Update_log_textbox("Click xác nhận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Xếp hạng
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 451, 28, 94, 94),Resources.xephangdong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 450));
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Chọn roll
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 149, 101, 410, 410),Resources.chonroll) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(500);
                        ClickOnPoint(_Handle, new Point(483, 550));
                        Update_log_textbox("Đã chọn roll support");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
                    #region Tìm trận
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675),Resources.timtran) == true && Pause_Auto == false && Stop_Auto == false)
                    {

                        SetForegroundWindow(_Handle);
                        Thread.Sleep(200);
                        ClickOnPoint(_Handle, new Point(618, 685));
                        Update_log_textbox("Click tỉm trận");
                        SetPause(); SetStop();
                    }
                    #endregion
                    Thread.Sleep(300);
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
                    Thread.Sleep(300);
                    #region Cấm
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 125, 40, 580, 1),Resources.cam) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        //Thread.Sleep(500);
                        int x = 380; //tọa độ atroxx
                        int y = 145;
                        SetForegroundWindow(_Handle);
                        Thread.Sleep(1500);

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
                            Update_log_textbox("Đã cấm tướng");
                            SetPause(); SetStop();
                        }
                    }
                    #endregion

                    //if (Check(_Khoa_cam, 120, 40, 580, 1) == true && Pause_Auto == false && Stop_Auto == false)
                    //{
                    //    Thread.Sleep(1500);
                    //    ClickOnPoint(_Handle, new Point(640, 580));    //click khóa
                    //    SetPause(); SetStop();
                    //}
                    Thread.Sleep(300);
                    #region Chọn tướng
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 100, 20, 590, 10), Resources.chontuong) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        Thread.Sleep(500);
                        int x = 1280; //tọa độ atroxx
                        int y = 345;

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
                    Thread.Sleep(300);
                    #region vào game
                    if (check_vao_game() == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        #region kiểm tra đội xanh hay đỏ
                        //1270 883 mã (163,44,44) = #A32C2C
                        Color team_red = System.Drawing.ColorTranslator.FromHtml("#172634");
                        Color team_blue = System.Drawing.ColorTranslator.FromHtml("#2d2125");
                        var inGame_Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                        var inGame_gfxScreenshot = Graphics.FromImage(inGame_Screenshot);
                        inGame_gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        inGame_Screenshot.Save(@"Source\Screenshot_ingame.png", ImageFormat.Png);
                        inGame_Screenshot.Dispose();
                        Bitmap src_2 = Image.FromFile(@"Source\Screenshot_ingame.png") as Bitmap;

                        if (src_2.GetPixel(1227, 881) == team_red)
                        {
                            //đội đỏ
                            xx = 1227;
                            yy = 881;
                            Update_log_textbox("Xác định đội đỏ");
                        }
                        if (src_2.GetPixel(1423, 685) == team_blue)
                        {
                            //đội xanh
                            xx = 1421;
                            yy = 691;
                            Update_log_textbox("Xác định đội xanh");
                        }

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

                            #region kiểm tra xem kết thúc game chưa
                            #region chụp màn hình
                            var check_endgame_Screenshot_1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                            var check_endgame_gfxScreenshot_1 = Graphics.FromImage(check_endgame_Screenshot_1);
                            check_endgame_gfxScreenshot_1.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                            check_endgame_Screenshot_1.Save(@"Source\Screenshot_1.png", ImageFormat.Png);


                            #endregion
                            #region kiểm tra

                            Bitmap test = Image.FromFile(@"Source\Screenshot_1.png") as Bitmap;
                            Color[,] pic = new Color[test.Width, test.Height];
                            for (int i = 680; i < 760; i++)
                            {
                                for (int j = 535; j < 560; j++)
                                {
                                    pic[i, j] = test.GetPixel(i, j);
                                }
                            }

                            Bitmap b = new Bitmap(80, 25);
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    b.SetPixel(i, j, pic[i + 680, j + 535]);
                                }
                            }
                            b.Save(@"Source\b.png", ImageFormat.Png);


                            Bitmap endgame = Image.FromFile(@"Source\b.png") as Bitmap; //endgame là hình chụp màn hình đã được cắt nhỏ ở phần chữ "Tiếp tục"
                            Bitmap sample_endgame = Image.FromFile(@"Source\endgame.png") as Bitmap;

                            int endgame_pixel = 0;
                            for (int i = 0; i < 80; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    if (endgame.GetPixel(i, j) == sample_endgame.GetPixel(i + 680, j + 535))
                                    {
                                        endgame_pixel += 1;
                                    }
                                }
                            }

                            #region đọc file log ghi số trận
                            StreamReader sr = File.OpenText(@"SoTran\SoTran.txt");

                            so_tran = Int32.Parse(sr.ReadLine()) + 1;
                            sr.Dispose();
                            #endregion



                            if (endgame_pixel > 1800)
                            {
                                DoMouseClick(720, 545);
                                m = 1;
                                Update_log_textbox("Xong trận");
                            }
                            #endregion
                            #endregion

                            sample_endgame.Dispose();
                            check_endgame_Screenshot_1.Dispose();
                            test.Dispose();
                            endgame.Dispose();
                            #endregion
                            SetPause(); SetStop();
                        }
                        src_2.Dispose();
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
                    #endregion
                    Thread.Sleep(300);
                    if (Compare(CropBitmap(PrintWindow1(_Handle), 155, 25, 535, 675),Resources.choilai) == true && Pause_Auto == false && Stop_Auto == false)
                    {
                        SetForegroundWindow(_Handle);
                        ClickOnPoint(_Handle, new Point(616, 687));
                        Update_log_textbox("Chơi lại");
                        SetPause(); SetStop();
                    }

                    Thread.Sleep(300);
                    SetPause(); SetStop();
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
    


        #endregion  

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
