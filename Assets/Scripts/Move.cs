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

    public override IEnumerator Execute(float deltaTime)
    {
        while (Vector3.Distance(player.transform.position, destination) > 0.1f)
        {
            float speed = 5 * deltaTime;
            Vector3 moveposition = player.transform.position;
            moveposition.x = Mathf.MoveTowards(player.transform.position.x, destination.x, speed);
            moveposition.z = Mathf.MoveTowards(player.transform.position.z, destination.z, speed);
            player.GetComponent<Rigidbody>().MovePosition(moveposition);
            player.GetComponent<Rigidbody>().MoveRotation(rotation);
            yield return new WaitForSeconds(5);
        }
        player.transform.position = destination;
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

    public override IEnumerator Execute(float deltaTime)
    {
        yield return null;
    }
}