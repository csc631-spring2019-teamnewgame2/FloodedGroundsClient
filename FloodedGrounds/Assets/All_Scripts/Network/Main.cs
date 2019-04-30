using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
    private static bool loggedIn = false;
    private static bool inGame = false;
    private static string character;

    public static void setLoggedIn(bool _loggedIn)
    {
        loggedIn = _loggedIn;
    }

    public static void setInGame(bool _inGame)
    {
        inGame = _inGame;
    }

    public static void setCharacter(string _character)
    {
        character = _character;
    }

    public static string getCharacter()
    {
        return character;
    }

    void Awake() {
		DontDestroyOnLoad(gameObject);
		
		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();
		
		NetworkRequestTable.init();
		NetworkResponseTable.init();
	}
	
	// Use this for initialization
	void Start () {
		//SceneManager.LoadScene ("Login");
		ConnectionManager cManager = gameObject.GetComponent<ConnectionManager>();

		if (cManager) {
			cManager.setupSocket();

            StartCoroutine(UpdateLoop(1f/Constants.updatesPerSecond));
		}
	}

	public IEnumerator UpdateLoop(float updateDelay) {
		yield return new WaitForSeconds(updateDelay);

		ConnectionManager cManager = gameObject.GetComponent<ConnectionManager>();

		if (cManager)
        {
            if (!loggedIn)
            {
                RequestLogin requestLogin = new RequestLogin();
                //Temporary credentials
                requestLogin.send("username", "password");
                cManager.send(requestLogin);
            }
            else
            {
                //Temporary: If not ingame, request access to connect to the game
                if (!inGame)
                {
                    RequestJoinGame joinGame = new RequestJoinGame();
                    joinGame.send();
                    cManager.send(joinGame); 
                }
                else
                {
                    //Create the two request objects that will be sent to the server
                    RequestHeartbeat heartbeat = new RequestHeartbeat();
                    RequestPushUpdate pushUpdate = new RequestPushUpdate();

                    //Create the messages to be sent
                    heartbeat.send();
                    pushUpdate.send();

                    //Send the messages
                    cManager.send(heartbeat);
                    cManager.send(pushUpdate);
                }
            }
		}

		StartCoroutine(UpdateLoop(updateDelay));
	}
}
