using System;
using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using UnityEngine;

public class RayCastAuto : RaycastWeapon
{
    private void Update()
    {
        if (CF2Input.GetButton("Fire") && !isReloading)
        {
            isFiring = true;
            
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                FireWeapon();
                currentTime = fireRate;
            }
        }
        else
        {
            isFiring = false;
        }
    }

    public override void ReloadWeapon()
    {
        
    }
    
    public override void FireWeapon()
    {
        
    }
}
