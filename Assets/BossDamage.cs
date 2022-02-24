using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private int damage = 10;
    private int health = 100;
    public GameObject boss;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //check if bullet has hit boss
        if(collision.gameObject.tag == "Bullet")
        {
            health = health - damage; //takes away health by 10 each time a bullet hits the boss
            collision.gameObject.SetActive(false); //removes bullet on hit
        }
        if(health == 0){ //removes boss when health hits zero
            boss.SetActive(false);
        }
    }
}
