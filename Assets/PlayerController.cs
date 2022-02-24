using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerDestination;
    public GameObject Canvas;
    public GameObject TargetChar;
    public NavMeshAgent agent;
    public AnimationClip WalkZombie;
    public RawImage image;
    public float playerSpot;
    public Transform[] waypoints;
    int wIndex;
    Vector3 waypoint;
    public TextMeshPro txt;
    private GameObject[] enemies;
    private int score;
    private int enemyTotal;
    // Start is called before the first frame update
    void Start()
    {
        playerSpot = 10f;
        UpdateWaypoint(); //update the waypoint of the enemy
        enemyTotal = 45;
    }

    // Update is called once per frame
    void Update()
    {   
        float dist = Vector3.Distance(TargetChar.transform.position, PlayerDestination.transform.position); //find distance between player and current enemy
        this.playerSpot = 10f; //used as detection range for the enemy
        TargetChar.GetComponent<Animation>().Play (WalkZombie.name); //set the animation of the enemy to be always walking
            if(dist < playerSpot){ //if the player is withing detection range then set the agents destination to be the players
                GetComponent<NavMeshAgent>().speed = 10f;
                agent.SetDestination(PlayerDestination.transform.position);
                TargetChar.GetComponent<Animation>().Play (WalkZombie.name);

            } else { //when player is no longer in detection range, agent goes back to patrolling to waypoint
                UpdateWaypoint();
            }
            
            if(Vector3.Distance(transform.position, waypoint) < 1){ //only updates waypoint when very close to it
                UpdateWaypointIndex();
                UpdateWaypoint();
             }
    }

    void UpdateWaypoint() {
        TargetChar.GetComponent<Animation>().Play (WalkZombie.name);
        waypoint = waypoints[wIndex].position; //sets new waypoint with acquired index
        agent.SetDestination(waypoint); //sets agents next position
    }

    void UpdateWaypointIndex(){
        wIndex = Random.Range(0,waypoints.Length); //gets a random index to allow for wandering type behaviour from the zombies
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
      
            
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.SetActive(false); //removes bullet on hit
            enemies = GameObject.FindGameObjectsWithTag("Enemy"); //find all remaining active enemies
            foreach(GameObject enemy in enemies){ //increase score for each zombie still remaining
                score++;
            }
            score = enemyTotal - score; //calculate the score by subtracting the total no. of enemies with the enemies still left alive
            txt.text = "Score: " + score.ToString(); //update the score field on the canvas
            TargetChar.SetActive(false); //remove zombie from game
        }
        if(collision.gameObject.tag == "Enemy"){ //update waypoint of zombie if it collides with zombie to prevent too much clustering 
            UpdateWaypointIndex();
            UpdateWaypoint();
        }        

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
    }
}
