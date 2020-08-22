using System.Collections;
using UnityEngine;

public class Move : Action
{
    Vector3 destination;
    float duration;

    public Move(Vector3 destination, float duration)
    {
        this.destination = destination;
        this.duration = duration;
    }

    public override IEnumerator Execute()
    {
        while (player.transform.position != destination)
        {
            yield return null;
        }
        yield return null;
    }
}