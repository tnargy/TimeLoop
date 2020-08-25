using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Queue<Action> actions;
    List<Ghost> ghosts;
    GameObject player;
    Rigidbody playerRB;
    float pollTime;
    bool playing;
    Vector3 lastRecordedPosition;
    Quaternion lastRecordedRotation;

    public Ghost[] Ghosts { get => ghosts.ToArray(); }

    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody>();
        actions = new Queue<Action>();
        ghosts = new List<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.position != lastRecordedPosition || playerRB.rotation != lastRecordedRotation)
        {
            var move = new Move(playerRB.position, playerRB.rotation);
            actions.Enqueue(move);
            lastRecordedPosition = playerRB.position;
            lastRecordedRotation = playerRB.rotation;
        }
        
        if (playing)
        {
            Play();
        }
    }

    void Play()
    {
        foreach (var ghost in Ghosts)
        {
            StartCoroutine(StartGhost(ghost));
        }
        playing = false;
    }

    IEnumerator StartGhost(Ghost ghost)
    {
        while (ghost.actions.Count > 0)
        {
            var action = ghost.actions.Dequeue();
            action.Execute();
            yield return new WaitForFixedUpdate();
        }
        Destroy(ghost.player);
    }

    public void AddInteract(GameObject target)
    {
        if (target.name == "Console")
        {
            var interact = new Interact(target);
            actions.Enqueue(interact);
        }
        else
        {
            target.transform.Find("HelpText").GetComponent<MeshRenderer>().enabled = false;
            foreach (var ghost in Ghosts)
            {
                SpawnGhost(ghost.actions);
            }
            ghosts.Add(SpawnGhost(actions));
            playing = true;
            actions.Clear();
        }
    }

    private Ghost SpawnGhost(Queue<Action> actions)
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
        return g;
    }
}
