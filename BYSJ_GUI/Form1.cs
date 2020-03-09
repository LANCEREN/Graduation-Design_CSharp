using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProcessClass;

namespace BYSJ_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Picture";
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] strNames = openFileDialog.FileNames;
                
                //将文件名添加到 listbox 中
                for (int i = 0; i < strNames.Length; i++)
                {
                    Console.WriteLine(strNames[i]);
                    Console.WriteLine(strNames.Length);
                }
            }

            //TestProcess testProcess = new TestProcess();
            //testProcess.testRun();

            PwshProcess pwshProcess = new PwshProcess();
            pwshProcess.pwshRun();

            //ShellProcess shellProcess = new ShellProcess("cmd.exe");
            //shellProcess.shellScriptSampleAdd();
            //shellProcess.shellRun();

        }
    }
}
