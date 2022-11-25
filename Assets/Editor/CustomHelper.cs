using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.ComponentModel;

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

[CustomEditor(typeof(GenerateLoot))]
public class CustomGenerateLootID : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    GenerateLoot generateLootID = (GenerateLoot)target;
    //    if (GUILayout.Button("Generate Loot ID"))
    //    {
    //        generateLootID.GenerateLootTableEnum();
    //        AssetDatabase.Refresh();
    //    }
    //    DrawDefaultInspector();
    //}
}