﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public bool isInvincible;
    public uint startLife;
    public UnityEvent<uint> onHealthChange;

    public delegate void Void();
    public Void onDeath;

    public delegate void CIEnumerator(IEnumerator routine);
    public CIEnumerator onDeathIE;

    public uint currentLife { get; private set; }
    public IEnumerator onDie;

    private bool isAlive = true;

    private void Awake()
    {
        /*LootManager.current.randLootAction(TryGetComponent(out LootTable lt));*/
    }
    private void Start()
    {
        currentLife = startLife;
    }

    public void TakeDamage(uint damage)
    {
        if (!isAlive || isInvincible) return;
        if (damage > currentLife)
            damage = currentLife;
        currentLife -= damage;
        onHealthChange.Invoke(currentLife);
        if (currentLife == 0)
            Die();
    }

    public void Heal(uint healAmount)
    {
        if (!isAlive || currentLife >= startLife) return;
        if (healAmount > currentLife)
            healAmount = currentLife;

        currentLife += healAmount;
        Mathf.Clamp(currentLife, 0, startLife);
        onHealthChange?.Invoke(currentLife);
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());

        IEnumerator DieCoroutine()
        {
            if (TryGetComponent(out LootTable lootTable))
            {
                LootManager.current.ChangePercentage(LootManager.current.dropBar.amountPercentageIncrease);
                LootManager.current.RandLoot(lootTable);
            }

            onDeath?.Invoke();
            isAlive = false;
            if (onDie != null)
                yield return StartCoroutine(onDie);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(1);
        }
    }
}
