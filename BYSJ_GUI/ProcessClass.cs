using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Windows.Forms;


namespace ProcessClasses
{
    public class ShellProcess
    {
        public Process process;
        public List<string> shellScriptInput;

        public ShellProcess()
        {
            ProcessSetting();
        }

        private void ProcessSetting()
        {
            shellScriptInput = new List<string>();
            process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "cmd.exe";
        }
        /// <summary>
        /// 添加shellScript脚本命令
        /// </summary>
        public void ShellScriptAdd(string script)
        {
            shellScriptInput.Add(script);
        }
        /// <summary>
        /// 通过shellScript执行脚本
        /// </summary>
        public void ShellRun()
        {
            ShellScriptAdd("exit");
            try
            {
                process.Start();
                process.StandardInput.AutoFlush = true;
                foreach (string shell_StringLine in shellScriptInput)
                {
                    process.StandardInput.WriteLine(shell_StringLine);
                }
                process.StandardInput.Close();
                string strOuput = process.StandardOutput.ReadToEnd();
                process.StandardOutput.Close();

                process.WaitForExit();
                process.Close();
                Console.WriteLine(strOuput);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n跟踪;" + ex.StackTrace);
            }
        }

    }

    public class CmdProcess : ShellProcess
    {
        public CmdProcess() : base()
        {
            CmdProcessSetting();
        }

        private void CmdProcessSetting()
        {
            process.StartInfo.FileName = "cmd.exe";
            Console.WriteLine("Create a cmd.exe ");
        }

    }

    public class GraduateDesignProcess : CmdProcess, GraduateDesignInferface.IGDprocessInterface
    {
        protected List<string> filesNames;
        protected string folderPath;

        /// <summary>
        /// 通过图片文件绝对路径进行Process_in_python构造
        /// </summary>
        /// <param name="fileNamesInput">图片文件绝对路径的List</param>
        public GraduateDesignProcess(List<string> fileNamesInput) : base()
        {
            filesNames = fileNamesInput;
        }
        /// <summary>
        /// 通过图片文件夹绝对路径进行Process_in_python构造
        /// </summary>
        /// <param name="folderPathInput">图片文件夹绝对路径</param>
        public GraduateDesignProcess(string folderPathInput) : base()
        {
            folderPath = folderPathInput;
        }

        /// <summary>
        /// 通过图片文件绝对路径进行Process_in_python构造
        /// </summary>
        public void GDprocessScript_ShowPicturesbyFiles()
        {
            if (filesNames.Count == 0)
            {
                Console.WriteLine("There is no selected pictures! ");
                return;
            }
            ShellScriptAdd("conda activate opencv");
            foreach (string selectedPicture in filesNames)
            {
                ShellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --file {selectedPicture}");
            }
        }
        /// <summary>
        /// 通过图片文件夹绝对路径进行Process_in_python
        /// </summary>
        public void GDprocessScript_ShowPicturesbyFolder()
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("There is no Path!");
                return;
            }
            ShellScriptAdd("conda activate opencv");
            ShellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --folder {folderPath}");
        }

    }

    public class TestPowershellProcess
    {
        Runspace runSpace;
        PowerShell powerShell;
        public TestPowershellProcess()
        {
            runSpace = RunspaceFactory.CreateRunspace();
            powerShell = PowerShell.Create();
        }
        public void TestPowershellRun()
        {
            runSpace.Open();
            powerShell.Runspace = runSpace;
            powerShell.AddScript("get-childitem -Force");

            foreach (PSObject result in powerShell.Invoke())
            {
                Console.WriteLine(result);
            }
        }
    }

}

namespace GraduateDesignInferface
{
    interface IGDprocessInterface
    {
        void GDprocessScript_ShowPicturesbyFiles();
        void GDprocessScript_ShowPicturesbyFolder();
    }
}

