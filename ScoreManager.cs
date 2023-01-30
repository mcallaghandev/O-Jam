namespace
{
    using UnityEditor;
    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}