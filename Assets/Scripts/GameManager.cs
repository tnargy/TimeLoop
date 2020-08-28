using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace GandyLabs.TimeLoop
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Photon Callbacks

        private void Start()
        {
            Instance = this;

            if (playerPrefab == null)
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            else
            {
                if (PlayerController.LocalPlayerInstance == null)
                {
                    Debug.Log($"We are Instantiating LocalPlayer from {SceneManagerHelper.ActiveSceneName}");
                    GameObject spawnLocation = GameObject.Find("Spawn Point");
                    PhotonNetwork.Instantiate(playerPrefab.name, spawnLocation.transform.position, spawnLocation.transform.rotation, 0);
                }
                else
                    Debug.Log($"Ignoring scene load for {SceneManagerHelper.ActiveSceneName}");
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"Welcome {newPlayer.NickName}");

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log($"IsMasterClient: {PhotonNetwork.IsMasterClient}");

                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"Goodbye {otherPlayer.NickName}");

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log($"IsMasterClient: {PhotonNetwork.IsMasterClient}");

                LoadArena();
            }
        }

        #endregion

        #region Public Methods

        public static GameManager Instance;
        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Trying to Load a level but we are not the master Client");
            }
            Debug.Log($"Loading level... Player Count: {PhotonNetwork.CurrentRoom.PlayerCount}");
            PhotonNetwork.LoadLevel("_SCENE_");
        }

        #endregion
    }
}