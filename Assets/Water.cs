using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    GameObject player;
    GameObject spawnLocation;

    void Start()
    {
        player = GameObject.Find("Player");
        spawnLocation = GameObject.Find("Spawn Point");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        player.transform.SetPositionAndRotation(spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
}
