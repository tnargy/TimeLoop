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
            transform.SetParent(other.transform);
        }
    }

    public void Cleanup()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Death").GetComponent<Death>().restart = true;
    }
}
