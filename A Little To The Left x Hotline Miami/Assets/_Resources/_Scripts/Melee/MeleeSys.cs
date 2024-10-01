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
        if( checker == true && other.tag == "Enemy" ) {
            other.GetComponent<Helath>().TakeDamage(damage);
            Debug.Log("Enemy Hit");        
        }
        
    }
}
