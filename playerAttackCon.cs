using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackCon : MonoBehaviour
{
    enum WeaponState
    {
       normalSword,
    }
    Player_Move_Script playerMoveScript;
    Player_Animation_Script playerAnimationScript;
    playerSpCon playerSpConScript;


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
    BoxCollider playerSwordBoxCollider;
    bool onceCheck02;
    bool onceCheck03;
    [HideInInspector]
   public bool isCool;




    private void Awake()
    {
        WeaponStateEnum = WeaponState.normalSword;

        playerSpConScript = GetComponent<playerSpCon>();
        playerMoveScript = GetComponent<Player_Move_Script>();
        playerAnimationScript = GetComponent<Player_Animation_Script>();
        playerSwordBoxCollider.enabled = false;

        onceCheck02 = false;
        onceCheck03 = false;
        isCool = false; 
        StartCoroutine("attackCool");
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

        playerMoveScript.state = PlayerState.attack;
        playerSwordBoxCollider.enabled = true;

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
    void normalSwordAttackCoolTimeCount()
    {
        if (playerMoveScript.state != PlayerState.attack)
        {
            isCool = false;
            onceCheck02 = false;
            onceCheck03 = false;

            noOfClicks = 0;
            playerSwordBoxCollider.enabled = false;

            attackCoolTime = false;
          //  playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;
        }
        else if (Time.time - lastClickedTime > maxComboDelay)
        {
            onceCheck02 = false;
            onceCheck03 = false;

            noOfClicks = 0;
            playerSwordBoxCollider.enabled = false;
            attackCoolTime = false;
            playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;

            playerMoveScript.state = PlayerState.idle;
        }
        else if (Time.time - lastClickedTime < maxComboDelay)
        {
            if (noOfClicks >= 2 && onceCheck02 == false)
            {
                onceCheck02 = true;
                playerSwordBoxCollider.enabled = true;

                playerAnimationScript.playerAniAttackLeftCombo(2);
                SoundManager.Instance.playerAttackSound(0);

                maxComboDelay = 0.8f;
            }
            else if (noOfClicks == 3 && onceCheck03 == false)
            {
                playerSpConScript.spDown();

                isCool = true;
                onceCheck03 = true;
                playerSwordBoxCollider.enabled = true;

                playerAnimationScript.playerAniAttackLeftCombo(3);
                SoundManager.Instance.playerAttackSound(1);
            }
        }
    }




    IEnumerator attackCool()
    {
        while (true)
        {
            yield return null;
            switch (WeaponStateEnum)
            {
                case WeaponState.normalSword:
                    normalSwordAttackCoolTimeCount();
                    break;
            }
        }
    }
}
