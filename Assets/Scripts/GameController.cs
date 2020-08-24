using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Queue<Action> actions;
    List<Ghost> ghosts;
    GameObject player;
    float pollTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        actions = new Queue<Action>();
        ghosts = new List<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        pollTime += Time.deltaTime;
        if (!Input.GetButtonDown("Interact"))
        {
            var move = new Move(player.transform.position, player.transform.rotation);
            move.duration = pollTime;
            actions.Enqueue(move);
            pollTime = 0;
        }

        foreach (var ghost in ghosts)
        {
            Action action = ghost.actions.Dequeue();
            StartCoroutine(action.Execute());
        }
    }

    public void AddInteract(GameObject target)
    {
        if (target.name == "Console")
        {
            var interact = new Interact(target);
            interact.duration = pollTime;
            actions.Enqueue(interact);
            pollTime = 0;
        }
        else
        {
            GameObject spawnLocation = GameObject.Find("Spawn Point");
            var ghostObj = Instantiate((GameObject)Resources.Load("Ghost"));
            ghostObj.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
            for (int i = 0; i < actions.Count; i++)
            {
                var action = actions.Dequeue();
                action.player = ghostObj;
                actions.Enqueue(action);
            }
            Ghost g = new Ghost(ghostObj, actions);
            ghosts.Add(g);
            pollTime = 0;
        }
    }
}
