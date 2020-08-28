using UnityEngine;
using UnityEngine.Events;

namespace GandyLabs.TimeLoop
{
    public class Trophy : MonoBehaviour
    {
        [SerializeField] public UnityEvent CollectTrophy;

        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Goal").GetComponent<SpawnExit>().DisableExit();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CollectTrophy.Invoke();
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.SetParent(other.transform);
            }
        }
    }
}