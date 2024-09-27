using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerInteraction;

public class Gun : MonoBehaviour
{
    [SerializeField] int Damage;

    [SerializeField] float shootingCooldown;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float shootForce;

    [SerializeField] float amountofBullets;

    

    [SerializeField] int magSize;
    [SerializeField] bool isAutomatic;
    [SerializeField] bool AutoReload;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask whatIsEnemy;

    public string target1;
    public string target2;
    private Rigidbody2D rb;

    private float amountOfSpread;

    private int bulletsLeft;

    private bool shooting;
    private bool reloading;
    private bool canShoot;

    PlayerInteraction playerInteraction;

     // Start is called before the first frame update
    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        bulletsLeft = magSize;
        canShoot = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isAutomatic)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
        {
             Reload();
        }

        if (this.gameObject.name == playerInteraction.obj && playerInteraction.hasWeapon && playerInteraction.weaponType == WeaponType.ranged && playerInteraction.weaponType != WeaponType.melee && canShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }

        if (AutoReload && bulletsLeft == 0 && !reloading)
        {
            Reload();
        }

    }


    private void Shoot()
{
    canShoot = false;

    for (int i = 0; i < amountofBullets; i++)
    {
        // Apply random spread to each bullet.
        float randomSpread = UnityEngine.Random.Range(-spread, spread);
        Quaternion spreadRotation = Quaternion.Euler(0, 0, randomSpread);

        // Instantiate the bullet with random spread applied to the firePoint's rotation.
        GameObject bulletCopy = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spreadRotation);

        // Apply force to the bullet in the firePoint's forward direction.
        bulletCopy.GetComponent<Rigidbody2D>().AddForce(bulletCopy.transform.up * shootForce, ForceMode2D.Impulse);
    }

    bulletsLeft--;
    Invoke("ResetShot", shootingCooldown);
}


    private void ResetShot()
    {
        canShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("FinishedReload", reloadTime);
    }

    private void FinishedReload()
    {
        reloading = false;
        bulletsLeft = magSize;
    }
}
