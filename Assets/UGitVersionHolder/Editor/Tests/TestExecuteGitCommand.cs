using NUnit.Framework;

namespace UGitVersionHolder.Editor.Tests
{
    public class TestExecuteGitCommand
    {
        [Test]
        public void ExecuteGitVersionStdoutTest()
        {
            var result = GitCommandExecutor.ExecuteGitCommand("--version");
            Assert.That(!string.IsNullOrEmpty(result.Stdout));
        }
        [Test]
        public void ExecuteGitVersionStderrTest()
        {
            var result = GitCommandExecutor.ExecuteGitCommand("--version");
            Assert.That(string.IsNullOrEmpty(result.Stderr));
        }

        [Test]
        public void ExecuteGitVersionExitCodeTest()
        {
            var result = GitCommandExecutor.ExecuteGitCommand("--version");
            Assert.That(result.ExitCode == 0);
        }

        [Test]
        public void CanExecuteGitCommandTest()
        {
            Assert.That(GitCommandExecutor.CanExecute());
        }
    }
}