using UnityEngine;

public class WeaponKnockdown : MonoBehaviour
{
    public EnemyFate fate;
    public enum EnemyFate { knockdown, death }

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController e = collision.gameObject.GetComponent<EnemyController>();
            ScorTrack SCR = collision.gameObject.GetComponent<ScorTrack>();

            if (rb.velocity.magnitude > 1f)
            {
                if (fate == EnemyFate.knockdown)
                {
                    e.baseState = EnemyController.enemyState.knockedDown;
                }

                else if (fate == EnemyFate.death)
                {
                    e.baseState = EnemyController.enemyState.dead;
                   // SCR.DeathBYMELTHROW();
                }
            }
        }
    }
}
