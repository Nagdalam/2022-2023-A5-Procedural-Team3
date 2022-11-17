using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootTable : MonoBehaviour
{
    public List<SDataLootTable> lootTable = new List<SDataLootTable>();
    public int totalLoots = 0;

    // Start is called before the first frame update
    void Start()
    {
        lootTable.Sort((l1, l2) => l1.lootDropRate.CompareTo(l2.lootDropRate));
        totalLoots = lootTable.Count;

        /*for (int i = 0; i < totalLoots; ++i)
            Debug.Log($"{i} : {lootTable[i].lootID} - {lootTable[i].lootDropRate}");*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DropLoot();
        }
    }

    public Pickup DropLoot()
    {
        float totalWeight = lootTable.Sum(x => x.lootDropRate);
        float rand = Random.Range(0, totalWeight);
        Debug.Log(totalWeight);
        Debug.Log(rand);
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
        for (int i = indexDrop; i < totalLoots; ++i)
        {
            if (lootTable[i].lootDropRate == selectedDropRate)
                occurrences.Add(lootTable[i]);
            else 
                break;
        }

        Debug.Log($"Selected : {lootTable[indexDrop].lootID} : {occurrences.Count}");
        Debug.Log(occurrences.Count());

        // Si plusieurs objets ont le même drop rate, alors il faut les départager équitablement
        if (occurrences.Count > 1)
        {

        }

        else
        {
            
        }

        // On génère l'objet loot
        /*Pickup pickup = new Pickup();*/

        /*return pickup*/;
        return null;
    }
}
