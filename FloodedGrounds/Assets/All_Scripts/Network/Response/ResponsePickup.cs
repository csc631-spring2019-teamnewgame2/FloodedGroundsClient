using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsePickup : NetworkResponse
{
    //The name of the object picked up
    private string pickupItem;

    override
    public void parse()
    {
        pickupItem = DataReader.ReadString(dataStream);
    }

    override
    public ExtendedEventArgs process()
    {
        //Disable the object
        GameObject.Find(pickupItem).SetActive(false);

        Debug.Log(pickupItem + " was picked up");

        return null;
    }
}