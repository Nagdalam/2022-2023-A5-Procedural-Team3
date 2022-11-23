using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBattle : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float timerToSpawn;
    [SerializeField]
    private float delayToSpawn;
    [SerializeField]
    private bool startFight = false;
    private float lastShootTime;
    [SerializeField]
    private float fireRate;
    [SerializeField] 
    private GameObject bullet;
    private Transform bulletsParent;
    [SerializeField]
    private float AmoutBullet = 12;
    [SerializeField]
    private float speedBullet;

    // Start is called before the first frame update
    void Start()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
    }


    private void SpawnEnemy()
    {
        if (Time.time < timerToSpawn + delayToSpawn) return;
        Instantiate (enemyPrefab, transform.position, transform.rotation);
        timerToSpawn = Time.time;
    }

    private void TryToShoot()
    {
        if (Time.time < lastShootTime + fireRate) return;
        int j = 0;
        for (int i = 0; i < AmoutBullet; i++)
        {
            
            var bulletPosition = transform.position;
            var newBullet = Instantiate(bullet, bulletPosition, Quaternion.identity, bulletsParent);
            newBullet.transform.up = transform.up;
            newBullet.transform.Rotate(0, 0, j);

            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bullet.GetComponentInChildren<Rigidbody2DMovement>().speed = speedBullet;
            j += 30;
        }
        lastShootTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (startFight)
        {
            TryToShoot();
            SpawnEnemy();
        }
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            startFight= true;
        }
    }
}
