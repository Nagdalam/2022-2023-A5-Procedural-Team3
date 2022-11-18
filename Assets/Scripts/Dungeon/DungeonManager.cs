using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager current;
    public DataPickup dataPickup = null;

    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        dataPickup = GenerateLootID.current.dataPickup;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F))
            Instantiate(dataPickup.dataPickupDictionary[ELootID.HEART_1]);*/
    }
}
