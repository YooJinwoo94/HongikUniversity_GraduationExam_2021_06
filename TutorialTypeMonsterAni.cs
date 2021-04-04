using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTypeMonsterAni : MonoBehaviour
{
    private Animator ani;
    private TutorialTypeMonsterMove moveScript;


    const float resetPatternTime = 0.5f;
    const float restartPatternTime = 2f;



    private void Start()
    {
        moveScript = GetComponent<TutorialTypeMonsterMove>();
        ani = GetComponent<Animator>();

        ani.SetBool("Bool_Enemy_Waiting", true);
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
        ani.SetBool("Bool_Enemy_PatternClose", true);
        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
    void patternFar()
    {
        ani.SetBool("Bool_Enemy_PatternFar", true);
    }
    public void patternFar02()
    {
        ani.SetBool("Bool_Enemy_PatternFar02", true);


        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime - 0.8f);
    }


    void resetPattern()
    {
        moveScript.enemyPatternStart();

        ani.SetBool("Bool_Enemy_Waiting", true);

        ani.SetBool("Bool_Enemy_PatternClose", false);
        ani.SetBool("Bool_Enemy_PatternFar", false);
        ani.SetBool("Bool_Enemy_PatternFar02", false);
    }
    void restartPattern()
    {
        moveScript.MakeEnemyPatternIdle();
        moveScript.bossWeaponSwordOff();
        moveScript.enemyPatternStart();
    }
    public void deadAniOn()
    {
        ani.SetBool("is_Enemy_Dead", true);
    }
}
