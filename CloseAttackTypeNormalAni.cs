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
    float resetPatternTime = 2f;

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
                Invoke("resetWeaponCollider", resetColliderTime);
                Invoke("resetPattern", resetPatternTime);
                break;

            // far
            case 1:
                ani.SetBool("Bool_Enemy_PatternFar", true);
                break;
        }
    }


    public void enemyHitted()
    {
        ani.SetTrigger("is_Enemy_Damaged");
    }

    public void patternFar02()
    {
        ani.SetBool("Bool_Enemy_PatternFar02", true);
        weaponColliderConScript.weaponColliderOn(0);
        Invoke("resetAniAndCollider", resetAniTime);
        Invoke("resetPattern", resetPatternTime);
    }
    void resetAni()
    {
        ani.SetBool("Bool_Enemy_Waiting", true);

        ani.SetBool("Bool_Enemy_PatternClose", false);
        ani.SetBool("Bool_Enemy_PatternFar", false);
        ani.SetBool("Bool_Enemy_PatternFar02", false);
    }
    void resetPattern()
    {
        enemyPattern = CloseAttackEnemyType01AtkPattern.patternZero;
    }
    public void deadAniOn()
    {
        ani.SetBool("is_Enemy_Dead", true);
    }
    void resetWeaponCollider()
    {
        weaponColliderConScript.weaponColliderOff(0);
    }
}
