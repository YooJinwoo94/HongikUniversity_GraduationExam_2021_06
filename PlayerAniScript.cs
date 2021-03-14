using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAniScript : MonoBehaviour
{
    private Animator ani;
    private PlayerInputScript inputScript;

    private void Start()
    {
        ani = GetComponent<Animator>();
        inputScript = GetComponent<PlayerInputScript>();
    }





    public void playerAniWait()
    {
        ani.SetBool("Bool_Player_Waiting", true);
        ani.SetBool("Bool_Player_Walking", false);

        ani.SetBool("Bool_Player_Attack_01", false);
        ani.SetBool("Bool_Player_Attack_02", false);
        ani.SetBool("Bool_Player_Attack_03", false);
    }
    public void playerAniWalk()
    {
        ani.SetBool("Bool_Player_Waiting", false);
        ani.SetBool("Bool_Player_Walking", true);

        ani.SetBool("Bool_Player_Attack_01", false);
        ani.SetBool("Bool_Player_Attack_02", false);
        ani.SetBool("Bool_Player_Attack_03", false);
    }

    //  구른 경우 
    //======================================================
    public void playerAniRollFront()
    {
        ani.SetBool("Bool_Player_Roll_Front", true);

        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
        ani.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollLeft()
    {
        ani.SetBool("Bool_Player_Roll_Left", true);

        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
        ani.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollRight()
    {
        ani.SetBool("Bool_Player_Roll_Right", true);

        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Back", false);
    }
    public void playerAniRollBack()
    {
         ani.SetBool("Bool_Player_Roll_Back", true);

        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
    }

    public void playerDodgeAniReset()
    {
        //playerAnimator.SetBool("Bool_Player_Waiting", true);
        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
        ani.SetBool("Bool_Player_Roll_Back", false);
    }

    //  공격 한 경우  
    //======================================================
    public void playerAniAttackLeftCombo(int comboCount)
    {
        switch (comboCount)
        {
            case 1 :
                ani.SetBool("Bool_Player_Waiting", false);
                ani.SetBool("Bool_Player_Walking", false);

                //====================================================================
                ani.SetBool("Bool_Player_Attack_01", true);
                //====================================================================
                ani.SetBool("Bool_Player_Attack_02", false);
                ani.SetBool("Bool_Player_Attack_03", false);
                break;

            case 2:
                ani.SetBool("Bool_Player_Waiting", false);
                ani.SetBool("Bool_Player_Walking", false);

                ani.SetBool("Bool_Player_Attack_01", false);
                //====================================================================
                ani.SetBool("Bool_Player_Attack_02", true);
                //====================================================================
                ani.SetBool("Bool_Player_Attack_03", false);
                break;

            case 3:
                ani.SetBool("Bool_Player_Waiting", false);
                ani.SetBool("Bool_Player_Walking", false);

                ani.SetBool("Bool_Player_Attack_01", false);
                ani.SetBool("Bool_Player_Attack_02", false);
                //====================================================================
                ani.SetBool("Bool_Player_Attack_03", true);
                //====================================================================
                break;

            case 0:
                ani.SetBool("Bool_Player_Waiting", true);
                ani.SetBool("Bool_Player_Attack_01", false);
                ani.SetBool("Bool_Player_Attack_02", false);
                ani.SetBool("Bool_Player_Attack_03", false);
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
                ani.SetBool("Bool_Player_Normal_Attacked", true);
                break;
            case 2:
                ani.SetBool("Bool_Player_Airborne_Attacked", true);
                break;
            case 3:
                ani.SetBool("Bool_Player_Stun_Attacked", true);
                break;
        }
    }
    void ifAttackedTurnOffOtherAni()
    {
        ani.SetBool("Bool_Player_Waiting", false);
        ani.SetBool("Bool_Player_Walking", false);
        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
        ani.SetBool("Bool_Player_Roll_Back", false);
        ani.SetBool("Bool_Player_Attack_01", false);
        ani.SetBool("Bool_Player_Attack_02", false);
        ani.SetBool("Bool_Player_Attack_03", false);
    }



    public void attackedAniReset()
    {
        ani.SetBool("Bool_Player_Normal_Attacked", false);
        ani.SetBool("Bool_Player_Airborne_Attacked", false);
        ani.SetBool("Bool_Player_Stun_Attacked", false);

        ani.SetBool("Bool_Player_Waiting", true);
    }
}
