using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactible
{
    private LootTable lootTable;

    void Awake()
    {
        TryGetComponent(out lootTable);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            lootTable.RandLoot();
    }
}
