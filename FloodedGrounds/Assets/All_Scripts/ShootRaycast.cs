using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRaycast : MonoBehaviour
{
    public int pistolDmg = 10;
    public float pistolRange = 50f;
    public float pistolRate = 0.25f;
    public float nextFire;

    public ManagementHUD hud; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * pistolRange, Color.magenta);

    }

    public void Shoot()
    {
        RaycastHit hit;

        if (hud.ammoIn > 0)
        {
            //Update HUD
            hud.AmmoCounter(1);

            // Conditions for if statement 
            // 1. Input returns true when left click down and false when released 
            // 2. Send out raycast straight forward from player camera 
            // 3. Check if raycast hit Bog lord
            // 4. Applying pistol fire rate
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, pistolRange) && hit.collider.gameObject.tag == "Bog_lord" && Time.time > nextFire)
            {
                // Update the time when our player can fire next
                nextFire = Time.time + pistolRate;

                Debug.Log("Bog lord has been shot!");
            }
        }
    }

}
