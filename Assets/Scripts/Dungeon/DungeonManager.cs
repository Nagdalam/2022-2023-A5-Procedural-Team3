using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager current;

    public List<DataEntity> spawnable = new List<DataEntity>();

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        /*dataPickup = GenerateLoot.current.dataPickup;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var spawned = Instantiate(spawnable[0].entity);
            var lootComponent = spawned.GetComponent<LootTable>();
            (lootComponent as ILootable).SetData(spawnable[0]);
        }
    }
}
