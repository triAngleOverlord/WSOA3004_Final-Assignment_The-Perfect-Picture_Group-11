using System.Collections;
using UnityEngine;

public class MeleeSys : MonoBehaviour
{
    public string target1;
    public string target2;
    private Rigidbody2D rb;

    private bool checker = false;


    PlayerInteraction playerInteraction;

    [SerializeField] private Animator anim;

    [SerializeField] private float meleeSpeed;

    [SerializeField] private float damage;

    private EnemyAttacked attacked;

    private GameObject player;

    float timeUntilMelee;


    //myMeleeAttack
    [SerializeField] private Transform meleeAttackRangePos;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private LayerMask targetMask;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (timeUntilMelee <= 0f)
        {

            if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerInteraction>().hasWeapon == true)
            {
                checker = true;
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
                StartCoroutine(meleedelay());

                MeleeAttack();
            }

        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private void MeleeAttack()
    {
        Collider2D[] targetCol = Physics2D.OverlapCircleAll(meleeAttackRangePos.position, meleeAttackRadius, targetMask);

        foreach (Collider2D col in targetCol)
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.baseState = EnemyController.enemyState.dead;
                Rigidbody2D enemyRB = col.GetComponent<Rigidbody2D>();

                Vector3 fallDir = (col.gameObject.transform.position - player.transform.position).normalized;
                float power = 15f;
                enemyRB.AddForce(fallDir * power, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(meleeAttackRangePos.position, meleeAttackRadius);
    }

    private IEnumerator meleedelay()
    {
        yield return new WaitForSeconds(0.1f);
        checker = false;
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if( checker == true && other.tag == "Enemy" ) {
    //        other.GetComponent<Helath>().TakeDamage(damage);
    //        attacked = other.gameObject.GetComponent<EnemyAttacked>();
    //        attacked.killMelee();
    //        Debug.Log("Enemy Hit");        
    //    }
    //    // ai attack

    //}

    //void OnCollisionEnter2D(Collision2D collision) 
    //{
    //    if(collision.gameObject.CompareTag("Wall"))
    //    {
    //        player.GetComponent<PlayerInteraction>().hasthrownWeapon = false;
    //    }
    //    else if (collision.gameObject.CompareTag("Enemy") && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "Sword") 
    //    { 
    //        attacked = collision.gameObject.GetComponent<EnemyAttacked>();
    //        attacked.killMelee();
    //        Debug.Log("Enemy Hit");
    //        this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
    //        this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
    //        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //        player.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
    //    }
    //    else if (collision.gameObject.CompareTag("Enemy") && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "WoodenAxe") 
    //    { 
    //        attacked = collision.gameObject.GetComponent<EnemyAttacked>();
    //        attacked.killMelee();
    //        Debug.Log("Enemy Hit");
    //        this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
    //        this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
    //        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //        player.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
    //    }
    //    else if (collision.gameObject.CompareTag("Enemy") && player.GetComponent<PlayerInteraction>().hasthrownWeapon == true)
    //    {
    //        attacked = collision.gameObject.GetComponent<EnemyAttacked>();
    //        attacked.knockDownEnemy();
    //        Debug.Log("Enemy KnockedDown");
    //        player.GetComponent<PlayerInteraction>().hasthrownWeapon = false;
    //    } 
    //}     
}
