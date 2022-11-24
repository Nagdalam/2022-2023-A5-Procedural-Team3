using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    private float lastShootTime;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private GameObject bullet;
    private Transform bulletsParent;
    [SerializeField]
    private float AmoutBullet = 4;
    [SerializeField]
    private float speedBullet;
    public bool startFight = false;
    void Start()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
    }
    void Update()
    {
        if (startFight)
        {
            TryToShoot();
        }
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
}
