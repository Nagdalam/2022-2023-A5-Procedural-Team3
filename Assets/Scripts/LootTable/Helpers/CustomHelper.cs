using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateEntitiesIDAndLootTable))]
public class CustomHelper : Editor
{
   /* public override void OnInspectorGUI()
    {
        GenerateEntitiesIDAndLootTable generateEandLT = (GenerateEntitiesIDAndLootTable)target;
        if (GUILayout.Button("Generate Entities ID And Loot Table"))
        {
            generateEandLT.GenerateEntitiesEnum();
            AssetDatabase.Refresh();
        }
        DrawDefaultInspector();
    }*/
}

[CustomEditor(typeof(GenerateLootID))]
public class CustomGenerateLootID : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateLootID generateLootID = (GenerateLootID)target;
        if (GUILayout.Button("Generate Loot ID"))
        {
            generateLootID.GenerateLootTableEnum();
            AssetDatabase.Refresh();
        }
        DrawDefaultInspector();
    }
}