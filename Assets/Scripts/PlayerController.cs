using Photon.Pun;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class PlayerController : MonoBehaviourPun
    {
        public Machine closeMachine;
        public static GameObject LocalPlayerInstance;

        private void Awake()
        {
            if (photonView.IsMine)
                PlayerController.LocalPlayerInstance = gameObject;

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
    }
}