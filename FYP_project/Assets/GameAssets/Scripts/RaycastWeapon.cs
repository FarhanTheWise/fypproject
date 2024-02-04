using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaycastWeapon : MonoBehaviour
{
    public string weaponName;
    public WeaponObject weaponObject;

    public WeaponObject.WeaponClass weaponClass;
    public WeaponObject.FireType fireType;
    
    public bool isFiring;
    public bool isReloading;
    
    public float currentTime;
    public float fireRate;

    public int currentBullets;
    public int bulletsInMag;
    public int totalBullets;

    private void OnEnable()
    {
        currentTime = 0;
        weaponName = weaponObject.WeaponName;
        fireRate = weaponObject.fireRate;
        weaponClass = weaponObject.weaponClass;
        fireType = weaponObject.fireType;
        currentBullets = weaponObject.magazineSize;
        bulletsInMag = weaponObject.magazineSize;
        totalBullets = weaponObject.totalBullets;
    }

    public abstract void ReloadWeapon();
    public abstract void FireWeapon();
}
