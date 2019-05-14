using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckToWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Izzy" || other.gameObject.tag == "Winston" || other.gameObject.tag == "Max")
        {

        }
    }
}
