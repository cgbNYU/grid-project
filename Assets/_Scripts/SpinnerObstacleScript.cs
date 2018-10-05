using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerObstacleScript : MonoBehaviour {

    //Rotates in a single direction
    //Enter a negative speed to reverse rotation

    public float speed;

    public GameObject player;
    public GameObject playerSpawn;

    public Vector3 playerStart;

    //Objectives
    public bool obNode1;
    public bool obNode2;
    public bool obNode3;
    public bool obNode4;
    public bool obNode5;
    public bool obNode6;
    public bool obNode7;
    public bool obNode8;
    public bool obNode9;

    public GameControllerScript gcScript;

    //Animation
    Animator animator;

    void Start()
    {
        gcScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        player = GameObject.Find("Player");
        playerSpawn = GameObject.Find("PlayerSpawn");

        animator = GetComponent<Animator>();
        animator.speed = speed;

        Spawn();
    }

    //Spawn
    public void Spawn()
    {
        //Move player into position
        playerSpawn.transform.position = playerStart;

        //Just run down the list of bools and spawn those ones
        //Add 1 to the obCount each spawn
        if (obNode1)
        {
            gcScript.SpawnObjective(1);
        }
        if (obNode2)
        {
            gcScript.SpawnObjective(2);
        }
        if (obNode3)
        {
            gcScript.SpawnObjective(3);
        }
        if (obNode4)
        {
            gcScript.SpawnObjective(4);
        }
        if (obNode5)
        {
            gcScript.SpawnObjective(5);
        }
        if (obNode6)
        {
            gcScript.SpawnObjective(6);
        }
        if (obNode7)
        {
            gcScript.SpawnObjective(7);
        }
        if (obNode8)
        {
            gcScript.SpawnObjective(8);
        }
        if (obNode9)
        {
            gcScript.SpawnObjective(9);
        }
    }
}
