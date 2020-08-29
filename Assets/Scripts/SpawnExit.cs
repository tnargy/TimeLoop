using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class SpawnExit : MonoBehaviourPun
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
                RPC_GameOver();
            }
        }

        [PunRPC]
        public void GameOver()
        {
            Announcement.enabled = true;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.DestroyAll();
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            StartCoroutine(End(5f));
        }

        public void RPC_GameOver() => photonView.RPC("GameOver", RpcTarget.AllBuffered);

        IEnumerator End(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.LeaveRoom();
        }
    }
}