using UnityEngine;

namespace UGitVersionHolder.Runtime
{
    public static class GitVersion
    {
        private static GitVersionContent _content;

        public static string GetHash()
        {
            if (_content == null) InitializeLoad();

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

        public static string GetDate()
        {
            if (_content == null) InitializeLoad();

            if (_content != null)
            {
                return _content.date;
            }
            else
            {
                return "XXXX";
            }
        }
        
        private static void InitializeLoad()
        {
            _content = Resources.Load<GitVersionContent>(GitVersionContent.AssetName);
        }
    }
}