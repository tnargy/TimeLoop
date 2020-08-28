using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Events;

namespace GandyLabs.TimeLoop
{
    public class MovingPlatform : MonoBehaviour
    {
        GameObject player;
        Vector3 current_target;
        int waypointIndex;

        public Vector3[] Waypoints;
        public bool Automatic;
        public float moveSpeed, tolerance;
        [SerializeField] public UnityEvent ReachedTarget;

        void Start()
        {
            if (Waypoints == null || Waypoints.Length == 0)
            {
                Waypoints = new Vector3[1] { new Vector3(transform.position.x, transform.position.y, transform.position.z) };
            }

            current_target = Waypoints[waypointIndex];
            tolerance = moveSpeed * Time.deltaTime;
        }

        void FixedUpdate()
        {
            if (transform.position != current_target)
            {
                if (player != null)
                {
                    player.GetComponent<vThirdPersonController>().useRootMotion = true;
                }

                MovePlatform();
            }
            else
            {
                if (player != null)
                {
                    player.GetComponent<vThirdPersonController>().useRootMotion = false;
                }

                ReachedTarget.Invoke();
            }
        }

        private void MovePlatform()
        {

            Vector3 heading = current_target - transform.position;
            transform.position += heading / heading.magnitude * moveSpeed * Time.deltaTime;
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
                player = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null;
                player = null;
            }
        }

        public void StartMovingPlatform(bool move)
        {
            Automatic = move;
            UpdateTarget();
        }
    }
}