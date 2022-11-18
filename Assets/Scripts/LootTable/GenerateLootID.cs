#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class GenerateLootID : MonoBehaviour
{
    public TextAsset csvFile = null;
    private List<string> enumNames = new List<string>();
    private int nbRows = 0;

    public static GenerateLootID current;

    private void Awake()
    {
        current = this;
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
                streamWriter.WriteLine("\t" + enumNames[i] + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif