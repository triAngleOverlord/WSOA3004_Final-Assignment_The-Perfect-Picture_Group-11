using UnityEngine;

public class PlayerWeaponData : MonoBehaviour
{
    public string weaponName;

    public Transform projectileSpawnPoint;
    public GameObject projectile;
    public float spread;
    public float speed;
    public float magazineSize;
    public float amountOfBullets;
    public float totalMag;
    public float waitTime;

    //melee
    public Transform meleeAttackRangePos;
    public float meleeAttackRadius;
    public LayerMask targetMask;
    public Animator anim;
    public float meleeWaitTime;

    [Header("Audio Sources")]

    public AudioSource sfxSource;
    public AudioSource loadsfxSource;

    [Header("Audio Clips")]
    public AudioClip rangedWeaponSFx;
    public AudioClip meleeWeaponSFx;

}



