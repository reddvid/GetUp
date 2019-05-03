using GetUp.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;
using Windows.UI.Popups;

namespace GetUp
{
    public class Program
    {
        public static MyCustomApplicationContext _Prog;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;

            using (Mutex mutex = new Mutex(true, "Tayô", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new MyCustomApplicationContext());
                }
                else
                {
                    MessageBox.Show("Tayô is already running and silently lives on the taskbar notification area.", "Tayô", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        public class MyCustomApplicationContext : ApplicationContext
        {
            public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            public NotifyIcon trayIcon;
            private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            private static readonly string StartupValue = "Tayô";
            private ContextMenuStrip contextMenuStrip;
            private ToolStripMenuItem toolStripCustomize = new ToolStripMenuItem();
            private ToolStripSeparator toolStripSep = new ToolStripSeparator();
            private ToolStripMenuItem toolStripAbout = new ToolStripMenuItem();
            private ToolStripMenuItem toolStripTest = new ToolStripMenuItem();
            private ToolStripMenuItem toolStripExit = new ToolStripMenuItem();


            public MyCustomApplicationContext()
            {
                _Prog = this;

                timer.Stop();

                toolStripCustomize.Text = "Settings";
                toolStripCustomize.Click += ToolStripCustomize_Click;
                toolStripCustomize.AutoSize = false;
                toolStripCustomize.Size = new Size(260, 30);
                toolStripCustomize.Margin = new Padding(0, 4, 0, 0);

                toolStripExit.Text = "Quit";
                toolStripExit.Click += ToolStripExit_Click;
                toolStripExit.AutoSize = false;
                toolStripExit.Size = new Size(260, 30);
                toolStripExit.Margin = new Padding(0, 0, 0, 4);

                toolStripAbout.Text = "About";
                toolStripAbout.Click += ToolStripAbout_ClickAsync;
                toolStripAbout.AutoSize = false;
                toolStripAbout.Size = new Size(260, 30);

                toolStripTest.Text = "Test";
                toolStripTest.Click += ToolStripTest_ClickAsync;

                //RemoveStartup();
                AddRunOnStartupRegistry();

                contextMenuStrip = new ContextMenuStrip()
                {
                    DropShadowEnabled = false,
                    ShowCheckMargin = true,
                    ShowImageMargin = false,
                    Size = new System.Drawing.Size(465, 170)
                };

                contextMenuStrip.Items.AddRange(new ToolStripItem[]
                {
                    toolStripCustomize,
                    //toolStripSep,
                    toolStripAbout,
                    toolStripExit
                });

                var verticalPadding = 4;
                this.contextMenuStrip.Items[0].Font = new Font(this.contextMenuStrip.Items[0].Font, FontStyle.Bold);
                // Set             
                bool appsUseLight = ReadRegistry();


                if (appsUseLight)
                {
                    this.contextMenuStrip.Renderer = new MyCustomRenderer { VerticalPadding = verticalPadding, HighlightColor = Color.White, ImageColor = Color.FromArgb(255, 238, 238, 238) };
                    this.contextMenuStrip.BackColor = Lighten(Color.White);
                    this.contextMenuStrip.ForeColor = Color.Black;
                }
                else
                {
                    this.contextMenuStrip.Renderer = new MyCustomRenderer { VerticalPadding = verticalPadding, HighlightColor = Color.Black, ImageColor = Color.FromArgb(255, 43,43,43) };
                    this.contextMenuStrip.BackColor = Lighten(Color.Black);
                    this.contextMenuStrip.ForeColor = Color.White;
                }

                this.contextMenuStrip.MinimumSize = new Size(240, 30);
                this.contextMenuStrip.AutoSize = false;
                this.contextMenuStrip.ShowImageMargin = false;
                this.contextMenuStrip.ShowCheckMargin = false;

                // Initialize Tray Icon
                trayIcon = new NotifyIcon()
                {
                    ContextMenuStrip = contextMenuStrip,
                    Icon = Resources.getupforlife,
                    Visible = true,
                    Text = "Tayô"
                };

                trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
                //trayIcon.MouseClick += TrayIcon_MouseClick;

                int index = Properties.Settings.Default.cbIndex;

                int[] minutes = new int[] { 30, 60, 90, 120, 180, 5 };
                timer.Interval = minutes[index] * 60 * 1000;

                trayIcon.Text = "Tayô (" + minutes[index] + " mins)";

                timer.Tick += new EventHandler(ShowNotification);

                timer.Start();
            }

            private bool ReadRegistry()
            {
                bool o;
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
                {
                    if (key != null)
                    {
                        var k = key.GetValue("AppsUseLightTheme");
                        if (k != null)
                        {
                            if (k.ToString() == "1")
                                o = true;
                            else
                                o = false;
                        }
                        else
                        {
                            o = true;
                        }
                    }
                    else
                        o = true;
                }

                return o;
            }

            Color Lighten(Color color)
            {
                int r;
                int g;
                int b;

                if (color.R == 0 && color.G == 0 && color.B == 0)
                {
                    r = color.R + 43;
                    g = color.G + 43;
                    b = color.B + 43;
                }
                else
                {
                    r = color.R - 17;
                    g = color.G - 17;
                    b = color.B - 17;
                }

                return Color.FromArgb(r, g, b);
            }

            private void ToolStripTest_ClickAsync(object sender, EventArgs e)
            {
                ShowNotification(sender, e);
            }

            public async void ShowNotification(object sender, EventArgs e)
            {
                // Show UWP Notification
                string str = "getupview://";

                string systemUptime = UpTime.ToString(@"d\d\:hh\h\:mm\m");
                // Uri uri = new Uri(str + "location?lat=" +
                // lat.ToString() + "&?lon=" + lon.ToString());

                Uri uri = new Uri(str + "design?font=" + Properties.Settings.Default.cbFontIndex + "&uptime=" + systemUptime);

                await Windows.System.Launcher.LaunchUriAsync(uri);

                //trayIcon.BalloonTipText = "Get up for Life";
                //trayIcon.ShowBalloonTip(5000);
            }

            public TimeSpan UpTime
            {
                get
                {
                    using (var uptime = new PerformanceCounter("System", "System Up Time"))
                    {
                        uptime.NextValue();       //Call this an extra time before reading its value
                        return TimeSpan.FromSeconds(uptime.NextValue());
                    }
                }
            }

            private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
            {
                var frm = new Settings();

                if (!CheckOpened("Settings"))
                    frm.Show();
            }

            private bool CheckOpened(string name)
            {
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    if (frm.Text == name)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool IsProcessOpen(string name)
            {
                //here we're going to get a list of all running processes on
                //the computer
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    //now we're going to see if any of the running processes
                    //match the currently running processes. Be sure to not
                    //add the .exe to the name you provide, i.e: NOTEPAD,
                    //not NOTEPAD.EXE or false is always returned even if
                    //notepad is running.
                    //Remember, if you have the process running more than once, 
                    //say IE open 4 times the loop thr way it is now will close all 4,
                    //if you want it to just close the first one it finds
                    //then add a return; after the Kill
                    if (clsProcess.ProcessName.Contains(name))
                    {
                        //if the process is found to be running then we
                        //return a true
                        return true;
                    }
                }
                //otherwise we return a false
                return false;
            }

            private void ToolStripCustomize_Click(object sender, EventArgs e)
            {
                new Settings().Show();
            }

            private void ToolStripAbout_ClickAsync(object sender, EventArgs e)
            {
                throw new NotImplementedException();
            }

            private void ToolStripExit_Click(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            }

            private async void AddRunOnStartupRegistry()
            {
                // Set the application to run at startup
                //var key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                //key.SetValue(StartupValue, Application.ExecutablePath.ToString());

                //Debug.WriteLine(Application.ExecutablePath + " Added to Startup");

                StartupTask startupTask = await StartupTask.GetAsync("TayôTaskId"); // Pass the task ID you specified in the appxmanifest file
                switch (startupTask.State)
                {
                    case StartupTaskState.Disabled:
                        // Task is disabled but can be enabled.
                        StartupTaskState newState = await startupTask.RequestEnableAsync(); // ensure that you are on a UI thread when you call RequestEnableAsync()
                        Debug.WriteLine("Request to enable startup, result = {0}", newState);
                        break;
                    case StartupTaskState.DisabledByUser:
                        // Task is disabled and user must enable it manually.

                        break;
                    case StartupTaskState.DisabledByPolicy:
                        Debug.WriteLine("Startup disabled by group policy, or not supported on this device");
                        break;
                    case StartupTaskState.Enabled:
                        Debug.WriteLine("Startup is enabled.");
                        break;
                }
            }

            private void RemoveStartup()
            {
                // Set the application to run at startup
                var key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                key.DeleteValue(Application.ExecutablePath.ToString());

                Debug.WriteLine("Removed from Startup");
            }

            void Exit(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            }
        }

        public class MyColorTable : ProfessionalColorTable
        {
            public override Color ToolStripGradientBegin
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color ToolStripGradientEnd
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color MenuItemSelected
            {
                get { return Color.WhiteSmoke; }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(255, 43, 43, 43); }
            }
        }

        private class MyCustomRenderer : ToolStripProfessionalRenderer
        {
            public MyCustomRenderer() : base(new MyColorTable())
            {
            }

            public Color ImageColor { get; set; }
            public Color HighlightColor { get; set; }
            public int VerticalPadding { get; set; }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (null == e)
                { return; }
                e.TextFormat &= ~TextFormatFlags.HidePrefix;
                e.TextFormat |= TextFormatFlags.VerticalCenter;
                var rect = e.TextRectangle;
                rect.Offset(24, VerticalPadding);
                e.TextRectangle = rect;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                    base.OnRenderMenuItemBackground(myMenu);
                else
                {
                    if (myMenu.Item.Enabled)
                    {
                        Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                        //Fill Color
                        myMenu.Graphics.FillRectangle(new SolidBrush(RenderHighlight(HighlightColor)), menuRectangle);
                        // Border Color
                        // myMenu.Graphics.DrawRectangle(Pens.Lime, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                    }
                    else
                    {
                        Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                        //Fill Color
                        myMenu.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 128, 128, 128)), menuRectangle);
                    }

                }
            }

            private Color RenderHighlight(Color color)
            {
                int r;
                int g;
                int b;

                if (color.R == 0 && color.G == 0 && color.B == 0)
                {
                    r = color.R + 65;
                    g = color.G + 65;
                    b = color.B + 65;
                }
                else
                {
                    r = color.R;
                    g = color.G;
                    b = color.B;
                }

                return Color.FromArgb(r, g, b);
            }

            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
                r.Inflate(1, 1);
                e.Graphics.FillRectangle(new SolidBrush(ImageColor), r);
                //r.Inflate(-4, -4);
                e.Graphics.DrawLines(Pens.Gray, new Point[]
                {
                    new Point(r.Left + 4, 10), //2
                    new Point(r.Left - 2 + r.Width / 2,  r.Height / 2 + 4), //3
                    new Point(r.Right - 4, r.Top + 4)
                });
            }
        }
    }
}
