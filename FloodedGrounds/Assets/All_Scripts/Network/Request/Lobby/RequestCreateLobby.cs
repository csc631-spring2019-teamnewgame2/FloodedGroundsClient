using UnityEngine;

using System;

public class RequesCreateLobby : NetworkRequest
{

    public RequesCreateLobby()
    {
        request_id = Constants.CMSG_CREATELOBBY;
    }

    public void send()
    {
        packet = new GamePacket(request_id);
        Debug.Log("Requested Create Lobby");
    }
}