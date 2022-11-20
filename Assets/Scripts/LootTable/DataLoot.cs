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


[CreateAssetMenu(fileName = "L_", menuName = "ScriptableObjects/Data Loot", order = 1)]
public class DataLoot : ScriptableObject
{
    public DataLoot(GameObject loot, SLootData lootData)
    {
        this.loot = loot;
        this.lootData = lootData;
    }

    // Fields
    public GameObject loot;
    public SLootData lootData;
}