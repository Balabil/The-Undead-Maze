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
        if(collision.gameObject.tag == "Bullet")
        {
            health = health - damage;
            collision.gameObject.SetActive(false);
        }
        if(health == 0){
            boss.SetActive(false);
        }
    }
}
