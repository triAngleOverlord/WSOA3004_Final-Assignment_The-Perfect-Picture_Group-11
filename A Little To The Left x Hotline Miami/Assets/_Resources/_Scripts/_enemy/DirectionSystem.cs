using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSystem : MonoBehaviour
{
   private Bullet bullet;
    private EnemyAttacked attacked;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D (Collider2D collision)
    {
        
         if(collision.tag == "Bullet") {

              
              Debug.Log(gameObject.name);  
              attacked = GetComponentInParent<EnemyAttacked>();
              attacked.Disable();
              speed = collision.GetComponent<Bullet>().speed;
              Vector2 dir = (collision.transform.position - transform.position).normalized;
              GetComponentInParent<Rigidbody2D>().AddForce (dir * speed);
              attacked.killBullet(gameObject.name);
              

        }
       
        
    }


}
