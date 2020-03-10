using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;


namespace ProcessClass
{
    public class TestProcess
    {
        Process process;
        public TestProcess()
        {
           
        }
        public void testRun()
        {
            
        }
    }

    public class PwshProcess
    {
        Runspace runspace;
        PowerShell powerShell;

        public PwshProcess()
        {
            runspace = RunspaceFactory.CreateRunspace();
            powerShell = PowerShell.Create();
        }
        public void pwshRun()
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

    public class ShellProcess
    {
        Process process;
        List<string> ShellScriptInput;

        public ShellProcess(string shellname)
        {
            process = new Process();
            ShellScriptInput = new List<string>();
            Console.WriteLine($"Create a {shellname} Process.");
            process.StartInfo.FileName = shellname;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
           
        }
        public void shellScriptAdd(string script)
        {
            ShellScriptInput.Add(script);
        }

        public void shellScriptSampleAdd()
        {
            shellScriptAdd("conda activate opencv");
            shellScriptAdd(@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --input C:\Users\Lance\Desktop\2.JPG");
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
}

