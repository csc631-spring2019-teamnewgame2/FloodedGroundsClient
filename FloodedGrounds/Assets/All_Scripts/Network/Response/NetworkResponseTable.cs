using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkResponseTable {

	public static Dictionary<short, Type> responseTable { get; set; }
	
	public static void init() {
		responseTable = new Dictionary<short, Type>();
        add(Constants.SMSG_HEARTBEAT, "ResponseHeartbeat");

        add(Constants.SMSG_REGISTER, "ResponseRegister");
        add(Constants.SMSG_LOGIN, "ResponseLogin");

        add(Constants.SMSG_GETLOBBIES, "ResponseGetLobbies");
        add(Constants.SMSG_CREATELOBBY, "ResponseCreateLobby");
        add(Constants.SMSG_JOINLOBBY, "ResponsetJoinLobby");
        add(Constants.SMSG_STARTGAME, "ResponseStartGame");
        add(Constants.SMSG_JOINGAME, "ResponseJoinGame");
    }
	
	public static void add(short response_id, string name) {
		responseTable.Add(response_id, Type.GetType(name));
	}
	
	public static NetworkResponse get(short response_id) {
		init ();
		NetworkResponse response = null;
		if (responseTable.ContainsKey(response_id)) {
			response = (NetworkResponse) Activator.CreateInstance(responseTable[response_id]);
			response.response_id = response_id;
		} else {
			Debug.Log("Response [" + response_id + "] Not Found");
		}
		
		return response;
	}
}