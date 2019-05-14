using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.All_Scripts.Network.Request
{
    class RequestHit : NetworkRequest
    {
        private string hitPlayer;
        private int damage;
        private int numParticles;
        private List<Vector3> particlesPositions;

        public RequestHit()
        {
            request_id = Constants.CMSG_HIT;
        }

        public void setData(string hitPlayer, int damage, int numParticles, List<Vector3> particlesPositions)
        {
            this.hitPlayer = hitPlayer;
            this.damage = damage;
            this.numParticles = numParticles;
            this.particlesPositions = particlesPositions;
        }

        public void send()
        {
            packet = new GamePacket(request_id);
            //Add the character code of the hit player
            packet.addShort16((short)Constants.CharacterToID[hitPlayer]);
            //Add the damage dealt
            packet.addShort16((short)damage);
            //Add the number of particles
            packet.addShort16((short)numParticles);

            //Add all of the particles
            foreach (Vector3 position in particlesPositions)
            {
                packet.addFloat32(position.x);
                packet.addFloat32(position.y);
                packet.addFloat32(position.z);
            }
        }
    }
}
