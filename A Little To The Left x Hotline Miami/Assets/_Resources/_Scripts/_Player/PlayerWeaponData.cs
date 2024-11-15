using UnityEngine;

public class PlayerWeaponData : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectile;
    public float spread;
    public float speed;
    public float magazineSize;
    public float amountOfBullets;

    public float waitTime;

    //melee
    public Transform meleeAttackRangePos;
    public float meleeAttackRadius;
    public LayerMask targetMask;
    public Animator anim;
    public float meleeWaitTime;
}
