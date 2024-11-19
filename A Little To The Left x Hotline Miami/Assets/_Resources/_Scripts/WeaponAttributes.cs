using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    public string weaponName;

    [Header("Ranged Weapons")]
    public float speed;
    public GameObject projectilePrefab;
    public float spread;
    public float amountOfBullets;
    public Transform spawnPoint;
    public float timeBeforeNextShot;

    [Header("Melee Weapons")]
    //melee
    public float meleeAttackRadius;
    public LayerMask targetMask;
    public Animator anim;
    public float meleeWaitTime;

    [Header("Weapon SFX")]
    public AudioClip meleeSFx;
    public AudioClip rangedSFx;

    public void AnnounceSelf()
    {
        print(weaponName);
    }
}
