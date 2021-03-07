using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHaveWeaponUINo4 : MonoBehaviour
{
    [HideInInspector]
    public enum Num1WeaponType
    {
        None,
        weaponType1,
        weaponType2,
        weaponType3,
    }

    [HideInInspector]
    public enum Num2WeaponType
    {
        None,
        weaponType1,
        weaponType2,
        weaponType3,
    }




    [HideInInspector]
    public Num1WeaponType num1WeaponType = Num1WeaponType.weaponType1;

    [HideInInspector]
    public Num2WeaponType num2WeaponType = Num2WeaponType.None;
}
