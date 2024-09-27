using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string target1;
    public string target2;

    [SerializeField] private float damage;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(target1 == collision.name)
        {
            Debug.Log("Collision with wall detected");
            Destroy(gameObject);
        }
        if(target2 == collision.name)
        {
            Debug.Log("Collision with enemy detected");
             Destroy(gameObject);
        }
         if(collision.tag == "Enemy" ) {
            collision.GetComponent<Helath>().TakeDamage(damage);
            Debug.Log("Enemy Hit");        
        }
        
    }
}
