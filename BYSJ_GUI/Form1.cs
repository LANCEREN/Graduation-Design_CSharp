using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProcessClasses;

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
            #region 初始化
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Pictures";
            openFileDialog.Filter = "Picture files(*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*bmp;*.png| All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            #endregion
            #region Form处理
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
                    Console.WriteLine($"You have selected {openFileDialog.FileNames.Length} pictures.");
                    //将文件名添加到 listbox 中
                    for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                    {
                        filesNames.Add(openFileDialog.FileNames[i]);
                        Console.WriteLine(openFileDialog.FileNames[i]);
                        try
                        {
                            this.inputPictureBox.Image = Image.FromFile(openFileDialog.FileNames[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                    }
                }
            }
            #endregion
            #region 创建进程进行python处理
            GraduateDesignProcess graduate_DesignProcess = new GraduateDesignProcess(fileNamesInput: filesNames);
            graduate_DesignProcess.GDprocessScript_ShowPicturesbyFiles();
            graduate_DesignProcess.ShellRun();
            #endregion
        }

        private void BrowseFoldersButton_Click(object sender, EventArgs e)
        {
            #region 初始化
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please select picture folders.";
            folderBrowserDialog.ShowNewFolderButton = true;
            #endregion
            #region Form处理
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
            #endregion
            #region 创建进程进行python处理
            GraduateDesignProcess graduate_DesignProcess = new GraduateDesignProcess(folderPathInput: folderPath);
            graduate_DesignProcess.GDprocessScript_ShowPicturesbyFiles();
            graduate_DesignProcess.ShellRun();
            #endregion
        }
    }
}


