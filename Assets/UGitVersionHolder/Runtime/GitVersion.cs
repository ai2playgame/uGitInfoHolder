using UnityEngine;

namespace UGitVersionHolder.Runtime
{
    public static class GitVersion
    {
        private static GitVersionContent _content;

        public static string GetHash()
        {
            if (_content == null)
            {
                Initialize();
            }

            if (_content != null)
            {
                return _content.hash;
            }
            else
            {
#if UNITY_EDITOR
                return "(IN EDITOR)";
#else
                return "(NULL)";
#endif
            }
        }
        
        private static void Initialize()
        {
            _content = Resources.Load<GitVersionContent>(GitVersionContent.AssetName);
        }
    }
}