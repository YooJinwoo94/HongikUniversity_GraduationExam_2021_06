using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Animation_Script : MonoBehaviour
{
    private Animator playerAnimator;
    private Player_Move_Script playerMoveScript;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMoveScript = GetComponent<Player_Move_Script>();
    }





    public void playerAniWait()
    {
        playerAnimator.SetBool("Bool_Player_Waiting", true);
        playerAnimator.SetBool("Bool_Player_Walking", false);

        playerAnimator.SetBool("Bool_Player_Attack_01", false);
        playerAnimator.SetBool("Bool_Player_Attack_02", false);
        playerAnimator.SetBool("Bool_Player_Attack_03", false);
    }
    public void playerAniWalk()
    {
        playerAnimator.SetBool("Bool_Player_Waiting", false);
        playerAnimator.SetBool("Bool_Player_Walking", true);

        playerAnimator.SetBool("Bool_Player_Attack_01", false);
        playerAnimator.SetBool("Bool_Player_Attack_02", false);
        playerAnimator.SetBool("Bool_Player_Attack_03", false);
    }

    //  구른 경우 
    //======================================================
    public void playerAniRollFront()
    {
        playerAnimator.SetBool("Bool_Player_Roll_Front", true);

        playerAnimator.SetBool("Bool_Player_Roll_Left", false);
        playerAnimator.SetBool("Bool_Player_Roll_Right", false);
        playerAnimator.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollLeft()
    {
        playerAnimator.SetBool("Bool_Player_Roll_Left", true);

        playerAnimator.SetBool("Bool_Player_Roll_Front", false);
        playerAnimator.SetBool("Bool_Player_Roll_Right", false);
        playerAnimator.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollRight()
    {
        playerAnimator.SetBool("Bool_Player_Roll_Right", true);

        playerAnimator.SetBool("Bool_Player_Roll_Front", false);
        playerAnimator.SetBool("Bool_Player_Roll_Left", false);
        playerAnimator.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollBack()
    {
         playerAnimator.SetBool("Bool_Player_Roll_Back", true);

        playerAnimator.SetBool("Bool_Player_Roll_Front", false);
        playerAnimator.SetBool("Bool_Player_Roll_Left", false);
        playerAnimator.SetBool("Bool_Player_Roll_Right", false);
    }

    public void playerDodgeAniReset()
    {
        //playerAnimator.SetBool("Bool_Player_Waiting", true);
        playerAnimator.SetBool("Bool_Player_Roll_Front", false);
        playerAnimator.SetBool("Bool_Player_Roll_Left", false);
        playerAnimator.SetBool("Bool_Player_Roll_Right", false);
        playerAnimator.SetBool("Bool_Player_Roll_Back", false);
    }

    //  공격 한 경우  
    //======================================================
    public void playerAniAttackLeftCombo(int comboCount)
    {
        switch (comboCount)
        {
            case 1 :
                playerAnimator.SetBool("Bool_Player_Waiting", false);
                playerAnimator.SetBool("Bool_Player_Walking", false);

                //====================================================================
                playerAnimator.SetBool("Bool_Player_Attack_01", true);
                //====================================================================
                playerAnimator.SetBool("Bool_Player_Attack_02", false);
                playerAnimator.SetBool("Bool_Player_Attack_03", false);
                break;

            case 2:
                playerAnimator.SetBool("Bool_Player_Waiting", false);
                playerAnimator.SetBool("Bool_Player_Walking", false);

                playerAnimator.SetBool("Bool_Player_Attack_01", false);
                //====================================================================
                playerAnimator.SetBool("Bool_Player_Attack_02", true);
                //====================================================================
                playerAnimator.SetBool("Bool_Player_Attack_03", false);
                break;

            case 3:
                playerAnimator.SetBool("Bool_Player_Waiting", false);
                playerAnimator.SetBool("Bool_Player_Walking", false);

                playerAnimator.SetBool("Bool_Player_Attack_01", false);
                playerAnimator.SetBool("Bool_Player_Attack_02", false);
                //====================================================================
                playerAnimator.SetBool("Bool_Player_Attack_03", true);
                //====================================================================
                break;

            case 0:
                playerAnimator.SetBool("Bool_Player_Waiting", true);
                playerAnimator.SetBool("Bool_Player_Attack_01", false);
                playerAnimator.SetBool("Bool_Player_Attack_02", false);
                playerAnimator.SetBool("Bool_Player_Attack_03", false);
                break;
        }
    }



    //  공격 받은 경우 
    //======================================================

    public void attackedAni(int attackCount)
    {
        ifAttackedTurnOffOtherAni();
        switch (attackCount)
        {
            case 1:
                playerAnimator.SetBool("Bool_Player_Normal_Attacked", true);
                break;
            case 2:
                playerAnimator.SetBool("Bool_Player_Airborne_Attacked", true);
                break;
            case 3:
                playerAnimator.SetBool("Bool_Player_Stun_Attacked", true);
                break;
        }
    }
    void ifAttackedTurnOffOtherAni()
    {
        playerAnimator.SetBool("Bool_Player_Waiting", false);
        playerAnimator.SetBool("Bool_Player_Walking", false);
        playerAnimator.SetBool("Bool_Player_Roll_Front", false);
        playerAnimator.SetBool("Bool_Player_Roll_Left", false);
        playerAnimator.SetBool("Bool_Player_Roll_Right", false);
        playerAnimator.SetBool("Bool_Player_Roll_Back", false);
        playerAnimator.SetBool("Bool_Player_Attack_01", false);
        playerAnimator.SetBool("Bool_Player_Attack_02", false);
        playerAnimator.SetBool("Bool_Player_Attack_03", false);
    }



    public void attackedAniReset()
    {
        playerAnimator.SetBool("Bool_Player_Normal_Attacked", false);
        playerAnimator.SetBool("Bool_Player_Airborne_Attacked", false);
        playerAnimator.SetBool("Bool_Player_Stun_Attacked", false);

        playerAnimator.SetBool("Bool_Player_Waiting", true);
    }
}
