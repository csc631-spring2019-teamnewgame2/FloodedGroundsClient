using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHit : NetworkResponse
{
    private string attackingPlayer;
    private string hitPlayer;
    private int damage;
    private int numParticles;
    private List<Vector3> particlePositions;

    override
    public void parse()
    {
        particlePositions = new List<Vector3>();

        attackingPlayer = hitPlayer = Constants.IDtoCharacter[DataReader.ReadShort(dataStream)];
        hitPlayer = Constants.IDtoCharacter[DataReader.ReadShort(dataStream)];
        damage = DataReader.ReadShort(dataStream);
        numParticles = DataReader.ReadShort(dataStream);

        for (int i = 0; i < numParticles; i++)
            particlePositions.Add(new Vector3(DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream), DataReader.ReadFloat(dataStream)));
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

        foreach(Vector3 position in particlePositions)
        {
            // spawn monsterBlood particle effect, then destroy clone gameObject
            var rot = Quaternion.FromToRotation(Vector3.up, new Vector3(0.7f, 0f, 0.7f));
            Object.Destroy(Object.Instantiate(blood.gameObject, position, rot), 2f);
        }

        return null;
    }
}
