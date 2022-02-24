using UnityEngine;
using TMPro;

public class BossGun : MonoBehaviour
{
    
    public GameObject bullet;    
    public float shootForce, upwardForce, spread;   
    public bool allowButtonHold;
    bool shooting;
    public float coolDown = 1;
    public Transform point;
    public GameObject boss;

    private void Update()
    {
        /* if statement that only allows 1 bullet to be shot per frame */
        if(coolDown > 0){
            coolDown -= Time.deltaTime; 
        } else {
            Shoot();
        }
    }

    private void Shoot()
    {
        //direction of bullet
        Vector3 direction = point.position;
        //Create the bullet with correct position and direction
        GameObject currentBullet = Instantiate(bullet, point.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //rotate the bullet created to shoot it in the right direction
        currentBullet.transform.forward = direction.normalized;
        //Add a big force to the bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(boss.transform.forward.normalized * shootForce, ForceMode.Impulse);
        coolDown++;

    }
 
}
