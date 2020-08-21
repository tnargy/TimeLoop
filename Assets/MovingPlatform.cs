using Invector.vCharacterController;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    GameObject player;
    Vector3 current_target;
    int waypointIndex;

    public Vector3[] waypoints;
    public bool automatic;
    public float moveSpeed, delay_time, tolerance, delay_start;


    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            waypoints = new Vector3[1] { new Vector3(transform.position.x, transform.position.y, transform.position.z) };
        }

        current_target = waypoints[waypointIndex];
        tolerance = moveSpeed * Time.deltaTime;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.position != current_target)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void MovePlatform()
    {
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * moveSpeed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = current_target;
            delay_start = Time.time;
        }
    }
    private void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delay_start > delay_time)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        current_target = waypoints[waypointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            other.gameObject.GetComponent<vThirdPersonController>().useRootMotion = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.gameObject.GetComponent<vThirdPersonController>().useRootMotion = false;
        }
    }

}
