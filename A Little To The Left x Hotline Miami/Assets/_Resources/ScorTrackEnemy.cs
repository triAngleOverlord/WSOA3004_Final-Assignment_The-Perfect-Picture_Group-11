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


        } 
       else if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip" && AlreadyDead != true)
        {
            
        } 
       else if (this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep" && AlreadyDead != true)
        {
            
        } 
       
    }
}
