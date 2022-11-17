using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Player")]

public class PlayerWeapons : ScriptableObject
{
    public float weaponFireRate;
    public uint weaponDamage;
    public float weaponAreaBullet;
    public float weaponSpreadBullet;
    public float weaponSpeedBullet;
    public int weaponAmountBullet;

    public string weaponName;

}
