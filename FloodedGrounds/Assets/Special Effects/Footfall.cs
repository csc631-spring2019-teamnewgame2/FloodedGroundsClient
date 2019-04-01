﻿using System.Collections;
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
        if (anim.GetBool("isWalking") || anim.GetBool("isRunning"))
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Indoors")
        {
            isIndoors = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isIndoors = false;
    }

    void OnDisable()
    {
        isMoving = false;
    }
}