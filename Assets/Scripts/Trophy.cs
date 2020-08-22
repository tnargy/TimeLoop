using UnityEngine;
using UnityEngine.Events;

public class Trophy : MonoBehaviour
{
    [SerializeField] public UnityEvent CollectTrophy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectTrophy.Invoke();
        }
    }

    public void Cleanup()
    {
        Destroy(gameObject);
    }
}
