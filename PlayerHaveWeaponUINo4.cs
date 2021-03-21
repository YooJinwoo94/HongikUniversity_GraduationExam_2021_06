using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHaveWeaponUINo4 : MonoBehaviour
{
    [SerializeField]
    PlayerItemDataBase playerItemDataBase;

    [HideInInspector]
    public enum PlayersWeaponType
    {
        none,
        weaponType1,
        weaponType2,
        weaponType3
    }

    [SerializeField]
    public Image[] playersWeaponImage;

    [SerializeField]
    public Text[] playerWeaponName;

    [SerializeField]
    public Text[] playerWeaponDetails;


    public PlayersWeaponType[] playersWeaponType;


    //무기들의 이미지 보관
    [SerializeField]
    Sprite[] weaponImageSetToChangeInvenWeaponImage;


    private void Start()
    {
        playersWeaponType[0] = PlayersWeaponType.weaponType1;
        playersWeaponType[1] = PlayersWeaponType.none;
    }




    // 플레이어의 무기이미지 및 텍스트 교체
    public void changePlayersWeapon(int weaponSlot = 0,int weaponNum = 0)
    {
        if (playersWeaponImage[1].enabled == false) playersWeaponImage[1].enabled = true;

        switch (weaponNum)
        {
            case 0:
                playersWeaponType[weaponSlot] = PlayersWeaponType.weaponType1;
                break;

            case 1:
                playersWeaponType[weaponSlot] = PlayersWeaponType.weaponType2;
                break;

            case 2:
                playersWeaponType[weaponSlot] = PlayersWeaponType.weaponType3;
                break;
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
