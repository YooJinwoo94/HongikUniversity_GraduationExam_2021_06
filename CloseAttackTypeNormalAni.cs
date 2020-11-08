using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackTypeNormalAni : MonoBehaviour
{
    private Animator enemyAnimator;
    private CloseAttackTypeNormalMove closeAttackTypeNormalMoveScript;

    const float restartPatternTime = 2f;
    const float resetPatternTime = 0.5f;




    private void Awake()
    {
        closeAttackTypeNormalMoveScript = GetComponent<CloseAttackTypeNormalMove>();
        enemyAnimator = GetComponent<Animator>();

        enemyAnimator.SetBool("Bool_Enemy_Waiting", true);
    }


    public void patternChoice(int patternCount)
    {
        switch (patternCount)
        {
            case 0:
                patternClose();
                break;

            case 1:
                patternFar();
                break;
        }
    }

    void patternClose()
    {
        enemyAnimator.SetBool("Bool_Enemy_PatternClose", true);
        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
    void patternFar()
    {
        enemyAnimator.SetBool("Bool_Enemy_PatternFar", true);
    }
    public void patternFar02()
    {
        enemyAnimator.SetBool("Bool_Enemy_PatternFar02", true);


        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }


    void resetPattern()
    {
        closeAttackTypeNormalMoveScript.enemyPatternStart();

        enemyAnimator.SetBool("Bool_Enemy_Waiting", true);

        enemyAnimator.SetBool("Bool_Enemy_PatternClose", false);
        enemyAnimator.SetBool("Bool_Enemy_PatternFar", false);
        enemyAnimator.SetBool("Bool_Enemy_PatternFar02", false);
    }
    void restartPattern()
    {
        closeAttackTypeNormalMoveScript.MakeEnemyPatternIdle();
        closeAttackTypeNormalMoveScript.bossWeaponSwordOff();
        closeAttackTypeNormalMoveScript.enemyPatternStart();
    }
}
