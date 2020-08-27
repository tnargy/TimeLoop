using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Machine closeMachine;

    // Update is called once per frame
    void Update()
    {
        if (closeMachine != null && Input.GetButtonDown("Interact"))
        {
            closeMachine.SendMessage("Interact");
            GetComponentInChildren<GhostController>().AddInteract(closeMachine.gameObject);
        }
    }
}
