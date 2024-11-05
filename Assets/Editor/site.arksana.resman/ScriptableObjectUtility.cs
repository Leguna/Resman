using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.site.arksana.resman
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        /// Creates a new ScriptableObject instance of a specific type in a predefined folder.
        /// </summary>
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            // Define the directory path
            string directoryPath = "Assets/_MainAssets/ScriptableDataObject/";

            // Check if the directory exists, and create it if it doesn’t
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Generate a unique asset path to avoid overwriting existing files
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(directoryPath + typeof(T).Name + ".asset");

            // Create the asset
            AssetDatabase.CreateAsset(asset, assetPathAndName);

            // Save the asset and refresh the AssetDatabase to show the new file
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Highlight the created asset in the Project window
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}