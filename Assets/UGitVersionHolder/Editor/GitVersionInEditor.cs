using System;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public class GitVersionInEditor
    {
        public string Hash;
        public string Date;

        public static GitVersionInEditor Generate()
        {
            var revParseResult = GitCommandExecutor.ExecuteGitCommand("rev-parse --short HEAD");
            var hash = revParseResult.Stdout;

            var dateResult =
                GitCommandExecutor.ExecuteGitCommand(
                    "log --date=iso --date=format:\"%m%d\" --pretty=format:\"%ad\" -1");
            var date = dateResult.Stdout;
            
            Debug.Log($"{hash} | {date}");

            return new GitVersionInEditor()
            {
                Hash = hash.Replace(Environment.NewLine, ""),
                Date = date.Replace(Environment.NewLine, "")
            };
        }

        public static GitVersionInEditor Invalid => new GitVersionInEditor()
        {
            Hash = "INVALID",
            Date = "9999",
        };
    }
}