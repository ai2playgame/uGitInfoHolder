namespace UGitVersionHolder.Editor
{
    public class GitVersion
    {
        public GitVersion(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; private set; }

        public static GitVersion Generate()
        {
            var revParseResult = GitCommandExecutor.ExecuteGitCommand("rev-parse --short HEAD");
            var hash = revParseResult.Stdout;

            return new GitVersion(hash);
        }

        public static GitVersion Invalid => new GitVersion("INVALID");
    }
}