using TMPro;
using UnityEngine;

public class SpawnExit : MonoBehaviour
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
            Debug.Log("Game Over");       
    }
}
