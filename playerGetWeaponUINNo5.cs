using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGetWeaponUINNo5 : MonoBehaviour
{
    [SerializeField]
    playerHaveWeaponUINo4 playerHaveWeaponUINo4Script;

        int[] playerWeapon;



    public void getWeaponAndChangeIT(int numWeaponSlot , string weaponName )
        {
            switch (numWeaponSlot)
            {
                case 1:
                if (weaponName == "weapon1") playerHaveWeaponUINo4Script.num1WeaponType = playerHaveWeaponUINo4.Num1WeaponType.weaponType1;       
                else if (weaponName == "weapon2") playerHaveWeaponUINo4Script.num1WeaponType = playerHaveWeaponUINo4.Num1WeaponType.weaponType2;
                else if (weaponName == "weapon3") playerHaveWeaponUINo4Script.num1WeaponType = playerHaveWeaponUINo4.Num1WeaponType.weaponType3;
                //else if (weaponName == 0) num1WeaponType = Num1WeaponType.None;
                break;

                case 2:
                    if (weaponName == "weapon1") playerHaveWeaponUINo4Script.num2WeaponType = playerHaveWeaponUINo4.Num2WeaponType.weaponType1;
                    else if (weaponName == "weapon2") playerHaveWeaponUINo4Script.num2WeaponType = playerHaveWeaponUINo4.Num2WeaponType.weaponType2;
                    else if (weaponName == "weapon3") playerHaveWeaponUINo4Script.num2WeaponType = playerHaveWeaponUINo4.Num2WeaponType.weaponType3;
                    //else if (numWeapon == 0) num2WeaponType = Num2WeaponType.None;
                break;
            }
        Debug.Log(numWeaponSlot);
        Debug.Log(weaponName);
        Debug.Log(playerHaveWeaponUINo4Script.num1WeaponType);
        Debug.Log(playerHaveWeaponUINo4Script.num2WeaponType);
    }

}
