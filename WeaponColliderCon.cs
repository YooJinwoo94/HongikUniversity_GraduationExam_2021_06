using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderCon : MonoBehaviour
{
    [SerializeField]
    BoxCollider[] weaponCollider;
    [SerializeField]
    GameObject weaponObj;




    public void weaponColliderOff(int num = 100)
    {
        switch(num)
        {
            case 100:
                weaponCollider[0].enabled = false;
                weaponCollider[1].enabled = false;
                weaponObj.tag = "enemyWeapon";
                break;

            case 0:
                weaponCollider[0].enabled = false;
                break;
        }

    }

    public void weaponColliderOn(int num)
    {
        weaponCollider[num].enabled = true;
    }

    public void weaponTypeChageToStun()
    {
        weaponObj.tag = "enemyStun";
    }
}
