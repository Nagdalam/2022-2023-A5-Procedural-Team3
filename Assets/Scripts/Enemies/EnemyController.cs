﻿using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Rigidbody2DMovement movement;
    public Life life;
    private RoomEnemiesManager enemiesManager;
    public bool enemyInvok;
    private EnemyTurret enemyTurret; 

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        player = FindObjectOfType<PlayerController>().transform;
        enemiesManager = GetComponentInParent<RoomEnemiesManager>();
        enemiesManager.AddEnemyToRoom(this);
        if(!enemyTurret)
        enemyTurret= GetComponent<EnemyTurret>();
    }

    private void Start()
    {
        if (enemyInvok)
        {
            enabled = true;
        }
        else
        enabled = false;
    }

    private void OnDisable()
    {
        movement.SetDirection(Vector2.zero);
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;
        enemyTurret.startFight = true;
        movement.SetDirection(player.position - transform.position);
    }

    private void OnDestroy()
    {
        enemiesManager.RemoveEnemyFromRoom(this);
    }
}
