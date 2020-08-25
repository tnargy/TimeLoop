using UnityEngine;

public class Machine : MonoBehaviour
{
    public MeshRenderer helpText;
    public bool isPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.enabled = true;
            isPlayer = other.name.Equals("Player");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.enabled = false;
        }
    }
}
