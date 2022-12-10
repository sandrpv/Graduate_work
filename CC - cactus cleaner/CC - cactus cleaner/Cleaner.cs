using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC___cactus_cleaner
{
    public partial class Cleaner : Form
    {
        public Cleaner()
        {
            InitializeComponent();
            this.FormClosed += Cleaner_FormClosed;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            int i = 0;
            foreach (DriveInfo d in allDrives)
            {
                checkedListBox1.Items.Add(d.Name);
                checkedListBox1.SetItemChecked(i, true);
                i++;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                checkedListBox2.Enabled = false;
            }
            Char3();
            progressBar1.Maximum = 50000;
        }//MAIN
        public string DriversName;
        public string Deletedfiles;
        public int j = 0;
        public long suma = 0;
        void Cleaner_FormClosed(object sender, FormClosedEventArgs e)
        {
            new System.Threading.Thread(() => { Application.Run(new CC___cactus_cleaner()); }).Start();
        }//OPEN CC-HOME
        private void Char3()
        {
            checkedListBox3.Items.Add("*.---");
            checkedListBox3.Items.Add("*.#res");
            checkedListBox3.Items.Add("*.$db");
            checkedListBox3.Items.Add("*.*$");
            checkedListBox3.Items.Add("*.*~");
            checkedListBox3.Items.Add("*.?$?");
            checkedListBox3.Items.Add("*.?~?");
            checkedListBox3.Items.Add("*.@@@");
            checkedListBox3.Items.Add("*.^*");
            checkedListBox3.Items.Add("*._detmp");
            checkedListBox3.Items.Add("*._mp");
            checkedListBox3.Items.Add("*.~*");
            checkedListBox3.Items.Add("*.1st");
            checkedListBox3.Items.Add("*.bac");
            checkedListBox3.Items.Add("*.back*");
            checkedListBox3.Items.Add("*.bak");
            checkedListBox3.Items.Add("*.bup");
            checkedListBox3.Items.Add("*.chk");
            checkedListBox3.Items.Add("*.cpy");
            checkedListBox3.Items.Add("*.dir");
            checkedListBox3.Items.Add("*.dmp");
            checkedListBox3.Items.Add("*.err");
            checkedListBox3.Items.Add("*.fic");
            checkedListBox3.Items.Add("*.fnd");
            checkedListBox3.Items.Add("*.ftg");
            checkedListBox3.Items.Add("*.fts");
            checkedListBox3.Items.Add("*.gid");
            checkedListBox3.Items.Add("*.log");
            checkedListBox3.Items.Add("*.MS");
            checkedListBox3.Items.Add("*.nav");
            checkedListBox3.Items.Add("*.old");
            checkedListBox3.Items.Add("*.prv");
            checkedListBox3.Items.Add("*.sdi");
            checkedListBox3.Items.Add("*.shd");
            checkedListBox3.Items.Add("*.sik");
            checkedListBox3.Items.Add("*.syd");
            checkedListBox3.Items.Add("*.temp");
            checkedListBox3.Items.Add("*.tmp");
            checkedListBox3.Items.Add("*.wbk");
            checkedListBox3.Items.Add("*.xlk");
            checkedListBox3.Items.Add("*__ofidx*.*");
            checkedListBox3.Items.Add("*~tmp.*");
            checkedListBox3.Items.Add("*log.txt");
            checkedListBox3.Items.Add("_istmp*.*");
            checkedListBox3.Items.Add("~*.*");
            checkedListBox3.Items.Add("0*.nch");
            checkedListBox3.Items.Add("bootlog.*");
            checkedListBox3.Items.Add("brndlog.txt");
            checkedListBox3.Items.Add("chklist.*");
            checkedListBox3.Items.Add("ffastun.*");
            checkedListBox3.Items.Add("iebak.dat");
            checkedListBox3.Items.Add("modemdet.txt");
            checkedListBox3.Items.Add("ntbtlog.txt");
            checkedListBox3.Items.Add("pspbrwse.jbf");
            checkedListBox3.Items.Add("scandisk.log");
            checkedListBox3.Items.Add("setuplog.txt");
            checkedListBox3.Items.Add("suhdlog.txt");
            checkedListBox3.Items.Add("t3v?????.*");
            checkedListBox3.Items.Add("thumbs.db");
            checkedListBox3.Items.Add("twain???.mtx");
            checkedListBox3.Items.Add("*.___");
        }//FILE-TYPE
        private List<string> GetRecursFiles(string start_path)
        {
            List<string> ls = new List<string>();
            try
            {
                string[] folders = Directory.GetDirectories(start_path);
                foreach (string folder in folders)
                {
                    ls.Add("Папка: " + folder);
                    ls.AddRange(GetRecursFiles(folder));
                }
                foreach (object itemChecked in checkedListBox3.CheckedItems)
                {
                    string[] files = Directory.GetFiles(start_path, checkedListBox3.GetItemText(itemChecked));
                    foreach (string filename in files)
                    {
                        ls.Add("File: " + filename);
                        checkedListBox2.Items.Add(filename);
                        System.IO.FileInfo file = new System.IO.FileInfo(filename);
                        progressBar1.Value += 1;
                        suma = suma + file.Length;
                        j++;
                    }
                }
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            string[] suf = { "Байт", "КБ", "МБ", "ГБ" };
            if (suma == 0)
                label3.Text = "0" + " " + suf[0];
            else
            {
                long bytes = Math.Abs(suma);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                label3.Text = "Размер: " + (Math.Sign(suma) * num).ToString() + " " + suf[place] + " Количество: " + j;
            }
            return ls;
        }//SEARCH
        private void Delete()
        {
            foreach (object itemChecked in checkedListBox2.CheckedItems)
            {
                string deleted = itemChecked.ToString();
                Deletedfiles = deleted;
                System.IO.File.Delete(deleted);
            }
        }//DELETE
        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = " ";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            button5.Enabled = false;
            checkedListBox1.Enabled = false;
            checkedListBox2.Enabled = false;
            checkedListBox3.Enabled = false;
            progressBar1.Value = 0;
            suma = 0;
            j = 0;
            checkedListBox2.Items.Clear();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                string NameDrivers = itemChecked.ToString();
                DriversName = NameDrivers;
                List<string> ls = GetRecursFiles(NameDrivers);
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;
            button5.Enabled = true;
            checkedListBox1.Enabled = true;
            checkedListBox2.Enabled = true;
            checkedListBox3.Enabled = true;
            progressBar1.Value = 50000;
        }//START SEARCH        
        private void button1_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(Delete));
            myThread.Start();
        }//START DELETE
        private void button3_Click(object sender, EventArgs e)
        {

            True();
        }//START TRUE
        private void button2_Click(object sender, EventArgs e)
        {
            False();
        }//START FALSE  
        private void button5_Click(object sender, EventArgs e)
        {
            True2();
        }//START TRUE2
        private void button6_Click(object sender, EventArgs e)
        {
            False2();
        }//START FALSE2
        private void True()
        {
            for (int i = 0; i <= j - 1; i++)
                checkedListBox2.SetItemChecked(i, true);
        }//TRUE
        private void False()
        {
            for (int i = 0; i <= j - 1; i++)
                checkedListBox2.SetItemChecked(i, false);
        }//FALSE
        private void True2()
        {
            for (int i = 0; i <= 60; i++)
                checkedListBox3.SetItemChecked(i, true);
        }//TRUE2
        private void False2()
        {
            for (int i = 0; i <= 60; i++)
                checkedListBox3.SetItemChecked(i, false);
        }//FALSE2
    }
}
