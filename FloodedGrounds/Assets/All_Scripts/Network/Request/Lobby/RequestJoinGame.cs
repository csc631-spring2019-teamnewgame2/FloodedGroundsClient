using UnityEngine;
using System;

public class RequestJoinGame : NetworkRequest
{
    public RequestJoinGame()
    {
        request_id = Constants.CMSG_JOINGAME;
    }

    public void send()
    {
        packet = new GamePacket(request_id);
        Debug.Log("Requested Join Game");
    }
}