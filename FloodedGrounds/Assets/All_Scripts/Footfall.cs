using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footfall : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSFX;

    Animator anim;

    public bool isIndoors;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        InvokeRepeating("CallFootsteps", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isWalking"))
        {
            isMoving = true;
        }

        else
            isMoving = false;
    }

    void CallFootsteps()
    {
        if (isMoving && isIndoors)
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputSFX);
            Debug.Log("Playing footstep sounds...");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isIndoors |= other.gameObject.tag == "Indoors";
    }

    void OnTriggerExit(Collider other)
    {
        isIndoors = false;
    }
}