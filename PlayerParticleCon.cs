using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwordArray
{
    public ParticleSystem[] swordAttackParticle;

}

public class PlayerParticleCon : MonoBehaviour
{
    [SerializeField]
    GameObject stunParticle;

    [SerializeField]
    SwordArray[] swordType;


    [SerializeField]
    PlayerAttackCon playerAttackConScript;
    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUINo4Script;
    [SerializeField]
    PlayerWeaponInGameUI playerWeaponInGameUiScript;

    int playerWeaponType = 0;





    // 현재 들고 있는 무기가 무엇인지 확인하고
    // 파티클을 조절한다.
    public void swordParticleOn()
    {
        // playerWeaponInGameUiScript.checkNowWeaponUISelected => 현재 플레이어가 들고있는 무기가 어떤 슬롯에 있는 무기인지 알려준다.
        // 현재 플레이어가 들고 있는 무기는 1번슬롯이다.
        // 현재 플레이어가 들고 있는 무기는 2번슬롯이다.

        // 현재 들고 있는 무기의 정보값을 가져온다.
        switch (playerHaveWeaponUINo4Script.playersWeaponType[playerWeaponInGameUiScript.checkNowWeaponUISelected])
        {
            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType1:
                playerWeaponType = 0;
                break;
            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType2:
                playerWeaponType = 1;
                break;
            case PlayerHaveWeaponUINo4.PlayersWeaponType.weaponType3:
                playerWeaponType = 2;
                break;
        }

        if (playerWeaponType == 0) return;

        swordType[playerWeaponType].swordAttackParticle[playerAttackConScript.noOfClicks].Play();
    }



}
