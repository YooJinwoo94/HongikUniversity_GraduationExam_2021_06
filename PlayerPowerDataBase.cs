using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerDataBase : MonoBehaviour
{
    public int[] playersPowerNum;

    int playerPower = 1;
    int playerHpUpDown = 1;


    [SerializeField]
    PlayerWeaponInGameUI weaponInGameUIScript;
    [SerializeField]
    PlayerHavePowerUINo3 playerHavePowerUINo3Script;
    [SerializeField]
    PlayerPowerGetUINo2 playerPowerGetUINo2Script;

    string  thisIsChangeWhenplayersPowerUp ;

    // Start is called before the first frame update
    void Start()
    {
       for (int i =0; i<3; i++)
        {
            playersPowerNum[i] = 0;
        }
    }




    // 강화내용 선택시 인벤창 + 인게임창에서 변화를 준다.
    public void whenPlayerChoicePower(int count)
    {
        //changeTextToMakeSameWithInven();
        playersPowerNum[count]++;

        for (int i = 0; i< 3; i++)
        {
            weaponInGameUIScript.playersPowerText[i].text = playersPowerNum[i].ToString();
            playerHavePowerUINo3Script.playersPowerCount[i].text = playersPowerNum[i].ToString();
        }
        changeTextToMakeSameWithInven(count);
    }


    public void changeTextToMakeSameWithInven (int count = 4)
    {
        switch (count)
        {
            case 0:

                playerHavePowerUINo3Script.playersPowerDetails[count].text = "공격력이 " + (playersPowerNum[count] * playerPower).ToString() + "만큼 증가했습니다\n" + 
                                                                             "방어력이 " + (playersPowerNum[count] * playerHpUpDown).ToString() + "만큼 감소 했습니다\n";
                break;

            case 1:
                playerHavePowerUINo3Script.playersPowerDetails[count].text = "공격력이 " + (playersPowerNum[count] * playerPower).ToString() + "만큼 감소했습니다\n" +
                                                                             "방어력이 " + (playersPowerNum[count] * playerHpUpDown).ToString() + "만큼 증가 했습니다\n";
                break;

            case 2:
                playerHavePowerUINo3Script.playersPowerDetails[count].text = "공격력 " +  (playersPowerNum[count] * (playerPower /2)).ToString() + "만큼 증가했습니다\n" +
                                                             "방어력이 " + (playersPowerNum[count] * (playerHpUpDown / 2)).ToString() + "만큼 증가 했습니다\n";
                break;

            default:
                playerPowerGetUINo2Script.playerPowerDetails[0].text = "예상치 : 공격력이 " + ((playersPowerNum[0] + 1) * playerPower).ToString() + "만큼 증가했습니다\n" +
                                                                         "              방어력이 " + ((playersPowerNum[0] + 1) * playerHpUpDown).ToString() + "만큼 감소 했습니다\n";
                playerPowerGetUINo2Script.playerPowerDetails[1].text = "예상치 : 공격력이 " + ((playersPowerNum[1] + 1) * playerPower).ToString() + "만큼 감소했습니다\n" +
                                                                         "               방어력이 " + ((playersPowerNum[1] + 1) * playerHpUpDown).ToString() + "만큼 증가 했습니다\n";
                playerPowerGetUINo2Script.playerPowerDetails[2].text = "예상치 : 공격력 " + ((playersPowerNum[2] + 1) * (playerPower )).ToString() + "만큼 증가했습니다\n" +
                                                                         "                   방어력이 " + ((playersPowerNum[2] + 1) * (playerHpUpDown )).ToString() + "만큼 증가 했습니다\n";
                break;
        }
    }
}
