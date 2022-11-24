using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject prefabBullet;
    public Transform bullet;
    public PlayerWeapons currentWeapon;

    private PlayerWeapons lastCurrentWeapon;

    //Stat
    [SerializeField]
    private float fireRatePercent;
    [SerializeField]
    private uint damageBulletPercent;
    [SerializeField]
    private float areaBulletPercent;
    [SerializeField]
    private float speedBulletPercent;

    [SerializeField]
    private float fireRateResult;
    [SerializeField]
    private uint damageBulletResult;
    [SerializeField]
    private float areaBulletResult;
    [SerializeField]
    private float spreadBulletResult;
    [SerializeField]
    private float speedBulletResult;
    [SerializeField]
    private int amountBulletResult;


    private bool isShooting;
    private Vector2 lastDirection;
    private Transform bulletsParent;
    private float lastShootTime;


    private InflictDamageOnTrigger2D setDamageBullet;

    private void Awake()
    {
        bulletsParent = new GameObject($"{name} bullets").transform;
        setDamageBullet = prefabBullet.GetComponentInChildren<InflictDamageOnTrigger2D>();
    }

    private void Start()
    {
        lastShootTime = currentWeapon.weaponFireRate;

        lastCurrentWeapon = currentWeapon;

        fireRateResult = currentWeapon.weaponFireRate;

        damageBulletResult = currentWeapon.weaponDamage;

        areaBulletResult = currentWeapon.weaponAreaBullet;

        speedBulletResult = currentWeapon.weaponSpeedBullet;

        SetAllStat();
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
        if(currentWeapon != lastCurrentWeapon)
            SwitchWeapon();
    }

    public void StopShooting()
    {
        isShooting = false;
    }
    public void SetRandomPercent()
    {
        int i = 0;

        i = Random.Range(1, 5);

        switch (i)
        {
            case 1:
                fireRatePercent += 25;

                SetAllStat();
                break;
            case 2:
                damageBulletPercent += 25;

                SetAllStat();
                break;
            case 3:
                areaBulletPercent += 25;

                SetAllStat();
                break;
            case 4:
                speedBulletPercent += 25;

                SetAllStat();
                break;

        }
    }
    public void SetAllPercent()
    {
        fireRatePercent += 50;

        damageBulletPercent += 50;

        areaBulletPercent += 50;

        speedBulletPercent += 50;

        SetAllStat();
    }

    public void SetAllStat()
    {
        if (fireRatePercent!=0) 
        {
            fireRateResult = currentWeapon.weaponFireRate - (currentWeapon.weaponFireRate / fireRatePercent);
        }
        else
        {
            fireRateResult = currentWeapon.weaponFireRate;
        }

        if (damageBulletPercent != 0)
        {
            damageBulletResult = currentWeapon.weaponDamage + (currentWeapon.weaponDamage / damageBulletPercent);
        }
        else
        {
            damageBulletResult = currentWeapon.weaponDamage;
        }

        if (areaBulletPercent != 0)
        {
            areaBulletResult = currentWeapon.weaponAreaBullet+(currentWeapon.weaponAreaBullet/areaBulletPercent);
        }
        else
        {
            areaBulletResult = currentWeapon.weaponAreaBullet;
        }

        if (speedBulletPercent != 0)
        {
            speedBulletResult = currentWeapon.weaponSpeedBullet+(currentWeapon.weaponSpeedBullet/speedBulletPercent);
        }
        else
        {
            speedBulletResult = currentWeapon.weaponSpeedBullet;
        }
    }

    public void SwitchWeapon()
    {
        if (currentWeapon != null)
        {
            SetAllStat();
            lastCurrentWeapon = currentWeapon;
        }
    }

    //shoot set bullet and damagebullet
    private void TryToShoot()
    {
        Debug.Log(lastShootTime);
        if (Time.time < lastShootTime + fireRateResult) return;
        for(int i = 0; i < currentWeapon.weaponAmountBullet; i++)
        {
            var bulletPosition = transform.position;
            var newBullet = Instantiate(bullet, bulletPosition, Quaternion.identity, bulletsParent);
            var newBulletController = newBullet.GetComponent<BulletController>();
            newBulletController._parentName = "Player";
            newBullet.up = transform.up;
            newBullet.Rotate(0, 0, Random.Range(-currentWeapon.weaponSpreadBullet, currentWeapon.weaponSpreadBullet));

            bullet.transform.localScale = new Vector3(areaBulletResult, areaBulletResult, 1);
            bullet.GetComponentInChildren<Rigidbody2DMovement>().speed = speedBulletResult;
            newBulletController.damageOnTouch = damageBulletResult;
        }
        lastShootTime = Time.time;
    }
}
