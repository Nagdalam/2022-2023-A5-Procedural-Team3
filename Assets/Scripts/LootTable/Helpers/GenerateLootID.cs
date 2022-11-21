#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class GenerateLootID : MonoBehaviour
{
    public TextAsset csvFile = null;
    private List<string> enumNames = new List<string>();
    private int nbRows = 0;

    /*[ReadOnly] public List<DataLoot> dataLoots = new List<DataLoot>();*/

    public static GenerateLootID current;

    private void Awake()
    {
        current = this;
    }

    public void SetData()
    {
        /*dataLoots = new List<DataLoot>();*/

        string[] records = csvFile.text.Split('\n');
        nbRows = records.Length;
        Debug.Log(nbRows);
        for (int i = 1; i < nbRows - 1; ++i)
        {
            // On cherche la loot table associée à notre entité depuis la colonne 0 (= ID field)
            string lootIDField = records[i].Split(';')[0]; 
            string prefabPathField = records[i].Split(';')[1];
            string lootNameField = records[i].Split(';')[2];
            string lootRarityField = records[i].Split(';')[3];

            string path = "Assets/Scripts/LootTable/Loot/L_" + lootIDField + ".asset";

            if (!File.Exists(path))
            {
                DataLoot newLoot = ScriptableObject.CreateInstance<DataLoot>();

                // path has to start at "Assets"
                AssetDatabase.CreateAsset(newLoot, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.FocusProjectWindow();
                Selection.activeObject = newLoot;
            }

            var loot = (DataLoot) AssetDatabase.LoadAssetAtPath(path, typeof(DataLoot));

            /*if (current.dataLoots.Contains(loot))
            {
                current.dataLoots.Add(loot);
            }*/

            var prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPathField, typeof(GameObject));
            loot.loot = prefab;
            loot.lootData.lootName = lootNameField;
            loot.lootData.lootRarity = (ERarity)System.Enum.Parse(typeof(ERarity), lootRarityField);
        }
    }

    [MenuItem("Tools/Generate All Loot ID")]
    public void GenerateLootTableEnum()
    {
        enumNames.Clear();
        string[] records = csvFile.text.Split('\n');
        nbRows = records.Length;

        // Pour chaque ligne de donnée sauf la 1ère (= titre des colonnes)
        for (int i = 1; i < nbRows - 1; ++i)
            enumNames.Add(records[i].Split(';')[0]); // entity ID Field

        string enumName = "ELootID";
        string filePathAndName = "Assets/Scripts/LootTable/Enums/ELootID.cs";

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < enumNames.Count; i++)
            {
                /*if (enumNames[i] == "")
                    continue;*/

                streamWriter.WriteLine("\t" + enumNames[i] + ",");
            }
            streamWriter.WriteLine("}");
        }

        AssetDatabase.Refresh();

        SetData();
    }
}
#endif