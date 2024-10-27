using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWeapon : MonoBehaviour
{
    public string weaponname;
   public float weaponpower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0) {  weaponname = this.GetComponentInChildren<WeaponNameandPower>().weaponname;
        weaponpower = this.GetComponentInChildren<WeaponNameandPower>().weaponpower; }
       
    }
}
