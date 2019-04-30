using System.Collections.Generic;

public class Constants {

    //Constants
    public static readonly string CLIENT_VERSION = "1.00";
    public static readonly string REMOTE_HOST = "13.52.133.88";
    //public static readonly string REMOTE_HOST = "localhost";
    public static readonly int REMOTE_PORT = 9252;
    public static readonly int updatesPerSecond = 30;

    //Net code
    //Request: 1xx
    //Response: 2xx

    //General APIs:    x0x
    public static readonly short CMSG_HEARTBEAT = 101;
    public static readonly short SMSG_HEARTBEAT = 201;
    public static readonly short CMSG_PUSHUPDATE = 102;

    //Authentication:  x1x
    public static readonly short CMSG_REGISTER = 111;
    public static readonly short SMSG_REGISTER = 211;
    public static readonly short CMSG_LOGIN = 112;
    public static readonly short SMSG_LOGIN = 212;

    //Lobby APIs:      x2x
    public static readonly short CMSG_GETLOBBIES = 121;
    public static readonly short SMSG_GETLOBBIES = 221;
    public static readonly short CMSG_CREATELOBBY = 122;
    public static readonly short SMSG_CREATELOBBY = 222;
    public static readonly short CMSG_JOINLOBBY = 123;
    public static readonly short SMSG_JOINLOBBY = 223;
    public static readonly short CMSG_STARTGAME = 124;
    public static readonly short SMSG_STARTGAME = 224;
    public static readonly short CMSG_JOINGAME = 125;
    public static readonly short SMSG_JOINGAME = 225;



    //Inventory Items

    //Guns:        1xx
    //Grenades:    2xx



    //Actions

    //Other
    public static readonly string IMAGE_RESOURCES_PATH = "Images/";
	public static readonly string PREFAB_RESOURCES_PATH = "Prefabs/";
	public static readonly string TEXTURE_RESOURCES_PATH = "Textures/";
	
	//GUI Window IDs
	public enum GUI_ID {
		Login
	};

	public static int USER_ID = -1;



    //Character constants
    public static string MONSTER = "Bog_lord";
    public static string GIRL = "Izzy";
    public static string GUY1 = "Max";
    public static string GUY2 = "Winston";

    //Dictionary to map ids to the characters
    public static Dictionary<int, string> characterIDs;

    //Animation Parameters
    public static readonly string[] generalAnimParams = { "isJumping", "isWalking" };
    public static readonly string[] monsterAnimParams = { "isDead", "isAttacking", "isHit", "isShouting" };
    public static readonly string[] girlAnimParams = { "isDead", "isShooting" };
    public static readonly string[] maxAnimParams = { "isShooting", "isRunning", "isForward", "isBackward", "isLeft", "isRight" };

    //Dictionary to map characters to animation parameters
    public static Dictionary<string, string[]> characterAnimations;

    //Dictionary to map characters to movement scripts
    public static Dictionary<string, string> movementScripts;


    //Static constructor to populate the dictionarys
    static Constants()
    {
        characterIDs = new Dictionary<int, string>();
        characterIDs.Add(0, MONSTER);
        characterIDs.Add(1, GIRL);
        characterIDs.Add(2, GUY1);
        characterIDs.Add(3, GUY2);

        characterAnimations = new Dictionary<string, string[]>();
        characterAnimations.Add(MONSTER, monsterAnimParams);
        characterAnimations.Add(GIRL, girlAnimParams);
        characterAnimations.Add(GUY1, maxAnimParams);
        characterAnimations.Add(GUY2, maxAnimParams);

        movementScripts = new Dictionary<string, string>();
        movementScripts.Add(MONSTER, "MonsterMovement");
        movementScripts.Add(GIRL, "PlayerMovement2");
        movementScripts.Add(GUY1, "MaxMovement");
        movementScripts.Add(GUY2, "MaxMovement");
    }
}