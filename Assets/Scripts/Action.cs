using System.Collections;
using UnityEngine;

public abstract class Action
{
    public GameObject player;
    public float duration;
    public abstract IEnumerator Execute();
}