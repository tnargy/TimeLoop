using GandyLabs.TimeLoop;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class Machine : MonoBehaviour
    {
        public MeshRenderer helpText;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                helpText.enabled = true;
                other.GetComponent<PlayerController>().closeMachine = this;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                helpText.enabled = false;
                other.GetComponent<PlayerController>().closeMachine = null;
            }
        }
    }
}