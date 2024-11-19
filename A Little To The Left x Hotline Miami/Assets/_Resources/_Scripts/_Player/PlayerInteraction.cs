using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    internal PlayerWeaponData weaponData;
    private bool detectSound;

    private Vector2 bulletDir;
    private Rigidbody2D rb;

    void Start()
    {
        hasthrownWeapon = false;
        StartCoroutine("CallInteract", .3f);
        status_Dead.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    //ui
    [SerializeField] private TMP_Text magazineTxt;
    [SerializeField] private TMP_Text weaponNameTxt;
    [SerializeField] private GameObject magHolderUI;
    [SerializeField] private GameObject weaponNameUI;

    void Update()
    {
        PickWeapons();
        InteractWithObjects();
        HearingCast();
        Attacking();
        CooldownTimeUpdate();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (weaponData != null)
        {
            weaponNameUI.SetActive(true);
            weaponNameTxt.text = weaponData.weaponName;

            if (weaponType == WeaponTypeNew.ranged)
            {
                magHolderUI.SetActive(true);
                magazineTxt.text = weaponData.magazineSize + "/ " + weaponData.totalMag.ToString();
            }
        }
        else
        {
            magHolderUI.SetActive(false);
            weaponNameUI.SetActive(false);
        }
    }

    private void HearingCast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearingRadius, hearingMask);

        foreach (Collider2D collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if (equippedWeapon != null)
                {
                    if (detectSound)
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

    #region Weapons and Player Attack 

    private void Attacking()
    {
        if (hasWeapon)
        {
            if (weaponType == WeaponTypeNew.ranged)
            {
                if (Input.GetMouseButton(0))
                if (Input.GetButton("Fire1"))
                {
                    if (weaponData.magazineSize > 0)
                    {
                        Gun(weaponData.projectileSpawnPoint, weaponData.projectile, weaponData.amountOfBullets, weaponData.speed, weaponData.spread, weaponData.magazineSize, weaponData.waitTime);
                        Gun(weaponData.projectileSpawnPoint, weaponData.projectile, weaponData.amountOfBullets, weaponData.speed, weaponData.spread, weaponData.magazineSize, weaponData.waitTime, weaponData.rangedWeaponSFx, weaponData.sfxSource);
                        detectSound = true;
                    }
                    else
                    {
                        detectSound = false;
                    }
                }
                else
                {
                    detectSound = false;
                }
            }
            else if (weaponType == WeaponTypeNew.melee)
            {
                if (Input.GetMouseButton(0))
                {
                    MeleeAttack(weaponData.meleeAttackRangePos, transform, weaponData.meleeAttackRadius, weaponData.targetMask, weaponData.meleeWaitTime, weaponData.anim);
                    MeleeAttack(weaponData.meleeAttackRangePos, transform, weaponData.meleeAttackRadius, weaponData.targetMask, weaponData.meleeWaitTime, weaponData.anim, weaponData.meleeWeaponSFx, weaponData.sfxSource);
                }
                detectSound = false;
            }
        }
    }

    private float timeBetweenShots;

    private float timeUntilMelee;

    internal void Gun(Transform firePoint, GameObject projectile, float amountOfBullets, float speed, float spread, float magSize, float waitTime)
    internal void Gun(Transform firePoint, GameObject projectile, float amountOfBullets, float speed, float spread, float magSize, float waitTime, AudioClip sfx, AudioSource source)
    {
        if (timeBetweenShots <= 0)
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                var rot = Random.Range(-spread, spread);
                Quaternion newRot = Quaternion.Euler(0, 0, rot);

                GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation * newRot);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bullet.transform.up * speed, ForceMode2D.Impulse);
                Destroy(bullet, 5f);
            }

            PlayerWeaponData weaponData = equippedWeapon.gameObject.GetComponent<PlayerWeaponData>();
            if (weaponData != null)
            {
                weaponData.magazineSize--;
            }

            source.clip = sfx;
            source.Play();

            timeBetweenShots = waitTime;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
    internal void MeleeAttack(Transform attackPos, Transform player, float attackRadius, LayerMask mask, float waitTime, Animator anim)

    internal void MeleeAttack(Transform attackPos, Transform player, float attackRadius, LayerMask mask, float waitTime, Animator anim, AudioClip sfx, AudioSource source)
    {
        if (timeUntilMelee < 0)
        if (timeUntilMelee <= 0)
        {
            anim.SetTrigger("Attack");
<<<<<<< HEAD
           
=======
            Collider2D[] targetCol = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, mask);

            foreach (Collider2D col in targetCol)
            {
                EnemyController enemy = col.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.baseState = EnemyController.enemyState.dead;
                }
            }


            source.clip = sfx;
            source.Play();

>>>>>>> Wandile
            timeUntilMelee = waitTime;
        }
        else
    }

    internal void CooldownTimeUpdate()
    {
        if (timeBetweenShots > 0)
        {
            if (weaponData != null)
            {
                if (weaponData.weaponName == "Shotgun")
                {
                    if (weaponData.magazineSize > 0)
                    {
                        if (!weaponData.loadsfxSource.isPlaying)
                        {
                            weaponData.loadsfxSource.Play();
                        }
                    }
                }
            }

            timeBetweenShots -= Time.deltaTime;
        }

        if (timeUntilMelee > 0.000001f)
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    #endregion

    private void PickWeapons()
    {
        if (hasWeapon)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (equippedWeapon != null)
                {
                    equippedWeapon.parent = null;

                    if (weaponType == WeaponTypeNew.melee)
                    {
                        equippedWeapon.GetComponent<Animator>().enabled = false;
                    }

                    equippedWeapon.SetParent(null);
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;


                    equippedWeaponRB.AddForce(transform.up * throwPower, ForceMode2D.Impulse);
                    equippedWeaponRB.AddTorque(spinningSpeed, ForceMode2D.Impulse);
                    equippedWeaponRB.angularDrag = 2f;

                    int layerIndex = LayerMask.NameToLayer(weaponMask);
                    equippedWeapon.gameObject.layer = layerIndex;

                    weaponData = null;
                    equippedWeaponBC.isTrigger = false;
                    equippedWeapon = null;
                }
                hasthrownWeapon = true;
                StartCoroutine(waiter());
                hasWeapon = false;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                if (equippedWeapon != null)
                {
                    equippedWeapon.parent = null;
                    equippedWeapon.SetParent(null);
                    equippedWeaponRB.bodyType = RigidbodyType2D.Dynamic;

                    equippedWeaponBC.isTrigger = false;

                    equippedWeaponRB.AddForce(transform.right * dropPower, ForceMode2D.Impulse);
                    equippedWeaponRB.angularDrag = 2f;

                    int layerIndex = LayerMask.NameToLayer(weaponMask);
                    equippedWeapon.gameObject.layer = layerIndex;

                    weaponData = null;
                    equippedWeapon = null;
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
                if (equippedWeapon.tag == "Ranged")
                {
                    weaponType = WeaponTypeNew.ranged;
                }
                else if (equippedWeapon.tag == "Melee")
                {
                    weaponType = WeaponTypeNew.melee;
                }

                equippedWeaponRB = equippedWeapon.GetComponent<Rigidbody2D>();
                equippedWeaponRB.bodyType = RigidbodyType2D.Kinematic;
                equippedWeaponRB.angularDrag = 0.2f;
                equippedWeaponBC = equippedWeapon.GetComponent<BoxCollider2D>();

                int layerName = LayerMask.NameToLayer("Fallback");
                equippedWeapon.gameObject.layer = layerName;

                if (weaponType == WeaponTypeNew.ranged)
                {
                    equippedWeapon.SetParent(rangedWeaponPos);
                }
                else if (weaponType == WeaponTypeNew.melee)
                {
                    equippedWeapon.SetParent(meleeWeaponPos);
                    equippedWeapon.GetComponent<Animator>().enabled = true;
                }

                weaponData = equippedWeapon.GetComponent<PlayerWeaponData>();
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
        GameManager.gameOver = true;
        magHolderUI.SetActive(false);
        weaponNameUI.SetActive(false);

        status_Dead.SetActive(true);
        status_Alive.SetActive(false);
        if (equippedWeapon != null)
        {
            equippedWeapon.parent = null;
        }

        weaponData = null;
        //Destroy(gameObject.GetComponent<Rigidbody2D>());

        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<PlayerInteraction>());
        Destroy(gameObject.GetComponent<PlayerController>());
        hasWeapon = false;
        gameObject.layer = default;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            StatusUpdate();
            bulletDir = (collision.transform.position - transform.position).normalized;


            Vector3 fallDir = (transform.position - collision.gameObject.transform.position).normalized;
            float zAxis = Mathf.Atan2(fallDir.y, fallDir.x) * Mathf.Rad2Deg - 90f;
            rb.transform.rotation = Quaternion.Euler(0, 0, -zAxis);

            float power = 10f;
            rb.AddForce(-fallDir * power, ForceMode2D.Impulse);
            rb.drag = 5f;
        }
    }
}
