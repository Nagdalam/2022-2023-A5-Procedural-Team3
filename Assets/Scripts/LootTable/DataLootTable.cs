using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Loot Table", menuName = "ScriptableObjects/LootTable", order = 1)]

public class DataLootTable : ScriptableObject 
{
    public List<SDataLootTable> data;
}
