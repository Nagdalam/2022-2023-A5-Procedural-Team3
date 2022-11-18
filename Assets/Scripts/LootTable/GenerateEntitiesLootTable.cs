#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Globalization;

public static class GenerateEntitiesLootTable
{
    static TextAsset csvFile = Resources.Load<TextAsset>("CSV/Entities_Dungeon_1");
    public static List<string> enumNames = new List<string>();

    // Read data from CSV file
    public static void ReadData()
    {
        string[] records = csvFile.text.Split('\n');
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

    public static void SetData(EEntities entity, DataPickupDictionary lootData)
    {
        string[] records = csvFile.text.Split('\n');
        string[] columnNames = records[0].Split(';');
        List<string> entityID = new List<string>();
        string field = "";
        for (int i = 1; i < columnNames.Length; ++i)
        {
            field = records[i].Split(';')[0];
            Debug.Log($"ID : {field}");
            /*entityID.Add(field);*/

            if (field == entity.ToString())
            {
                Debug.Log("Data Row Found");

                // Ajouter chaque Loot ID au dictionnaire
                string lootField = records[i].Split(';')[3];
                string dropRateField = records[i].Split(';')[4];

                string[] lootIDs = lootField.Split(',');
                string[] dropRateFields = dropRateField.Split(',');


                for (int j = 0; j < lootIDs.Length; ++j)
                {
                    SDataLootTable dataLootTable = new SDataLootTable();

                    ELootID lootID = (ELootID) System.Enum.Parse(typeof(ELootID), lootIDs[j]);

                    dataLootTable.lootID = lootID;
                    dataLootTable.lootName = "";
                    dataLootTable.lootDropRate = float.Parse(dropRateFields[j], CultureInfo.InvariantCulture.NumberFormat);

                    Debug.Log($"Loot ID : {lootIDs[j]} && Drop Rate {dataLootTable.lootDropRate}");

                    lootData.Add(lootID, dataLootTable);
                }

                break;
            }
        }
        Debug.Log(lootData.Count);
    }

    [MenuItem("Tools/Generate Entities")]
    public static void GenerateEntitiesEnum()
    {
        ReadData();
        string enumName = "EEntities";
        string filePathAndName = "Assets/Scripts/LootTable/Enums/EEntities.cs";

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