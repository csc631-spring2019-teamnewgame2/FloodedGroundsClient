using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaxMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 5.0f;
    public bool gravityEnabled = false;
    public GameObject HPCanvas;
    private bool died = false;
    public float GameOverResetTime;
    private float playerHealth;
    Vector3 moveDir = Vector3.zero;
    public GameObject youLoseScreen;

    Animator anim;
    private Rigidbody rb;
    CharacterController controller;

    public Transform gunPoint;
    public GameObject bullet;

    public float WaterHeight = 15.5f;
     
    // Camera Code
    public float sensitivity = 30.0f;
    public GameObject cam;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    public float rotX, rotY;
    public bool webGLRightClickRotation = true;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        youLoseScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        
        //LockCursor ();
        if (Application.isEditor)
        {
            webGLRightClickRotation = false;
            sensitivity = sensitivity * 1.5f;
        }
    }

    void LateUpdate()
    {
        //CheckCamera();
        CheckForWaterHeight();
        CheckIfDead();
        Movement();
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

        rotY = Mathf.Clamp(rotY, minVert, maxVert);
        
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
        if (Input.GetMouseButton(0)) // left mouse button is pressed
        {
            anim.SetBool("isShooting", true);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)) { 
            anim.SetBool("isJumping",true);
            Debug.Log("jump");
        }
    }

    void Shooting()
    {
        Debug.Log("Shots fired");
    }

    void Movement()
    {
        float vdir = Input.GetAxis("Vertical");
        float hdir = Input.GetAxis("Horizontal");

        // Holding shift Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
            speed = 6.0f;
        }

        // Is not running
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false);
            speed = 3.0f;
        }

        // Moving forward-left
        if (hdir < 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetBool("isLeft", true);

            anim.SetBool("isRight", false);
            anim.SetBool("isBackward", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving backwards-left
        else if (hdir < 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetBool("isLeft", true);

            anim.SetBool("isRight", false);
            anim.SetBool("isForward", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving forward-right
        else if (hdir > 0 && vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);
            anim.SetBool("isRight", true);

            anim.SetBool("isLeft", false);
            anim.SetBool("isBackward", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving backwards-right
        else if (hdir > 0 && vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);
            anim.SetBool("isRight", true);

            anim.SetBool("isLeft", false);
            anim.SetBool("isForward", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving forward
        else if (vdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isForward", true);

            anim.SetBool("isBackward", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isLeft", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving backwards
        else if (vdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBackward", true);

            anim.SetBool("isForward", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isLeft", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving towards right
        else if (hdir > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRight", true);

            anim.SetBool("isForward", false);
            anim.SetBool("isBackward", false);
            anim.SetBool("isLeft", false);
            anim.SetFloat("Speed", speed);
        }

        // Moving towards left
        else if (hdir < 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isLeft", true);

            anim.SetBool("isForward", false);
            anim.SetBool("isBackward", false);
            anim.SetBool("isRight", false);
            anim.SetFloat("Speed", speed);
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


        if (!died)
        {
            // Update character's location and gravity
            if (gravityEnabled)
                moveDir = new Vector3(hdir, gravity, vdir);
            else
                moveDir = new Vector3(hdir, 0, vdir);

            moveDir *= speed;
            moveDir = transform.TransformDirection(moveDir);
            //moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }

    void CheckForWaterHeight()
    {
        if (transform.position.y < WaterHeight)
            gravity = 0f;

        else
            gravity = -9.8f;
    }

    void CheckIfDead()
    {
        playerHealth = HPCanvas.GetComponent<RectTransform>().rect.width;
        //Debug.Log(HPCanvas.GetComponent<RectTransform>().rect.width);
        if (playerHealth <= 0)
        {
            youLoseScreen.SetActive(true);
            anim.SetBool("isDead", true);

            // Debug.Log("Is dead");
            died = true;

            //Turn off shooting layer
            anim.SetLayerWeight(1, 0);

            //Reset game in X amount of seconds
            Invoke("GameOverSceneReset", GameOverResetTime);

        }
        else
        {
            youLoseScreen.SetActive(false);
        }
    }

    public void TakeDamageFromBogLord()
    {
        //Store current visual HP bar into "Playerhealth"
        playerHealth = HPCanvas.GetComponent<RectTransform>().rect.width;

        //Set the new visual HP bar's stat the same width minus 50
        HPCanvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerHealth - 50f);
    }

    void GameOverSceneReset()
    {
        //Reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
