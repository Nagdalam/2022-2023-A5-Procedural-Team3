using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Tools/Generate Scriptable Object")]
    public static void CreateMyAsset()
    {
        DataLootTable asset = ScriptableObject.CreateInstance<DataLootTable>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/LootTable/Test.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}