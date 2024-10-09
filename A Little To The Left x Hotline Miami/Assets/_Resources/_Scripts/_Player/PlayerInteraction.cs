using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   [SerializeField] private float interactRadius;

    [SerializeField] private LayerMask targetWeaponMask;
    [SerializeField] private List<Transform> foundWeapons = new List<Transform>();

    [SerializeField] private Transform meleeWeaponPos;
    [SerializeField] private Transform rangedWeaponPos;

    [SerializeField] private Transform equippedWeapon;

    public string obj;

    public bool hasWeapon;
    public bool hasthrownWeapon;
    public Rigidbody2D equippedWeaponRB;
    private BoxCollider2D equippedWeaponBC;

    private Animator animator;

    [SerializeField] private float throwPower;
    [SerializeField] private float dropPower;
    [SerializeField] private float spinningSpeed;

    public WeaponTypeNew weaponType;

    public enum WeaponTypeNew
    {
        melee, ranged
    }

    void Start()
    {
        hasthrownWeapon = false;
        StartCoroutine("CallInteract", .3f);
    }


    void Update()
    {
        PickWeapons();
    }

    private IEnumerator CallInteract (float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds (delay);
            Interact();
        }
    }
    private void Interact()
    {
        foundWeapons.Clear();
        Collider2D[] weaponsWithinRange = Physics2D.OverlapCircleAll(transform.position, interactRadius, targetWeaponMask);

        for (int i = 0; i < weaponsWithinRange.Length; i++)
        {
            Transform targetWeapon = weaponsWithinRange[i].transform;
            foundWeapons.Add (targetWeapon);
        }
    }

    private void PickWeapons()
    {

        for (int i = 0; i < foundWeapons.Count; i++)
        {
            weaponType = foundWeapons[i].gameObject.tag == "Ranged" ? WeaponTypeNew.ranged: WeaponTypeNew.melee;
        }

        if (hasWeapon)
        {
            if (Input.GetMouseButtonDown(1) && hasthrownWeapon == false)
            {
                 if (equippedWeapon!= null)
                {
                    hasthrownWeapon = true;
                    equippedWeapon.parent = null;
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;
                    
                    equippedWeaponBC.isTrigger = false;

                    if (weaponType == WeaponTypeNew.melee && equippedWeapon.gameObject.tag == "Melee")
                    {
                        animator = equippedWeapon.GetComponent<Animator>();
                        animator.enabled = false;
                    }
                    StartCoroutine(waiter());    
                }
                
                hasWeapon = false;
            }

            if (Input.GetKeyDown (KeyCode.G))
            {
                if (equippedWeapon != null)
                {
                    equippedWeapon.parent = null;
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;
                    
                    equippedWeaponBC.isTrigger = false;
                    if (weaponType == WeaponTypeNew.melee && equippedWeapon.gameObject.tag == "Melee")
                    {
                        animator = equippedWeapon.GetComponent<Animator>();
                        animator.enabled = false;
                    }
                    
                    equippedWeaponRB.AddForce(transform.right* dropPower, ForceMode2D.Impulse);
                    equippedWeaponRB.angularDrag = 2f;
                    equippedWeapon = null;

                }

                hasWeapon = false;
            }
        }


        if (foundWeapons.Count == 0)  return; 

        if (Input.GetMouseButtonDown (1))
        {
            if (equippedWeapon== null && !hasWeapon)
            {
                hasWeapon = true;

                equippedWeapon = foundWeapons[0];
                obj = equippedWeapon.gameObject.name;
                equippedWeaponRB = equippedWeapon.GetComponent<Rigidbody2D>();
                equippedWeaponRB.bodyType = RigidbodyType2D.Kinematic;
                equippedWeaponRB.angularDrag = 0.2f;
                equippedWeaponBC = equippedWeapon.GetComponent<BoxCollider2D>();

                if(weaponType == WeaponTypeNew.melee && equippedWeapon.gameObject.tag == "Melee")
                {
                    animator = equippedWeapon.GetComponent<Animator>();
                    animator.enabled = true;
                }
                          
                equippedWeapon.parent = weaponType == WeaponTypeNew.ranged? rangedWeaponPos : meleeWeaponPos;
                equippedWeapon.localRotation = Quaternion.identity;
                equippedWeapon.localPosition = Vector3.zero;
                equippedWeaponBC.isTrigger = true;    
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, interactRadius);

        Gizmos.color = Color.red;
        foreach (var weapon in foundWeapons)
        {
            Gizmos.DrawLine(transform.position, weapon.position);
        }
    }

    IEnumerator waiter()
    {  
        equippedWeaponRB.AddForce (transform.up * throwPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);
        equippedWeaponRB.AddTorque (spinningSpeed, ForceMode2D.Impulse);
        equippedWeaponRB.angularDrag = 2f;
        equippedWeapon = null;
        yield return new WaitForSeconds(0.25f);
        hasthrownWeapon = false;
    }
}
