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

    //interact with obstacles
    [SerializeField] private Transform pickedObjectPos;

    public LayerMask obstacleMask;
    private GameObject pickedObject;
    [SerializeField] private float interactDist;
    bool hasObject;

    //hearing (enemy)
    [SerializeField] private float hearingRadius;
    [SerializeField] private LayerMask hearingMask;

    //sprites
    [SerializeField] private GameObject status_Alive;
    [SerializeField] private GameObject status_Dead;

    private string weaponMask = "Weapons";

    [SerializeField] private List<GameObject> meleeWeapons = new List<GameObject>();
    [SerializeField] private List<GameObject> rangedWeapons = new List<GameObject>();

    void Start()
    {
        hasthrownWeapon = false;
        StartCoroutine("CallInteract", .3f);
        status_Dead.SetActive(false);
    }


    void Update()
    {
        PickWeapons();
        InteractWithObjects();
        HearingCast();
    }

    private void HearingCast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearingRadius, hearingMask);

        foreach (Collider2D collider in colliders)
        {
            Gun weaponClass = FindObjectOfType<Gun>();
            EnemyController enemy = collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if (weaponClass != null)
                {
                    if (weaponClass.detectSound)
                    {
                        enemy.HearSound(transform.position);
                    }
                }
            }
        }
    }

    private void InteractWithObjects()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, interactDist, obstacleMask);
            Debug.DrawRay(transform.position, transform.up * interactDist, Color.green);

            if (hit.collider != null && !hasObject)
            {
                pickedObject = hit.collider.gameObject;
                pickedObject.transform.parent = pickedObjectPos;
                pickedObject.GetComponent<Collider2D>().enabled = false;
                pickedObject.transform.localPosition = Vector3.zero;
                pickedObject.transform.localRotation = Quaternion.identity;
                hasObject = true;
            }
            else if (hasObject)
            {
                pickedObject.transform.parent = null;
                pickedObject.GetComponent<Collider2D>().enabled = true;
                pickedObject = null;
                hasObject = false;
            }
        }
    }

    private IEnumerator CallInteract(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
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
            foundWeapons.Add(targetWeapon);
        }
    }

    private void PickWeapons()
    {

        for (int i = 0; i < foundWeapons.Count; i++)
        {
            if (foundWeapons[i].gameObject.tag == "Ranged")
            {
                weaponType = WeaponTypeNew.ranged;
            }
            else if (foundWeapons[i].gameObject.tag == "Melee")
            {
                weaponType = WeaponTypeNew.melee;
            }
        }

        if (hasWeapon)
        {
            if (Input.GetMouseButtonDown(1) && hasthrownWeapon == false)
            {
                if (equippedWeapon != null)
                {
                    hasthrownWeapon = true;
                    equippedWeapon.parent = null;
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;
                    int layerIndex = LayerMask.NameToLayer(weaponMask);
                    equippedWeapon.gameObject.layer = layerIndex;
                    equippedWeaponBC.isTrigger = false;

                    StartCoroutine(waiter());


                    if (weaponType == WeaponTypeNew.ranged)
                    {
                        equippedWeapon.GetComponent<Gun>().enabled = false;
                    }
                    else if (weaponType == WeaponTypeNew.melee)
                    {
                        if (equippedWeapon.GetComponent<MeleeSys>()!= null)
                        {
                            equippedWeapon.GetComponent<MeleeSys>().enabled = false;
                        }
                        if (equippedWeapon.GetComponent<Animator>()!= null)
                        {
                            animator = equippedWeapon.GetComponent<Animator>();
                            animator.enabled = false;
                        }
                    }
                }

                hasWeapon = false;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                if (equippedWeapon != null)
                {
                    equippedWeapon.parent = null;
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;

                    equippedWeaponBC.isTrigger = false;

                    equippedWeaponRB.AddForce(transform.right * dropPower, ForceMode2D.Impulse);
                    equippedWeaponRB.angularDrag = 2f;
                    equippedWeapon = null;

                    int layerIndex = LayerMask.NameToLayer(weaponMask);
                    equippedWeapon.gameObject.layer = layerIndex;


                    if (weaponType == WeaponTypeNew.ranged)
                    {
                        equippedWeapon.GetComponent<Gun>().enabled = false;
                    }
                    else if (weaponType == WeaponTypeNew.melee)
                    {
                        equippedWeapon.GetComponent<MeleeSys>().enabled = false;
                        animator = equippedWeapon.GetComponent<Animator>();
                        animator.enabled = false;
                    }
                }

                hasWeapon = false;
            }
        }


        if (foundWeapons.Count == 0) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (equippedWeapon == null && !hasWeapon)
            {
                hasWeapon = true;

                var closestDistance = Mathf.Infinity;
                Transform closestWeapon = null;

                foreach (var weapon in foundWeapons)
                {
                    var distance = Vector2.Distance(transform.position, weapon.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestWeapon = weapon;
                    }
                }

                equippedWeapon = closestWeapon;
                obj = equippedWeapon.gameObject.name;
                equippedWeaponRB = equippedWeapon.GetComponent<Rigidbody2D>();
                equippedWeaponRB.bodyType = RigidbodyType2D.Kinematic;
                equippedWeaponRB.angularDrag = 0.2f;
                equippedWeaponBC = equippedWeapon.GetComponent<BoxCollider2D>();

                int layerName = LayerMask.NameToLayer("Fallback");
                equippedWeapon.gameObject.layer = layerName;

                if (weaponType == WeaponTypeNew.ranged)
                {
                    equippedWeapon.GetComponent<Gun>().enabled = true;
                    equippedWeapon.SetParent(rangedWeaponPos);
                }
                else if (weaponType == WeaponTypeNew.melee)
                {
                    equippedWeapon.GetComponent<MeleeSys>().enabled = true;
                    equippedWeapon.SetParent(meleeWeaponPos);
                    animator = equippedWeapon.GetComponent<Animator>();
                    animator.enabled = true;
                }

                equippedWeapon.localRotation = Quaternion.identity;
                equippedWeapon.localPosition = Vector3.zero;
                equippedWeaponBC.isTrigger = true;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

        Gizmos.color = Color.red;
        foreach (var weapon in foundWeapons)
        {
            Gizmos.DrawLine(transform.position, weapon.position);
        }

        //enemy hearing distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
    }

    IEnumerator waiter()
    {
        equippedWeaponRB.AddForce(transform.up * throwPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);
        equippedWeaponRB.AddTorque(spinningSpeed, ForceMode2D.Impulse);
        equippedWeaponRB.angularDrag = 2f;
        equippedWeapon = null;
        yield return new WaitForSeconds(0.25f);
        hasthrownWeapon = false;
    }

    public void StatusUpdate()
    {
        status_Dead.SetActive(true);
        status_Alive.SetActive(false);
        if (equippedWeapon != null)
        {
            equippedWeapon.parent = null;
        }

        if (meleeWeapons.Count > 0)
        {
            foreach (var weapon in meleeWeapons)
            {
                Destroy(weapon.GetComponent<MeleeSys>());
            }
        }
        if (rangedWeapons.Count>0)
        {
            foreach (var weapon in rangedWeapons)
            {
                Destroy(weapon.GetComponent<Gun>());
            }
        }
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<PlayerInteraction>());
        Destroy(gameObject.GetComponent<PlayerController>());
        gameObject.layer = default;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            print("Dead");
            StatusUpdate();
        }
    }
}
