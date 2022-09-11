using UnityEditor;
using UnityEngine;

namespace UGitVersionHolder.Editor
{
    public static class GitUtilityMenu
    {
        [MenuItem("Git/GetGitHash")]
        private static void GetGitHash()
        {
            if (!GitCommandExecutor.CanExecute())
            {
                Debug.LogError("Cannot execute command");
                return;
            }
            
            var result = GitCommandExecutor.ExecuteGitCommand("rev-parse --short HEAD");
            if (result != null)
                Debug.Log(result.Stdout);
        }
    }
}
