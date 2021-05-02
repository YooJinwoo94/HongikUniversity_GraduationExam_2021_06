using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPowerGetUINo2 : MonoBehaviour
{
    [SerializeField]
    PlayerUISeletManger playerUISelectManagerScript;
    [SerializeField]
    PlayerPowerDataBase playerPowerDataBaseScript;

    [SerializeField]
    Animator[] playerPowerButtonAni;
    [SerializeField]
   public GameObject bg;

    [SerializeField]
    GameObject[] playerGetPowerUi;
    [SerializeField]
    GameObject[] powerButtonUi;

    [SerializeField]
   public Text[] playerPowerDetails;

    [SerializeField]
    public GameObject bgUiNo2Obj;



    //첫 강화내용 문양 선택시
    public void buttonClick(int num)
    {
        playerPowerButtonAni[num].SetTrigger("Normal");
        powerButtonUi[num].SetActive(false);
        playerGetPowerUi[num].SetActive(true);
    }

    // 2번쨰 강화 내용 선택시
    public void uiClick(int num)
    {
        playerPowerDataBaseScript.whenPlayerChoicePower(num);
        playerUISelectManagerScript.resetPlayerPowerGetUiSet();
        //기존에 있는 거 초기화
        for (int i = 0; i< 3; i ++)
        {
            playerPowerButtonAni[i].SetTrigger("Normal");
            powerButtonUi[i].SetActive(true);
            playerGetPowerUi[i].SetActive(false);
        }
    }
}
