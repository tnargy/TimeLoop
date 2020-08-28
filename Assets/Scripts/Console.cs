using UnityEngine;
using UnityEngine.Events;

namespace GandyLabs.TimeLoop
{
    public class Console : Machine
    {
        [SerializeField] public UnityEvent ConsoleButtonPressed;

        // Update is called once per frame
        public void Interact()
        {
            ConsoleButtonPressed.Invoke();
        }

        public void ChangeConsoleButton(bool enabled)
        {
            if (enabled)
                transform.Find("Console Button").GetComponent<MeshRenderer>().material.color = Color.green;
            else
                transform.Find("Console Button").GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}