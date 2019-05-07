using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    //Determine what state the game is in
    private static bool loggedIn = false;
    private static bool inLobby = false;
    private static bool inGame = false;
    private static string character = null;
    private static ConnectionManager cManager;

    //How long in seconds the client will wait for a response before sending the request again
    private const int requestDelay = 1;
    //The times when login or joinGame were last requested
    float lastLogin = 0;
    float lastJoinGame = 0;

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

    public static bool joinLobby(Lobby lobby)
    {
        // make sure the player can actually join a lobby
        if (!loggedIn || inLobby || inGame) return false;
        


        return true;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.AddComponent<MessageQueue>();
        gameObject.AddComponent<ConnectionManager>();

        NetworkRequestTable.init();
        NetworkResponseTable.init();
    }

    // Use this for initialization
    void Start()
    {
        //SceneManager.LoadScene ("Login");
        cManager = gameObject.GetComponent<ConnectionManager>();

        if (cManager)
        {
            cManager.setupSocket();

            StartCoroutine(UpdateLoop(1f / Constants.updatesPerSecond));
        }
    }

    public IEnumerator UpdateLoop(float updateDelay)
    {
        yield return new WaitForSeconds(updateDelay);

        if (cManager)
        {
            //If the user is not logged in and enough time has passed since the last request, send a login request
            if (!loggedIn)
            {
                if (Time.time > lastLogin + requestDelay)
                {
                    //Create the request
                    RequestLogin requestLogin = (RequestLogin)NetworkRequestTable.get(Constants.CMSG_LOGIN);
                    //Populate it with the temporary credentials
                    requestLogin.send("Temp2", "Temp");
                    //Send the request
                    cManager.send(requestLogin);
                    //Set the last login time
                    lastLogin = Time.time;
                }
            }
            else
            {
                //If the user is not in the game and enough time has passed since the request, send a join game request
                if (!inGame)
                {
                    if (Time.time > lastJoinGame + requestDelay)
                    {
                        RequestJoinGame joinGame = (RequestJoinGame)NetworkRequestTable.get(Constants.CMSG_JOINGAME);
                        joinGame.send();
                        cManager.send(joinGame);

                        //Set the last join game time
                        lastJoinGame = Time.time;
                    }
                }
                else
                {
                    //Create the two request objects that will be sent to the server
                    RequestHeartbeat heartbeat = (RequestHeartbeat)NetworkRequestTable.get(Constants.CMSG_HEARTBEAT);
                    RequestPushUpdate pushUpdate = (RequestPushUpdate)NetworkRequestTable.get(Constants.CMSG_PUSHUPDATE);

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