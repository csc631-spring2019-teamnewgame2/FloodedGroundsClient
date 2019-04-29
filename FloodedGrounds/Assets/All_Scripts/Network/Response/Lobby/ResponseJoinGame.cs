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
        Main.setInGame(true);

        GameObject player = GameObject.FindGameObjectWithTag(character);

        //Disble the network movement script
        player.GetComponent<NetworkMovement>().enabled = false;

        //Enable the first person camera
        foreach (Transform t in player.transform.GetComponentsInChildren<Transform>(true))
            if (t.gameObject.name == "FPS Camera")
                t.gameObject.SetActive(true);

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

        return null;
    }
}