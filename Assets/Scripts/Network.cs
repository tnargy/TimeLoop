using Photon.Pun;
using UnityEngine;

public class Network : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log($"We are connected to the { PhotonNetwork.CloudRegion} server!");
    }
}
