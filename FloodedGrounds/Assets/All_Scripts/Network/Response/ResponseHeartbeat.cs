using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHeartbeat : NetworkResponse
{
    override
    public void parse()
    {
    }

    override
    public ExtendedEventArgs process()
    {
        short numPlayers = DataReader.ReadShort(dataStream);

        for (int i = 0; i < numPlayers; i++)
        {
            //Get the character this update is for
            string character = Constants.characterIDs[DataReader.ReadShort(dataStream)];
            GameObject player = GameObject.FindWithTag(character);

            //Get the parameters for the character model
            Vector3 position = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
            Vector3 lookAngle = new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream));
            float rotation = DataReader.ReadFloat(dataStream);
            //Update the model
            player.GetComponent<NetworkMovement>().updateParams(position, lookAngle, rotation);

            //Get the animator for the player
            Animator animator = player.GetComponent<Animator>();
            
            //Set the speed of the animation
            animator.SetFloat("Speed", DataReader.ReadFloat(dataStream));
            
            //Set the animation parameters based on the player
            foreach (string parameter in Constants.characterAnimations[character])
                animator.SetBool(parameter, DataReader.ReadBool(dataStream));


            //Get the length of the inventory array
            int inventoryLength = DataReader.ReadShort(dataStream);

            //Read all of the intentory items
            for (int j = 0; j < inventoryLength; j++)
                DataReader.ReadShort(dataStream);

            //Get the length of the actions array
            int actionsLength = DataReader.ReadShort(dataStream);

            //Read all of the actions
            for (int j = 0; j < actionsLength; j++)
                DataReader.ReadShort(dataStream);
        }

        return null;
    }
}