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


[CreateAssetMenu(fileName = "Data Pickup", menuName = "ScriptableObjects/Pickup", order = 1)]

public class DataPickup : ScriptableObject
{
    [UDictionary.Split(55, 45)]
    public DataPickupDictionary dataPickup;
    [Serializable]
    public class DataPickupDictionary : UDictionary<ELootID, GameObject> { }

    public class Key
    {
        public ELootID lootID;
    }

    [Serializable]
    public class Value
    {
        public GameObject prefab;
    }

}