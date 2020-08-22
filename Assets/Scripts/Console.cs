using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Console : MonoBehaviour
{
    MeshRenderer helpText;
    public GameObject Target;
    [SerializeField] public UnityEvent ConsoleButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        helpText = GameObject.Find("HelpText").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (helpText.enabled && Input.GetButtonDown("Interact"))
        {
            ConsoleButtonPressed.Invoke();
        }
    }
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

    public void ChangeConsoleButton(bool enabled)
    {
        if (enabled)
            transform.Find("Console Button").GetComponent<MeshRenderer>().material.color = Color.green;
        else
            transform.Find("Console Button").GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
