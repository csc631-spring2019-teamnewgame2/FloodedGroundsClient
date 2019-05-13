using System;
using UnityEngine;
using UnityEngine.UI;

using Michsky.UI.FieldCompleteMainMenu;

class LobbyManager : MonoBehaviour
{
    [Header("RESOURCES")]
    public SwitchToMainPanels switchPanelMain;
    public UIElementSound soundScript;
    public Animator wrongAnimator;

    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;

    void Awake()
    {
        mainObject = GameObject.Find("MainObject");
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_JOINLOBBY, ResponseJoinLobby);
        msgQueue.AddCallback(Constants.SMSG_CREATELOBBY, ResponseCreateLobby);
        msgQueue.AddCallback(Constants.SMSG_GETLOBBIES, ResponseGetLobbies);
    }

    private void Start()
    {
        if (cManager)
        {
            cManager.setupSocket();
        }
    }

    // Lobby Retrieval Methods
    public void GetLobbies()
    {
        /*
         * send request for lobbies
         * process list returned
         */

    }

    public void ResponseGetLobbies(ExtendedEventArgs eventArgs)
    {
        ResponseGetLobbiesEventArgs args = eventArgs as ResponseGetLobbiesEventArgs;
        if (args.status == 0)
        {

        }
        else
        {
            wrongAnimator.Play("Notification In");
            soundScript.Notification();
        }

    }

    // Create Lobby Methods
    public void CreateLobby()
    {
        /*
         * Send create request
         * wait for reply
         * on reply: join lobby created - reply contains port number
         */
        
    }

    public void ResponseCreateLobby(ExtendedEventArgs eventArgs)
    {
        ResponseCreateLobbyEventArgs args = eventArgs as ResponseCreateLobbyEventArgs;
        if (args.status == 0)
        {   

        }
        else
        {
            wrongAnimator.Play("Notification In");
            soundScript.Notification();
        }
    }


    // Join Lobby Methods
    public void JoinLobby(int port)
    {
        /*
         * send join request
         * change port
         * wait for reply
         * on no reply: change port back
         * on reply: switch to lobby screen/scene 
         */
    }

    public void ResponseJoinLobby(ExtendedEventArgs eventArgs)
    {
        ResponseJoinLobbyEventArgs args = eventArgs as ResponseJoinLobbyEventArgs;
        if (args.status == 0)
        {
            
        }
        else
        {
            wrongAnimator.Play("Notification In");
            soundScript.Notification();
        }
    }

}
