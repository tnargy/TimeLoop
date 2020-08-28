using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class Move : Action
    {
        Vector3 destination;
        Quaternion rotation;

        public Move(Vector3 destination, Quaternion rotation)
        {
            this.destination = destination;
            this.rotation = rotation;
        }

        public override void Execute()
        {
            player.transform.SetPositionAndRotation(destination, rotation);
        }
    }
}