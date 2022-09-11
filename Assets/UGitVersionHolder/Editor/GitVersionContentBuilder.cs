using System.IO;
using UGitVersionHolder.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public static class GitVersionContentBuilder
    {
        private const string GitVersionContentRootDirectory = "Assets/UGitVersionHolder/";
        private const string GitVersionContentDirectory = GitVersionContentRootDirectory + "Resources/";
        private const string VersionContentPath = GitVersionContentDirectory + GitVersionContent.AssetName;
        
        [MenuItem("Git/Generate Git version content resources")]
        public static void GenerateGitVersionContent()
        {
            GenerateGitignore();

            var versionAsset = ScriptableObject.CreateInstance<GitVersionContent>();

            GitVersion gitVersion = GitVersion.Invalid;
            if (GitCommandExecutor.CanExecute())
            {
                gitVersion = GitVersion.Generate();
            }

            // コマンド実行結果から、ScriptableObjectに値を代入する
            versionAsset.hash = gitVersion.Hash;
            
            // 古いScriptableObjectアセットが残っていたら削除
            AssetDatabase.DeleteAsset(VersionContentPath);

            // ScriptableObjectをアセットとしてResources以下に保存する
            if (!Directory.Exists(GitVersionContentDirectory))
                Directory.CreateDirectory(GitVersionContentDirectory);
            AssetDatabase.CreateAsset(versionAsset, VersionContentPath);
            
            AssetDatabase.SaveAssets();
        }

        private static void GenerateGitignore()
        {
            if (!File.Exists(GitVersionContentRootDirectory + ".gitignore"))
            {
                Debug.Log($"Create .gitignore");
            }

            using (var file = File.CreateText(GitVersionContentRootDirectory + ".gitignore"))
            {
                file.WriteLine("# Generate by uGitVersionFolder");
                file.WriteLine(".gitignore");
                file.WriteLine("Resources/*");
                file.WriteLine("Resources.meta");
            }
        }
    }
}