using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    string creator;

    //EnemyAttacked attacked;

    public GameObject bloodImpact,wallImpact;

    float timer = 10.0f;

    [SerializeField] public float speed;
    public string target1;
    public string target2;

    [SerializeField] private float damage;
    private Rigidbody2D rb;

   private ContactPoint2D[] contacts = new ContactPoint2D[2];

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update () {
        
        


        timer -= Time.deltaTime;
        if(timer<=0)
        {
            Destroy(this.gameObject);
        }

    }
    
    

    private void OnTriggerEnter2D (Collider2D collision)
    {
        //if(target1 == collision.name)
        //{
        //    Debug.Log("Collision with wall detected");
         //   Destroy(gameObject);
        //}
        //if(target2 == collision.name)
        //{
          //  Debug.Log("Collision with enemy detected");
            // Destroy(gameObject);
        //}
         if(collision.tag == "Enemy" ) {
            //  attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            //  attacked.killBullet();
            collision.GetComponent<Helath>().TakeDamage(damage);

            // And finally we add force in the direction of dir and multiply it by force. 
             // This will push back the player
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce (dir * speed);
           

		
            //Instantiate (bloodImpact, this.transform.position, this.transform.rotation);
            Destroy (this.gameObject);
            Debug.Log("Enemy Hit");        
        }
        else if (collision.tag == "Wall") 
        {
            //Instantiate (wallImpact, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        
    }
}
