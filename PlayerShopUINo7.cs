using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerShopUINo7 : MonoBehaviour
{
    public GameObject bg;

    [SerializeField]
    PlayerCurseUI playerCurseUiScript;
    [SerializeField]
    PlayerHpManager playerHpManagerScript;
    [SerializeField]
    PlayerItemDataBase playerItemDataBaseScript;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;
    [SerializeField]
    PlayerUISeletManger playerUISelectManagerScript;
    [SerializeField]
    CoinManager coinManagerScript;

    [SerializeField]
   public Text coinText;

    [SerializeField]
   public GameObject[] uiSet;
    [SerializeField]
    public Animator[] alarmUiSet;
    string posionType = "";

    public void buyPosion (int count )
    {
        if (coinManagerScript.coinCount - coinManagerScript.cost < 0 || coinManagerScript.coinCount == 0)
        {
            alarmUiSet[1].SetTrigger("aniStart");
            return;
        }

        switch(posionType)
        {
            case "yellowPosion":
                if (playerHpManagerScript.hp.fillAmount >= 1)
                {
                    alarmUiSet[2].SetTrigger("aniStart");
                    return;
                }
                else
                {
                    coinManagerScript.coinCountToUi(count, false);
                    coinManagerScript.costCountUpDown();

                    playerHpManagerScript.hp.fillAmount += 0.1f;
                }
                break;
            case "greenPosion":
                if (playerHpManagerScript.hp.fillAmount >= 1)
                {
                    alarmUiSet[2].SetTrigger("aniStart");
                    return;
                }
                else
                {
                    coinManagerScript.coinCountToUi(count, false);
                    coinManagerScript.costCountUpDown();

                    playerHpManagerScript.hp.fillAmount += 0.5f;
                }
                break;
            case "bluePosion":
                if (playerCurseUiScript.playerCurseCount == 0) return;
                playerCurseUiScript.ifPlayerDrinkBluePosion();

                coinManagerScript.coinCountToUi(count, false);
                coinManagerScript.costCountUpDown();
                break;
        }
    }

    public void checkWhatPosion(string posionName)
    {
        posionType = posionName;
    }


    public void buyWeapon(int count)
    {
        if (coinManagerScript.coinCount - coinManagerScript.cost < 0 || coinManagerScript.coinCount == 0)
        {
            alarmUiSet[1].SetTrigger("aniStart");
            return;
        }
        coinManagerScript.coinCountToUi(count,false);
        uiSet[0].SetActive(false);
        uiSet[1].SetActive(true);
        playerGetWeaponUINo5Script.settingNameAndSprite();
    }
    public void checkWeaponType(int typeNum)
    {
        playerGetWeaponUINo5Script.dropedWeaponImage.sprite = playerItemDataBaseScript.playerWeaponImage[typeNum];
        playerGetWeaponUINo5Script.dropedWeaponName.text = playerItemDataBaseScript.playerWeaponNameSpace[typeNum];
        playerGetWeaponUINo5Script.dropedWeaponDetails.text = playerItemDataBaseScript.playersWeaponDetails[typeNum];
    }
}
