using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponInGameUI : MonoBehaviour
{
    [SerializeField]
    PlayerWeaponObjCon playerWeaponObjConScript;
    [SerializeField]
    PlayerItemDataBase playerItemDataBaseScript;

    [SerializeField]
    Image[] ingameWeaponImage;

    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUINo4Script;

    [SerializeField]
    GameObject[] playerWeapon;

    //"0 == 왼쪽 , 1 == 오른쪽."
    [HideInInspector]
    public int checkNowWeaponUISelected;
    [HideInInspector]
    public bool isWeaponChangeCoolTime;


    [SerializeField]
   public Text[] playersPowerText;




    private void Start()
    {
        isWeaponChangeCoolTime = false;
        checkNowWeaponUISelected = 0;
    }




    public void ifPlayerGetWeaponChangeIngameWeaponImage(int weaponNum)
    {
        for (int i = 0; i<2; i++)
        {
            switch (playerHaveWeaponUINo4Script.playersWeaponType[i])
            {
                case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType1:
                    ingameWeaponImage[i].sprite = playerItemDataBaseScript.ingameWeaponImages[0];
                    break;
                case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType2:
                    ingameWeaponImage[i].sprite = playerItemDataBaseScript.ingameWeaponImages[1];
                    break;
                case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType3:
                    ingameWeaponImage[i].sprite = playerItemDataBaseScript.ingameWeaponImages[2];
                    break;
            }
         }

        playerWeaponObjConScript.changePlayerWeaponObj();
    }



    //마우스 클릭으로 인게임창에 나오는 무기 이미지 변화 
    public void playerWeaponUISelect()
    {
        if (playerHaveWeaponUINo4Script.playersWeaponType[1] == PlayerHaveWeaponUINo4.PlayersWeaponType.none) return;

        if (checkNowWeaponUISelected == 0)
        {
            playerWeapon[0].SetActive(false);
            playerWeapon[1].SetActive(true);
             checkNowWeaponUISelected = 1;
        }
        else
        {
            playerWeapon[0].SetActive(true);
            playerWeapon[1].SetActive(false);
            checkNowWeaponUISelected = 0;
        }
        playerWeaponObjConScript.changePlayerWeaponObj();

        isWeaponChangeCoolTime = true;

        StartCoroutine("resetCoolTime");
    }
  
    IEnumerator resetCoolTime()
    {
        yield return new WaitForSeconds(0.2f);
        isWeaponChangeCoolTime = false;
        StopCoroutine("resetCoolTime");
    }




}
