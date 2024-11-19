using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDirectionSystem : MonoBehaviour
{
   
     private GameObject playerobject;
    public string DirectionOfBullet;
     public string DirectionOfMelee;

     public string DirectionOfmelwep;

     public string DirectionOfgun;

     private bool wasinteractedwith = false;

     public string stateofenemy;


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
                    weaponhurtby = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName;
                    Vector3 dir = (transform.position - player.transform.position).normalized;
                    rb.AddForce(dir * power, ForceMode2D.Impulse);
                    rb.transform.up = dir;
                    

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
                    stateofenemy = "DeadbyBull";


                }    
            else if(other.gameObject.tag =="Melee" && player.GetComponent<PlayerInteraction>()== true && player.GetComponent<PlayerInteraction>().hasthrownWeapon == false && player.GetComponent<PlayerInteraction>().hasWeapon == true )
            {
                power = origpower;
                weaponhurtby = GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName;
                Vector3 dir = (transform.position - player.transform.position).normalized;
                rb.AddForce(dir * power, ForceMode2D.Impulse);
                rb.transform.up = dir;
                

                Vector2 direction = dir;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;

                // Now you can use hitPoint and direction as needed
                

                DirectionOfMelee = GetHitDirection(angle);
                
                
                stateofenemy = "DeadbyMelSwip";
            Debug.Log("ded");
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
                                   weaponhurtby = GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName;
                                   Vector3 dir = (transform.position - player.transform.position).normalized;
                                    rb.AddForce(dir * power, ForceMode2D.Impulse);
                                    rb.transform.up = dir;


                                    Vector2 direction = dir;
                                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;

                                    DirectionOfmelwep  =   GetHitDirection(angle);
                                    
                                    stateofenemy = "DeadbyMeleeWep";
            Debug.Log("sweeped");
            }
            else if(other.gameObject.tag == "Ranged" && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true) //probably wont be needing this as this knocks down the enemy so the directionofgun is not necessary
            {
                
                  
                                power = origpower;
                                weaponhurtby = GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName;
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
        
               if (ang<= 30 && ang >= -30)
            {
                Debug.Log("North");
                return "North";
                
            }
            else if (ang < -30  && ang > 275)
            {
                Debug.Log("NorthEast");
                return "NorthEast";
                
            }

            else if (ang <= 275 && ang > 245)
            {
               Debug.Log("East");
                return "East";
            }

            else if (ang <=245  && ang > 200)
            {
                Debug.Log("SouthEast");
                return "SouthEast";
            }

            else if (ang <= 200  && ang > 150)
            {
               Debug.Log("South");
                return "South";
            }

            else if (ang <= 150 && ang > 110)
            {
                Debug.Log("SouthWest");
                return "SouthWest";
            }

            else if (ang <= 110 && ang > 70)
            {
                Debug.Log("West");
                return "West";
            }

            else if (ang <= 70 && ang > 30)
            {
                Debug.Log("NorthWest");
                return "NorthWest";
            }
            else return "Error";
        
    }
}
  

    









