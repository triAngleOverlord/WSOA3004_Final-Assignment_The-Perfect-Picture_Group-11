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

    public bool hasWeapon;
    private Rigidbody2D equippedWeaponRB;
    private BoxCollider2D equippedWeaponBC;

    [SerializeField] private float throwPower;
    [SerializeField] private float spinningSpeed;

    internal WeaponType weaponType;

    public enum WeaponType
    {
        melee, ranged
    }

    void Start()
    {
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
        Collider2D[]  weaponsWithinRange = Physics2D.OverlapCircleAll (transform.position, interactRadius, targetWeaponMask);

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
            weaponType = foundWeapons[i].tag == "Ranged" ? WeaponType.ranged : WeaponType.melee;
        }

        if (hasWeapon)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (equippedWeapon!= null)
                {
                    equippedWeapon.parent = null;
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;
                    equippedWeaponBC.enabled = true;
                    equippedWeaponRB.AddForce (transform.up * throwPower, ForceMode2D.Impulse);
                    equippedWeaponRB.AddTorque (spinningSpeed, ForceMode2D.Impulse);
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
                equippedWeaponRB = equippedWeapon.GetComponent<Rigidbody2D>();
                equippedWeaponRB.bodyType = RigidbodyType2D.Kinematic;
                equippedWeaponRB.angularDrag = 0.2f;
                equippedWeaponBC = equippedWeapon.GetComponent<BoxCollider2D>();
                equippedWeaponBC.enabled = false;
                equippedWeapon.parent = weaponType == WeaponType.ranged? rangedWeaponPos : meleeWeaponPos;
                equippedWeapon.localRotation = Quaternion.identity;
                equippedWeapon.localPosition = Vector3.zero;
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
}
