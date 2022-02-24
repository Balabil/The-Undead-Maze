using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script handles all interactions with objects (mainly obstacles such as boxes) around the level, additionally this script contains the win condition*/
public class PickUp : MonoBehaviour

{

    public float moveForce = 250f;
    public float pickUpRange = 5f;
    public float idetifyRange = 50f;
    public GameObject Boss;
    public Collider key_collider;
    private GameObject objInHand;
    private GameObject heldObj2;
    public Transform holdParent;
    public Transform holdParent2;
    private string nameObject;
    private bool hitObject;
    public GUI pog;
    private bool key;

    // Update is called once per frame
    
    void Start(){
        key = false; //initially set key boolean to false (important for key boolean)
    }

    void Update()

    {
            hitObject = false;
            RaycastHit hit2;
            /* Sends out ray from the camera attatched to my player*/                                
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit2, idetifyRange))

                {
                        /*Checks if the ray hit a movable object*/
                        if(hit2.collider.gameObject.tag != "Untagged" && hit2.collider.gameObject.tag != "Obstacle" && hit2.collider.gameObject.tag != "Enemy" && hit2.collider.gameObject.tag != "Bullet")
                        {
                            //sets hit object to true to allow the gui to show the user they can interact with the object
                        hitObject = true;
                        nameObject = "Press E"; 
                            if(hit2.collider.gameObject.name == "rust_key"){ //checks if the ray hit the key to allow the user to more easily identify the object when picking it up
                                nameObject = "Rusty Key"; //updates name in GUI so the user is able to differentiate the key from a different object
                            }
                        }
                        //if statement that handles the win condition, the player must be in possession of the key, have found and interacted with the door and killed the boss monster
                        if(hit2.collider.gameObject.name == "CloseDoor" && key == true && Input.GetKeyDown(KeyCode.E) && Boss.activeSelf == false){
                            Cursor.lockState = CursorLockMode.None;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            
                    }
                } 
        

        if (Input.GetKeyDown(KeyCode.E)) //E is used to interact with the objects 
        {
            if (objInHand == null) //check if nothing is currenty in users hand 
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange)) //casts another ray 

                {      
                    grabObject(hit.transform.gameObject); //calls method to grab the object the player is looking at 
                } 
            }
            else{
               releaseObject(); //calls method to release the object that is currently in users hand
            }
        }

        if (objInHand != null) //if there is currently an object in the users hand then update the position of the object accordingly

        {
            updateObject();
        }
    }

    void OnGUI()
    {
        if (hitObject == true) //check to see if user is looking at an object they can interact with
        {
 
            GUI.Box(new Rect(Screen.width/2, Screen.height/2, Screen.width/10, Screen.height/35), nameObject);
        }
    }
    void updateObject()

    {
        
        if (Vector3.Distance(objInHand.transform.position, holdParent.position) > 0.1f) //check if the object is over 0.1f away from the players hold position 

        {
            Vector3 moveDirection = holdParent.position - objInHand.transform.position; //find the direction the obstacle needs to move in get to the desired position
            objInHand.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce); //add force to the obstacle to move it as all objects are rigidbodies to allow for proper physics
        }
    }

    /* function to grab the object the player is looking at */
    void grabObject(GameObject pickObj)

    {
        if (pickObj.GetComponent<Rigidbody>() && pickObj.tag != "Enemy" && pickObj.tag != "Boss") //check if the object is a rigidbody and not an enemy of some sort
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false; //turn off the objects gravity to allow for a smoother experience while holding the object
            objRig.drag = 30f; //add aditional drag to alleviate some of the shaking that occurs when holding a rigidbody object
            if(pickObj.name == "rust_key"){ //check if the player wants to grab the key
                pickObj.transform.position = new Vector3(holdParent2.transform.position.x,holdParent2.transform.position.y,holdParent2.transform.position.z); //update the position of the key to something specific
                objRig.transform.parent = holdParent2; //change the object to be a child of the player object
                key_collider.enabled = !key_collider.enabled; //remove the collider for the key to allow the key to be held by the player model without casuing issue
                objRig.isKinematic = true; //change the rigidbody of the key to be a kinematic rigidbody
                key = true; //set the boolean for the key to be true, which allows for part of the win condition to be completed
            } else {
                /*all other objects simply become children of the player object and update the boolean value of something being in the players hand to be true*/
                objRig.transform.parent = holdParent; 
                objInHand = pickObj;
            }
        }

    }


    /* function to release the object */
    void releaseObject()

    {
        objInHand.GetComponent<Rigidbody>().useGravity = true; //turn gravity back on for the object
        objInHand.GetComponent<Rigidbody>().drag = 1f; //decrease the drag of the object
        objInHand.transform.parent = null; //object is no longer a child object
        objInHand = null; //updates boolean to show there is no longer an object in the players hand
    }


}
