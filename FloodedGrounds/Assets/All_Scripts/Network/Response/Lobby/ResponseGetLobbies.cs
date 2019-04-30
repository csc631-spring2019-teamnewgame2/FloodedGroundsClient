using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseGetLobbies : NetworkResponse
{
    override
    public void parse()
    {

    }

    override
    public ExtendedEventArgs process()
    {
        //Debug.Log("Received Lobby List");
        //Debug.Log("Failed To Get Lobbies");
        return null;
    }
}