using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProcessClass
{
    public class CmdProcess
    {
        Process process = new Process();
        List<string> CmdScriptInput = new List<string>();

        public CmdProcess()
        {
            Console.WriteLine("Create a Cmd Process.");
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
           
        }
        public void cmdScriptAdd(string script)
        {
            CmdScriptInput.Add(script);
            CmdScriptInput.Add(" & ");
        }

        public void cmdScriptSampleAdd()
        {
            cmdScriptAdd("conda activate opencv");
            cmdScriptAdd(@"python C:\Users\Lance\source\repos\BYSJ\BYSJ_GUI\test.py --input C:\Users\Lance\Desktop\2.JPG");
        }

        public void cmdRun()
        {
            CmdScriptInput.Add("exit");
            try
            {
                process.Start();
                foreach (string CmdStr in CmdScriptInput)
                {
                    Console.WriteLine(CmdStr);
                    process.StandardInput.WriteLine(CmdStr);
                }
                process.StandardInput.AutoFlush = true;
                string strOuput = process.StandardOutput.ReadToEnd();
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

