using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliding : MonoBehaviour
{
    private bool removeBullet;
    private float coolDown;

    void Start() {
        coolDown = 1;
        removeBullet = false;
    }
    void Update(){
        /*check for boolean to start the timer for the bullet to be removed from the level*/
        if(removeBullet == true){
            coolDown -= Time.deltaTime; 
        }
        if(coolDown < 0){
            this.gameObject.SetActive(false);
            coolDown++;
            removeBullet = false;
        }
    }
    // Start is called before the first frame update
     void OnCollisionEnter(Collision collision)
    {
        /* set the gravity of the bullet back on if it collides with any obstacles bar the characters */
        if(collision.transform.tag != "Player" && collision.transform.tag != "Boss"){ 
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        //checks if bullet has collided with floor to start the process to remove the bullet after a few seconds
        if(collision.transform.name == "Floor"){
            removeBullet = true;
        }
    }
}
