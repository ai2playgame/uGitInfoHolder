using System.IO;
using UGitVersionHolder.Runtime;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public class GitVersionContentBuilder : IPreprocessBuildWithReport
    {
        private const string GitVersionContentRootDirectory = "Assets/UGitVersionHolder/";
        private const string GitVersionContentDirectory = GitVersionContentRootDirectory + "Resources/";
        private const string VersionContentPath = GitVersionContentDirectory + GitVersionContent.AssetName + ".asset";
        
        [MenuItem("Git/Update GitVersion Content")]
        private static void GenerateGitVersionContent()
        {
            GenerateGitignore();

            GitVersionInEditor gitVersionInEditor = GitVersionInEditor.Invalid;
            if (GitCommandExecutor.CanExecute())
            {
                gitVersionInEditor = GitVersionInEditor.Generate();
            }

            // コマンド実行結果から、RuntimeでアクセスできるScriptableObjectを生成する
            var versionAsset = gitVersionInEditor.ConvertToRuntimeScriptableObject();
            
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

        public int callbackOrder => 0;
        public void OnPreprocessBuild(BuildReport report)
        {
            GenerateGitVersionContent();
        }
    }
}