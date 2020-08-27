using UnityEngine;

public abstract class Action
{
    public GameObject player;
    public float waitTime;
    public abstract void Execute();
}