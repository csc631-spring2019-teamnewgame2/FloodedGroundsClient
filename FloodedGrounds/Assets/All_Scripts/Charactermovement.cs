using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour
{

    CharacterController cc;
    public float moveSpeed = 4f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cc = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Inputs()
    {
        
    }

    void Movement()
    {
        //Left/Right/Up/Down Input
        float hdir = Input.GetAxis("Horizontal");
        float vdir = Input.GetAxis("Vertical");
        if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.D))
        {
            hdir = 0f;
            vdir = 0f;
        }

        Debug.Log("hdir: " + hdir + "\n" + "vdir: " + vdir);

        MoveAnimations(hdir,vdir);

        //Moving the character
        Vector3 direction = new Vector3(hdir, 0, vdir);
        Vector3 velocity = direction * moveSpeed * Time.deltaTime;
        cc.Move(velocity);
    }

    void MoveAnimations(float hdir, float vdir)
    {
        //hdir = Mathf.Round(hdir);
        //vdir = Mathf.Round(hdir);
        anim.SetFloat("horizontal", hdir);
        anim.SetFloat("vertical", vdir);

        
    }
}
