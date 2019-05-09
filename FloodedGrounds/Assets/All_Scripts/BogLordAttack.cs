using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogLordAttack : MonoBehaviour
{
    public float AttackDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnCollisionEnter(Collision other)
    {
        //This onCollisionEnter function currently works on the (self) of whichever player you're controlling.
        //Ex. If you hit "Max" and you're the bog lord, instead of his HP decreasing, your own decreases. This could be handled with networking.

        if (other.gameObject.tag == "Izzy")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Winston")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Max")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }

    }
}
