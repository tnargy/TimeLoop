using System.Collections;
using UnityEngine;

public class Interact : Action
{
    GameObject target;

    public Interact(GameObject target)
    {
        this.target = target;
    }

    public override IEnumerator Execute(float deltaTime)
    {
        target.GetComponent<Console>().ConsoleButtonPressed.Invoke();
        yield return null;
    }
}