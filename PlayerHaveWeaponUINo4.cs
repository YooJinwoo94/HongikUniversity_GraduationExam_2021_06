using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHaveWeaponUINo4 : MonoBehaviour
{
    [SerializeField]
    PlayerItemDataBase playerItemDataBase;

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

    [SerializeField]
    public Image[] playersWeaponImage;

    [SerializeField]
    public Text[] playerWeaponName;

    [SerializeField]
    public Text[] playerWeaponDetails;


    [HideInInspector]
    public Num1WeaponType num1WeaponType = Num1WeaponType.weaponType1;

    [HideInInspector]
    public Num2WeaponType num2WeaponType = Num2WeaponType.None;

    //무기들의 이미지 보관
    [SerializeField]
    Sprite[] weaponImageSetToChangeInvenWeaponImage;







    // 플레이어의 무기이미지 및 텍스트 교체
    public void changePlayersWeapon(int weaponSlot = 0,int weaponNum = 0)
    {
        if (weaponSlot == 0)
        {
            switch (weaponNum)
            {
                case 0:      
                    num1WeaponType = Num1WeaponType.weaponType1;
                    break;

                case 1:
                    num1WeaponType = Num1WeaponType.weaponType2;
                    break;

                case 2:
                    num1WeaponType = Num1WeaponType.weaponType3;
                    break;
            }  
        }
        else
        {
            if (playersWeaponImage[1].enabled == false) playersWeaponImage[1].enabled = true; 

            switch (weaponNum)
            {
                case 0:           
                    num2WeaponType = Num2WeaponType.weaponType1;
                    break;

                case 1:    
                    num2WeaponType = Num2WeaponType.weaponType2;
                    break;

                case 2:
                    num2WeaponType = Num2WeaponType.weaponType3;
                    break;
            }
        }

        weaponNameSetting(weaponSlot, weaponNum);
        playersWeaponImage[weaponSlot].sprite = playerItemDataBase.playerWeaponImage[weaponNum];
    }

    // 무기의 text를 가져옴
    void weaponNameSetting(int weaponSlot, int count)
    {
        playerWeaponName[weaponSlot].text = playerItemDataBase.playerWeaponNameSpace[count];
        playerWeaponDetails[weaponSlot].text = playerItemDataBase.playersWeaponDetails[count];
    }
}
