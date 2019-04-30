using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    private float num1, num2;
    public RectTransform healthbar;
    //public RectTransform healthHUD;
    Animator anim;

    public SkinnedMeshRenderer rend;
    public Canvas canvas;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetBool("isDead", true);
            Debug.Log("Dead");
            num1 = Random.Range(-10.0f, 10.0f);
            num2 = Random.Range(-10.0f, 10.0f);
            StartCoroutine(Respawn());
        }

        //healthHUD.sizeDelta = new Vector2((float)(currentHealth * 1.5), healthHUD.sizeDelta.y);
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    }
    
    IEnumerator Respawn()
    {
        canvas.enabled = false;
        yield return new WaitForSeconds(2);
        rend.enabled = false;
        yield return new WaitForSeconds(3);
        this.transform.position = new Vector3(num1, 0, num2);
        anim.SetBool("isDead", false);
        anim.Play("idle");
        currentHealth = 100;
        //healthHUD.sizeDelta = new Vector2((float)(currentHealth * 1.5), healthHUD.sizeDelta.y);
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
        rend.enabled = true;
        canvas.enabled = true;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
    }
}
