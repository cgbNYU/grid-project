using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControllerScript : MonoBehaviour {

    //Controls spawning of the player and score/progression objects
    
    //Might simplify all spawning and stuff to just me putting obstacles into scenes and then loading scene to scene
    //That might slow things down a lot, but it's possible if I end up with too many obstacles in a single, infinite
    //scene it would also be slow. Maybe make the obstacles more self reliant and just instantiate them instead of this
    //object pool stuff. It's a little complex and I am not sure this game will be taxing enough for it to matter.\
    
    //Future: Have this script just handle scoring, menus, and coin drops. Make authored levels that either run off
    //a unique script, or that just have obstacles placed in the scene
    
    public GameObject player;

    public GameObject playerSpawner;

    //Player Spawning
    public bool playerIsAlive; //A lot of these variables don't do anything
    private float playerSpawnTimer;
    public float playerSpawnTimeMax;
    public float readyTime;
    public float goTime;

    private bool playerIsSpawning;

    public PlayerScript pScript;

    //Nodes
    public GameObject node1; //Probably need to keep these around so I can spawn coins on the nodes
    public GameObject node2;
    public GameObject node3;
    public GameObject node4;
    public GameObject node5;
    public GameObject node6;
    public GameObject node7;
    public GameObject node8;
    public GameObject node9;

    //Button
    public GameObject button; //Not used now, but I should have a more proper menu

    //Text
    public Text startText; //Ditto to the above menu stuff
    public string readyString;
    public string goString;
    private string emptyString;

    ////Obstacles and Objectives

    //Obstacle
    public GameObject currentObstacle; //Why only one obstacle? This stuff is really restrictive.
    public GameObject obSpawn;

    //Objectives
    public GameObject objective;
    public int obCount;

    //Spinner
    public GameObject spinner;
    private SpinnerObstacleScript spinnerScript;

    //Slice
    public GameObject slice;
    private SliceObstacleScript sliceScript;

    //Obstacle Enumerator
    enum ObstacleState {Spinner, Slice}

    int obStateCount;

	// Use this for initialization
	void Start ()
    {
        pScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        obStateCount = Enum.GetNames(typeof(ObstacleState)).Length;
        print(obStateCount.ToString());
        SpawnObstacle();
	}

    //ObjectiveGet
    public void ObjectiveGet()
    {
        obCount--;
        if (obCount <= 0)
        {
            Destroy(currentObstacle);
            SpawnObstacle();
        }
    }

    //SpawnObstacle
    public void SpawnObstacle()
    {
        ObstacleState whichObstacle = (ObstacleState)UnityEngine.Random.Range(0, obStateCount);

        SpawnPlayer();

        if (whichObstacle == ObstacleState.Spinner)
        {
            SpinnerSpawn();
        }
        else if (whichObstacle == ObstacleState.Slice)
        {
            SliceSpawn();
        }
    }

    //SpawnObjective
    public void SpawnObjective(int nodeNum)
    {
        if (nodeNum == 1)
        {
            obCount++;
            Instantiate(objective, node1.transform.position, node1.transform.rotation);
        }
        else if (nodeNum == 2)
        {
            obCount++;
            Instantiate(objective, node2.transform.position, node2.transform.rotation);
        }
        else if (nodeNum == 3)
        {
            obCount++;
            Instantiate(objective, node3.transform.position, node3.transform.rotation);
        }
        else if (nodeNum == 4)
        {
            obCount++;
            Instantiate(objective, node4.transform.position, node4.transform.rotation);
        }
        else if (nodeNum == 5)
        {
            obCount++;
            Instantiate(objective, node5.transform.position, node5.transform.rotation);
        }
        else if (nodeNum == 6)
        {
            obCount++;
            Instantiate(objective, node6.transform.position, node6.transform.rotation);
        }
        else if (nodeNum == 7)
        {
            obCount++;
            Instantiate(objective, node7.transform.position, node7.transform.rotation);
        }
        else if (nodeNum == 8)
        {
            obCount++;
            Instantiate(objective, node8.transform.position, node8.transform.rotation);
        }
        else if (nodeNum == 9)
        {
            obCount++;
            Instantiate(objective, node9.transform.position, node9.transform.rotation);
        }
    }

    //SpawnPlayer
    public void SpawnPlayer()
    {
        pScript.spawning = true;
        player.transform.position = new Vector3(-30, 0, 0);
    }

    
    //Obstacle Functions


    //SpinnerSpawn
    public void SpinnerSpawn()
    {
        currentObstacle = Instantiate(spinner, obSpawn.transform.position, obSpawn.transform.rotation);
    }


    //Slice Spawn
    public void SliceSpawn()
    {
        currentObstacle = Instantiate(slice, obSpawn.transform.position, obSpawn.transform.rotation);
    }
}