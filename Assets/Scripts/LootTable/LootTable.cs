using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

[Serializable]
public class DataPickupDictionary : UDictionary<ELootID, SDataLootTable> { }

public class LootTable : MonoBehaviour
{
    public EEntities entity = EEntities.CHEST_1;
    public DataLootTable lootTable;
    private int totalLoots = 0;

    [UDictionary.Split(55, 45)]
    public DataPickupDictionary dicoLootTable;

    public class Key
    {
        public ELootID lootID;
    }

    [Serializable]
    public class Value
    {
        public SDataLootTable dataLootTable;
    }

    private void Awake()
    {
        GenerateEntitiesLootTable.SetData(entity, dicoLootTable);
        Debug.Log(dicoLootTable.Count);

    }
    // Start is called before the first frame update
    void Start()
    {
        
        lootTable.data.Sort((l1, l2) => l1.lootDropRate.CompareTo(l2.lootDropRate));
        totalLoots = lootTable.data.Count;

        /*for (int i = 0; i < totalLoots; ++i)
            Debug.Log($"{i} : {lootTable.data[i].lootID} - {lootTable.data[i].lootDropRate}");*/

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
        float totalWeight = lootTable.data.Sum(x => x.lootDropRate);
        float rand = UnityEngine.Random.Range(0, totalWeight);
        /*Debug.Log(totalWeight);
        Debug.Log(rand);*/
        float sumWeight = 0;
        int indexDrop = 0;
        float selectedDropRate = 0;

        // On sélectionne le premier objet tiré au sort en fonction du poids total
        for (int i = 0; i < totalLoots; ++i)
        {
            sumWeight += lootTable.data[i].lootDropRate;
            if (rand <= sumWeight)
            {
                indexDrop = i;
                selectedDropRate = lootTable.data[i].lootDropRate;
                break;
            }
        }

        // On cherche s'il existe d'autres objets ayant le même drop rate que l'objet sélectionné
        var occurrences = new List<SDataLootTable>();
        for (int i = 0; i < totalLoots; ++i)
        {
            if (lootTable.data[i].lootDropRate == selectedDropRate)
                occurrences.Add(lootTable.data[i]);
            /*else 
                break;*/
        }

        Debug.Log($"Selected : {lootTable.data[indexDrop].lootID} : {occurrences.Count}");
        Debug.Log(occurrences.Count());

       /* rand = 0;
        // Si plusieurs objets ont le même drop rate, alors il faut les départager équitablement
        if (occurrences.Count > 1)
        {
            rand = Random.Range(0, occurrences.Count);
            Debug.Log($"Rerand Selected : {lootTable.data[(int)rand].lootID} : {occurrences.Count}");

        }*/

        return null;
    }
}
