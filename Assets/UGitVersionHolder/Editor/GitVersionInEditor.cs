namespace UGitVersionHolder.Editor
{
    public class GitVersionInEditor
    {
        public GitVersionInEditor(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; private set; }

        public static GitVersionInEditor Generate()
        {
            var revParseResult = GitCommandExecutor.ExecuteGitCommand("rev-parse --short HEAD");
            var hash = revParseResult.Stdout;

            return new GitVersionInEditor(hash);
        }

        public static GitVersionInEditor Invalid => new GitVersionInEditor("INVALID");
    }
}