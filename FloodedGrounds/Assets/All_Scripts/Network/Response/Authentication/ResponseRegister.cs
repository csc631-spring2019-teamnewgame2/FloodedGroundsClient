using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseRegister : NetworkResponse
{
    override
    public void parse()
    {

    }

    override
    public ExtendedEventArgs process()
    {
        //Debug.Log("Registed");
        //Debug.Log("Register Failed");
        return null;
    }
}