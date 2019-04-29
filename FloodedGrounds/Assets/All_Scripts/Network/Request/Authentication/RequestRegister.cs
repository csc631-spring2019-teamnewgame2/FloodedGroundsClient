using UnityEngine;
using System;

public class RequestRegister : NetworkRequest
{
    public RequestRegister()
    {
        request_id = Constants.CMSG_REGISTER;
    }

    public void send()
    {
        packet = new GamePacket(request_id);
    }
}