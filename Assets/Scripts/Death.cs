using UnityEngine;

namespace GandyLabs.TimeLoop
{
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

            Transform trophy = player.transform.Find("Trophy");
            if (trophy != null)
            {
                GameObject goalLocation = GameObject.Find("Goal Spawn");
                trophy.SetParent(null);
                trophy.GetComponent<MeshRenderer>().enabled = true;
                trophy.position = goalLocation.transform.position;
            }
        }
    }
}