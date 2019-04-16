using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementHUD : MonoBehaviour
{
    public int ammoIn = 30;
    public int ammoOut = 120;

    public int _medPack = 1;
    public int _smokeGrenade = 2;
    public int _grenade = 2;

    public Text ammoDisplay;
    public Text medpack;
    public Text smokeGrenade;
    public Text grenade;

    public void AmmoCounter(int amount)
    {
        ammoIn -= amount;

        if(ammoIn <= 0 && ammoOut <= 0)
        {
            ammoIn = 0;
            ammoOut = 0;
            Debug.Log("Out of ammo! Find more ammo!");
        }
        else if(ammoIn <= 0 && ammoOut > 0)
        {
            ammoIn = 0;
            Debug.Log("Reload!");
        }

        ammoDisplay.text = ammoIn.ToString() + " <size=8>/ " + ammoOut.ToString() + "</size>";
    }

    public void AmmoReload()
    {
        if(ammoIn > 0 && ammoOut >= 30)
        {
            ammoOut = ammoOut - (30 - ammoIn);
            ammoIn = 30;
        }
        if(ammoIn == 0 && ammoOut >= 30)
        {
            ammoIn = 30;
            ammoOut -= 30;
        }
        if(ammoIn > 0 && ammoOut < 30 && ammoIn + ammoOut <= 30)
        {
            if(ammoIn + ammoOut == 30)
            {
                ammoIn = 30;
                ammoOut = 0;
            }
            else
            {
                ammoIn = ammoIn + ammoOut;
                ammoOut = 0;
            }
        }
        if(ammoIn == 0 && ammoOut < 30)
        {
            ammoIn = ammoOut;
            ammoOut = 0;
        }

        ammoDisplay.text = ammoIn.ToString() + " <size=8>/ " + ammoOut.ToString() + "</size>";
    }

    public void MedCounter(int add)
    {
        _medPack += add;

        medpack.text = _medPack.ToString();
    }

    public void SmokeCounter(int add)
    {
        _smokeGrenade += add;

        smokeGrenade.text = _smokeGrenade.ToString();
    }

    public void GrenadeCounter(int add)
    {
        _grenade += add;

        grenade.text = _grenade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
