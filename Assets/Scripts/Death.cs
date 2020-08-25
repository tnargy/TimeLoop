using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    GameObject player;
    GameObject spawnLocation;
    public bool restart = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnLocation = GameObject.Find("Spawn Point");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.name == "Player")
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (restart)
            Restart();

        player.SetActive(false);
        player.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
        player.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
