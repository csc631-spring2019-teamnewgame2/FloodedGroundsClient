using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseJoinGame : NetworkResponse
{
    private string character;
    private string movementScript;
    override
    public void parse()
    {
        character = Constants.characterIDs[DataReader.ReadShort(dataStream)];
        movementScript = Constants.movementScripts[character];
    }

    override
    public ExtendedEventArgs process()
    {
        Main.setCharacter(character);
        WeaponSelectionAnim.player = GameObject.Find(character).GetComponent<MaxMovement>();
        Main.setInGame(true);

        GameObject player = GameObject.FindGameObjectWithTag(character);

        //Disble the network movement script
        player.GetComponent<NetworkMovement>().enabled = false;

        //Find the first person camera for this character
        GameObject myCamera = null;

        foreach (Transform t in player.transform.GetComponentsInChildren<Transform>(true))
            if (t.gameObject.name == "FPS Camera")
                myCamera = t.gameObject;

        //Enable the first person camera
        myCamera.SetActive(true);

        //Make all of the player tags face the camera

        //Loop through all of the characters
        foreach (string name in Constants.characterIDs.Values)
            //Skip the character being played
            if (name != Main.getCharacter())
                //Loop through all of the children of the object
                foreach (Transform t in GameObject.FindGameObjectWithTag(name).transform.GetComponentsInChildren<Transform>(true))
                    //If the child is the player status, enable the billboard script and set it's camera to this camera
                    if (t.gameObject.name == "PlayerStatus")
                    {
                        t.gameObject.SetActive(true);
                        t.gameObject.GetComponent<Billboard>().cam = myCamera.GetComponent<Camera>();
                    }

        //Enable the movement script and enable gravity for this player
        switch (movementScript)
        {
            case "MonsterMovement":
                player.GetComponent<MonsterMovement2>().enabled = true;
                player.GetComponent<MonsterMovement2>().gravityEnabled = true;
                break;
            case "PlayerMovement2":
                player.GetComponent<MaxMovement>().enabled = true;
                player.GetComponent<MaxMovement>().gravityEnabled = true;
                break;
            case "MaxMovement":
                player.GetComponent<MaxMovement>().enabled = true;
                player.GetComponent<MaxMovement>().gravityEnabled = true;
                break;
        }

        //Set the player object in the RequestPushUpdate script
        ((RequestPushUpdate)NetworkRequestTable.get(Constants.CMSG_PUSHUPDATE)).setPlayer(character);

        Debug.Log("Joined Game");

        return null;
    }
}