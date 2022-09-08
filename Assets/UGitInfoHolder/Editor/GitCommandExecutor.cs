namespace UGitInfoHolder.Editor
{
    public static class GitCommandExecutor
    {
        public static bool CanExecute() => false;
        public static string ExecuteGitCommand(string arguments) => string.Empty;

        private class ExecutionResult
        {
            public string Stdout;
            public string Stderr;
            public int ExitCode;

            public ExecutionResult(string stdout, string stderr, int exitCode)
            {
                Stdout = stdout;
                Stderr = stderr;
                ExitCode = exitCode;
            }
            public bool IsSuccess => ExitCode != 0;
        }

        private static ExecutionResult ExecuteCommand(string arguments) => null;
    }
}