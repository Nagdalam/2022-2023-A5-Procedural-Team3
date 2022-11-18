using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Tools/Generate Scriptable Object")]
    public static void CreateMyAsset()
    {
        DataLootTable asset = ScriptableObject.CreateInstance<DataLootTable>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/LootTable/D1 Monster 1.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}