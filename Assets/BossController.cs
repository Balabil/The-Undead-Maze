using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public GameObject PlayerDestination;
    public GameObject Canvas;
    public GameObject TargetChar;
    public GameObject Gun;
    public NavMeshAgent agent;
    public RawImage image;
    public float playerSpot;
    public Transform[] waypoints;
    int wIndex;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        playerSpot = 10f;
        UpdateWaypoint();
        Gun.GetComponent<BossGun>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        float dist = Vector3.Distance(TargetChar.transform.position, PlayerDestination.transform.position);
        this.playerSpot = 12f;
            if(dist < playerSpot){

                agent.SetDestination(PlayerDestination.transform.position);
                Gun.GetComponent<BossGun>().enabled = true;

            } else {
                UpdateWaypoint();
                Gun.GetComponent<BossGun>().enabled = false;
            }
            
            if(Vector3.Distance(transform.position, target) < 1){
                UpdateWaypointIndex();
                UpdateWaypoint();
             }
            
    }

    void UpdateWaypoint() {
        target = waypoints[wIndex].position; //sets target to be the next waypoint in the array to allow for more patrolling behaviour
        agent.SetDestination(target);
    }

    void UpdateWaypointIndex(){
        wIndex++;
        if(wIndex == waypoints.Length){ //allows boss to start the patrolling process from the start
            wIndex = 0;
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "BagFace_Blue")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("hit");
        }      

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
    }
}
