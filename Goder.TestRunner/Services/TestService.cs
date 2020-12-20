using Goder.TestRunner.Models;
using Goder.TestRunner.Configuartions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goder.TestRunner.Services
{
    public class TestService
    {
        private ProcessOptions _options;

        public TestService(ProcessOptions options)
        {
            _options = options;
        }

        public void SaveScriptInFile(string directory, string script)
        {
            File.WriteAllText($"{directory}main.py", script);
        }

        public string StartTestInNewProcess(Test testData)
        {
            using Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory = _options.WorkingDirectory;
            p.StartInfo.Environment.Add(nameof(_options.PYTHONHOME), _options.PYTHONHOME);
            p.StartInfo.Environment.Add(nameof(_options.PYTHONPATH), _options.PYTHONPATH);
            p.StartInfo.FileName = _options.FileName;
            p.StartInfo.Arguments = "main.py";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;

            p.Start();

            using StreamWriter myStreamWriter = p.StandardInput;
            myStreamWriter.WriteLine(testData.Input);
            myStreamWriter.Close();

            p.WaitForExit();

            var result = p.StandardOutput.ReadToEnd();
            p.Close();

            return result;
        }

        public bool CompareResults(string expected, string current)
        {
            return expected.Replace("\r\n", "") == current.Replace("\r\n", "");
        }
    }
}
