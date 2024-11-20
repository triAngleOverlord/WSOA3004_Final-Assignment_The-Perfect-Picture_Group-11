using Unity.VisualScripting;
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.layer == 6)
        {
            //rb.totalForce = Vector2.zero;
        }
    }
}
