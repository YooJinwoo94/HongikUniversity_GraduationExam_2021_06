using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponObjCon : MonoBehaviour
{
    [SerializeField]
    GameObject[] playersWeaponObj;

    [SerializeField]
    PlayerWeaponInGameUI playerWeaponInGameUiScript;
    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUiNo4Script;


    //ȣ�� �ñ� =>
    // ���콺�� ���⸦ �ٲܶ���
    // ���⸦ ��������� 
   public void changePlayerWeaponObj ()
    {
        switch (playerHaveWeaponUiNo4Script.playersWeaponType[playerWeaponInGameUiScript.checkNowWeaponUISelected])
        {
            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType1:
                playersWeaponObj[0].SetActive(true);
                playersWeaponObj[1].SetActive(false);
                playersWeaponObj[2].SetActive(false);
                break;

            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType2:
                playersWeaponObj[0].SetActive(false);
                playersWeaponObj[1].SetActive(true);
                playersWeaponObj[2].SetActive(false);
                break;

            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType3:
                playersWeaponObj[0].SetActive(false);
                playersWeaponObj[1].SetActive(false);
                playersWeaponObj[2].SetActive(true);
                break;
        }
    }
}
