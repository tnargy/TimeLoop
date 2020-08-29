using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GandyLabs.TimeLoop
{
    public class GhostController : MonoBehaviourPun
    {
        List<Action> actions;
        List<Ghost> ghosts;
        Rigidbody playerRB;
        float pollTime;
        bool playing;
        Vector3 lastRecordedPosition;
        Quaternion lastRecordedRotation;

        // Start is called before the first frame update
        void Start()
        {
            playing = false;
            playerRB = transform.parent.GetComponent<Rigidbody>();
            actions = new List<Action>();
            ghosts = new List<Ghost>();
        }

        // Update is called once per frame
        void Update()
        {
            pollTime += Time.deltaTime;
            if (Vector3.Distance(playerRB.position, lastRecordedPosition) > 0.01f || playerRB.rotation != lastRecordedRotation)
            {
                var move = new Move(playerRB.position, playerRB.rotation)
                {
                    waitTime = pollTime
                };
                actions.Add(move);
                lastRecordedPosition = playerRB.position;
                lastRecordedRotation = playerRB.rotation;
                pollTime = 0;
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
                yield return new WaitForSeconds(action.waitTime);
            }
            Destroy(ghost.player);
        }

        public void AddInteract(GameObject target)
        {
            if (target.name == "Console")
            {
                var interact = new Interact(target)
                {
                    waitTime = pollTime
                };
                actions.Add(interact);
                pollTime = 0;
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
                Death.Respawn(transform.parent.gameObject);
            }
        }

        private Ghost SpawnGhost(List<Action> actions)
        {
            GameObject spawnLocation = GameObject.Find("Spawn Point");
            // var ghostObj = Instantiate((GameObject)Resources.Load("Ghost"), transform.parent);
            // ghostObj.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
            var ghostObj = PhotonNetwork.Instantiate("Ghost", spawnLocation.transform.position, spawnLocation.transform.rotation, 0);
            for (int i = 0; i < actions.Count; i++)
            {
                var action = actions[i];
                action.player = ghostObj;
            }
            Ghost g = new Ghost(ghostObj, actions);
            return g;
        }
    }
}