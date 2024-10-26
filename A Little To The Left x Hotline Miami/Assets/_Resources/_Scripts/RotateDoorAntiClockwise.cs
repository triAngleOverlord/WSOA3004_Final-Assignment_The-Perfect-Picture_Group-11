using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoorAntiClockwise : MonoBehaviour
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
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector3.Distance (player.transform.position, this.gameObject.transform.position) < 5.0f && other.gameObject.tag == "Enemy") {
            EnemyAttacked ea = other.gameObject.GetComponent<EnemyAttacked>();
            ea.knockDownEnemy();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        dm.rotateAntiClockWiseMethod();
        dm.beingOpened = true;
        dm.mod = 6;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        dm.beingOpened = false;
        dm.mod = 0;
    }
}
