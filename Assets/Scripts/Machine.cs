using UnityEngine;

public class Machine : MonoBehaviour
{
    public MeshRenderer helpText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.enabled = true;
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
