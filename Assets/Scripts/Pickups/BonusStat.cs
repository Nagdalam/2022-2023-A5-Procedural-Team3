using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusStat : MonoBehaviour
{
    protected void Awake()
    {

    }

    public float duration = 0;
    public float Duration { get => duration; set => duration = value; }

    protected abstract void OnTriggerEnter2D(Collider2D col);
}