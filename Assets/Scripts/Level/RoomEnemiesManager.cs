using System.Collections.Generic;
using UnityEngine;

public class RoomEnemiesManager : MonoBehaviour
{
    //private readonly List<EnemyController> enemiesInRoom = new();
    public List<EnemyController> enemyControllers= new List<EnemyController>();
    public void SetAllEnemiesInRoomActive(bool isActive)
    {
        foreach (var enemy in enemyControllers)
        {
            enemy.enabled = isActive;
        }
    }

    //public void AddEnemyToRoom(EnemyController enemyToAdd)
    //{
    //    enemiesInRoom.Add(enemyToAdd);
    //}

    //public void RemoveEnemyFromRoom(EnemyController enemyToRemove)
    //{
    //    enemiesInRoom.Remove(enemyToRemove);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player et enterer");
            SetAllEnemiesInRoomActive(true);
        }
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            enemyControllers.Add(collision.GetComponent<EnemyController>());
        }
    }

    //private void OnTriggerLeave(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("player et enterer");
    //        SetAllEnemiesInRoomActive(false);
    //    }
    //}
}
