using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootTable : MonoBehaviour, ILootable
{
    [ReadOnly] List<int> dropRates = new List<int>();
    [ReadOnly] public DataEntity dataEntity;
    [ReadOnly] public List<DataLoot> dataLoot;

    private int totalLoots = 0;

    private void Awake()
    {
    }

    void Start()
    {
    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            LootManager.current.RandLoot(this);
        }
    }

    public void RandLoot()
    {
        float totalWeight = dropRates.Sum();
        float rand = UnityEngine.Random.Range(0, totalWeight);
        /*Debug.Log(totalWeight);
        Debug.Log(rand);*/
        float sumWeight = 0;
        int indexDrop = 0;

        // On sélectionne le premier objet tiré au sort en fonction du poids total
        for (int i = 0; i < totalLoots; ++i)
        {
            sumWeight += dropRates[i];
            if (rand <= sumWeight)
            {
                indexDrop = i;
                break;
            }
        }

        var keys = dataEntity.lootTable.Keys;
        Debug.Log($"Selected : {dataLoot[indexDrop].lootData.lootName}");
        var selectedLoot = keys[indexDrop].lootPrefab;

        if (!selectedLoot)
            return;

        Instantiate(selectedLoot);
    }

    void ILootable.SetData(DataEntity dataEntity)
    {
        this.dataEntity = dataEntity;
        dataLoot = dataEntity.lootTable.Keys;
        dropRates = dataEntity.lootTable.Values;
        totalLoots = dataEntity.lootTable.Count;
    }
}
