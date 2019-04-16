using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;
    public GameObject bullet;
    public Transform gunPoint;
    public Animator anim;
    public ManagementHUD counter;
    public PlayerRaycasting pickup;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();
        counter = GetComponent<ManagementHUD>();
        pickup = GetComponentInChildren<PlayerRaycasting>();
    }

    void Update()
    {
        Movement();
        Shooting();
        Reload();
        Interact();
    }

    public void Movement()
    {
        //Calculate movement velocity as a 3D vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        //Vector3 _moveHorizontal = transform.right * _xMove;
        //Vector3 _moveVertical = transform.forward * _zMove;
        Vector3 _moveHorizontal = new Vector3(_xMove, 0f, 0f);
        Vector3 _moveVertical = new Vector3(0f, 0f, _zMove);

        if (_xMove != 0 && _zMove != 0)
        {
            System.Console.WriteLine("Testing...");
        }
        //Final movement vector
        //Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;
        Vector3 _velocity = _moveHorizontal + _moveVertical;
        _velocity = _velocity.normalized;
        _velocity = _velocity * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector
        //Turning around
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector
        //Turning around
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation
        motor.RotateCamera(_cameraRotation);
    }

    public void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (counter.ammoIn > 0)
            {
                anim.SetBool("isShooting", true);

                if (counter != null)
                {
                    counter.AmmoCounter(1);
                }

                GameObject tempBullet = (GameObject)Instantiate(bullet);
                tempBullet.transform.position = gunPoint.position;
                Destroy(tempBullet, 1f);
            }
            else
            {
                Debug.Log("Player must reload!");
            }
        }
        
        /*if(Input.GetMouseButtonUp(0))
        {
            StartCoroutine(shootingRoutine());
        }*/
    }

    /*IEnumerator shootingRoutine()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isShooting", false);
    }*/

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            counter.AmmoReload();
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(pickup.medPack)
            {
                pickup.medPack = false;
                counter.MedCounter(1);
            }
            if (pickup.smokeGrenade)
            {
                pickup.smokeGrenade = false;
                counter.SmokeCounter(1);
            }
            if (pickup.grenade)
            {
                pickup.grenade = false;
                counter.GrenadeCounter(1);
            }
            //Debug.Log("Picked Up Item");
        }
    }
    
}
