using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;


namespace ProcessClassesNamespace
{
    public class ShellProcess
    {
        public Process process;
        public List<string> ShellScriptInput;

         public ShellProcess()
        {
            ProcessSetting();
        }

        private void ProcessSetting()
        {
            ShellScriptInput = new List<string>();
            process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "cmd.exe";
        }

        public void shellScriptAdd(string script)
        {
            ShellScriptInput.Add(script);
        }

        public void shellRun()
        {
            shellScriptAdd("exit");
            try
            {
                process.Start();
                process.StandardInput.AutoFlush = true;
                foreach (string shell_StringLine in ShellScriptInput)
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

    public class Graduate_DesignProcess : CmdProcess
    {
        protected List<string> filesNames;
        protected string folderPath;

        public Graduate_DesignProcess(List<string> fileNamesInput) : base()
        {
            filesNames = fileNamesInput;
        }
        public Graduate_DesignProcess(string folderPathInput) : base()
        {
            folderPath = folderPathInput;
        }

        public void GD_processScript_showPicturesbyFiles()
        {
            if (filesNames.Count == 0)
            {
                Console.WriteLine("There is no selected pictures! ");
                return;
            }
            shellScriptAdd("conda activate opencv");
            foreach (string selectedPicture in filesNames)
            {
                shellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --file {selectedPicture}");
            }
        }

        public void GD_processScript_showPicturesbyFolder()
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("There is no Path!");
                return;
            }
            shellScriptAdd("conda activate opencv");
            //shellScriptAdd($@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --folder {folderPath}");
        }

    }

    public class TestPowershellProcess
    {
        Runspace runspace;
        PowerShell powerShell;
        public TestPowershellProcess()
        {
            runspace = RunspaceFactory.CreateRunspace();
            powerShell = PowerShell.Create();
        }
        public void TestPowershellRun()
        {
            runspace.Open();
            powerShell.Runspace = runspace;
            powerShell.AddScript("get-childitem -Force");

            foreach (PSObject result in powerShell.Invoke())
            {
                Console.WriteLine(result);
            }
        }
    }

}

