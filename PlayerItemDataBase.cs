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
        playerWeaponNameSpace[0] = "저주받은 진소의 검";
        playerWeaponNameSpace[1] = "삼천호검";
        playerWeaponNameSpace[2] = "최후의 섬광";

        playersWeaponDetails[0] = "이름만 거창한 기본 검입니다.";
        playersWeaponDetails[1] = "가만히 있으면 시간이 느리게 갑니다.";
        playersWeaponDetails[2] = "네?";
    }




}
