using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


using TMPro;
using UnityEngine.UI;
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

        private int DIAMONDHANDS;

        public TMP_Text SCALEOFPERFECTION;

        private GameObject GETMEMANAGER;

       public GameObject GETMESCALE;
       public Canvas canvas; 

    private ScoreDirectionSystem scoreDirect;

    private string[] directions = new string[] {"North", "NorthEast", "East", "SouthEast", "South", "SouthWest", "West", "NorthWest"};
    



    // Start is called before the first frame update
    void Start()
    {
        AlreadyDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreobj = GameObject.FindGameObjectWithTag("scoring");
        scoreDirect = GetComponent<ScoreDirectionSystem>();
        GETMEMANAGER = GameObject.FindGameObjectWithTag("GAMEMANAGER");
        GETMESCALE = GameObject.FindGameObjectWithTag("SCALEOFPERFECT");
        DIAMONDHANDS = 0;
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
                DIAMONDHANDS += 250;

            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
                DIAMONDHANDS -= 250;
            }

            checkDirectionDistance(GetComponent<ScoreDirectionSystem>().DirectionOfBullet);
            DisplayThatScore();
            GETMEMANAGER.GetComponent<GameManager>().scorevalue += DIAMONDHANDS;

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
                DIAMONDHANDS += 250;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<WeaponAttributes>().weaponName != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
                DIAMONDHANDS -= 250;
            }

            checkDirectionDistance(GetComponent<ScoreDirectionSystem>().DirectionOfMelee);
            DisplayThatScore();
            GETMEMANAGER.GetComponent<GameManager>().scorevalue += DIAMONDHANDS;
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
                DIAMONDHANDS += 250;
            }
            else if (GetComponent<EnemyController>().deathBy != DeathbyWhat)
            {
                Debug.Log("Incorrect weapon");
                DIAMONDHANDS -= 250;
            }

            checkDirectionDistance(GetComponent<ScoreDirectionSystem>().DirectionOfBullet);
            DisplayThatScore();
            GETMEMANAGER.GetComponent<GameManager>().scorevalue += DIAMONDHANDS;

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
       
    public void checkDirectionDistance(string givenDirection)
    {
        //Debug.Log(givenDirection);
        int directionPosAr = System.Array.IndexOf(directions, givenDirection);
        //Debug.Log(directionPosAr);
        string[] shiftedDirectArray = arrayShifter.ShiftArray(directions, 3 - directionPosAr);
        directionPosAr = Array.IndexOf(shiftedDirectArray, givenDirection);
        Debug.Log("Shifted array: " + string.Join(", ", shiftedDirectArray));
        //check direction
        Debug.Log("Third:"+ shiftedDirectArray[directionPosAr - 3]);
        if (shiftedDirectArray[directionPosAr] == WhatDirection)
        {
            Debug.Log("Perfect Direction");
           
            DIAMONDHANDS += 1000;
        }
        else if (shiftedDirectArray[directionPosAr - 1] == WhatDirection || (shiftedDirectArray[directionPosAr + 1] == WhatDirection))
        {
            Debug.Log("One direction off");
            
            DIAMONDHANDS += 750;
        }
        else if (shiftedDirectArray[directionPosAr - 2] == WhatDirection || (shiftedDirectArray[directionPosAr + 2] == WhatDirection))
        {
            Debug.Log("Two directions off");

            DIAMONDHANDS += 500;
        }
        
        else if (shiftedDirectArray[directionPosAr - 3] == WhatDirection || (shiftedDirectArray[directionPosAr + 3] == WhatDirection))
        {
            Debug.Log("Three directions off");
            DIAMONDHANDS += 250;
        }
        else if (shiftedDirectArray[7] == WhatDirection)
        {
            Debug.Log("Opposite Direction");
            DIAMONDHANDS -= -250;
        }

        //check distance
        if (scoreDirect.deadDistance == 0f)
        {
            Debug.Log("Perfect Distance");
            DIAMONDHANDS += 1000;
        }
        else if (scoreDirect.deadDistance == 0.7f)
        {
            Debug.Log("Distance Too Close");
            DIAMONDHANDS += 500;
        }
        else if (scoreDirect.deadDistance == 1.5f)
        {
            Debug.Log("Distance A lil too far");
            DIAMONDHANDS += 250;
        }
        else{
            Debug.Log("Distance Too far");
            DIAMONDHANDS -= Mathf.RoundToInt(scoreDirect.deadDistance / 2f) * 100;
            }
    }

    public void DisplayThatScore()
    {
        if(DIAMONDHANDS >= 2200)
        {
            StartCoroutine(PERFECTION());
        }
        else if(DIAMONDHANDS < 2200 && DIAMONDHANDS >= 1500)
        {
           // SCALEOFPERFECTION.text = "GOOD ENOUGH!";
        }
        else if(DIAMONDHANDS < 1500 && DIAMONDHANDS >= 1000)
        {
           // SCALEOFPERFECTION.text = "MEDIOCRE";
        }
        else if(DIAMONDHANDS < 1000 && DIAMONDHANDS >= 500)
        {
           // SCALEOFPERFECTION.text = "MISTAKES, MISTAKES!!!";
        }
        else if(DIAMONDHANDS < 500)
        {
           // SCALEOFPERFECTION.text = "YOU ARE USELESS TO MY ART!";
        }
    }
    
    
    public IEnumerator PERFECTION()
   {
     
        

        // Position it in the top-left corner
        
       
    
        GameObject textComponent = GETMESCALE;
        textComponent.GetComponent<Text>().enabled = true;
       
        textComponent.GetComponent<Text>().text = "!!!PERFECTION!!!";
        

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        textComponent.GetComponent<Text>().enabled = false;
        // Destroy the instantiated object
        
      
      
   }

}
