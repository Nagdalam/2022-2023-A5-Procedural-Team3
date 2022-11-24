using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class FirstBossBattle : MonoBehaviour
{
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
    private float AmoutBullet = 12;
    [SerializeField]
    private float speedBullet;
    public GameObject teleporter;
    public PlayerWeapons pistol;
    // Start is called before the first frame update
    void Start()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
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
        if (col.CompareTag("Bullet"))
        {
            Debug.Log("Start fight");
            startFight= true;
        }
    }

    void OnDestroy()
    {
        Instantiate(teleporter, transform.position, Quaternion.identity);
        if(FindObjectOfType<PlayerController>().canon.GetComponent<Shooter>().currentWeapon == pistol)
        {
            Destroy(FindObjectOfType<BigDoor>().gameObject);
        }
    }
}
