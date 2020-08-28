using System.Collections;
using TMPro;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class SpawnExit : MonoBehaviour
    {
        public TextMeshProUGUI Announcement;
        private Color defaultColor = Color.white;

        public void EnableExit()
        {
            transform.GetComponent<CapsuleCollider>().enabled = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        public void DisableExit()
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<MeshRenderer>().material.color = defaultColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.transform.Find("Trophy") != null)
            {
                StartCoroutine(GameOver());
            }
        }

        IEnumerator GameOver()
        {
            Announcement.GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(5);
            GameManager.Instance.LeaveRoom();
        }
    }
}