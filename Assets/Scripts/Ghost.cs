using System.Collections.Generic;
using UnityEngine;

public struct Ghost
{
    public GameObject player;
    public Queue<Action> actions;

    public Ghost(GameObject player, Queue<Action> actions)
    {
        this.player = player;
        this.actions = new Queue<Action>();
        foreach (var a in actions)
        {
            this.actions.Enqueue(a);
        }
    }
}