using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycasting : MonoBehaviour
{
    public ManagementHUD counter;
    public Animator medAnim;
    public Animator ammoAnim;

    public float maxDistance = 3;
    public float sphereRadius = 0.5f;

    private float currentHitDistance;
    
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(this.transform.position, this.transform.forward * maxDistance, Color.magenta);
        //Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * maxDistance, sphereRadius);

        RaycastHit hit;

        if (Physics.SphereCast(this.transform.position, sphereRadius, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Medpack")
        {
            medAnim = hit.collider.gameObject.GetComponent<Animator>();
            medAnim.enabled = true;

            currentHitDistance = hit.distance;

            counter.InteractHint();

            if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    counter.MedCounter(1);
                }
        }
        else
        {
            currentHitDistance = maxDistance;
        }

        if (Physics.SphereCast(this.transform.position, sphereRadius, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Ammo")
        {
            ammoAnim = hit.collider.gameObject.GetComponent<Animator>();
            ammoAnim.enabled = true;

            currentHitDistance = hit.distance;

            counter.InteractHint();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hit.collider.gameObject);
                counter.AmmoPickup(120);
            }
        }
        else
        {
            currentHitDistance = maxDistance;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * currentHitDistance, sphereRadius);
    }

}

//if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Medpack")
//{
//    medAnim = hit.collider.gameObject.GetComponent<Animator>();
//    medAnim.enabled = true;

//    if (Input.GetKeyDown(KeyCode.E))
//    {
//        Destroy(hit.collider.gameObject);
//        //medPack = false;
//        counter.MedCounter(1);
//    }
//}
//if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Ammo")
//{
//    ammoAnim = hit.collider.gameObject.GetComponent<Animator>();
//    ammoAnim.enabled = true;

//    if (Input.GetKeyDown(KeyCode.E))
//    {
//        Destroy(hit.collider.gameObject);
//        counter.AmmoPickup(120);
//    }
//}
