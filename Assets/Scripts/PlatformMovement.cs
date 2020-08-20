using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool IsMoving;
    public GameObject[] Waypoints;
    int waypointIndex = 0;
    float moveSpeed = 5;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            float tolerance = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, GetWaypointPosition(), tolerance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }

    Vector3 GetWaypointPosition()
    {
        if (AtWaypoint())
        {
            waypointIndex = (waypointIndex + 1) % Waypoints.Length;
        }

        return Waypoints[waypointIndex].transform.position;
    }

    bool AtWaypoint()
    {
        if (Vector3.Distance(transform.position, Waypoints[waypointIndex].transform.position) <= .5f)
        {
            IsMoving = false;
            return true;
        }

        return false;
    }
}
