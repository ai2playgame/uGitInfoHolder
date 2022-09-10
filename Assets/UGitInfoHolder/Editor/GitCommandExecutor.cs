namespace UGitInfoHolder.Editor
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
            public string Stdout;
            public string Stderr;
            public readonly int ExitCode;

            public ExecutionResult(string stdout, string stderr, int exitCode)
            {
                Stdout = stdout;
                Stderr = stderr;
                ExitCode = exitCode;
            }
            public bool IsNotError => ExitCode != 0;
        }

        private static ExecutionResult ExecuteGitCommand(string arguments, float timeoutSec = 10.0f) => null;
        private static ExecutionResult ExecuteCommand(string arguments, float timeoutSec = 10.0f) => null;
    }
}