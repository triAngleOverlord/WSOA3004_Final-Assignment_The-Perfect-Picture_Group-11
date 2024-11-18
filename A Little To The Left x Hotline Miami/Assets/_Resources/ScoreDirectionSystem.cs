using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        


        if (other.gameObject.tag == "Bullet" && player.GetComponent<PlayerInteraction>().hasthrownWeapon == false && player.GetComponent<PlayerInteraction>().hasWeapon == true ) //what about enemy bullets? this need to be updated
                {
                    power = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponpower;
                    weaponhurtby = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname;
                    Vector3 dir = (transform.position - player.transform.position).normalized;
                    //rb.AddForce(dir * power, ForceMode2D.Impulse);
                    //rb.transform.up = dir;
                    

                    Vector2 direction = dir;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;
           
                    // Now you can use hitPoint and direction as needed
    
                    DirectionOfBullet = GetHitDirection(angle);
                    if(DirectionOfBullet != "Error")
                    {
                        if(DirectionOfBullet == "South")
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
            setTransformRotation(DirectionOfBullet);
            determineDistanceAndSnap();
            stateofenemy = "DeadbyBull";
            //Debug.Log("Bullet");//ded

        }    
            else if(other.gameObject.tag =="Melee" && player.GetComponent<PlayerInteraction>()== true && player.GetComponent<PlayerInteraction>().hasthrownWeapon == false && player.GetComponent<PlayerInteraction>().hasWeapon == true && other.gameObject.transform.parent.name != "weaponPos" )
            {
                power = origpower;
                Vector3 dir = (transform.position - player.transform.position).normalized;
                //rb.AddForce(dir * power, ForceMode2D.Impulse);
                //rb.transform.up = dir;
                

                Vector2 direction = dir;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;

                // Now you can use hitPoint and direction as needed
                

                DirectionOfMelee = GetHitDirection(angle);
            setTransformRotation(DirectionOfMelee);
            determineDistanceAndSnap();

            stateofenemy = "DeadbyMelSwip";
            //Debug.Log(other.gameObject.name);//ded
            }
            
            
    }

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
                                rb.AddForce(dir * power, ForceMode2D.Impulse);
                                rb.transform.up = dir;
                                

                                Vector2 direction = dir;
                                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;
                                   

                                    // Now you can use hitPoint and direction as needed
                                Debug.Log("Hit");
                                    

                                
                                
                                
                    
            }

    }
    

   private string GetHitDirection(float ang)
    {
        
            if (ang<= 30 && ang > -30)
            {
                Debug.Log("North");
            transform.rotation = Quaternion.Euler(0, 0, 0);
                return "North";
                
            }
            else if (ang <= -30  && ang > -60)//275
            {
                Debug.Log("NorthEast");
                return "NorthEast";
                
            }

            else if ((ang <= 270 && ang > 240) || (ang <=-60 && ang >= -90))
            {
               Debug.Log("East");
                return "East";
            }

            else if (ang <=240  && ang > 210)
            {
                Debug.Log("SouthEast");
                return "SouthEast";
            }

            else if (ang <= 210  && ang > 150)
            {
               Debug.Log("South");
                return "South";
            }

            else if (ang <= 150 && ang > 120)
            {
                Debug.Log("SouthWest");
                return "SouthWest";
            }

            else if (ang <= 120 && ang > 60)
            {
                Debug.Log("West");
                return "West";
            }

            else if (ang <= 60 && ang > 30)
            {
                Debug.Log("NorthWest");
                return "NorthWest";
            }
            else return "Error";
        
    }

    private void setTransformRotation(string dir)
    {
        //Debug.Log("before: " + rb.transform.rotation);
        if (dir == "North")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (dir == "NorthEast")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        else if (dir == "East")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        else if (dir == "SouthEast")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        else if (dir == "South")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
        else if (dir == "SouthWest")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        else if (dir == "West")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 90);
        else if (dir == "NorthWest")
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 45f);

        //Debug.Log("after: " + transform.rotation);
    }

    private void determineDistanceAndSnap()
    {
        Vector2 direction = (transform.position - targetArea.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetArea.transform.position);
        Debug.Log(distance);

        // Determine which range the distance falls into and snap
        if (distance >= 0 && distance < 0.7)
        {
            SnapToDistance(direction, 0.7f);
            Debug.Log("A little too close");
        }
        else if (distance >= 0.7 && distance < 1.5)//perfect distance
        {
            SnapToDistance(direction, 0);
            Debug.Log("Perfect");
        }
        else if (distance >= 1.5 && distance < 2)
        {
            SnapToDistance(direction, 1.5f);
            Debug.Log("A little too far");
        }
        else
            Debug.Log("Body is too far from target area");
    }

    private void SnapToDistance(Vector2 direction, float snapDistance)
    {
        // Set the position while maintaining the angle
        transform.position = (Vector2)targetArea.transform.position + direction * snapDistance;
        float distance = Vector2.Distance(transform.position, targetArea.transform.position);
        Debug.Log("Snaping");
    }
}

  

    









