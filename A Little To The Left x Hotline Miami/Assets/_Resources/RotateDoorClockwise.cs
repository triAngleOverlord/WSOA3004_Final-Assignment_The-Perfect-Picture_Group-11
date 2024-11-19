using UnityEngine;

public class RotateDoorClockwise : MonoBehaviour
{
    DoorMovement dm;
    GameObject player;

    void Start()
    {
        dm = this.GetComponentInParent<DoorMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = transform.position;

        if ((playerPos - thisPos).sqrMagnitude < 25.0f && other.CompareTag("Enemy") && dm.mod!= 0)
        {
            EnemyController ec = other.gameObject.GetComponent<EnemyController>();
            ec.baseState = EnemyController.enemyState.knockedDown;
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
