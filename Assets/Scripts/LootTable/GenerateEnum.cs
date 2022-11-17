#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class GenerateEnum
{
    static TextAsset csvFile = Resources.Load<TextAsset>("CSV/Loot_Table_Dungeon_1");
    public static List<string> enumNames = new List<string>();

    // Read data from CSV file
    public static void ReadData()
    {
        string[] records = csvFile.text.Split('\n');
        Debug.Log(records.Length);

        string[] columnNames = records[0].Split(';');

        /*for (int i = 0; i < columnNames.Length; ++i)*/
            /*Debug.Log(columnNames[i]);*/
       
        for (int i = 1; i < columnNames.Length; ++i)
        {
            string[] fields = records[i].Split(';');
            /*Debug.Log($"ID : {fields[0]}");*/

            enumNames.Add(fields[0]);

           /* for (int j = 1; j < columnNames.Length; ++j)
            {
                *//*Debug.Log(fields[j]);*//*
            }*/
            /*Debug.Log("----------");*/
        }
    }

    [MenuItem("Tools/GenerateEnum")]
    public static void GenerateLootTableEnum()
    {
        ReadData();
        string enumName = "ELootTable";
        string filePathAndName = "Assets/Scripts/LootTable/Enums/ELootTable.cs";

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < enumNames.Count; i++)
            {
                streamWriter.WriteLine("\t" + enumNames[i] + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif