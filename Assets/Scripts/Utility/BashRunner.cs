#region

using System;
using System.Diagnostics;
using System.Text;

#endregion

namespace Utility
{
    public static class BashRunner
    {
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static string Run(string commandLine)
        {
            var errorBuilder = new StringBuilder();
            var outputBuilder = new StringBuilder();
            var arguments = $"-c \"{commandLine}\"";
            using (var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            })
            {
                process.Start();
                process.OutputDataReceived += (_, args) => { outputBuilder.AppendLine(args.Data); };
                process.BeginOutputReadLine();
                process.ErrorDataReceived += (_, args) => { errorBuilder.AppendLine(args.Data); };
                process.BeginErrorReadLine();
                if (!process.WaitForExit(2000))
                {
                    var timeoutError = $@"Process timed out. Command line: bash {arguments}.
Output: {outputBuilder}
Error: {errorBuilder}";
                    throw new Exception(timeoutError);
                }

                if (process.ExitCode == 0) return outputBuilder.ToString();

                var error = $@"Could not execute process. Command line: bash {arguments}.
Output: {outputBuilder}
Error: {errorBuilder}";
                throw new Exception(error);
            }
        }
    }
}