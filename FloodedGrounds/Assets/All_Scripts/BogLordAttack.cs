using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogLordAttack : MonoBehaviour
{
    public float AttackDamage;

    public ParticleSystem humanBlood;

    private Animator animIzzy;
    private Animator animWinston;
    private Animator animMax;

    // Start is called before the first frame update
    void Start()
    {
        animIzzy = GameObject.FindGameObjectWithTag("Izzy").GetComponent<Animator>();
        animWinston = GameObject.FindGameObjectWithTag("Winston").GetComponent<Animator>();
        animMax = GameObject.FindGameObjectWithTag("Max").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnCollisionEnter(Collision other)
    {
        //This onCollisionEnter function currently works on the (self) of whichever player you're controlling.
        //Ex. If you hit "Max" and you're the bog lord, instead of his HP decreasing, your own decreases. This could be handled with networking.

        if (other.gameObject.tag == "Izzy")
        {
            // Trigger isHit animation for Izzy
            //animIzzy.SetBool("isHit", true);

            // spawn humanBlood particle effect, then destroy clone gameObject
            var rot = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
            Destroy(Instantiate(humanBlood.gameObject, other.contacts[0].point, rot), 2f);

            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Winston")
        {
            // Trigger isHit animation for Winston
            //animWinston.SetBool("isHit", true);

            // spawn humanBlood particle effect, then destroy clone gameObject
            var rot = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
            Destroy(Instantiate(humanBlood.gameObject, other.contacts[0].point, rot), 2f);

            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }
        if (other.gameObject.tag == "Max")
        {
            // Trigger isHit animation for Max
            //animMax.SetBool("isHit", true);

            // spawn humanBlood particle effect, then destroy clone gameObject
            var rot = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
            Destroy(Instantiate(humanBlood.gameObject, other.contacts[0].point, rot), 2f);

            other.gameObject.GetComponent<MaxMovement>().TakeDamageFromBogLord(AttackDamage);
            Debug.Log(other.gameObject.name + " was hit!!");
        }

    }
}
