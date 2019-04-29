using UnityEngine;
using System;

public class RequestGetLobbies : NetworkRequest
{
    public RequestGetLobbies()
    {
        request_id = Constants.CMSG_GETLOBBIES;
    }

    public void send()
    {
        packet = new GamePacket(request_id);
    }
}