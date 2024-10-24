using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerInteraction;
public class MeleeSys : MonoBehaviour
{
     public string target1;
    public string target2;
    private Rigidbody2D rb;

    private bool checker = false;

    private int bottletally = 2;
    PlayerInteraction playerInteraction;

    [SerializeField] private Animator anim;

    [SerializeField] private float meleeSpeed;

    [SerializeField] private float damage;

    private EnemyAttacked attacked;

    private GameObject name;

    float timeUntilMelee;

    private void Start()
    {
        name = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();

    }

    private void Update(){
        if (timeUntilMelee <= 0f)
        {
            
            if(  Input.GetMouseButton(0) && name.GetComponent<PlayerInteraction>().hasWeapon == true   )
            {
                
                checker = true;
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
                StartCoroutine(meleedelay());
            }
            
        }
        else {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    

    private IEnumerator meleedelay()
    {
        yield return new WaitForSeconds(0.5f);
        checker = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if( checker == true && other.tag == "Enemy" && this.gameObject.name == "Bottle" && bottletally == 2) { //add update sprite to bottle and hitbox and attack animation speed
            other.GetComponent<Helath>().TakeDamage(damage);
            attacked = other.gameObject.GetComponent<EnemyAttacked>();
            attacked.knockDownEnemy();
            Debug.Log("Enemy knockeddown"); 
            bottletally-=1;       
        }
        else if(checker == true && other.tag == "Enemy" && this.gameObject.name == "Bottle" && bottletally == 1)
        {
            other.GetComponent<Helath>().TakeDamage(damage);
            attacked = other.gameObject.GetComponent<EnemyAttacked>();
            attacked.killMelee();
            Debug.Log("Enemy Hit");  
        }
        else if( checker == true && other.tag == "Enemy" ) {
            other.GetComponent<Helath>().TakeDamage(damage);
            attacked = other.gameObject.GetComponent<EnemyAttacked>();
            attacked.killMelee();
            Debug.Log("Enemy Hit");        
        }
        // ai attack
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "Sword") 
        { 
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            attacked.killMelee();
            Debug.Log("Enemy Hit");
            this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
            this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "Bat") 
        { 
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            attacked.knockDownEnemy();
            Debug.Log("Enemy KnockedDown");
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "Chainsaw") 
        { 
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            attacked.killMelee();
            Debug.Log("Enemy Hit");
            this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
            this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "knife") 
        { 
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            attacked.killMelee();
            Debug.Log("Enemy Hit");
            this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
            this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true && this.gameObject.name == "Bottle") 
        {
            if(bottletally == 2)//update sprite
            {
                    attacked = collision.gameObject.GetComponent<EnemyAttacked>();
                    attacked.knockDownEnemy();
                    Debug.Log("Enemy KnockedDown");
                    this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
                    this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
            }
            else if(bottletally == 1)
            {
                attacked = collision.gameObject.GetComponent<EnemyAttacked>();
                attacked.killMelee();
                Debug.Log("Enemy Hit");
                this.gameObject.GetComponent<Rigidbody2D>().drag = 10000;
                this.gameObject.GetComponent<Rigidbody2D>().angularDrag = 10000;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;  
            }
           
        }
        else if (collision.gameObject.CompareTag("Enemy") && name.GetComponent<PlayerInteraction>().hasthrownWeapon == true)
        {
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            attacked.knockDownEnemy();
            Debug.Log("Enemy KnockedDown");
            name.GetComponent<PlayerInteraction>().hasthrownWeapon = false;
        } 
    } 

    
}
