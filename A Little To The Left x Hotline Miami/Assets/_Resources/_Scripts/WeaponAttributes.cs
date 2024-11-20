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
        //print(weaponName);
    }

    private SlotAssignHelper slotAssignHelper;

    private void Start()
    {
        slotAssignHelper = FindObjectOfType<SlotAssignHelper>();

        #region weapon Sound effects
        if (weaponName == "Revolver" || weaponName == "Pistol")
        {
            rangedSFx = slotAssignHelper.handguns;
        }
        else if (weaponName == "Uzi" || weaponName == "Assault" || weaponName == "MP5")
        {
            rangedSFx = slotAssignHelper.warArms;
        }
        else if (weaponName == "Bottle" || weaponName == "Bat")
        {
            meleeSFx = slotAssignHelper.whoosh;
        }
        else if (weaponName == "Knife" || weaponName == "Wooden Axe" || weaponName == "Sword")
        {
            meleeSFx = slotAssignHelper.slash;
        }
        else if (weaponName == "Shotgun")
        {
            rangedSFx = slotAssignHelper.shotgun;
        }
        else if (weaponName == "Chainsaw")
        {
            meleeSFx = slotAssignHelper.chainsaw;
        }
        #endregion
    }
}
