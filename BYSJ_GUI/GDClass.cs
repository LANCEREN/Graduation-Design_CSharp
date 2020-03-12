using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Windows.Forms;

namespace GDClasses
{
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
            protected GDProcessEventArgs gDProcessEventArgs;

            /// <summary>
            /// 通过图片文件绝对路径进行Process_in_python构造
            /// </summary>
            /// <param name="gDProcessEventArgsInput">Process参数类</param>
            public GraduateDesignProcess(GDProcessEventArgs gDProcessEventArgsInput) : base()
            {
                gDProcessEventArgs = gDProcessEventArgsInput;
            }

            /// <summary>
            /// 通过图片文件绝对路径进行Process_in_python构造
            /// </summary>
            public void GDprocessScript_ShowPicturesbyFiles()
            {
                if (gDProcessEventArgs.filesNames.Count == 0)
                {
                    Console.WriteLine("There is no selected pictures! ");
                    return;
                }
                ShellScriptAdd("conda activate opencv");
                foreach (string selectedPicture in gDProcessEventArgs.filesNames)
                {
                    ShellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --file {selectedPicture}");
                }
            }
            /// <summary>
            /// 通过图片文件夹绝对路径进行Process_in_python
            /// </summary>
            public void GDprocessScript_ShowPicturesbyFolder()
            {
                if (string.IsNullOrEmpty(gDProcessEventArgs.folderPath))
                {
                    Console.WriteLine("There is no Path!");
                    return;
                }
                ShellScriptAdd("conda activate opencv");
                ShellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --folder {gDProcessEventArgs.folderPath}");
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
    public class GDProcessEventArgs
    {
        public bool imageEnhanceState;
        public bool outputCarInfoState;
        public string folderPath;
        public List<string> filesNames;
        public string initMode;

        /// <summary>
        /// 通过图片文件夹绝对路径进行构造
        /// </summary>
        /// <param name="imageEnhanceStateInput">是否开启图像增强</param>
        /// <param name="folderPathInput">图片文件夹绝对路径</param>
        public GDProcessEventArgs
            (bool imageEnhanceStateInput = false,
            bool outputCarInfoStateInput =true,
            string folderPathInput = "",
            string initModeInput =""
            )
        {
            imageEnhanceState = imageEnhanceStateInput;
            outputCarInfoState = outputCarInfoStateInput;
            folderPath = folderPathInput;
            initMode = initModeInput;
            filesNames = new List<string>();
        }
        /// <summary>
        /// 通过图片文件绝对路径进行构造
        /// </summary>
        /// <param name="imageEnhanceStateInput">是否开启图像增强</param>
        /// <param name="filesNamesInput">图片文件绝对路径的List</param>
        public GDProcessEventArgs
            (List<string> filesNamesInput, 
            bool imageEnhanceStateInput = false,
            bool outputCarInfoStateInput = true,
            string folderPathInput = "",
            string initModeInput = ""
            )
        {
            imageEnhanceState = imageEnhanceStateInput;
            outputCarInfoState = outputCarInfoStateInput;
            folderPath = folderPathInput;
            initMode = initModeInput;
            filesNames = filesNamesInput;
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

