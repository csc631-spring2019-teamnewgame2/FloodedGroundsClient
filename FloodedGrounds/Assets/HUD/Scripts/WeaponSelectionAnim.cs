﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionAnim : MonoBehaviour {

    public Animator weaponPanelAnimator;
    public List<GameObject> weaponButtons = new List<GameObject>();

    private GameObject currentWeapon;
    private GameObject selectedWeapon;

    private GameObject currentButton;
    private GameObject nextButton;

    public int currentButtonIndex = 3;
    private int nextButtonIndex = 3;

    private Animator currentWeaponAnimator;

    private Animator currentButtonAnimator;
    private Animator nextButtonAnimator;

    public ManagementHUD hud;
    public static MaxMovement player;

    void Start ()
    {
        nextButton = weaponButtons[nextButtonIndex];
        nextButtonAnimator = nextButton.GetComponent<Animator>();
        nextButtonAnimator.Play("WS Fade-in");
        hud = GameObject.Find("MainObject").GetComponent<ManagementHUD>();
    }

    void Update()
    {
        // For Pistol 
        if (Input.GetKeyDown("1"))
        {
            if (HasGun("Pistol"))
            {
                currentButton = weaponButtons[currentButtonIndex];
                nextButtonIndex = 0;
                nextButton = weaponButtons[nextButtonIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                currentButtonAnimator.Play("WS Fade-out");

                nextButtonAnimator = nextButton.GetComponent<Animator>();
                nextButtonAnimator.Play("WS Fade-in");
                currentButtonIndex = 0;

                weaponPanelAnimator.Play("WS Fade-in");
            }
            
        }

        // For AK-47
        else if (Input.GetKeyDown("2"))
        {
            if (HasGun("AK-47"))
            {
                currentButton = weaponButtons[currentButtonIndex];
                nextButtonIndex = 1;
                nextButton = weaponButtons[nextButtonIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                currentButtonAnimator.Play("WS Fade-out");

                nextButtonAnimator = nextButton.GetComponent<Animator>();
                nextButtonAnimator.Play("WS Fade-in");
                currentButtonIndex = 1;

                weaponPanelAnimator.Play("WS Fade-in");
            }
            
        }

        //For Shotgun
        else if (Input.GetKeyDown("3"))
        {
            if (HasGun("Shotgun"))
            {
                currentButton = weaponButtons[currentButtonIndex];
                nextButtonIndex = 2;
                nextButton = weaponButtons[nextButtonIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                currentButtonAnimator.Play("WS Fade-out");

                nextButtonAnimator = nextButton.GetComponent<Animator>();
                nextButtonAnimator.Play("WS Fade-in");
                currentButtonIndex = 2;

                weaponPanelAnimator.Play("WS Fade-in");
            }
            
        }

        //For Knife/Melee
        else if (Input.GetKeyDown("4"))
        {
            player.equipped = null;
            currentButton = weaponButtons[currentButtonIndex];
            nextButtonIndex = 3;
            nextButton = weaponButtons[nextButtonIndex];

            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play("WS Fade-out");

            nextButtonAnimator = nextButton.GetComponent<Animator>();
            nextButtonAnimator.Play("WS Fade-in");
            currentButtonIndex = 3;

            weaponPanelAnimator.Play("WS Fade-in");
        }
    }

    private bool HasGun(string gunType)
    {
        foreach (GameObject gun in player.heldGuns)
        {
            if (gun.tag == gunType)
            {
                player.equipped = gun;
                return true;
            }
        }
        return false;
    }
}