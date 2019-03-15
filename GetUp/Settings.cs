using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void CbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save
            Properties.Settings.Default.Save();

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.cbIndex = cbOptions.SelectedIndex;

            Properties.Settings.Default.Save();

            int index = Properties.Settings.Default.cbIndex;

            int[] minutes = new int[] { 30, 60, 90, 120, 180, 5 };

            timer.Interval = minutes[index] * 60 * 1000;

            Program._Prog.trayIcon.Text = "Get Up For Life (" + minutes[index] + " mins)";

            timer.Start();

            this.Close();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.cbIndex = cbOptions.SelectedIndex;

            Properties.Settings.Default.Save();

            int index = Properties.Settings.Default.cbIndex;

            int[] minutes = new int[] { 30, 60, 90, 120, 180, 5 };

            timer.Interval = minutes[index] * 60 * 1000;

            Program._Prog.trayIcon.Text = "Get Up For Life (" + minutes[index] + " mins)";

            timer.Start();
        }
    }
}
