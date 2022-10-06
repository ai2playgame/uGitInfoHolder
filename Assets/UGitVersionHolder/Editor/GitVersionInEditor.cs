using System;
using System.Runtime.CompilerServices;
using UGitVersionHolder.Runtime;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public class GitVersionInEditor
    {
        private string _hash;
        private string _date;

        public static GitVersionInEditor Generate()
        {
            return new GitVersionInEditor()
            {
                _hash = GetHash(),
                _date = GetDate(),
            };
        }

        private static string GetHash()
        {
            var revParseResult = GitCommandExecutor.ExecuteGitCommand("rev-parse --short HEAD");
            var hash = revParseResult.Stdout;
            hash = hash.Replace("\n", "").Replace("\r", "");
            return hash;
        }

        private static string GetDate()
        {
            var dateResult =
                GitCommandExecutor.ExecuteGitCommand(
                    "log --date=iso --date=format:\"%m%d\" --pretty=format:\"%ad\" -1");
            var date = dateResult.Stdout;
            date = date.Replace("\n", "").Replace("\r", "");
            return date;
        }

        public GitVersionContent ConvertToRuntimeScriptableObject()
        {
            var versionAsset = ScriptableObject.CreateInstance<GitVersionContent>();
            versionAsset.hash = _hash;
            versionAsset.date = _date;

            return versionAsset;
        }

        public static GitVersionInEditor Invalid => new GitVersionInEditor()
        {
            _hash = "INVALID",
            _date = "9999",
        };
    }
}