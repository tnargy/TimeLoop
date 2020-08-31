using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GandyLabs.TimeLoop
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Photon Callbacks

        private void Start()
        {
            Instance = this;

            spawnLocations = new Queue<Transform>();
            foreach (var spawn in GameObject.FindGameObjectsWithTag("Respawn"))
            {
                spawnLocations.Enqueue(spawn.transform);
            }

            if (playerPrefab == null)
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            else
            {
                if (PlayerController.LocalPlayerInstance == null)
                {
                    Debug.Log($"We are Instantiating LocalPlayer from {SceneManagerHelper.ActiveSceneName}");
                    // var player = PhotonNetwork.Instantiate(playerPrefab.name, spawnLocations.Peek().position, spawnLocations.Peek().rotation, 0);
                    var player = Instantiate(playerPrefab, spawnLocations.Peek().position, spawnLocations.Peek().rotation);

                    playerList.Add(player);
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
        public Queue<Transform> spawnLocations;

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        /// <summary>
        /// When ball enters net, score is called
        /// </summary>
        /// <param name="net">Name of gameobject Net or Net(1)</param>
        public void Score(string net)
        {
            foreach (var player in playerList)
            {
                
            }
        }

        [PunRPC]
        public void GameOver(string msg)
        {
            Announcement.text = msg;
            Announcement.enabled = true;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.DestroyAll();
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            StartCoroutine(End(5f));
        }

        public void RPC_GameOver()
        {
            photonView.RPC("GameOver", RpcTarget.OthersBuffered, "You Lose");
        }

        #endregion

        #region Private Methods

        private List<GameObject> playerList;
        private TextMeshProUGUI Announcement;

        IEnumerator End(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.LeaveRoom();
        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Trying to Load a level but we are not the master Client");
            }
            Debug.Log($"Loading level... Player Count: {PhotonNetwork.CurrentRoom.PlayerCount}");
            PhotonNetwork.LoadLevel("Soccer");
        }

        #endregion
    }
}