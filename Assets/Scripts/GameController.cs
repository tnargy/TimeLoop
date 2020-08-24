using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Queue<Action> actions;
    List<Ghost> ghosts;
    GameObject player;

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
        if (!Input.GetButtonDown("Interact"))
        {
            actions.Enqueue(new Move(player.transform.position, player.transform.rotation));
        }

        foreach (var ghost in ghosts)
        {
            Action action = ghost.actions.Dequeue();
            StartCoroutine(action.Execute(Time.deltaTime));
        }
    }

    public void AddInteract(GameObject target)
    {
        if (target.name == "Console")
        {
            actions.Enqueue(new Interact(target));
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
        }
    }
}
