using TMPro;
using UnityEngine;

public class SpawnExit : MonoBehaviour
{
    public TextMeshProUGUI Announcement;

    public void EnableExit()
    {
        transform.GetComponent<CapsuleCollider>().enabled = true;
        transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Over");       
    }
}
