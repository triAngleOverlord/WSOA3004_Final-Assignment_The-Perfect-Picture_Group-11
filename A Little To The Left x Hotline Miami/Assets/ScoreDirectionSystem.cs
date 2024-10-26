using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDirectionSystem : MonoBehaviour
{
    Transform player;
     private GameObject playerobject;
    public string DirectionOfBullet;
     public string DirectionOfMelee;

     public string DirectionOfmelwep;

     public string DirectionOfgun;

     private bool wasinteractedwith = false;

     public string stateofenemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerobject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Ranged")
        {
             
            if(playerobject.GetComponent<PlayerInteraction>().hasthrownWeapon == true && wasinteractedwith == false)
                {   
                                Vector2 hitPoint =  (player.position - transform.position).normalized; // Closest point of contact
                                Vector2 directiontobesent = (hitPoint - (Vector2)transform.position).normalized;

                                // Now you can use hitPoint and direction as needed
                                Debug.Log("Hit point: " + hitPoint);

                               DirectionOfgun = GetHitDirection(directiontobesent);
                               
                               
                }
        }
        else if(other.tag =="Melee")
        {
            if(playerobject.GetComponent<PlayerInteraction>().hasthrownWeapon == true && wasinteractedwith == false ) // MUST UPDATE AS THIS NEEDS TO TAKE INTO ACCOUNT SWORD OR BAT ie blunt or sharp
                {
                                wasinteractedwith = true;   
                                Vector2 hitPoint =  (player.position - transform.position).normalized; // Closest point of contact
                                Vector2 directiontobesent = (hitPoint - (Vector2)transform.position).normalized;

                                // Now you can use hitPoint and direction as needed
                                Debug.Log("Hit point: " + hitPoint);

                                DirectionOfmelwep  =   GetHitDirection(directiontobesent);
                                
                                stateofenemy = "DeadbyMeleeWep";
                }
        }
        else if (other.tag == "Bullet" && wasinteractedwith == false)
        {
            wasinteractedwith = true;
           Vector2 hitPoint = other.ClosestPoint(transform.position); // Closest point of contact
           Vector2 directiontobesent = (hitPoint - (Vector2)transform.position).normalized;

            // Now you can use hitPoint and direction as needed
            Debug.Log("Hit point: " + hitPoint);

           DirectionOfBullet = GetHitDirection(directiontobesent);
           stateofenemy = "DeadbyBull";

        }else if(other.tag == "Melee" && wasinteractedwith == false)
        {
            wasinteractedwith = true;
            Vector2 hitPoint =  (player.position - transform.position).normalized; // Closest point of contact
            Vector2 directiontobesent = (hitPoint - (Vector2)transform.position).normalized;

            // Now you can use hitPoint and direction as needed
            Debug.Log("Hit point: " + hitPoint);

           DirectionOfMelee = GetHitDirection(directiontobesent);
           stateofenemy = "DeadbyMelSwip";
        }
    }

    string GetHitDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

if (angle >= -22.5 && angle < 22.5)
{
    Debug.Log("East");
    return "East";
}
else if (angle >= 22.5 && angle < 67.5)
{
    Debug.Log("Northeast");
    return "Northeast";
}
else if (angle >= 67.5 && angle < 112.5)
{
    Debug.Log("North");
    return "North";
}
else if (angle >= 112.5 && angle < 157.5)
{
    Debug.Log("Northwest");
    return "Northwest";
}
else if (angle >= 157.5 || angle < -157.5)
{
    Debug.Log("West");
    return "West";
}
else if (angle >= -157.5 && angle < -112.5)
{
    Debug.Log("Southwest");
    return "Southwest";
}
else if (angle >= -112.5 && angle < -67.5)
{
    Debug.Log("South");
    return "South";
}
else
{
    Debug.Log("Southeast");
    return "Southeast";
}
    }


}
