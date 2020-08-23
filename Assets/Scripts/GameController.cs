﻿using System.Collections.Generic;
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
            actions.Enqueue(new Move(player.transform.position));
        }

        foreach (var ghost in ghosts)
        {
            foreach (var action in ghost.actions)
            {
                StartCoroutine(action.Execute());
            }
        }
    }

    public void AddInteract(GameObject target)
    {
        if (target.name == "Console")
            actions.Enqueue(new Interact(target));
        else
        {
            Ghost g = new Ghost();
            g.actions = actions.ToList<Action>();
            g.player = Instantiate((GameObject)Resources.Load("Ghost.prefab"),
                GameObject.Find("Spawn Point").transform.position,
                GameObject.Find("Spawn Point").transform.rotation);
            ghosts.Add(g);
            actions.Clear();
        }
    }
}
