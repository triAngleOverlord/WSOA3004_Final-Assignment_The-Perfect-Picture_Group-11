using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class ScorTrack : MonoBehaviour
{
    
        Rigidbody2D rb;
        
        private GameObject player;

        private GameObject scoreobj;
        
        private GameObject weapongun;

        public string weaponhurtbyscore;

        [SerializeField] GameObject DesiredGoal;
        [SerializeField] GameObject[] furntiurewanted;

        [SerializeField] string DeathbyWhat;

        [SerializeField] string WhatDirection;

        [SerializeField] bool DoWeWantFurniture;

        private bool AlreadyDead;

        

    // Start is called before the first frame update
    void Start()
    {
        AlreadyDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreobj = GameObject.FindGameObjectWithTag("scoring");
        
    }

    // Update is called once per frame
    
    public void DeathBYBULLET()
    {
        float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        Debug.Log(distanceToTarget);
        if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyBull" && AlreadyDead != true)
        {
            AlreadyDead =true; 
            Debug.Log("WE IN");
            Debug.Log(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName);
            
            if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for correct weapon and correct direction , case a
                Debug.Log("WE IN 1");
                scoreobj.GetComponent<ScoreTracker>().score += 20;
            }
            else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for incorrect weapon and correct direction , case b
                Debug.Log("WE IN 2");
                scoreobj.GetComponent<ScoreTracker>().score += 15;
            }
            else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
                Debug.Log("WE IN 3");
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
                Debug.Log("WRONG WAY");
                scoreobj.GetComponent<ScoreTracker>().score += 9;
            }
            else if (distanceToTarget > 2)
            {
                Debug.Log("WE IN B");
                if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 8;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 6;
                }
                
                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                
                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }

                if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 2;
                    Debug.Log("WE IN S");
                }

                else 
                {

                    scoreobj.GetComponent<ScoreTracker>().score += 1;

                }

            }
        } 
    }

    public void DeathBYMELSWIP() //work on
    {
        float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        Debug.Log(distanceToTarget);
       
       if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip" && AlreadyDead != true)
        {
            Debug.Log("WE IN AGAIN");
            AlreadyDead =true; 
            if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                Debug.Log("Line113");
                scoreobj.GetComponent<ScoreTracker>().score += 20;
                //check allignment score code for correct weapon and correct direction , case a
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for correct weapon but incorrect direction, case b
                scoreobj.GetComponent<ScoreTracker>().score += 15;
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
                scoreobj.GetComponent<ScoreTracker>().score += 9;
            }
            else if (distanceToTarget > 2){
            
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
                {
                    Debug.Log("line118");
                    scoreobj.GetComponent<ScoreTracker>().score += 8;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
                {
                    Debug.Log("line123");
                    scoreobj.GetComponent<ScoreTracker>().score += 6;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 2;
                }
                else
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 1;
                }
            }
        } 
    }
        
    public void DeathBYMELTHROW()
    {
        float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        Debug.Log(distanceToTarget);
        
        

        if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep" && AlreadyDead != true)
        {
            AlreadyDead =true;
            if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                Debug.Log("Line113");
                scoreobj.GetComponent<ScoreTracker>().score += 20;
                //check allignment score code for correct weapon and correct direction , case a
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for correct weapon but incorrect direction, case b
                scoreobj.GetComponent<ScoreTracker>().score += 15;
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
                scoreobj.GetComponent<ScoreTracker>().score += 9;
            }
        
            else if (distanceToTarget > 2)
            { 
            
                
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 8;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 6;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  == DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 2;
                }
                else
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 1;
                }
            }
        } 

    } 
       
       
    
}
