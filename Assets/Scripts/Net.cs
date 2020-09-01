using GandyLabs.TimeLoop;
using Photon.Pun;
using UnityEngine;

public class Net : MonoBehaviourPun
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Score!");
            GameManager.Instance.Score(transform);
        }
    }
}
