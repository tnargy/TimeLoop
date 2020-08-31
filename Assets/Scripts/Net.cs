using System.Collections;
using UnityEngine;

public class Net : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
            Debug.Log("Score!");
    }
}
