using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossBattel : MonoBehaviour
{

    private Rigidbody2DMovement movement;
    [SerializeField]
    private GameObject enemyPrefab;
    private float timerToSpawn;
    [SerializeField]
    private float delayToSpawn;
    [SerializeField]
    private float amoutEnemy;
    [SerializeField]
    private Transform[] spawns;
    [SerializeField]
    private bool startFight = false;
    private float lastShootTime;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private GameObject bullet;
    private Transform bulletsParent;
    [SerializeField]
    private float AmoutBullet =4;
    [SerializeField]
    private float speedBullet;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
        player = FindObjectOfType<PlayerController>().transform;
        movement = GetComponent<Rigidbody2DMovement>();
    }


    private void SpawnEnemy()
    {
        if (Time.time < timerToSpawn + delayToSpawn) return;
        for (int i = 0; i < amoutEnemy; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawns[i].transform.position, transform.rotation);
            Debug.Log(newEnemy.GetComponent<EnemyController>());
            newEnemy.GetComponent<EnemyController>().enabled = true;
        }

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
            newBullet.GetComponent<BulletController>()._parentName = "Enemies";
            newBullet.transform.up = transform.up;
            newBullet.transform.Rotate(0, 0, j);

            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bullet.GetComponentInChildren<Rigidbody2DMovement>().speed = speedBullet;
            j += 90;
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
        if (col.CompareTag("Bullet"))
        {
            Debug.Log("Start fight");
            startFight = true;
        }
    }
    private void OnDisable()
    {
        movement.SetDirection(Vector2.zero);
    }

    private void FixedUpdate()
    {
        if (player == null && startFight)
            return;
        movement.SetDirection(player.position - transform.position);
    }
}
