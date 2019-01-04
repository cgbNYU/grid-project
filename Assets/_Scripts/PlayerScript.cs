using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Player character moves in 4 directions
    //Before player moves, it Raycasts to check to see if there is a node in the direction it is moving
    //If there is, the player teleports to that location
    
    //Future: Make it so the raycasts for movement can detect if it is hitting a destructable object and have it destroy that object
    //This will avoid issues where the player moves into a destructable obstacle and then destroys that object and also gets destroyed
    
    //Future: Get rid of the player spawning stuff
    
    //Future: Restructure levels so that the player is not just warping out every time they collect all the coins
    
    //Future: Make the player have a short animation when moving. Maybe still make them invulnerable during movement, but don't just snap. Give it some juice

    //Movement Variables
    public float rayLength; //Determines how long the movement raycasts the player uses are
    public float moveSpeed; //How fast the player snaps into position
    public float cantMoveDistance; //how far the player moves wen it can't move

    //Button press variables
    public bool buttonPressedV; //used to determine if a button has been pressed. Must be false before another button can be pressed
    public bool buttonPressedH;
    private float buttonTimer; //Not sure what this timer stuff is doing
    public float buttonTimeMax;

    //Game controller
    public GameControllerScript gcScript;

    //Spawning
    public bool spawning = false;
    public GameObject playerSpawn;

    //Defeat
    public bool defeated;
    
    //Squash and Stretch Variables
    //Private variables Set in SetVariables
    public Vector3 moveSquash;
    private float moveSquashSpeed;
    public Vector3 stopSquash;
    private float stopSquashSpeed;
    private Vector3 defaultScale;
    private float resetSpeed;

	// Use this for initialization
	void Start ()
    {
        buttonPressedV = false;
        buttonPressedH = false;

        defeated = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMove();

        ButtonRelease();
	}
    
    //SetVariables
    //Uses the moveSpeed variable to determine the speed for the other parts of the animation
    public void SetVariables()
    {
        defaultScale = transform.localScale;
        
    }

    //PlayerMove
    public void PlayerMove()
    {
        if (!defeated && !spawning)
        {
            if (Input.GetAxisRaw("Vertical") > 0 && !buttonPressedV || Input.GetKeyDown(KeyCode.W)) //Up
            {
                buttonPressedV = true;
                RaycastHit hit;
                bool nodeCast = Physics.Raycast(transform.position, transform.up, out hit, rayLength, 1 << LayerMask.NameToLayer("Node"));
                if (nodeCast)
                {
                    Vector3 hitPosition = hit.transform.position;
                    MoveTween(hitPosition);
                }
                else if (!nodeCast)
                {
                    Vector3 hitPosition = new Vector3(0, cantMoveDistance, 0);
                    CantMoveTween(hitPosition);
                }
            }
            else if (Input.GetAxisRaw("Vertical") < 0 && !buttonPressedV) //Down
            {
                buttonPressedV = true;
                RaycastHit hit;
                bool nodeCast = Physics.Raycast(transform.position, -transform.up, out hit, rayLength, 1 << LayerMask.NameToLayer("Node"));
                if (nodeCast)
                {
                    Vector3 hitPosition = hit.transform.position;
                    MoveTween(hitPosition);
                }
                else if (!nodeCast)
                {
                    Vector3 hitPosition = new Vector3(0, -cantMoveDistance, 0);
                    CantMoveTween(hitPosition);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && !buttonPressedH) //Left
            {
                buttonPressedH = true;
                RaycastHit hit;
                bool nodeCast = Physics.Raycast(transform.position, -transform.right, out hit, rayLength, 1 << LayerMask.NameToLayer("Node"));
                if (nodeCast)
                {
                    Vector3 hitPosition = hit.transform.position;
                    MoveTween(hitPosition);
                }
                else if (!nodeCast)
                {
                    Vector3 hitPosition = new Vector3(-cantMoveDistance, 0, 0);
                    CantMoveTween(hitPosition);
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && !buttonPressedH) //Right
            {
                buttonPressedH = true;
                RaycastHit hit;
                bool nodeCast = Physics.Raycast(transform.position, transform.right, out hit, rayLength, 1 << LayerMask.NameToLayer("Node"));
                if (nodeCast)
                {
                    Vector3 hitPosition = hit.transform.position;
                    MoveTween(hitPosition);
                }
                else if (!nodeCast)
                {
                    Vector3 hitPosition = new Vector3(cantMoveDistance, 0, 0);
                    CantMoveTween(hitPosition);
                }
            }
        }
        else if (spawning)
        {
            if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0  && ! buttonPressedH) || (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0 && !buttonPressedV))
            {
                buttonPressedV = true;
                buttonPressedH = true;
                transform.position = playerSpawn.transform.position;
                playerSpawn.transform.position = new Vector3(-30, 0, 0);
                spawning = false;
            }
        }
        else if (defeated)
        {
            if (Input.GetAxisRaw("Submit") > 0)
            {
                Destroy(gcScript.currentObstacle);
                gcScript.SpawnObstacle();
                defeated = false;
            }
            else if (Input.GetAxisRaw("Cancel") > 0)
            {
                print("Game Over");
            }
        }
    }
    
    //MoveTween
    //When the player moves, tween to the node and squash and stretch
    public void MoveTween(Vector3 hitPosition)
    {
        //Create sequence for movement animation
        Sequence moveSequence = DOTween.Sequence();
       
        //Movement
        Tweener moveTween = DOTween.To(() => gameObject.transform.position, x => gameObject.transform.position = x,
            hitPosition, moveSpeed);
        moveTween.SetEase(Ease.OutBack);
        moveSequence.Append(moveTween);
        
        //Squash and stretch during movement
        //moveSequence.Join(transform.DOScale(moveSquash, moveSquashSpeed));

        //Squash and stretch at the end of movement
        //I'm going to need to make this play slightly before the movement ends. Need to time it out somehow. Make a variable with an equation
        //moveSequence.Append(transform.DOScale(stopSquash, stopSquashSpeed));
        
        //Reset
        //moveSequence.Append(transform.DOScale(defaultScale, resetSpeed));
    }

    public void CantMoveTween(Vector3 hitPosition)
    {
        transform.DOPunchPosition(hitPosition, moveSpeed, 10, 0);
    }

    //ButtonRelease
    public void ButtonRelease()
    {
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            buttonPressedV = false;
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            buttonPressedH = false;
        }
    }

    //PlayerDeath
    public void PlayerDeath()
    {
        defeated = true;
    }

    //Triggers
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Objective")
        {
            gcScript.ObjectiveGet();
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Obstacle")
        {
            PlayerDeath();
        }
    }
}
