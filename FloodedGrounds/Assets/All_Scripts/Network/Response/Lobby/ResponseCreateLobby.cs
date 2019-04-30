using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseCreateLobby : NetworkResponse
{
    override
    public void parse()
    {

    }

    override
    public ExtendedEventArgs process()
    {
        //Debug.Log("Lobby Created");
        //Debug.Log("Failed To Create Lobby");
        return null;
    }
}