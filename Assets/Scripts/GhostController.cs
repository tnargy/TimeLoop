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
        pollTime += Time.deltaTime;
        if (playerRB.position != lastRecordedPosition || playerRB.rotation != lastRecordedRotation)
        {
            var move = new Move(playerRB.position, playerRB.rotation);
            actions.Enqueue(move);
            pollTime = 0;
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
        foreach (var ghost in ghosts)
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
        ghosts.Remove(ghost);
    }

    public void AddInteract(GameObject target)
    {
        if (target.name == "Console")
        {
            var interact = new Interact(target);
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
            playing = true;
            actions.Clear();
        }
    }
}
