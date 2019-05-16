﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHit : NetworkResponse
{
    private string attackingPlayer;
    private string hitPlayer;
    private int damage;
    private int numParticles;
    private List<Vector3> particlePositions;
    private List<Vector3> particleAngles;

    override
    public void parse()
    {
        particlePositions = new List<Vector3>();
        particleAngles = new List<Vector3>();

        attackingPlayer = hitPlayer = Constants.IDtoCharacter[DataReader.ReadShort(dataStream)];
        hitPlayer = Constants.IDtoCharacter[DataReader.ReadShort(dataStream)];
        damage = DataReader.ReadShort(dataStream);
        numParticles = DataReader.ReadShort(dataStream);

        //Get all of the positions and angles
        for (int i = 0; i < numParticles; i++)
        {
            particlePositions.Add(new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream)));
            particleAngles.Add(new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream)));
        }
    }

    override
    public ExtendedEventArgs process()
    {
        Debug.Log(hitPlayer + " got hit for " + damage);

        ParticleSystem blood = null;

        if (attackingPlayer != Constants.MONSTER)
        {
            GameObject player = GameObject.FindGameObjectsWithTag(attackingPlayer)[0];
            blood = player.transform.Find("FPS Camera").gameObject.GetComponent<ShootRaycast>().monsterBlood;
        }

        for(int i = 0; i < numParticles; i++)
        {
            // spawn monsterBlood particle effect, then destroy clone gameObject
            Quaternion angle = Quaternion.Euler(particleAngles[i].x, particleAngles[i].y, particleAngles[i].z);
            Object.Destroy(Object.Instantiate(blood.gameObject, particlePositions[i], angle), 2f);
        }

        return null;
    }
}
