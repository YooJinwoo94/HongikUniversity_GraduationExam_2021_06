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



    public void playerAniParring()
    {
        aniReset();

        ani.SetTrigger("Trigger_PlayerParring");
    }

    public void playerAniWait()
    {
        aniReset();

        ani.SetBool("Bool_Player_Waiting", true);
    }
    public void playerAniWalk()
    {
        aniReset();

        ani.SetBool("Bool_Player_Walking", true);
    }

    //  구른 경우 
    //======================================================
    public void playerAniRollFront()
    {
        aniReset();

        ani.SetBool("Bool_Player_Roll_Front", true);
    }
    public void playerAniRollLeft()
    {
        aniReset();

        ani.SetBool("Bool_Player_Roll_Left", true);
    }
    public void playerAniRollRight()
    {
        aniReset();

        ani.SetBool("Bool_Player_Roll_Right", true);
    }
    public void playerAniRollBack()
    {
        aniReset();

        ani.SetBool("Bool_Player_Roll_Back", true);
    }

    public void playerDodgeAniReset()
    {
        aniReset();

        ani.SetBool("Bool_Player_Waiting", true);
    }

    //  공격 한 경우  
    //======================================================
    public void playerAniAttackLeftCombo(int comboCount)
    {
        switch (comboCount)
        {
            case 1 :
                aniReset();

                ani.SetBool("Bool_Player_Attack_01", true);
                break;

            case 2:
                aniReset();
               
                ani.SetBool("Bool_Player_Attack_02", true);
                break;

            case 3:
                aniReset();

                ani.SetBool("Bool_Player_Attack_03", true);
                break;

            case 0:
                aniReset();

                ani.SetBool("Bool_Player_Waiting", true);
                break;
        }
    }



    //  공격 받은 경우 
    //======================================================

    public void attackedAni(int attackCount)
    {
        aniReset();

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

    public void attackedAniReset()
    {
        aniReset();

        ani.SetBool("Bool_Player_Waiting", true);
    }

    void aniReset()
    {
        ani.SetBool("Bool_Player_Waiting", false);
        ani.SetBool("Bool_Player_Walking", false);

        ani.SetBool("Bool_Player_Attack_01", false);
        ani.SetBool("Bool_Player_Attack_02", false);
        ani.SetBool("Bool_Player_Attack_03", false);

        ani.SetBool("Bool_Player_Roll_Left", false);
        ani.SetBool("Bool_Player_Roll_Front", false);
        ani.SetBool("Bool_Player_Roll_Right", false);
        ani.SetBool("Bool_Player_Roll_Back", false);

        ani.SetBool("Bool_Player_Normal_Attacked", false);
        ani.SetBool("Bool_Player_Airborne_Attacked", false);
        ani.SetBool("Bool_Player_Stun_Attacked", false);
    }
}
