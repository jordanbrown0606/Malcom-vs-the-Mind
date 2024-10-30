using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int magazineSize;
    public int extraAmmo;
    [HideInInspector] public int curAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip chargingHandleSound;

    // Start is called before the first frame update
    void Start()
    {
        curAmmo = magazineSize;
    }

    public void Reload()
    {
        if(extraAmmo >= magazineSize)
        {
            int ammoToReload = magazineSize - curAmmo;
            extraAmmo -= ammoToReload;
            curAmmo += ammoToReload;
        }
        else if(extraAmmo > 0)
        {
            if(extraAmmo + curAmmo > magazineSize)
            {
                int leftOverAmmo = extraAmmo + curAmmo - magazineSize;
                extraAmmo = leftOverAmmo;
                curAmmo = magazineSize;
            }
            else
            {
                curAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
