using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    //public GameObject _medpack, _smokeGrenade, grenade;
    public RaycastHit hit;
    public Ray ray;
    public ManagementHUD counter;

    public bool medPack;
    public float maxDistance = 5;

    //private Vector3 raycastPos;
    
        
    // Start is called before the first frame update
    void Start()
    {
        medPack = false;

        //foreach (Transform t in transform.GetComponentInChildren<Transform>(true))
        //    if (t.gameObject.name == "CC_Base_Eye")
        //        raycastPos = t.gameObject.transform.position;

        //counter = GetComponentInParent<ManagementHUD>();
        //smokeGrenade = false;
        //grenade = false;
        //var screen
        //ray = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * maxDistance, Color.magenta);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Medpack")
        {
            //isInteractable = true;
            //medPack = true;
            //Debug.Log("Medpack is interactable");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hit.collider.gameObject);
                //medPack = false;
                counter.MedCounter(1);
            }
        }
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Ammo")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hit.collider.gameObject);
                counter.AmmoPickup(120);
            }
        }
        /*if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "SmokeGrenade")
        {
            //isInteractable = true;
            smokeGrenade = true;
            //Debug.Log("Smoke Grenade is interactable");
        }
        else if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Grenade")
        {
            //isInteractable = true;
            grenade = true;
            //Debug.Log("Grenade is interactable");
        }*/
    }
}
