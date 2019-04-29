using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 5.0f;
    public bool gravityEnabled = false;

    Vector3 moveDir = Vector3.zero;

    Animator anim;
    private Rigidbody rb;
    CharacterController controller;

    public Transform gunPoint;
    public GameObject bullet;
    //public GameObject shout;

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
        GetInput();
        Shooting();
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

    void GetInput()
    {
        // Attack
        if (Input.GetMouseButton(0)) // left mouse button is held down
            anim.SetBool("isShooting", true);

        // Jump
        if (Input.GetKey(KeyCode.Space))
            anim.SetBool("isJumping", true);
    }

    void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    void EndJump()
    {
        anim.SetBool("isJumping", false);
    }

    void Shooting()
    {
        anim.SetBool("isShooting", true);
        GameObject tempBullet = (GameObject)Instantiate(bullet);
        tempBullet.transform.position = gunPoint.position;
        Destroy(tempBullet, 1.0f);
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

        if (gravityEnabled)
            moveDir = new Vector3(hdir, gravity, vdir);
        else
            moveDir = new Vector3(hdir, 0, vdir);

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
