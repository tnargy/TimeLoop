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

        private void Start()
        {
            CameraWork _cameraWork = GetComponent<CameraWork>();

            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (closeMachine != null && Input.GetButtonDown("Interact") && photonView.IsMine)
            {
                closeMachine.SendMessage("Interact");
                GetComponentInChildren<GhostController>().AddInteract(closeMachine.gameObject);
            }
        }
    }
}