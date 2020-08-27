using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Respawn(other.gameObject);
        }
    }

    public static void Respawn(GameObject player)
    {
        GameObject spawnLocation = GameObject.Find("Spawn Point");
        player.SetActive(false);
        player.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
        player.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
