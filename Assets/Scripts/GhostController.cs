using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    List<Action> actions;
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
        actions = new List<Action>();
        ghosts = new List<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.position != lastRecordedPosition || playerRB.rotation != lastRecordedRotation)
        {
            var move = new Move(playerRB.position, playerRB.rotation);
            actions.Add(move);
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
        foreach (var action in ghost.actions)
        {
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
            actions.Add(interact);
        }
        else
        {
            target.transform.Find("HelpText").GetComponent<MeshRenderer>().enabled = false;
            foreach (var ghost in ghosts)
            {
                SpawnGhost(ghost.actions);
            }
            ghosts.Add(SpawnGhost(actions));
            playing = true;
            actions.Clear();
        }
    }

    private Ghost SpawnGhost(List<Action> actions)
    {
        GameObject spawnLocation = GameObject.Find("Spawn Point");
        var ghostObj = Instantiate((GameObject)Resources.Load("Ghost"));
        ghostObj.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
        for (int i = 0; i < actions.Count; i++)
        {
            var action = actions[i];
            action.player = ghostObj;
        }
        Ghost g = new Ghost(ghostObj, actions);
        return g;
    }
}
