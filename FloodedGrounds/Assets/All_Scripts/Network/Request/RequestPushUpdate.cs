﻿using UnityEngine;
using System;

public class RequestPushUpdate : NetworkRequest
{
    //A reference to the player object
    private GameObject player;

    public RequestPushUpdate()
    {
        request_id = Constants.CMSG_PUSHUPDATE;
        //Get the player object based on the character string assigned by the server
        player = GameObject.FindWithTag(Main.getCharacter());
    }

    public void send()
    {
        packet = new GamePacket(request_id);

        //Add the player's position
        packet.addFloat32(player.transform.position.x);
        packet.addFloat32(player.transform.position.y);
        packet.addFloat32(player.transform.position.z);

        //Placeholder values
        //Add the angle that the player is looking at
        packet.addFloat32(0f);
        packet.addFloat32(0f);
        packet.addFloat32(0f);

        //Add the y rotation of the player
        packet.addFloat32(player.transform.rotation.eulerAngles.y);

        //Get the animation controller
        Animator animator = player.GetComponent<Animator>();

        //Add the speed of the animation
        packet.addFloat32(animator.GetFloat("Speed"));
        
        //Add the shared parameters
        foreach(string param in Constants.generalAnimParams)
            packet.addBool(animator.GetBool(param));

        //Add the parameters for the specific character
        foreach (string param in Constants.characterAnimations[Main.getCharacter()])
            packet.addBool(animator.GetBool(param));

        //Placeholders for inventory and actions arrays
        short[] inventory = { };
        short[] actions = { };

        //Add the size and the values of the inventory array
        packet.addShort16((short)inventory.Length);

        foreach (short item in inventory)
            packet.addShort16(item);

        //Add the size and the values of the action array
        packet.addShort16((short)actions.Length);

        foreach (short action in actions)
            packet.addShort16(action);

        return;
    }
}