using UnityEngine;
using System;

public class RequestJoinLobby : NetworkRequest
{
    public RequestJoinLobby()
    {
        request_id = Constants.CMSG_JOINGAME;
    }

    public void send()
    {
        packet = new GamePacket(request_id);
        Debug.Log("Requested Join Lobby");
    }
}