using UnityEngine;

public class RotateDoorAntiClockwise : MonoBehaviour
{
    DoorMovement dm;
    GameObject player;

    void Start()
    {
        dm = this.GetComponentInParent<DoorMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5.0f && other.gameObject.tag == "Enemy" && dm.mod != 0)
        {
            EnemyController ec = other.gameObject.GetComponent<EnemyController>();
            ec.baseState = EnemyController.enemyState.knockedDown;
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
