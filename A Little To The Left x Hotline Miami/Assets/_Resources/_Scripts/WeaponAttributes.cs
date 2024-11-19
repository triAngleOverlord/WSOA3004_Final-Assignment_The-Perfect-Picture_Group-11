using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    [SerializeField] public string weaponName;

    [Header("********Ranged Weapons********")]
    public float speed;
    public GameObject projectilePrefab;
    public float spread;
    public float amountOfBullets;
    public Transform spawnPoint;
    public float timeBeforeNextShot;

    [Header("********Melee Weapons********")]
    //melee
    public float meleeAttackRadius;
    public LayerMask targetMask;
    public Animator anim;
    public float meleeWaitTime;

    private void Start()
    {
        weaponName = transform.name;
    }

    public void AnnounceSelf()
    {
        print(weaponName);
    }
}
