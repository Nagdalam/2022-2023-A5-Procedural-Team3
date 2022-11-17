[System.Serializable]
public struct SDataLootTable
{
    public ELootID lootID;
    public string lootName;
    [UnityEngine.HideInInspector]
    public EType lootType;
    [UnityEngine.HideInInspector]
    public ERarity lootRarity;
    public float lootDropRate;
};
