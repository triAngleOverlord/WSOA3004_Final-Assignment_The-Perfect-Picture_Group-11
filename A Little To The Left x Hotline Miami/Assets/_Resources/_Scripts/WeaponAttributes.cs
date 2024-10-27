using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    [SerializeField] private string weaponName;

    [Header("********Ranged Weapons********")]
    public float speed;
    public GameObject projectilePrefab;
    public float spread;
    public float amountOfBullets;
    public Transform spawnPoint;
    public float timeBeforeNextShot;

    [Header("********Melee Weapons********")]
    public float timeBeforeNextSlash;

    private void Start()
    {
        weaponName = transform.name;
    }

    public void AnnounceSelf()
    {
        print(weaponName);  
    }
}
