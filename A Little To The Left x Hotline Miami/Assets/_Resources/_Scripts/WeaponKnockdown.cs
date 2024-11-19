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

    public void KnockBackEffect(Collider2D collision, EnemyController e)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {Debug.Log("Here Knockback");

            
                if (fate == EnemyFate.knockdown)
                {
                    e.baseState = EnemyController.enemyState.knockedDown;
                }

                else if (fate == EnemyFate.death)
                {
                    e.baseState = EnemyController.enemyState.dead;
                }
            
        }
    }
}
