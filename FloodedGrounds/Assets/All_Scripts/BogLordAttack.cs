using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogLordAttack : MonoBehaviour
{
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
        if(other.gameObject.tag == "Izzy")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord();
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Winston")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord();
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Max")
        {
            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord();
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        else
        {
            Debug.Log("Something was hit");
        }
    }
}
