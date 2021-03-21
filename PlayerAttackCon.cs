﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCon : MonoBehaviour
{
    enum WeaponState
    {
       normalSword,
    }
    //  수정하기  class는 꼭 대문자로 하자.
    PlayerInputScript inputScript;
    PlayerAniScript playerAnimationScript;
    PlayerSpCon playerSpConScript;


    WeaponState WeaponStateEnum;

    // 공격 관련 
    int noOfClicks = 0;
    float lastClickedTime = 0;

    [Header("평타를 때릴시 콤보완성을 위한 딜레이")]
    [SerializeField]
    float maxComboDelay = 2f;
    bool attackCoolTime = false;

    [Header("플래이어의 무기 콜라이더")]
    [SerializeField]
    BoxCollider[] playerSwordBoxCollider;

    bool[] onceCheck = new bool[4];




    // 필요한지 확인.
    [HideInInspector]
   public bool isCool;

    // 
    private void Start()
    {
        playerSpConScript = GetComponent<PlayerSpCon>();
        inputScript = GetComponent<PlayerInputScript>();
        playerAnimationScript = GetComponent<PlayerAniScript>();

        WeaponStateEnum = WeaponState.normalSword;

       
        for (int i = 0; i<3; i++)
        {
            playerSwordBoxCollider[i].enabled = false;
        }
        
        onceCheck[2] = false;
        onceCheck[3] = false;

        isCool = false;
    }



    private void Update()
    {
        switch (WeaponStateEnum)
        {
            case WeaponState.normalSword:
                normalSwordAttackCoolTimeCount();
                break;
        }
    }

    public void whenAttackCheckWeapon()
    {
        switch (WeaponStateEnum)
        {
            case WeaponState.normalSword:
                normalSwordAttack();
                break;
        }
    }





    void normalSwordAttack()
    {
        if (noOfClicks >= 3) return;

        inputScript.state = PlayerState.attack;
        for (int i = 0; i < 3; i++)
        {
            playerSwordBoxCollider[i].enabled = true;
        }


        attackCoolTime = true;
        lastClickedTime = Time.time;

        noOfClicks++;
        if (noOfClicks == 1)
        {
            playerAnimationScript.playerAniAttackLeftCombo(1);
            SoundManager.Instance.playerAttackSound(0);
        }

        // 숫자가 최소 최대값을 넘지 않도록
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }

    // 추상화 attack with cooltime in normalsword
    //attack with cooltime in normalsword
    //매개함수를 적어서 표현하는것도 방법이다. 이거 중요함
    void normalSwordAttackCoolTimeCount(int coolTime = 0)
    {
        if (inputScript.state != PlayerState.attack)
        {
            isCool = false;
            onceCheck[2] = false;
            onceCheck[3] = false;

            noOfClicks = 0;
            for (int i = 0; i < 3; i++)
            {
                playerSwordBoxCollider[i].enabled = false;
            }

            attackCoolTime = false;
            //  playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;
            return;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            onceCheck[2] = false;
            onceCheck[3] = false;

            noOfClicks = 0;
            for (int i = 0; i < 3; i++)
            {
                playerSwordBoxCollider[i].enabled = false;
            }
            attackCoolTime = false;
            playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;

            inputScript.state = PlayerState.idle;
            return;
        }
              
        if (noOfClicks >= 2 && onceCheck[2] == false)
        {
            onceCheck[2] = true;
            for (int i = 0; i < 3; i++)
            {
                playerSwordBoxCollider[i].enabled = true;
            }

            playerAnimationScript.playerAniAttackLeftCombo(2);
             SoundManager.Instance.playerAttackSound(0);

             maxComboDelay = 0.8f;
            return;
        }
        if (noOfClicks == 3 && onceCheck[3] == false)
        {
             playerSpConScript.spDown();

             isCool = true;
             onceCheck[3] = true;
            for (int i = 0; i < 3; i++)
            {
                playerSwordBoxCollider[i].enabled = true;
            }

            playerAnimationScript.playerAniAttackLeftCombo(3);
             SoundManager.Instance.playerAttackSound(1);
             return;
        }
        
    }
}
