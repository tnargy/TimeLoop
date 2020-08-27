using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GandyLabs.TimeLoop
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [Tooltip("The max number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        #endregion

        #region Private Fields

        string gameVersion = "1";

        #endregion

        #region MonoBehavior Callbacks

        private void Awake()
        {
            // #Critical
            // This makes sure we can use PhotonNetowrk.LoadLevel() on master client and all other clients sync
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log($"We are connected to the { PhotonNetwork.CloudRegion} server!");
            
            // #Critical
            // The first we try to do is to join a potential existing room. 
            // If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.LogWarning($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"No random rooms available.  Creating one now.");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Now this client is in a room.");
        }

        #endregion

        #region Public Methods


        [Tooltip("The UI Panel to let the user enter name, connect and player")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;
        
        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }


        #endregion
    }
}