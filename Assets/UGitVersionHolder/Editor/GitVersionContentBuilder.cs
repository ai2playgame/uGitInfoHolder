using System.IO;
using UGitVersionHolder.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public static class GitVersionContentBuilder
    {
        private const string GitVersionContentRootDirectory = "Assets/UGitVersionHolder/Resources/";
        private const string VersionContentPath = GitVersionContentRootDirectory + GitVersionContent.AssetName;
        
        [MenuItem("Git/Generate Git version content resources")]
        private static void GenerateGitVersionContent()
        {
            var versionAsset = AssetDatabase.LoadAssetAtPath<GitVersionContent>(VersionContentPath);
            if (!versionAsset)
            {
                versionAsset = ScriptableObject.CreateInstance<GitVersionContent>();
            }

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
            if (!Directory.Exists(GitVersionContentRootDirectory))
                Directory.CreateDirectory(GitVersionContentRootDirectory);
            AssetDatabase.CreateAsset(versionAsset, VersionContentPath);
            
            AssetDatabase.SaveAssets();
        }
    }
}