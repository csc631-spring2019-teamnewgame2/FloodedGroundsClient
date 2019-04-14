using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame updat

    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200;
    public float fireRate = 0.1f;

    public int currentBullets;

    public Transform shootPoint;
    public ParticleSystem muzzleFlash;

    float fireTimer;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentBullets = bulletsPerMag;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(currentBullets > 0)
            {
                Fire();
            }

            else
            {
                Reload();
            }
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets<=0) return;

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleFlash.Play();
        //anim.SetBool("Fire", true);
        currentBullets--;
        fireTimer = 0.0f;
    }

    private void Reload()
    {
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }
}
