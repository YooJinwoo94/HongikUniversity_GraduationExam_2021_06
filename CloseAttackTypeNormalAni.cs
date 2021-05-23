using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CloseAttackEnemyType01AtkPattern
{
    patternZero,
    patternIdle,
    patternClose,
    patternFar,
}



public class CloseAttackTypeNormalAni : MonoBehaviour
{
    private Animator ani;

    const float resetColliderTime = 1.6f;
    const float resetAniTime = 0.5f;
    float resetPatternTime = 3f;

    public CloseAttackEnemyType01AtkPattern enemyPattern;
    WeaponColliderCon weaponColliderConScript;

    float startTime;


    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("Bool_Enemy_Waiting", true);

        enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;

        weaponColliderConScript = GetComponent<WeaponColliderCon>();
    }


    public void patternChoice(int patternCount)
    {
       switch(patternCount)
        {
            // close
            case 0:
                ani.SetBool("Bool_Enemy_PatternClose", true);
                weaponColliderConScript.weaponColliderOn(0);
                Invoke("resetAni", resetAniTime);
                Invoke("resetWeaponCollider", 1.6f);
                //Invoke("resetPattern", resetPatternTime);
                break;

            // far
            case 1:
                ani.SetBool("Bool_Enemy_PatternFar", true);
                break;
        }
    }

    public void aniSet(string name)
    {
        switch(name)
        {
            case "Reset_Ani":
               // aniOff();
                ani.SetBool("Bool_Enemy_Waiting", true);             
                break;

            case "Run_Reset":
                aniOff();
                ani.SetBool("is_Run", false);
                break;

            case "Run":
                aniOff();
                ani.SetBool("is_Run", true);
                break;

            case "Hitted":
                aniOff();
                ani.SetTrigger("is_Enemy_Damaged");
                break;

            case "Dead":
                aniOff();
                ani.SetBool("is_Enemy_Dead", true);
                break;

            case "patternFar02":
                aniOff();
                ani.SetBool("Bool_Enemy_PatternFar02", true);
                weaponColliderConScript.weaponColliderOn(0);
                Invoke("resetWeaponCollider", resetAniTime);
                break;

            case "Rest":
                aniOff();
                ani.SetBool("is_Enemy_Rest", true);
                break;

            case "Stuned":
                aniOff();
                ani.SetBool("is_Enemy_Stuned", true);
                Invoke("aniOff", 1f);
                break;
        }    
    }

    public void aniOff()
    {
        ani.SetBool("is_Enemy_Damaged", false);
        ani.SetBool("is_Enemy_Stuned", false);
        ani.SetBool("is_Enemy_Rest", false);
        ani.SetBool("is_Run", false);
        ani.SetBool("Bool_Enemy_PatternClose", false);
        ani.SetBool("Bool_Enemy_PatternFar", false);
        ani.SetBool("Bool_Enemy_PatternFar02", false);
        ani.SetBool("Bool_Enemy_Waiting", false);
    }
    public void resetAni()
    {
        ani.SetBool("is_Enemy_Damaged", false);
        ani.SetBool("is_Enemy_Stuned", false);
        ani.SetBool("is_Enemy_Rest", false);

        ani.SetBool("is_Run", false);
        ani.SetBool("Bool_Enemy_PatternClose", false);
        ani.SetBool("Bool_Enemy_PatternFar", false);
        ani.SetBool("Bool_Enemy_PatternFar02", false);

        ani.SetBool("Bool_Enemy_Waiting", true);
    }
    public void resetPattern()
    {
        enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;
    }  
    void resetWeaponCollider()
    {
        aniOff();
        ani.SetBool("Bool_Enemy_Waiting", true);
        weaponColliderConScript.weaponColliderOff(0);
    }


   public void forIdle()
    {
        ani.SetBool("is_Run", false);
        ani.SetBool("Bool_Enemy_Waiting", true);
    }
}
