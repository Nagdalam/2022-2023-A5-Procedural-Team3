using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataGeneralLoots : ScriptableObject
{
    private static DataGeneralLoots _currentGeneralGameData;

    public static DataGeneralLoots CurrentGeneralGameData
    {
        get
        {
            if (_currentGeneralGameData == null)
            {
                if (AssetDatabase.FindAssets("DataLootAll", new[] { "Assets/Scripts/LootTable/Data/" }).Length != 1)
                {
                    _currentGeneralGameData = CreateInstance<DataGeneralLoots>();

                    AssetDatabase.CreateAsset(_currentGeneralGameData, "Assets/Scripts/LootTable/Data/DataLootAll.asset");
                }
            }

            _currentGeneralGameData = AssetDatabase.LoadAssetAtPath<DataGeneralLoots>("Assets/Scripts/LootTable/Data/DataLootAll.asset");

            return _currentGeneralGameData;
        }
    }

    public List<DataLoot> dataLoots = new List<DataLoot>();

}
