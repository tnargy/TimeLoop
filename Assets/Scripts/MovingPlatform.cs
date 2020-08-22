using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Events;

public class MovingPlatform : MonoBehaviour
{
    GameObject player;
    Vector3 current_target;
    int waypointIndex;
    bool playerOnPlatform;

    public Vector3[] Waypoints;
    public bool Automatic;
    public float moveSpeed, tolerance;
    [SerializeField] public UnityEvent OnReachedTarget;

    void Start()
    {
        if (Waypoints == null || Waypoints.Length == 0)
        {
            Waypoints = new Vector3[1] { new Vector3(transform.position.x, transform.position.y, transform.position.z) };
        }

        current_target = Waypoints[waypointIndex];
        tolerance = moveSpeed * Time.deltaTime;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.position != current_target)
        {
            if (playerOnPlatform)
            {
                player.GetComponent<vThirdPersonController>().useRootMotion = true;
            }
            
            MovePlatform();
        }
        else
        {
            if (playerOnPlatform)
            {
                player.GetComponent<vThirdPersonController>().useRootMotion = false;
            }

            OnReachedTarget.Invoke();
        }
    }

    private void MovePlatform()
    {
        
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * moveSpeed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = current_target;
        }
    }
    private void UpdateTarget()
    {
        if (Automatic)
        {
            NextPlatform();
        }
    }

    public void NextPlatform()
    {
        waypointIndex = (waypointIndex + 1) % Waypoints.Length;
        current_target = Waypoints[waypointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            playerOnPlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            playerOnPlatform = false;
        }
    }

    public void StartMovingPlatform(bool move)
    {
        Automatic = move;
        UpdateTarget();
    }
}
