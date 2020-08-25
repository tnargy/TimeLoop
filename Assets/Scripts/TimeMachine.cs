using UnityEngine;
using UnityEngine.Events;

public class TimeMachine : Machine
{
    [SerializeField] public UnityEvent ThruSpaceAndTime;

    // Update is called once per frame
    void Update()
    {
        if (helpText.enabled && Input.GetButtonDown("Interact") && isPlayer)
        {
            helpText.enabled = false;
            ThruSpaceAndTime.Invoke();
        }
    }
}
