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
    public AnimationClip IdleZombie;
    public RawImage image;
    public float playerSpot;
    private bool followPlayer;
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
        followPlayer = false;
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
            
            if(Vector3.Distance(transform.position, waypoint) < 1){
                UpdateWaypointIndex();
                UpdateWaypoint();
             }
    }

    void UpdateWaypoint() {
        TargetChar.GetComponent<Animation>().Play (WalkZombie.name);
        waypoint = waypoints[wIndex].position;
        agent.SetDestination(waypoint);
    }

    void UpdateWaypointIndex(){
        wIndex = Random.Range(0,waypoints.Length-1);
        if(wIndex == waypoints.Length){
            wIndex = 0;
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.SetActive(false);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies){
                score++;
            }
            score = enemyTotal - score;
            txt.text = "Score: " + score.ToString();
            TargetChar.SetActive(false);
        }
        if(collision.gameObject.tag == "Enemy"){
            UpdateWaypointIndex();
            UpdateWaypoint();
        }        

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
    }
}
