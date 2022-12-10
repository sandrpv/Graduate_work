using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC___cactus_cleaner
{
    public partial class CC___cactus_cleaner : Form
    {
        public CC___cactus_cleaner()
        {
            InitializeComponent();
        }//MAIN
        //SRART-MENU
        private void turnOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAutorunValue(true);
        }//AutoRun+
        private void turnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAutorunValue(false);
        }//AutoRun-
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(() => { Application.Run(new Help()); }).Start();
        }//Help
        //END-MENU
        public bool SetAutorunValue(bool autorun)
        {
            string name = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }//AutoRun-func
        private void button1_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(() => { Application.Run(new Cleaner()); }).Start();
            Close();
        }//OPEN CLEANER
    }
}
