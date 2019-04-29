﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3_IJKL_Movement : MonoBehaviour
{
    Animator anim;
    private Rigidbody rb;
    CharacterController controller;

    Vector3 moveDir = Vector3.zero;
    float speed = 1.0f;
    float gravity = -9.8f;
    float hdir;
    float vdir;

    public Transform gunPoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.I))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetFloat("Speed", speed);
            vdir = 1;
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isForward", false);
            anim.SetFloat("Speed", 0.0f);
            vdir = 0;
        }

        if (Input.GetKey(KeyCode.K))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetFloat("Speed", speed);
            vdir = -1;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isBackward", false);
            anim.SetFloat("Speed", 0.0f);
            vdir = 0;
        }

        if (Input.GetKey(KeyCode.J))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isLeft", true);
            anim.SetFloat("Speed", speed);
            hdir = -1;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetFloat("Speed", 0.0f);
            hdir = 0;
        }

        if (Input.GetKey(KeyCode.L))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRight", true);
            anim.SetFloat("Speed", speed);
            hdir = 1;
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRight", false);
            anim.SetFloat("Speed", 0.0f);
            hdir = 0;
        }

        moveDir = new Vector3(hdir, gravity, vdir);
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);
        //moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("isShooting", true);
            GameObject tempBullet = (GameObject)Instantiate(bullet);
            tempBullet.transform.position = gunPoint.position;
            Destroy(tempBullet, 1.0f);
        }

        else
        {
            anim.SetBool("isShooting", false);
        }
    }
}
