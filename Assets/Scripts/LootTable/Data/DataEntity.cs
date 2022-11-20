using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "Data Entity", menuName = "ScriptableObjects/Data Entity", order = 1)]

public class DataEntity : ScriptableObject
{
    // Fields
    public GameObject entity; // Prefab

    [UDictionary.Split(75, 25)]
    public LootDictionary lootTable; // Loot Table associated to the prefab

    [Serializable]
    public class LootDictionary : UDictionary<DataLoot, int> { }
    public class Key { public DataLoot loot; } // Loot Prefab to spawn
    public class Value { public int lootDropRate; } // Rate associated to the Loot Prefab
}