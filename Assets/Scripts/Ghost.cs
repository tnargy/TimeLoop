using System.Collections.Generic;
using UnityEngine;

public struct Ghost
{
    public GameObject player;
     public Queue<Action> actions;
}