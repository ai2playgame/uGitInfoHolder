using System;
using System.Runtime.CompilerServices;
using UGitVersionHolder.Runtime;
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
            hash = hash.Replace("\n", "").Replace("\r", "");

            var dateResult =
                GitCommandExecutor.ExecuteGitCommand(
                    "log --date=iso --date=format:\"%m%d\" --pretty=format:\"%ad\" -1");
            var date = dateResult.Stdout;
            date = date.Replace("\n", "").Replace("\r", "");
            
            return new GitVersionInEditor()
            {
                Hash = hash,
                Date = date,
            };
        }

        public GitVersionContent ConvertToRuntimeScriptableObject()
        {
            var versionAsset = ScriptableObject.CreateInstance<GitVersionContent>();
            versionAsset.hash = Hash;
            versionAsset.date = Date;

            return versionAsset;
        }

        public static GitVersionInEditor Invalid => new GitVersionInEditor()
        {
            Hash = "INVALID",
            Date = "9999",
        };
    }
}