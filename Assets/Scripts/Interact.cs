using System.Collections;
using UnityEngine;

public class Interact : Action
{
    GameObject target;

    public Interact(GameObject target)
    {
        this.target = target;
    }

    public override void Execute()
    {
        target.GetComponent<Console>().ConsoleButtonPressed.Invoke();
    }
}