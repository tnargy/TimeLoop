using Photon.Pun;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class PlayerController : MonoBehaviourPun
    {
        public static GameObject LocalPlayerInstance;
        public Machine closeMachine;
        public Transform spawnLocation;

        private void Awake()
        {
            if (photonView.IsMine || !PhotonNetwork.IsConnected)
                PlayerController.LocalPlayerInstance = gameObject;

            spawnLocation = GameManager.Instance.spawnLocations.Dequeue();

            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (closeMachine != null && Input.GetButtonUp("Interact") && photonView.IsMine)
            {
                closeMachine.SendMessage("Interact");
                GetComponentInChildren<GhostController>().AddInteract(closeMachine.gameObject);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.spawnLocations.Enqueue(spawnLocation);
        }
    }
}