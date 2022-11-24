using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public DropBar dropBar = null;
    public delegate bool RandLootAction(LootTable lootTable);
    public RandLootAction randLootAction;

    public Action<float> onChangePercentage;
    public Action<float> onSetPercentage;

    public static LootManager current;

    private void Awake()
    {
        current = this;   
    }

    void Start()
    {
        
    }

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.B))
            ChangePercentage(5);

        if (Input.GetKeyDown(KeyCode.N))
            ChangePercentage(-10);*/
    }

    public void ChangePercentage(float amount)
    {
        onChangePercentage?.Invoke(amount);
    }

    public bool CanDropLoot()
    {
        float rand = UnityEngine.Random.Range(dropBar.minPercentage, dropBar.maxPercentage);
        /*Debug.Log(rand);*/
        return (rand <= dropBar.slider.value);
    }

    public void RandLoot(LootTable lootTable)
    {
        // Unlucky
        if (!CanDropLoot())
        {
            Debug.Log("Unlucky !");
            return;
        }

        // Lucky
        Debug.Log("Lucky !");
        onSetPercentage?.Invoke(0);

        lootTable.RandLoot();
    }
}
