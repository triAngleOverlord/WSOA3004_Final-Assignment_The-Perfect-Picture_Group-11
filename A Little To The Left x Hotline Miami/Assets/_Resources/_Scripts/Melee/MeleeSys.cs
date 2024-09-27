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

    PlayerInteraction playerInteraction;

    [SerializeField] private Animator anim;

    [SerializeField] private float meleeSpeed;

    [SerializeField] private float damage;

    [SerializeField] private GameObject name;

    float timeUntilMelee;

    private void Update(){
        if (timeUntilMelee <= 0f)
        {
            if(  Input.GetMouseButtonDown(0)  )
            {
                

                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
            }
        }
        else {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" ) {
            other.GetComponent<Helath>().TakeDamage(damage);
            Debug.Log("Enemy Hit");        
        }
        
    }
}
