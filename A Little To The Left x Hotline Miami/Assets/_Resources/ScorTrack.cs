using System;
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

        [SerializeField] public string WhatDirection;

        [SerializeField] bool DoWeWantFurniture;

        private bool AlreadyDead;

    private ScoreDirectionSystem scoreDirect;

    private string[] directions = new string[] {"North", "NorthEast", "East", "SouthEast", "South", "SouthWest", "West", "NorthWest"};
    



    // Start is called before the first frame update
    void Start()
    {
        AlreadyDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreobj = GameObject.FindGameObjectWithTag("scoring");
        scoreDirect = GetComponent<ScoreDirectionSystem>();
        
    }

    // Update is called once per frame
    
    public void DeathBYBULLET()
    {
        //float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        //Debug.Log(distanceToTarget);
        if (gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyBull" && AlreadyDead != true)
        {
            AlreadyDead =true;
            //check if correct weapon
            if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat)
            {
                Debug.Log("Correct weapon");
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
            }

            checkDirectionDistanceAndScore(GetComponent<ScoreDirectionSystem>().DirectionOfBullet);

            /*else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat)// && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for incorrect weapon and correct direction , case b
                Debug.Log("WE IN 2");
            }
            else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
                Debug.Log("WE IN 3");
            }
            else if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
                Debug.Log("WRONG WAY");
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

            }*/
        } 
    }

    public void DeathBYMELSWIP() //work on
    {
        //float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        //Debug.Log(distanceToTarget);
       
       if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip" && AlreadyDead != true)
        {
            if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat)
            {
                Debug.Log("Correct weapon");
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
            }

            checkDirectionDistanceAndScore(GetComponent<ScoreDirectionSystem>().DirectionOfMelee);

            /*Debug.Log("WE IN AGAIN");
            AlreadyDead =true; 
            if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                Debug.Log("Line113");
                //check allignment score code for correct weapon and correct direction , case a
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for correct weapon but incorrect direction, case b
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
            }
            else if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
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
            }*/
        } 
    }
        
    public void DeathBYMELTHROW()
    {
        //float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
        //Debug.Log(distanceToTarget);
        if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep" && AlreadyDead != true)
        {
            AlreadyDead =true;
            AlreadyDead = true;
            //check if correct weapon
            if (GetComponent<EnemyController>().deathBy == DeathbyWhat)
            {
                Debug.Log("Correct weapon");
            }
            else if (GetComponent<EnemyController>().deathBy != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
            }

            checkDirectionDistanceAndScore(GetComponent<ScoreDirectionSystem>().DirectionOfBullet);

            /*if (GetComponent<EnemyController>().deathBy == DeathbyWhat )//&& distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                Debug.Log("Line113");
                //check allignment score code for correct weapon and correct direction , case a
            }
            else if(GetComponent<EnemyController>().deathBy != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                //check allignment score code for correct weapon but incorrect direction, case b
            }
            else if(GetComponent<EnemyController>().deathBy == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for correct weapon but wrong direction, case c
            }
            else if(GetComponent<EnemyController>().deathBy != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                //check allignment score code for incorrect weapon and wrong direction, case d
            }
        
            else if (distanceToTarget > 2)
            { 
            
                
                if (GetComponent<EnemyController>().deathBy  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 8;
                }
                if (GetComponent<EnemyController>().deathBy  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 6;
                }
                if (GetComponent<EnemyController>().deathBy  == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }   
                if (GetComponent<EnemyController>().deathBy  == DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GetComponent<EnemyController>().deathBy  == DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 5;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 4;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 6 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 3;
                }
                if (GetComponent<EnemyController>().deathBy  != DeathbyWhat && distanceToTarget > 6 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 2;
                }
                else
                {
                    scoreobj.GetComponent<ScoreTracker>().score += 1;
                }
            }*/
        } 

    } 
       
    public void checkDirectionDistanceAndScore(string givenDirection)
    {
        Debug.Log(givenDirection);
        int directionPosAr = System.Array.IndexOf(directions, givenDirection);
        Debug.Log(directionPosAr);
        string[] shiftedDirectArray = arrayShifter.ShiftArray(directions, 3 - directionPosAr);
        Debug.Log("Shifted array: " + string.Join(", ", shiftedDirectArray));
        //check direction
        if (directions[directionPosAr] == WhatDirection)
        {
            Debug.Log("Perfect Direction");
        }
        else if (directions[directionPosAr - 1] == WhatDirection || (directions[directionPosAr - 1] == WhatDirection))
        {
            Debug.Log("One direction off");
        }
        else if (directions[directionPosAr - 2] == WhatDirection || (directions[directionPosAr - 2] == WhatDirection))
        {
            Debug.Log("Two directions off");
        }
        else if (directions[directionPosAr - 3] == WhatDirection || (directions[directionPosAr - 3] == WhatDirection))
        {
            Debug.Log("Three directions off");
        }
        else if (directions[7] == WhatDirection)
        {
            Debug.Log("Opposite Direction");
        }

        //check distance
        if (scoreDirect.deadDistance == 0f)
        {
            Debug.Log("Perfect Distance");
        }
        else if (scoreDirect.deadDistance == 0.7f)
        {
            Debug.Log("Too Close");
        }
        else if (scoreDirect.deadDistance == 1.5f)
        {
            Debug.Log("A lil too far");
        }
        else
            Debug.Log("Too far");
    }
    
}
