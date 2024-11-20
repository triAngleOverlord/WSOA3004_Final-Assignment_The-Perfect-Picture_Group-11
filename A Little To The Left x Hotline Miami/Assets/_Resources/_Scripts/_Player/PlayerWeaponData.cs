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


    private SlotAssignHelper slotAssignHelper;

    private void Start()
    {
        slotAssignHelper = FindObjectOfType<SlotAssignHelper>();
        sfxSource = slotAssignHelper.Sfx;
        loadsfxSource = slotAssignHelper.loadSFx;
        meleeAttackRangePos = slotAssignHelper.meleeAttackRange;

        #region weapon Sound effects
        if (weaponName == "Revolver" || weaponName == "Pistol")
        {
            rangedWeaponSFx = slotAssignHelper.handguns;
        }
        else if (weaponName == "Uzi" || weaponName == "Assault" || weaponName == "MP5")
        {
            rangedWeaponSFx = slotAssignHelper.warArms;
        }
        else if (weaponName == "Bottle" || weaponName == "Bat")
        {
            meleeWeaponSFx = slotAssignHelper.whoosh;
        }
        else if (weaponName == "Knife" || weaponName == "Wooden Axe" || weaponName == "Sword")
        {
            meleeWeaponSFx = slotAssignHelper.slash;
        }
        else if (weaponName == "Shotgun")
        {
            rangedWeaponSFx = slotAssignHelper.shotgun;
        }
        else if (weaponName == "Chainsaw")
        {
            meleeWeaponSFx = slotAssignHelper.chainsaw;
        }
        #endregion
    }
}



