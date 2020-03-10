using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProcessClassesNamespace;

namespace BYSJ_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BrowseFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Pictures";
            openFileDialog.Filter = "Picture files(*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*bmp;*.png| All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<string> filesNames = new List<string>();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if(openFileDialog.FileNames.Length == 0)
                {
                    MessageBox.Show("文件不能为空", "提示");
                    return;
                }
                else
                {
                    string[] filesNamesArray = openFileDialog.FileNames;
                    Console.WriteLine($"You have selected {filesNamesArray.Length} pictures.");
                    //将文件名添加到 listbox 中
                    for (int i = 0; i < filesNamesArray.Length; i++)
                    {
                        filesNames.Add(filesNamesArray[i]);
                        Console.WriteLine(filesNamesArray[i]);
                    }
                }
            }
            Graduate_DesignProcess graduate_DesignProcess = new Graduate_DesignProcess(fileNamesInput: filesNames);
            graduate_DesignProcess.GD_processScript_showPicturesbyFiles();
            graduate_DesignProcess.shellRun();
        }

        private void BrowseFoldersButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please select picture folders.";
            folderBrowserDialog.ShowNewFolderButton = true;

            string folderPath = null;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    folderPath = folderBrowserDialog.SelectedPath;
                }
            }
            Graduate_DesignProcess graduate_DesignProcess = new Graduate_DesignProcess(folderPathInput: folderPath);
            graduate_DesignProcess.GD_processScript_showPicturesbyFiles();
            graduate_DesignProcess.shellRun();

        }
    }
}


