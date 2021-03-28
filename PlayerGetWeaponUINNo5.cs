using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerGetWeaponUINNo5 : MonoBehaviour
{
    [SerializeField]
   public Text dropedWeaponName;
    [SerializeField]
    Text dropedWeaponDetails;
    [SerializeField]
    Image dropedWeaponImage;

    [HideInInspector]
    public GameObject dropWeaponObj = null;

    [SerializeField]
    PlayerItemDataBase playerItemDataBaseScript;
    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUINo4Script;
    [SerializeField]
    public Image[] playersWeaponImage;

    [SerializeField]
    Text[] playerWeaponName;
    [SerializeField]
    Text[] playerWeaponDetails;

    [SerializeField]
   public GameObject imageWhenPlayerTouchTheWeapon;

    [SerializeField]
    public GameObject bgUiNo5Obj;





    // 드랍된 무기가 무엇인가?에 따라 얻은 무기의 이미지및 이름, 상세정보창을 바꾼다.
    public void whatIsThisWeapon()
    {
        //무기를 가져온다
        // 콜라이더로 가져와야 하나
        // 구분을 어떻게 해야 하나
        //  충돌시에 이름을 가져올 수 있나? 

        switch (dropWeaponObj.name)
        {
            case "DropedPlayerWeaponNum01":
                dropedWeaponImage.sprite = playerItemDataBaseScript.playerWeaponImage[0];
                dropedWeaponName.text = playerItemDataBaseScript.playerWeaponNameSpace[0];
                dropedWeaponDetails.text = playerItemDataBaseScript.playersWeaponDetails[0];
                break;
            case "DropedPlayerWeaponNum02":
                dropedWeaponImage.sprite = playerItemDataBaseScript.playerWeaponImage[1];
                dropedWeaponName.text = playerItemDataBaseScript.playerWeaponNameSpace[1];
                dropedWeaponDetails.text = playerItemDataBaseScript.playersWeaponDetails[1];
                break;
            case "DropedPlayerWeaponNum03":
                dropedWeaponImage.sprite = playerItemDataBaseScript.playerWeaponImage[2];
                dropedWeaponName.text = playerItemDataBaseScript.playerWeaponNameSpace[2];
                dropedWeaponDetails.text = playerItemDataBaseScript.playersWeaponDetails[2];
                break;
        }
    }


    // 인벤토리에서 가지고 있는 정보를 그대로 복붙한다.
   public void settingNameAndSprite()
    {
        for (int i = 0; i<2; i ++)
        {
            playerWeaponName[i].text = playerHaveWeaponUINo4Script.playerWeaponName[i].text;
            playerWeaponDetails[i].text = playerHaveWeaponUINo4Script.playerWeaponDetails[i].text;

            playersWeaponImage[i].sprite = playerHaveWeaponUINo4Script.playersWeaponImage[i].sprite;
        }
    }
}
