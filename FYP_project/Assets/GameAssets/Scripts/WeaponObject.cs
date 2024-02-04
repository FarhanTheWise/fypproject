using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons", fileName = "WeaponObject")]
public class WeaponObject : ScriptableObject
{

    [Header("Weapon ID")]
    public string WeaponName;
    public WeaponClass weaponClass;
    public FireType fireType;
    
    //weapon Stats
    [Header("Weapon Stats")] 
    public float fireRate;
    public int magazineSize;
    public int totalBullets;

    public enum WeaponClass
    {
        Pistol = 0,
        Smg = 1,
        Shotgun = 2,
        Assault = 3,
        Sniper = 4
    }

    public enum FireType
    {
        Auto = 0,
        SemiAuto = 1,
        Shotgun = 2
    }
}
