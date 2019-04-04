using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = 10f;

    Vector3 moveDir = Vector3.zero;

    Animator anim;
    private Rigidbody rb;
    CharacterController controller;

    public Transform gunPoint;
    public GameObject bullet;

    public Transform spine;
    public Transform leftLeg;
    public Transform rightLeg;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Movement();
        Jumping();
        GetInput();
        Shooting();
    }

    void GetInput()
    {
        if (Input.GetMouseButton(0)) // left mouse button is held down
        {
            anim.SetBool("isAttacking", true);
        }

        if (Input.GetMouseButtonUp(0)) // left mouse button is let go
        {
            Attacking();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("JUMP");
            if (anim.GetBool("isJumping") == false)
            {
                anim.SetBool("isJumping", true);
            }

            else
            {
                anim.SetBool("isJumping", false);
            }
        }

        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    void Jumping()
    {
        StartCoroutine(JumpRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isAttacking", false);
    }

    IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isJumping", false);
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject tempBullet = (GameObject)Instantiate(bullet);
            tempBullet.transform.position = gunPoint.position;
            Destroy(tempBullet, 1f);
        }
    }

    void Movement()
    {
        float vdir = Input.GetAxis("Vertical");
        float hdir = Input.GetAxis("Horizontal");

        // Holding shift Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3.0f;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 1.0f;
        }

        if (vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", speed);
        }

        else if (vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", -speed);
        }

        else
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("Speed", 0.0f);
        }

        // Turn body left+forward
        if (hdir > 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        // Turn body left+backward
        else if (hdir > 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", -speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        // Turn body right+forward
        else if (hdir < 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Turn body right+backward
        else if (hdir < 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", -speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Turn body left
        else if (hdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Turn body right
        else if (hdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        moveDir = new Vector3(hdir, 0, vdir);
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
