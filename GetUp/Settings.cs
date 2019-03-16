using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetUp
{
    public partial class Settings : Form
    {

        public Timer timer = null;

        public Settings()
        {
            InitializeComponent();

            LoadComboBox();

            this.CenterToScreen();

            timer = Program._Prog.timer;

            timer.Stop();
        }

        private void LoadComboBox()
        {
            cbOptions.SelectedIndex = Properties.Settings.Default.cbIndex;
            cbFonts.SelectedIndex = Properties.Settings.Default.cbFontIndex;
        }

        private void CbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.cbIndex = cbOptions.SelectedIndex;
            Properties.Settings.Default.cbFontIndex = cbFonts.SelectedIndex;

            Properties.Settings.Default.Save();

            int index = Properties.Settings.Default.cbIndex;

            int[] minutes = new int[] { 30, 60, 90, 120, 180, 5 };

            timer.Interval = minutes[index] * 60 * 1000;

            Program._Prog.trayIcon.Text = "Tayô (" + minutes[index] + " mins)";

            timer.Start();

            this.Close();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.cbIndex = cbOptions.SelectedIndex;
            Properties.Settings.Default.cbFontIndex = cbFonts.SelectedIndex;

            Properties.Settings.Default.Save();

            int index = Properties.Settings.Default.cbIndex;

            int[] minutes = new int[] { 30, 60, 90, 120, 180, 5 };

            timer.Interval = minutes[index] * 60 * 1000;

            Program._Prog.trayIcon.Text = "Tayô (" + minutes[index] + " mins)";

            timer.Start();
        }

        private void CbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFont(cbFonts.SelectedIndex);
        }

        int fontLength;
        byte[] fontdata;

        private void ChangeFont(int selectedIndex)
        {
            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
           

            switch (selectedIndex)
            {
                case 0:
                    fontLength = Properties.Resources.Segoe_UI_Bold.Length;
                    fontdata = Properties.Resources.Segoe_UI_Bold;
                    break;

                case 1:
                    fontLength = Properties.Resources.Montserrat_Bold.Length;
                    fontdata = Properties.Resources.Montserrat_Bold;
                    break;

                case 2:
                    fontLength = Properties.Resources.Raleway_Bold.Length;
                    fontdata = Properties.Resources.Raleway_Bold;
                    break;
            }
            
            // create an unsafe memory block for the font data
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            //After that we can create font and assign font to label
            lblPreview.Font = new Font(pfc.Families[0], lblPreview.Font.Size);
            lblPreview.Text = "Tayô";
            lblPreview.ForeColor = Color.White;

            lblPreview.Left = (this.groupBox1.Width - lblPreview.Width) / 2;
            lblPreview.Top = 5 + (this.groupBox1.Height - lblPreview.Height) / 2;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string str = "getupview://";

            // Uri uri = new Uri(str + "location?lat=" +
            // lat.ToString() + "&?lon=" + lon.ToString());

            Uri uri = new Uri(str + "design?font=" + cbFonts.SelectedIndex + "&?lon=");
            Debug.WriteLine(cbFonts.SelectedIndex);

            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
