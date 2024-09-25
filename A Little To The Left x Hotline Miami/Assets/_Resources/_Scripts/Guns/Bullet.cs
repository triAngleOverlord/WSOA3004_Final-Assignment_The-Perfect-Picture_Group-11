using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string target1;
    public string target2;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(target1 == collision.name)
        {
            Debug.Log("Collision with wall detected");
        }
        if(target2 == collision.name)
        {
            Debug.Log("Collision with enemy detected");
        }
        
    }
}
