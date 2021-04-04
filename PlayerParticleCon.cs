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





    // ���� ��� �ִ� ���Ⱑ �������� Ȯ���ϰ�
    // ��ƼŬ�� �����Ѵ�.
    public void swordParticleOn()
    {
        // playerWeaponInGameUiScript.checkNowWeaponUISelected => ���� �÷��̾ ����ִ� ���Ⱑ � ���Կ� �ִ� �������� �˷��ش�.
        // ���� �÷��̾ ��� �ִ� ����� 1�������̴�.
        // ���� �÷��̾ ��� �ִ� ����� 2�������̴�.

        // ���� ��� �ִ� ������ �������� �����´�.
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
