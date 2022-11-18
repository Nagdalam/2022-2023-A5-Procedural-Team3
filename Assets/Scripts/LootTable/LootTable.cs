using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using System.ComponentModel;

[Serializable]
public class DataPickupDictionary : UDictionary<ELootID, SDataLootTable> { }

public class LootTable : MonoBehaviour
{
    [ReadOnly] public EEntities entity = EEntities.CHEST_1;
    /*public DataLootTable lootTable;*/
    private int totalLoots = 0;

    [UDictionary.Split(55, 45)]
    [ReadOnly] public DataPickupDictionary dicoLootTable;

    public class Key { [ReadOnly] public ELootID lootID; }
    public class Value { [ReadOnly] public SDataLootTable dataLootTable; }

    [ReadOnly] List<SDataLootTable> lootTable = new List<SDataLootTable>();

    private void Awake()
    {
        GenerateEntitiesIDAndLootTable.current.SetData(entity, dicoLootTable);
        lootTable = dicoLootTable.Values;
        totalLoots = dicoLootTable.Count;
    }

    void Start()
    {
        /*for (int i = 0; i < totalLoots; ++i)
            Debug.Log($"{i} : {lootTable[i].lootID} - {lootTable[i].lootDropRate}");*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            RandLoot();
        }*/
    }

    public Pickup RandLoot()
    {
        float totalWeight = lootTable.Sum(x => x.lootDropRate);
        float rand = UnityEngine.Random.Range(0, totalWeight);
        /*Debug.Log(totalWeight);
        Debug.Log(rand);*/
        float sumWeight = 0;
        int indexDrop = 0;
        float selectedDropRate = 0;

        // On sélectionne le premier objet tiré au sort en fonction du poids total
        for (int i = 0; i < totalLoots; ++i)
        {
            sumWeight += lootTable[i].lootDropRate;
            if (rand <= sumWeight)
            {
                indexDrop = i;
                selectedDropRate = lootTable[i].lootDropRate;
                break;
            }
        }

        // On cherche s'il existe d'autres objets ayant le même drop rate que l'objet sélectionné
        var occurrences = new List<SDataLootTable>();
        for (int i = 0; i < totalLoots; ++i)
        {
            if (lootTable[i].lootDropRate == selectedDropRate)
                occurrences.Add(lootTable[i]);
            /*else 
                break;*/
        }

        Debug.Log($"Selected : {lootTable[indexDrop].lootID} : {occurrences.Count}");
        Debug.Log(occurrences.Count());

        ELootID selectedLoot = lootTable[indexDrop].lootID;

        if (!(DungeonManager.current.dataPickup.dataPickupDictionary[selectedLoot]))
        {
            Debug.Log("No Prefab Associated with the Loot ID");
            return null;
        }

        Instantiate(DungeonManager.current.dataPickup.dataPickupDictionary[
            lootTable[indexDrop].lootID]);

        return null;
    }
}
