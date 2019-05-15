using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckToWin : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Max" || other.gameObject.tag == "Winston" || other.gameObject.tag == "Izzy")
        {

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
