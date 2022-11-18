#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine.Events;

public class GenerateEntitiesIDAndLootTable : MonoBehaviour
{
    public TextAsset csvFile = null;
    private List<string> enumNames = new List<string>();
    private int nbRows = 0;

    public static GenerateEntitiesIDAndLootTable current;

    private void Awake()
    {
        current = this;
    }

    public void  SetData(EEntities entity, DataPickupDictionary lootData)
    {
        /*csvFile = Resources.Load<TextAsset>(pathCSVFile);*/
        string[] records = csvFile.text.Split('\n');
        string[] columnNames = records[0].Split(';');

        for (int i = 1; i < columnNames.Length; ++i)
        {
            // On cherche la loot table associée à notre entité depuis la colonne 0 (= ID field)
            string field = records[i].Split(';')[0]; // ID field

            if (field == entity.ToString())
            {
                /*Debug.Log("Data Row Found");*/

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

                    /*Debug.Log($"Loot ID : {lootIDs[j]} && Drop Rate {dataLootTable.lootDropRate}");*/

                    lootData.Add(lootID, dataLootTable);
                }

                break;
            }
        }
    }

    [MenuItem("Tools/Generate All Entities ID")]
    public  void GenerateEntitiesEnum()
    {
        /*csvFile = Resources.Load<TextAsset>(pathCSVFile);*/
        enumNames.Clear();
        string[] records = csvFile.text.Split('\n');
        nbRows = records.Length;

        // Pour chaque ligne de donnée sauf la 1ère (= titre des colonnes)
        for (int i = 1; i < nbRows - 1; ++i)
            enumNames.Add(records[i].Split(';')[0]); // entity ID Field

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