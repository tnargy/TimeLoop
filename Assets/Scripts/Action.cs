using System.Collections;
using UnityEngine;

public abstract class Action
{
    public GameObject player;
    public abstract IEnumerator Execute();
}