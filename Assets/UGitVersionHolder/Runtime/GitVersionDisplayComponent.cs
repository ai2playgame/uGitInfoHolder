using UnityEngine;

namespace UGitVersionHolder.Runtime
{
    public class GitVersionDisplayComponent : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(new Rect(30, 30, 400, 100), $"Ver. {GitVersion.GetHash()}");
        }
    }
}
