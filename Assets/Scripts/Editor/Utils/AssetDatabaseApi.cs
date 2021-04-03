using UnityEditor;
using UnityEngine;

namespace Editor.Utils
{
    public static class AssetDatabaseApi
    {
        public static void SaveAsset<T>(T asset) where T : Object
        {
            string path = EditorUtility.SaveFilePanelInProject($"Save {typeof(T).Name}", asset.name, "asset",
                "Please enter a file name");
            if (path.Length != 0)
            {
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        public static bool LoadAsset<T>(out T result) where T : Object
        {
            var path = EditorUtility.OpenFilePanel("Select map to load", "Assets", "asset");
            if (path.Length > 0)
            {
                var relativepath = "Assets" + path.Substring(Application.dataPath.Length);
                var asset = AssetDatabase.LoadAssetAtPath<T>(relativepath);
                result = asset;
                return result;
            }

            result = default;
            return false;
        }
    }
}