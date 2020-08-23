using System.Collections;
using UnityEngine;

public class Move : Action
{
    Vector3 destination;

    public Move(Vector3 destination)
    {
        this.destination = destination;
    }

    public override IEnumerator Execute()
    {
        while (player.transform.position != destination)
        {
            float step = 5f;
            player.transform.position = Vector3.MoveTowards(player.transform.position, destination, step);
            yield return new WaitForSeconds(5);
        }
        yield return "Done";
    }
}

public class Interact : Action
{
    GameObject target;

    public Interact(GameObject target)
    {
        this.target = target;
    }

    public override IEnumerator Execute()
    {
        yield return null;
    }
}