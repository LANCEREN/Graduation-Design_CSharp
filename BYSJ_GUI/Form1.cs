using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GDClasses;
using GDClasses.ProcessClasses;
using GDClasses.GDArgsClasses;
using GDClasses.GDExceptionClass;

namespace BYSJ_GUI
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            InitializeParam();
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
            GDProcessEventArgs gDProcessEventArgs;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.FileNames.Length == 0)
                    {
                        throw new MyException("There is no selected pictures!");
                    }
                }
                catch(MyException myException)
                {
                    MessageBox.Show(myException.Message);
                }

                Console.WriteLine($"You have selected {openFileDialog.FileNames.Length} pictures.");
                //将文件名添加到 listbox 中
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    filesNames.Add(openFileDialog.FileNames[i]);
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
            gDProcessEventArgs = new GDProcessEventArgs(
                filesNamesInput: filesNames,
                imageEnhanceStateInput:imageEnhanceCheckBox.Checked,
                outputCarInfoStateInput:OutputCarInfoCheckBox.Checked,
                initModeInput:initMode.inputByFiles.ToString()
                );
            #endregion
            #region 创建进程进行python处理
            GraduateDesignProcess graduate_DesignProcess = new GraduateDesignProcess(gDProcessEventArgsInput: gDProcessEventArgs);
            graduate_DesignProcess.GDprocessScript_ShowPicturesbyFiles();
            graduate_DesignProcess.ShellRun();
            #endregion
            try
            {
                this.plateLocatePictureBox.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\lpl\plateImg_general.jpg");
                this.opticalCutPictureBox.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\lpl\plateImg_precise.jpg");
                this.plateProvincePictureBox.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\0.jpg");
                this.plateLetterPictureBox.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\1.jpg");
                this.plateDigitPictureBox1.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\2.jpg");
                this.plateDigitPictureBox2.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\3.jpg");
                this.plateDigitPictureBox3.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\4.jpg");
                this.plateDigitPictureBox4.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\5.jpg");
                this.plateDigitPictureBox5.Image = Image.FromFile(@"c:\Users\Lance\Desktop\Graduation-Design_Py\output\ocr\6.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            GDProcessEventArgs gDProcessEventArgs;
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
            gDProcessEventArgs = new GDProcessEventArgs(
                folderPathInput:folderPath,
                imageEnhanceStateInput: imageEnhanceCheckBox.Checked,
                outputCarInfoStateInput: OutputCarInfoCheckBox.Checked,
                initModeInput: initMode.inputByFiles.ToString()
                );
            #endregion
            #region 创建进程进行python处理
            GraduateDesignProcess graduate_DesignProcess = new GraduateDesignProcess(gDProcessEventArgsInput: gDProcessEventArgs);
            graduate_DesignProcess.GDprocessScript_ShowPicturesbyFiles();
            graduate_DesignProcess.ShellRun();
            #endregion
        }

        protected void InitializeParam()
        {

        }
    }
}


