using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Queue<Action> actions;
    List<GameObject> ghosts;
    int pollTime = 5;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        actions = new Queue<Action>();
        ghosts = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime % pollTime == 0)
        {
            // StartCoroutine(actions.Dequeue().Execute());
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Interact");
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Debug.Log("Move");
            }
        }
    }
}
