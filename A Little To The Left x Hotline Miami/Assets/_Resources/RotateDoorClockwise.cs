using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoorClockwise : MonoBehaviour
{
    DoorMovement dm;
    GameObject player;

    
    // Start is called before the first frame update
    void Start()
    {
        dm = this.GetComponentInParent<DoorMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = this.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = this.transform.position;

           
        if ((playerPos - thisPos).sqrMagnitude < 25.0f && other.CompareTag("Enemy"))
            {
            EnemyAttacked ea = other.gameObject.GetComponent<EnemyAttacked> ();
            ea.knockDownEnemy();
            Debug.Log("hit");
            }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        dm.rotateClockWiseMethod();
        dm.beingOpened = true;
        dm.mod = 6;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        dm.beingOpened = false;
        dm.mod = 0;
    }
}
