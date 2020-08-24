using System.Collections;
using UnityEngine;

public class Move : Action
{
    Vector3 destination;
    Quaternion rotation;

    public Move(Vector3 destination, Quaternion rotation)
    {
        this.destination = destination;
        this.rotation = rotation;
    }

    public override IEnumerator Execute()
    {
        player.GetComponent<Rigidbody>().MovePosition(destination);
        player.GetComponent<Rigidbody>().MoveRotation(rotation);
        yield return new WaitForSeconds(duration);
    }
}