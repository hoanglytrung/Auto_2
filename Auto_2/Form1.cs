﻿using Auto_2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Resources;
using WindowScrape;
using WindowsFormsApplication1;

namespace Auto_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        Auto _Auto = new Auto();
        Auto_new _AutoN = new Auto_new();

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
        #endregion 
        
     
        private static int _Click_Delay = 20000;
        public int Click_Delay
        {
            set
            {
                _Click_Delay = value;
            }
            get
            {
                return _Click_Delay;
            }
        }

        private static int _So_tran_dung_Auto = 99;
        public int Sotrandung_Auto
        {
            set
            {
                _So_tran_dung_Auto = value;
            }
            get
            {
                return _So_tran_dung_Auto;
            }
        }

        public static int SotrandungAuto()
        {
            return _So_tran_dung_Auto;
        }

        public static void GiamSotrandungAuto()
        {
            _So_tran_dung_Auto--;
        }

        private static bool pause = false;
        public static bool GetPauseState()
        {
            return pause;
        }
       
        private static bool stop = false;
        public static bool GetStopState()
        {
            return stop;
        }

        private int type_match;
        public int Type_match
        {
            set
            {
                this.type_match = value;
            }
            get
            {
                return this.type_match;
            }
        }

        private const uint WM_RBUTTONDOWN = 0x0204;
        private const uint WM_RBUTTONUP = 0x0205;
        private const uint WM_LBUTTONDOWN = 0x201;
        private const uint WM_LBUTTONUP = 0x202;
        private const uint MK_LBUTTON = 0x0001;
        private const uint WM_SETCURSOR = 0x0020;
        private const uint WM_PARENTNOTIFY = 0x0210;
        private const uint BM_CLICK = 0x00F5;
        private const uint WM_SHOWWINDOW = 0x0018;
        private const int SW_MINIMIZE = 6;

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;
        const int WM_IME_NOTIFY = 0x0282;
        const int WM_CLOSE = 0x0010;
        const int WM_DESTROY = 0x0002;
        const int WM_GETICON = 0x007F;

        private bool Form_Closed;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Taskbar.Show();
                e.Cancel = false;
                Taskbar.Show();
                Form_Closed = true;
                
                
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
       
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private void Take_pic()
        {
            IntPtr Window = FindWindow("FIFA ONLINE3 - Developed by SPEARHEAD", null);
            IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "FIFANG", null);

            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);

            IntPtr Window_2 = FindWindow("League of Legends (TM) Client", null);
            IntPtr Class_2 = FindWindowEx(Window_1, IntPtr.Zero, "RiotWindowClass", null);


            Bitmap a = PrintWindow1(Class_1);
            a.Save("a.png", ImageFormat.Png);
            pictureBox1.Image = a;
            a.Dispose();
        }
        private Bitmap _Take_pic()
        {
            IntPtr Window = FindWindow("FIFA ONLINE3 - Developed by SPEARHEAD", null);
            IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "FIFANG", null);

            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);

            IntPtr Window_2 = FindWindow("League of Legends (TM) Client", null);
            IntPtr Class_2 = FindWindowEx(Window_1, IntPtr.Zero, "RiotWindowClass", null);


            Bitmap a = PrintWindow1(Class_1);
            return a;
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

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                if (FindWindow("RCLIENT", "League of Legends") != IntPtr.Zero)
                {
                    IntPtr Window_1 = FindWindow("League Client", null);
                    IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);
                    ShowWindow(Class_1, WindowShowStyle.ShowNormal);
                   
                    checkBox1.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox4.Enabled = true;
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                { MessageBox.Show("Game chưa chạy. Vui lòng chạy game trước."); }
            }
            else
            {
                MessageBox.Show("Auto đang chạy");
            }


           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr Window = FindWindow("PVP.net Client", null);
            IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "ApolloRuntimeContentWindow", null);

            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);

            if (Class_1 != IntPtr.Zero)
            {
                Process[] proc = Process.GetProcessesByName("LeagueClient");
                proc[0].Kill();
            }
            //SendMessage((int)Class_1, WM_CLOSE, 0, 0);
        }

        private void test_click()
        {
            IntPtr Window = FindWindow("FIFA ONLINE3 - Developed by SPEARHEAD", null);
            IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "FIFANG", null);
            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);
            IntPtr Window_2 = FindWindow("League of Legends (TM) Client", null);
            IntPtr Class_2 = FindWindowEx(Window_1, IntPtr.Zero, "RiotWindowClass", null);


            PostMessage(Class_2, WM_LBUTTONDOWN, 0, ((755 << 0x10) | 639));

            PostMessage(Class_2, WM_LBUTTONUP, 0, ((755 << 0x10) | 639));
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        
        private void button3_Click(object sender, EventArgs e)
        {
             //test_click();
          // Click_vao_game();

            //Bitmap a = new Bitmap(160, 40);
            //Bitmap b = Image.FromFile("a.png") as Bitmap;
            //for (int i = 0; i < 160; i++)
            //{
            //    for (int j = 0; j < 40; j++)
            //    {
            //        a.SetPixel(i, j, b.GetPixel(30 + i, 20 + j));
            //    }
            //}
            //a.Save("a1.png", ImageFormat.Png);
            //b.Dispose();

            //Color[,] a = Auto.Auto1();
            //Bitmap b = new Bitmap(100, 20);
            //for (int i = 0; i < b.Width; i++)
            //{
            //    for (int j = 0; j < b.Height; j++)
            //    {
            //        b.SetPixel(i, j, a[i, j]);
            //    }
            //}
            //pictureBox1.Image = b;


            //MessageBox.Show(Auto.Check(Auto.Xacnhan(), 155, 25, 535, 675).ToString());
            //pictureBox1.Image = Auto.Check(Auto.Xacnhan(), 155, 25, 535, 675);

            

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
        
        private void button4_Click(object sender, EventArgs e)
        {
            IntPtr Window = FindWindow("FIFA ONLINE3 - Developed by SPEARHEAD", null);
            IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "FIFANG", null);
            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);

            ShowWindow(Class_1, WindowShowStyle.Hide);         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IntPtr Window_1 = FindWindow("League Client", null);
            IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);
            //IntPtr Window = FindWindow("FIFA ONLINE3 - Developed by SPEARHEAD", null);
            //IntPtr Class = FindWindowEx(Window, IntPtr.Zero, "FIFANG", null);
            ShowWindow(Class_1, WindowShowStyle.ShowNormal);
        }
        
        private void Thread_Check_Client()
        {           
            while (!Form_Closed)
            {
                if (!_Auto.Is_handle_exist())
                {
                    label1.Invoke(new MethodInvoker(delegate { label1.Text = "chưa vào game"; }));
                    button2.Invoke(new MethodInvoker(delegate { button2.Enabled = false; }));
                }
                else
                {
                    label1.Invoke(new MethodInvoker(delegate { label1.Text = " đã vào game"; }));
                    button2.Invoke(new MethodInvoker(delegate { button2.Enabled = true; }));
                }
            }
            Thread.Sleep(2500);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (FindWindow("WindowsForms10.Window.8.app.0.141b42a_r14_ad1", "Auto LOL") == IntPtr.Zero)
            {
                Thread t = new Thread(new ThreadStart(Thread_Check_Client));
                t.SetApartmentState(ApartmentState.MTA);
                t.IsBackground = true;
                t.Start();
            }
           

        }

      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //int i = 0;
            //_Auto.test();
            if (this.type_match == 1)
            {
                _Auto.Click_vao_game();
            }
            if (this.type_match == 2)
            {
                _Auto.Feed_rank();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chế độ auto");
                this.type_match = 0;
            }
            //while (true)
            //{
            //    te = i++.ToString() + "\r\n";
            //    Thread.Sleep(1000);
            //    if (textBox3.InvokeRequired)
            //    {
            //        textBox3.Invoke(new MethodInvoker(delegate { textBox3.Text += i++.ToString() + "\r\n"; }));
            //        Thread.Sleep(1000);
            //    }
            //    te += i++.ToString() + "\r\n";
            //}
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (type_match != 0)
            {
                MessageBox.Show("Đã dừng Auto");
            }
        }

        #region Check_list
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = "99";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                Taskbar.Hide();
            }
            else Taskbar.Show();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                pause = true;
            }
            else
                pause = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
                textBox2.Enabled = true;
            else
                textBox2.Enabled = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                this.Hide();
                //notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(500);
            }
            //else if (FormWindowState.Normal == this.WindowState)
            //{
            //    notifyIcon1.Visible = false;
            //}
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                //checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                stop = true;
            }
            else
                stop = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox7.Checked == true)
            {
                this.TopMost = true;
            }
            else this.TopMost = false;
        }

        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (Int32.Parse(textBox2.Text) > 60)
                {
                    this.Click_Delay = 60000;
                }

                else
                    this.Click_Delay = Int32.Parse(textBox2.Text) * 1000;
            }
            else
            {
                textBox2.Text = "20";
                this.Click_Delay = 20000;
            }
        }

        public static int ClickDl()
        {
            return _Click_Delay;
        }

        


        private Control CTextbox1;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //this.Sotrandung_Auto = Int32.Parse(textBox1.Text);
            Regex regex = new Regex(@"[^0-9^]");
            MatchCollection matches = regex.Matches(textBox1.Text);
            if (matches.Count > 0)
            {

            }
            if (textBox1.Text != "")
                this.Sotrandung_Auto = Int32.Parse(textBox1.Text);
            else
            {
                textBox1.Text = "1";
                this.Sotrandung_Auto = 1;
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for a naughty character in the KeyDown event.
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;

                //ToolTip tp = new ToolTip();
                //tp.ToolTipIcon = ToolTipIcon.Error;
                //tp.AutoPopDelay = 500;
                //tp.ToolTipTitle = "Unacceptable Character";
                //tp.Show("You can only type a number here.", textBox1);
                
            }
            
        }




        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        private Control popup;

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            ////MessageBox.Show("A");
            //testUC mcu = new testUC();
            //this.popup = mcu; //save references to new control
            //this.Controls.Add(this.popup);
            //this.popup.Location = new Point(checkBox1.Location.X - 100, checkBox1.Location.Y - 100);
            ////this.popup.Location = new Point(100, 100);
            //this.popup.BringToFront();
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            //this.Controls.Remove(this.CTextbox1);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Take_pic();
            //string date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss tt");
            //textBox3.Text = date;
            
        }
       
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string path = "log.txt";
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            else if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                string log = sr.ReadToEnd();
                sr.Dispose();

                log += textBox3.Text;
                StreamWriter sw = new StreamWriter(path);
                sw.Write(log);
                sw.Dispose();
            }
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox3.Text += te;
        }

        
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            checkBox5.Checked = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //SetForegroundWindow(this.Handle);
                int x = Control.MousePosition.X;
                int y = Control.MousePosition.Y;
               
                y = y-25;
                this.contextMenuStrip1.Show(x, y);
            }
               
        }
         [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

         private void toolStripMenuItem1_Click(object sender, EventArgs e)
         {
             Application.Exit();
         }


         #region admin tool
         private bool admin = false;
         private bool admin_pw = false;
         private string pic_path;
         private void textBox4_TextChanged(object sender, EventArgs e)
         {
             if (textBox4.Text == "duyen")
             {
                 admin = true;
             }
         }

         private void textBox5_TextChanged(object sender, EventArgs e)
         {
             if (textBox5.Text == "duyen")
             {
                 admin_pw = true;
             }
         }

         private void button7_Click(object sender, EventArgs e)
         {
             if (admin == true && admin_pw == true)
             {
                 button8.Visible = true;
                 button9.Visible = true;
                 button10.Visible = true;
                 textBox6.Visible = true;
                 textBox7.Visible = true;
                 textBox8.Visible = true;
                 textBox9.Visible = true;
                 textBox10.Visible = true;
             }
             else
                 MessageBox.Show("Sai");
         }
        
         private string sFileName;
         private string[] arrAllFiles;
         private Bitmap pic;
         private void button10_Click(object sender, EventArgs e)
         {
             OpenFileDialog op = new OpenFileDialog();
             op.Filter = "Images (*.PNG;*.JPG;*.GIF)|*.PNG;*.JPG;*.GIF|" +
                                     "All files (*.*)|*.*";
             String file = op.FileName;
             if (op.ShowDialog() == DialogResult.OK)
             {
                 sFileName = op.FileName;
                 arrAllFiles = op.FileNames; //used when Multiselect = true           
             }
             if (sFileName != "")
                pic = new Bitmap(sFileName);
             op.Dispose();
             //MessageBox.Show(sFileName);             
         }

        private void crop()
         {
             Bitmap a = new Bitmap(Int32.Parse(textBox8.Text) - Int32.Parse(textBox6.Text), Int32.Parse(textBox9.Text) - Int32.Parse(textBox7.Text));

             for (int i = 0; i < Int32.Parse(textBox8.Text) - Int32.Parse(textBox6.Text); i++)
             {
                 for (int j = 0; j < Int32.Parse(textBox9.Text) - Int32.Parse(textBox7.Text); j++)
                 {
                     a.SetPixel(i, j, pic.GetPixel(Int32.Parse(textBox6.Text) + i, Int32.Parse(textBox7.Text) + j));
                 }
             }
                string save = "Feed\\" + textBox10.Text + ".png";
             a.Save(save, ImageFormat.Png);
             a.Dispose();
             pic.Dispose();
              textBox6.Clear();
              textBox7.Clear();
              textBox8.Clear();
              textBox9.Clear();
         }

        private void button9_Click(object sender, EventArgs e)
        {
            crop();
        }
        #endregion

        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.type_match = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.type_match = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap tmp = _Take_pic();
            Bitmap a = new Bitmap(32,32);

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    a.SetPixel(i, j, tmp.GetPixel(56 + i, 276 + j));
                }
            }
            string save = "Champions\\" + textBox10.Text + ".png";
            a.Save(save, ImageFormat.Png);
            a.Dispose();
            tmp.Dispose();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            textBox11.SelectionStart = textBox11.Text.Length;
            textBox11.ScrollToCaret();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                if (FindWindow("RCLIENT", "League of Legends") != IntPtr.Zero)
                {
                    IntPtr Window_1 = FindWindow("League Client", null);
                    IntPtr Class_1 = FindWindowEx(Window_1, IntPtr.Zero, "RCLIENT", null);
                    ShowWindow(Class_1, WindowShowStyle.ShowNormal);

                    checkBox1.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox4.Enabled = true;
                    backgroundWorker2.RunWorkerAsync();
                }
                else
                { MessageBox.Show("Game chưa chạy. Vui lòng chạy game trước."); }
            }
            else
            {
                MessageBox.Show("Auto đang chạy");
            }
           
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.type_match == 1)
            {
                _AutoN.Feed_bot();
            }
            if (this.type_match == 2)
            {
                _AutoN.Feed_rank_3();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chế độ auto");
                this.type_match = 0;
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("done");
        }

       
    }
}