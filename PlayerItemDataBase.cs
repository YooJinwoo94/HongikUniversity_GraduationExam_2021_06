using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDataBase : MonoBehaviour
{
    [SerializeField]
    public Sprite[] ingameWeaponImages;

    [SerializeField]
    public Sprite[] playerWeaponImage;
    [HideInInspector]
    public string[] playerWeaponNameSpace = new string[10];
    [HideInInspector]
    public string[] playersWeaponDetails = new string[10];
    //===========================================

    private void Start()
    {
        playerWeaponNameSpace[0] = "���ֹ��� ������ ��";
        playerWeaponNameSpace[1] = "��õȣ��";
        playerWeaponNameSpace[2] = "������ ����";

        playersWeaponDetails[0] = "�̸��� ��â�� �⺻ ���Դϴ�.";
        playersWeaponDetails[1] = "������ ������ �ð��� ������ ���ϴ�.";
        playersWeaponDetails[2] = "�̸��� ��â�� ���� ���Դϴ�.";
    }
}
