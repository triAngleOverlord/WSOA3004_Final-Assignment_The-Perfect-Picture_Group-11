using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ScoreDirectionSystem : MonoBehaviour
{
   
     private GameObject playerobject;
    public string DirectionOfBullet;
     public string DirectionOfMelee;

     public string DirectionOfmelwep;

     public string DirectionOfgun;

     private bool wasinteractedwith = false;

     public string stateofenemy;

    [SerializeField] public GameObject targetArea;
    private float perfectRadius;
    public float deadDistance;
         public SpriteRenderer img;
        public Sprite live;
        public Sprite dead;

        Rigidbody2D rb;
        public float power;
        private GameObject player;
        private float origpower;
        private GameObject weapongun;

        public string weaponhurtby;

         public enum CardinalDirection
    {
        North, Northeast, East, Southeast, South, Southwest, West, Northwest
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origpower = power;
        player = GameObject.FindGameObjectWithTag("Player");
        perfectRadius= FindRadiusOfObject();
        //Debug.Log("Radius is "+ perfectRadius.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        //checks if it was killed by a weapon
        //if (other.gameObject.transform.parent== true && other.gameObject.transform.parent.gameObject.layer == 7) //&& other.gameObject.transform.parent.name != "Cute Environment" && other.gameObject.transform.parent.gameObject.layer != 6)
        //{
            //Debug.Log(other.gameObject.name);
        if (player.GetComponent<PlayerInteraction>() == true && player.GetComponent<PlayerInteraction>().hasthrownWeapon == false && player.GetComponent<PlayerInteraction>().hasWeapon == true)// && other.gameObject.transform.parent == true && other.gameObject.transform.parent.gameObject.layer == 7)
        {
            if (other.gameObject.tag == "Bullet") //what about enemy bullets? this need to be updated
            {
                power = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponpower;
                weaponhurtby = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname;
                Vector3 dir = (transform.position - player.transform.position).normalized;
                //rb.AddForce(dir * power, ForceMode2D.Impulse);
                //rb.transform.up = dir;


                Vector2 direction = dir;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

                // Now you can use hitPoint and direction as needed

                DirectionOfBullet = GetHitDirection(angle);
                /*if (DirectionOfBullet != "Error")
                {
                    if (DirectionOfBullet == "South")
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }*/
                setTransformRotation(DirectionOfBullet);
                determineDistanceAndSnap();
                stateofenemy = "DeadbyBull";
                //EnemyController enemy = other.GetComponent<EnemyController>();
                //enemy.baseState = EnemyController.enemyState.dead;
                //enemy.deathBy = other.GetComponent<WeaponAttributes>().weaponName;
                Debug.Log("Killed by Bullet");//ded

            }
            else if (other.gameObject.tag == "Melee")//&& other.gameObject.transform.parent == true && other.gameObject.transform.parent.gameObject.layer == 7)
            {//&& player.GetComponent<PlayerInteraction>() == true && player.GetComponent<PlayerInteraction>().hasthrownWeapon == false&& player.GetComponent<PlayerInteraction>().hasWeapon == true 
                Debug.Log(other.gameObject.name);
                //power = origpower;
                Vector3 dir = (transform.position - player.transform.position).normalized;
                //rb.AddForce(dir * power, ForceMode2D.Impulse);
                //rb.transform.up = dir;


                Vector2 direction = dir;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

                // Now you can use hitPoint and direction as needed


                DirectionOfMelee = GetHitDirection(angle);
                setTransformRotation(DirectionOfMelee);
                determineDistanceAndSnap();
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.baseState = EnemyController.enemyState.dead;
                enemy.deathBy = other.GetComponent<WeaponAttributes>().weaponName;
                stateofenemy = "DeadbyMelSwip";
                Debug.Log("Killed by Melee");

            }
        }
        else if (player.GetComponent<PlayerInteraction>() == true && (other.gameObject.tag == "Melee"|| other.gameObject.tag == "Ranged") && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true && player.GetComponent<PlayerInteraction>().hasWeapon == false && other.gameObject.transform.parent == false)
        {
            Vector3 dir = (transform.position - player.transform.position).normalized;
                Vector2 direction = dir;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                DirectionOfBullet = GetHitDirection(angle);
                setTransformRotation(DirectionOfBullet);
                determineDistanceAndSnap();
            if (other.GetComponent<WeaponKnockdown>().fate == WeaponKnockdown.EnemyFate.death)
            {
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.baseState = EnemyController.enemyState.dead;
                enemy.deathBy = other.GetComponent<WeaponAttributes>().weaponName;
                stateofenemy = "DeadbyMeleeWep";
            }
            else
            {
                stateofenemy = "Knocked Out";
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.baseState = EnemyController.enemyState.knockedDown;
            }
                
            
            

            Debug.Log(stateofenemy);
        }
        
    }
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("sweeped");

        if (other.gameObject.tag =="Melee" && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true && player.GetComponent<PlayerInteraction>().hasWeapon == false )
            {
                 // MUST UPDATE AS THIS NEEDS TO TAKE INTO ACCOUNT SWORD OR BAT ie blunt or sharp
                    
                                      
                                    // Now you can use hitPoint and direction as needed
                                   power = origpower;
                                   Vector3 dir = (transform.position - player.transform.position).normalized;
                                    rb.AddForce(dir * power, ForceMode2D.Impulse);
                                    rb.transform.up = dir;


                                    Vector2 direction = dir;
                                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;

                                    DirectionOfmelwep  =   GetHitDirection(angle);
            setTransformRotation(DirectionOfmelwep);
            determineDistanceAndSnap();


                                    stateofenemy = "DeadbyMeleeWep";
            Debug.Log("sweeped");
            }
            else if(other.gameObject.tag == "Ranged" && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true) //probably wont be needing this as this knocks down the enemy so the directionofgun is not necessary
            {
                
                  
                                power = origpower;
                                Vector3 dir = (transform.position - player.transform.position).normalized;
                                //rb.AddForce(dir * power, ForceMode2D.Impulse);
                                //rb.transform.up = dir;
                                

                                Vector2 direction = dir;
                                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;
                                   

                                    // Now you can use hitPoint and direction as needed
                                Debug.Log("Hit");
                                    

                                
                                
                                
                    
            }

    }
    
*/
   private string GetHitDirection(float ang)
    {
        //Debug.Log(ang);
            if (ang<= 30 && ang > -30)
            {
                Debug.Log("North");
            transform.rotation = Quaternion.Euler(0, 0, 0);
                return "South";
                
            }
            else if (ang <= -30  && ang > -60)//275
            {
                Debug.Log("NorthEast");
                return "SouthWest";
                
            }

            else if ((ang <= 270 && ang > 240) || (ang <=-60 && ang >= -90))
            {
               Debug.Log("East");
                return "West";
            }

            else if (ang <=240  && ang > 210)
            {
                Debug.Log("SouthEast");
                return "NorthWest";
            }

            else if (ang <= 210  && ang > 150)
            {
               Debug.Log("South");
                return "North";
            }

            else if (ang <= 150 && ang > 120)
            {
                Debug.Log("SouthWest");
                return "NorthEast";
            }

            else if (ang <= 120 && ang > 60)
            {
                Debug.Log("West");
                return "East";
            }

            else if (ang <= 60 && ang > 30)
            {
                Debug.Log("NorthWest");
                return "SouthEast";
            }
            else return "Error";
        
    }

    private void setTransformRotation(string dir)
    {
        if (dir == "South")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
        else if (dir == "SouthWest")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        else if (dir == "West")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        else if (dir == "NorthWest")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        else if (dir == "North")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
        else if (dir == "NorthEast")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        else if (dir == "East")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 90);
        else if (dir == "SouthEast")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 45f);

        //Debug.Log("after: " + transform.rotation);
    }

    private void determineDistanceAndSnap()
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)targetArea.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetArea.transform.position);
        //Debug.Log(distance);

        // Determine which range the distance falls into and snap
        if (distance >= 0 && distance < (perfectRadius-0.1f))
        {
            SnapToDistance(direction, 0.7f);
            deadDistance = 0.7f;
            //Debug.Log("A little too close");
        }
        else if (distance >= (perfectRadius - 0.1f) && distance < (perfectRadius + 0.1f))//perfect distance
        {
            SnapToDistance(direction, 0f);
            deadDistance = 0f;
            //Debug.Log("Perfect");
        }
        else if (distance >= (perfectRadius + 0.1f) && distance < (perfectRadius + 0.2f))
        {
            SnapToDistance(direction, 1.5f);
            deadDistance = 1.5f;
            //Debug.Log("A little too far");
        }
        else
        {
            deadDistance = distance;
            Debug.Log("Body is too far from target area");
        }
    }

    private void SnapToDistance(Vector2 direction, float snapDistance)
    {
        // Set the position while maintaining the angle
        transform.position = (Vector2)targetArea.transform.position + direction * snapDistance;
        //float distance = Vector2.Distance(transform.position, targetArea.transform.position);
        //Debug.Log("Snaping");
    }

    public float FindRadiusOfObject()
    {
        Bounds bounds;
        Renderer renderer = targetArea.GetComponent<Renderer>();
        if (renderer != null)
        {
            bounds = renderer.bounds;
        }
        else
        {
            Debug.LogWarning("No Collider2D or Renderer found on this object!");
            return 0; // Default to object's position
        }
        // The bottom position in world coordinates
    Vector2 bottomPosition = new Vector2(bounds.center.x, bounds.min.y);
        float distance = Vector2.Distance(targetArea.transform.position, bottomPosition);
        return distance;
    }

   

   
}

  

    









