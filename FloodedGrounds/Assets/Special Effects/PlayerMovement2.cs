using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 1f;
    public float rotSpeed = 80f;
    public float rot = 0f;
    public float gravity = 10f;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private Rigidbody rb;
    public float jumpHeight;
    public Transform gunPoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0)) // left mouse button is held down
            {
                if (anim.GetBool("isWalking") == true)
                {
                    anim.SetBool("isWalking", false);
                }

                else
                    anim.SetBool("isAttacking", true);
            }

            if (Input.GetMouseButtonUp(0)) // left mouse button is let go
            {
                anim.SetBool("isAttacking", false);
            }
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("isAttacking") == true)
                {
                    return;
                }

                else if (anim.GetBool("isAttacking") == false)
                {
                    anim.SetFloat("Speed", speed);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetFloat("Speed", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("isAttacking") == true)
                {
                    return;
                }

                else if (anim.GetBool("isAttacking") == false)
                {
                    anim.SetFloat("Speed", speed);
                    moveDir = new Vector3(0, 0, -1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetFloat("Speed", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

        //float hdir = Input.GetAxisRaw("Horizontal");
        //float vdir = Input.GetAxisRaw("Vertical");

        //Vector3 directionVector = new Vector3(hdir, 0, vdir);
        //Vector3 unitVector = directionVector.normalized;
        //Vector3 forceVector = unitVector * speed * Time.deltaTime;

        //rb.AddForce(forceVector);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(Vector3.up * jumpHeight);
        //}

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    GameObject tempBullet = (GameObject)Instantiate(bullet);
        //    tempBullet.transform.position = gunPoint.position;
        //    Destroy(tempBullet, 1f);
        //}
    //}
}
