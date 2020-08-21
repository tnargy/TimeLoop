using Invector.vCharacterController;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameObject player;
    GameObject spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnLocation = GameObject.Find("Spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            other.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
            other.gameObject.SetActive(true);
        }
    }
}
