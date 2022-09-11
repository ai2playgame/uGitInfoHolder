using UnityEngine;

namespace UGitVersionHolder.Runtime
{
    public static class GitVersion
    {
        private static GitVersionContent _content = null;

        public static string GetHash()
        {
            if (_content == null)
                Initialize();

            return _content.hash;
        }
        
        private static void Initialize()
        {
            _content = Resources.Load<GitVersionContent>(GitVersionContent.AssetName);
        }
    }
}