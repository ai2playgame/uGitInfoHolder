using System;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace UGitVersionHolder.Editor
{
    public static class GitCommandExecutor
    {
        public static bool CanExecute()
        {
            var result = ExecuteGitCommand("--version");
            if (result == null) return false;
            return !(string.IsNullOrEmpty(result.Stdout)) && result.IsNotError;
        }
        
        public class ExecutionResult
        {
            public readonly string Stdout;
            public readonly string Stderr;
            public readonly int ExitCode;

            public ExecutionResult(string stdout, string stderr, int exitCode)
            {
                Stdout = stdout;
                Stderr = stderr;
                ExitCode = exitCode;
            }
            public bool IsNotError => ExitCode == 0;
        }

        public static ExecutionResult ExecuteGitCommand(string arguments, float timeoutSec = 10.0f)
        {
            return ExecuteCommand($"git {arguments}", timeoutSec);
        }

        private static ExecutionResult ExecuteCommand(string arguments, float timeoutSec = 10.0f)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    return ExecuteCommandInWindows(arguments, timeoutSec);
                
                default:
                    throw new NotSupportedException($"this platform ({Application.platform}) is not supported.");
            }
        }

        private static ExecutionResult ExecuteCommandInWindows(string arguments, float timeoutSec = 10.0f)
        {
            using (var process = new System.Diagnostics.Process())
            {
                var cmdPath = System.Environment.GetEnvironmentVariable("ComSpec");
                if (string.IsNullOrEmpty(cmdPath))
                {
                    Debug.LogError("Couldn't find cmd.exe");
                    return null;
                }
                process.StartInfo.FileName = cmdPath;
                process.StartInfo.Arguments = $"/c {arguments}";
                
                // Prevent to show window
                process.StartInfo.CreateNoWindow = true;
                
                // Enable to read command's output
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                // process.StartInfo.RedirectStandardError = true; // TODO: 標準出力と標準エラーを同時に取得する方法を探る
                process.StartInfo.RedirectStandardInput = false;

                process.Start();
                
                // Read stdout and stderr
                string stdout = process.StandardOutput.ReadToEnd();

                if (!process.WaitForExit((int)(timeoutSec * 1000)))
                {
                    // タイムアウトにつきプロセスを強制終了する
                    Debug.LogError("timeout");
                    process.Kill();
                    process.WaitForExit();
                    return null;
                }
                
                process.Close();

                return new ExecutionResult(stdout, string.Empty, /*process.ExitCode*/0);
            }
        }
    }
}