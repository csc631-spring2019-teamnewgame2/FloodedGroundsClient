using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkRequestTable {

	public static Dictionary<short, Type> requestTable { get; set; }
	
	public static void init() {
		requestTable = new Dictionary<short, Type>();
        add(Constants.CMSG_HEARTBEAT, "RequestHeartbeat");
        add(Constants.CMSG_PUSHUPDATE, "RequestPushUpdate");

        add(Constants.CMSG_REGISTER, "RequestRegister");
        add(Constants.CMSG_LOGIN, "RequestLogin");

        add(Constants.CMSG_GETLOBBIES, "RequestGetLobbies");
        add(Constants.CMSG_CREATELOBBY, "RequestCreateLobby");
        add(Constants.CMSG_JOINLOBBY, "RequestJoinLobby");
        add(Constants.CMSG_STARTGAME, "RequestStartGame");
        add(Constants.CMSG_JOINGAME, "RequestJoinGame");
    }
	
	public static void add(short request_id, string name) {
		requestTable.Add(request_id, Type.GetType(name));
	}
	
	public static NetworkRequest get(short request_id) {
		NetworkRequest request = null;
		
		if (requestTable.ContainsKey(request_id)) {
			request = (NetworkRequest) Activator.CreateInstance(requestTable[request_id]);
			request.request_id = request_id;
		} else {
			Debug.Log("Request [" + request_id + "] Not Found");
		}
		
		return request;
	}
}
