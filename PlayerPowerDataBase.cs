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




    // ��ȭ���� ���ý� �κ�â + �ΰ���â���� ��ȭ�� �ش�.
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

                playerHavePowerUINo3Script.playersPowerDetails[count].text = "���ݷ��� " + (playersPowerNum[count] * playerPower).ToString() + "��ŭ �����߽��ϴ�\n" + 
                                                                             "������ " + (playersPowerNum[count] * playerHpUpDown).ToString() + "��ŭ ���� �߽��ϴ�\n";
                break;

            case 1:
                playerHavePowerUINo3Script.playersPowerDetails[count].text = "���ݷ��� " + (playersPowerNum[count] * playerPower).ToString() + "��ŭ �����߽��ϴ�\n" +
                                                                             "������ " + (playersPowerNum[count] * playerHpUpDown).ToString() + "��ŭ ���� �߽��ϴ�\n";
                break;

            case 2:
                playerHavePowerUINo3Script.playersPowerDetails[count].text = "���ݷ� " +  (playersPowerNum[count] * (playerPower /2)).ToString() + "��ŭ �����߽��ϴ�\n" +
                                                             "������ " + (playersPowerNum[count] * (playerHpUpDown / 2)).ToString() + "��ŭ ���� �߽��ϴ�\n";
                break;

            default:
                playerPowerGetUINo2Script.playerPowerDetails[0].text = "����ġ : ���ݷ��� " + ((playersPowerNum[0] + 1) * playerPower).ToString() + "��ŭ �����߽��ϴ�\n" +
                                                                         "              ������ " + ((playersPowerNum[0] + 1) * playerHpUpDown).ToString() + "��ŭ ���� �߽��ϴ�\n";
                playerPowerGetUINo2Script.playerPowerDetails[1].text = "����ġ : ���ݷ��� " + ((playersPowerNum[1] + 1) * playerPower).ToString() + "��ŭ �����߽��ϴ�\n" +
                                                                         "               ������ " + ((playersPowerNum[1] + 1) * playerHpUpDown).ToString() + "��ŭ ���� �߽��ϴ�\n";
                playerPowerGetUINo2Script.playerPowerDetails[2].text = "����ġ : ���ݷ� " + ((playersPowerNum[2] + 1) * (playerPower )).ToString() + "��ŭ �����߽��ϴ�\n" +
                                                                         "                   ������ " + ((playersPowerNum[2] + 1) * (playerHpUpDown )).ToString() + "��ŭ ���� �߽��ϴ�\n";
                break;
        }
    }
}
