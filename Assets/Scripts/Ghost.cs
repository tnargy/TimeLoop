using System.Collections.Generic;
using UnityEngine;

public struct Ghost
{
    public GameObject player;
    public List<Action> actions;

    public Ghost(GameObject player, List<Action> actions)
    {
        this.player = player;
        this.actions = new List<Action>();
        foreach (var a in actions)
        {
            this.actions.Add(a);
        }
    }
}