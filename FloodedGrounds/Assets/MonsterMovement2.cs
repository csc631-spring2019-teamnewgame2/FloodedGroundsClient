using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement2 : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 5.0f;

    Vector3 moveDir = Vector3.zero;

    Animator anim;
    private Rigidbody rb;
    CharacterController controller;

    public GameObject shout;

    public Transform spine;
    public Transform leftLeg;
    public Transform rightLeg;

    public float sensitivity = 30.0f;
    public float WaterHeight = 15.5f;
    public GameObject cam;
    float rotX, rotY;
    public bool webGLRightClickRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        //LockCursor ();
        if (Application.isEditor)
        {
            webGLRightClickRotation = false;
            sensitivity = sensitivity * 1.5f;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckCamera();
        CheckForWaterHeight();
        Movement();
        //Jumping();
        GetInput();
    }

    void CameraRotation(GameObject cam, float rotX, float rotY)
    {
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);
    }

    void CheckCamera()
    {
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        if (webGLRightClickRotation)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                CameraRotation(cam, rotX, rotY);
            }
        }

        else if (!webGLRightClickRotation)
        {
            CameraRotation(cam, rotX, rotY);
        }

        moveDir = transform.rotation * moveDir;
    }

    void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    void EndJump()
    {
        anim.SetBool("isJumping", false);
    }

    void EndShout()
    {
        anim.SetBool("isShouting", false);
    }

    void GetInput()
    {
        // Attack
        if (Input.GetMouseButton(0)) // left mouse button is pressed
        {
            anim.SetBool("isAttacking", true);
        }

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (anim.GetBool("isJumping") == false)
            {
                anim.SetBool("isJumping", true);
            }
        }

        // Shout
        if (Input.GetKey(KeyCode.X) && anim.GetBool("isShouting") == false)
        {
            anim.SetBool("isShouting", true);
            Shouting();
        }
    }

    void Shouting()
    {
        GameObject tempShout = (GameObject)Instantiate(shout);
        tempShout.transform.position = transform.position;
        Destroy(tempShout, 5.0f);
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

        // Moving forward
        if (vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetFloat("Speed", speed);
        }

        // Moving backwards
        else if (vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetFloat("Speed", speed);
        }

        // Moving towards right
        else if (hdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRight", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        // Moving towards left
        else if (hdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isLeft", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Moving forward-left
        else if (hdir < 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetBool("isLeft", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        // Moving backwards-left
        else if (hdir < 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetBool("isLeft", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(-45, 0, 0);
            leftLeg.transform.Rotate(15, 0, 0);
            rightLeg.transform.Rotate(15, 0, 0);
        }

        // Moving forward-right
        else if (hdir > 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetBool("isRight", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Moving backwards-right
        else if (hdir > 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetBool("isRight", true);
            anim.SetFloat("Speed", speed);
            spine.transform.Rotate(45, 0, 0);
            leftLeg.transform.Rotate(-15, 0, 0);
            rightLeg.transform.Rotate(-15, 0, 0);
        }

        // Stationary
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isForward", false);
            anim.SetBool("isBackward", false);
            anim.SetFloat("Speed", 0.0f);
        }

        // Update character's location and gravity
        moveDir = new Vector3(hdir, gravity, vdir);
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);
        //moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void CheckForWaterHeight()
    {
        if (transform.position.y < WaterHeight)
        {
            gravity = 0f;
        }

        else
        {
            gravity = -9.8f;
        }
    }
}
