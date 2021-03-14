using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDataBase : MonoBehaviour
{
    [SerializeField]
    public Sprite[] playerWeaponImage;
    [HideInInspector]
    public string[] playerWeaponNameSpace = null;
    [HideInInspector]
    public string[] playersWeaponDetails = null;
    //===========================================

    private void Start()
    {
        playerWeaponNameSpace[0] = "���ֹ��� ������ ��";
        playerWeaponNameSpace[1] = "��õȣ��";
        playerWeaponNameSpace[2] = "������ ����";

        playersWeaponDetails[0] = "�̸��� ��â�� �⺻ ���Դϴ�.";
        playersWeaponDetails[1] = "������ ������ �ð��� ������ ���ϴ�.";
        playersWeaponDetails[2] = "��?";
    }




}
