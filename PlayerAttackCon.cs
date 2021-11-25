using System.Collections;
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
    PlayerParticleCon playerParticleConScript;

    WeaponState WeaponStateEnum;

    // 공격 관련 
    [HideInInspector]
    public int noOfClicks = 0;
    float lastClickedTime = 0;

    [Header("평타를 때릴시 콤보완성을 위한 딜레이")]
    [SerializeField]
    float maxComboDelay = 1f;
    bool attackCoolTime = false;

    [Header("플래이어의 무기 콜라이더")]
    [SerializeField]
    BoxCollider[] playerSwordBoxCollider;

    bool[] onceCheck = new bool[4];




    // 필요한지 확인.
    [HideInInspector]
   public bool isCool;


    private void Start()
    {
        playerSpConScript = GetComponent<PlayerSpCon>();
        inputScript = GetComponent<PlayerInputScript>();
        playerAnimationScript = GetComponent<PlayerAniScript>();
        playerParticleConScript = GetComponent<PlayerParticleCon>();

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
       // Debug.Log(isCool);

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

        playerParticleConScript.swordParticleOn();
        noOfClicks++;
        if (noOfClicks == 1)
        {           
            playerAnimationScript.playerAniAttackLeftCombo(1);
            SoundManager.Instance.playerAttackSound(0);
        }

        // 숫자가 최소 최대값을 넘지 않도록
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }



    void normalSwordAttackCoolTimeCount(int coolTime = 0)
    {
        if (Time.time - lastClickedTime > maxComboDelay && noOfClicks != 0 )
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
            maxComboDelay = 1f;
            isCool = false;

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
            maxComboDelay = 1f;
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
