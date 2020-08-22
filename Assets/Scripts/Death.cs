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
        spawnLocation = GameObject.Find("Spawn Point");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        player.SetActive(false);
        player.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
        player.SetActive(true);
    }    
}
