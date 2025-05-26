using UnityEditor;

namespace Editor.Mono
{
    public static class ScriptTemplates
    {
        public const string ScriptTemplatePath = "Assets/_Game/Editor/ScriptTemplates/Mono/";

        [MenuItem("Assets/Create/Mono/MonoBehaviour", priority = -1000)]
        public static void CreateMonoBehaviourScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"{ScriptTemplatePath}MonoBehaviourTemplate.txt", "MonoBehaviour.cs");
        }

        [MenuItem("Assets/Create/Mono/ScriptableObject", priority = -999)]
        public static void CreateScriptableObjectScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile($"{ScriptTemplatePath}ScriptableObjectTemplate.txt", "ScriptableObject.cs");
        }
    }
}
