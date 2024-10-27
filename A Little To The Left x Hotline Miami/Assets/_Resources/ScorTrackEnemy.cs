using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyBull" && AlreadyDead != true)
        {
            float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
            Debug.Log(distanceToTarget);
            AlreadyDead =true;
            if(GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 8;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 6;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
           else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playershootpoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget <= 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfBullet != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 2;
            }
            else
            {
                scoreobj.GetComponent<ScoreTracker>().score += 1;
            }

            
            


        } 
       else if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip" && AlreadyDead != true)
        {
            float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
            Debug.Log(distanceToTarget);
            AlreadyDead =true;
            if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                Debug.Log("Line113");
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                Debug.Log("line118");
                scoreobj.GetComponent<ScoreTracker>().score += 8;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                Debug.Log("line123");
                scoreobj.GetComponent<ScoreTracker>().score += 6;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
           else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfMelee != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 2;
            }
            else
            {
                scoreobj.GetComponent<ScoreTracker>().score += 1;
            }
        } 
       else if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep" && AlreadyDead != true)
        {
            float distanceToTarget = Vector3.Distance(transform.position, DesiredGoal.transform.position);
            Debug.Log(distanceToTarget);
            AlreadyDead =true;
            if(GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 10;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 8;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 6;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname == DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
           else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 5;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep == WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget <= 2 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 4;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 2 && distanceToTarget <= 4 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
            {
                scoreobj.GetComponent<ScoreTracker>().score += 3;
            }
            else if (GameObject.FindGameObjectWithTag("playermeleepoint").GetComponentInChildren<GrabWeapon>().weaponname != DeathbyWhat && distanceToTarget > 4 && distanceToTarget < 10 && this.gameObject.GetComponent<ScoreDirectionSystem>().DirectionOfmelwep != WhatDirection)
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
