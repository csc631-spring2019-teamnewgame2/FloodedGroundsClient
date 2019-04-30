using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseJoinLobby : NetworkResponse
{
    override
    public void parse()
    {

    }

    override
    public ExtendedEventArgs process()
    {
        //Debug.Log("Joined Lobby");
        //Debug.Log("Failed To Join Lobby");
        return null;
    }
}