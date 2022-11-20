using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject prefabBullet;
    public Transform bullet;
    //Stat
    private float fireDelayPercent;
    private uint damageBulletPercent;
    private float areaBulletPercent;
    private float spreadBulletPercent;
    private float speedBulletPercent;
    private int amountBullet;

    private float fireDelayResult;
    private uint damageBulletResult;
    private float areaBulletResult;
    private float spreadBulletResult;
    private float speedBulletResult;
    private int amountBulletResult;


    private bool isShooting;
    private Vector2 lastDirection;
    private Transform bulletsParent;
    private float lastShootTime;

    public PlayerWeapons currentWeapon;

    private InflictDamageOnTrigger2D setDamageBullet;

    private void Awake()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
        setDamageBullet = prefabBullet.GetComponentInChildren<InflictDamageOnTrigger2D>();
    }

    private void Start()
    {
        lastShootTime = currentWeapon.weaponFireRate * -1;
    }

    public void StartShooting()
    {
        isShooting = true;
        TryToShoot();
    }
    
    private void Update()
    {
        if (isShooting)
            TryToShoot();
    }

    public void StopShooting()
    {
        isShooting = false;
    }
    
    private void SetStat()
    {
        fireDelayResult = currentWeapon.weaponFireRate * fireDelayPercent;
        damageBulletResult = currentWeapon.weaponDamage * damageBulletPercent;
        areaBulletResult = currentWeapon.weaponAreaBullet * areaBulletPercent;
        spreadBulletResult = currentWeapon.weaponSpreadBullet * spreadBulletPercent;
        speedBulletResult = currentWeapon.weaponSpeedBullet * speedBulletPercent;
        amountBulletResult =  currentWeapon.weaponAmountBullet + amountBullet;
    }

    //shoot set bullet and damagebullet
    private void TryToShoot()
    {
        if (Time.time < lastShootTime + currentWeapon.weaponFireRate) return;
        for(int i = 0; i < currentWeapon.weaponAmountBullet; i++)
        {
            var bulletPosition = transform.position;
            var newBullet = Instantiate(bullet, bulletPosition, Quaternion.identity, bulletsParent);
            newBullet.up = transform.up;
            newBullet.Rotate(0, 0, Random.Range(-currentWeapon.weaponSpreadBullet, currentWeapon.weaponSpreadBullet));

            bullet.transform.localScale = new Vector3(currentWeapon.weaponAreaBullet, currentWeapon.weaponAreaBullet, 1);
            bullet.GetComponentInChildren<Rigidbody2DMovement>().speed = currentWeapon.weaponSpeedBullet;
            setDamageBullet.damageOnTouch = currentWeapon.weaponDamage;
        }
        lastShootTime = Time.time;
    }
}
