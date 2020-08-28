using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public abstract class Action
    {
        public GameObject player;
        public float waitTime;
        public abstract void Execute();
    }
}