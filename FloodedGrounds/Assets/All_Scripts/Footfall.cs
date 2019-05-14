﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footfall : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSFX;
    
    FMODUnity.StudioEventEmitter emitter;
    FMOD.Studio.ParameterInstance myParameter;
    Animator anim;
    
    public bool isIndoors;
    public bool isMoving;

    int frames = 0;
    int framesDiv = 20;

    void Awake()
    {
        var target = GameObject.Find("Max 1");
        emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.SetParameter("Surface_index", 1.1f);
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isRunning") == true)
            framesDiv = 20;
        else
            framesDiv = 30;

        if (frames % framesDiv == 0)
        {
            if (isIndoors)
            {
                emitter.SetParameter("Surface_index", 2.1f);
            }

            else
            {
                emitter.SetParameter("Surface_index", 1.1f);
            }

            isMoving = anim.GetBool("isWalking");

            if (isMoving)
            {
                GetComponent<FMODUnity.StudioEventEmitter>().Play();
            }

            else
            {
                GetComponent<FMODUnity.StudioEventEmitter>().Stop();
            }
        }

        frames += 1;
        if (frames == 60)
            frames = 0;
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