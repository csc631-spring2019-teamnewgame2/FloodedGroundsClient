using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    //public GameObject _medpack, _smokeGrenade, grenade;
    public RaycastHit hit;
    public Ray ray;

    public bool medPack, smokeGrenade, grenade;
    public float maxDistance = 5;
    
        
    // Start is called before the first frame update
    void Start()
    {
        medPack = false;
        smokeGrenade = false;
        grenade = false;
        //var screen
        //ray = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * maxDistance, Color.magenta);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "MedPack")
        {
            //isInteractable = true;
            medPack = true;
            //Debug.Log("Medpack is interactable");
        }
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "SmokeGrenade")
        {
            //isInteractable = true;
            smokeGrenade = true;
            //Debug.Log("Smoke Grenade is interactable");
        }
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Grenade")
        {
            //isInteractable = true;
            grenade = true;
            //Debug.Log("Grenade is interactable");
        }
    }
}
