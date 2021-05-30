using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTypeMonsterAni : MonoBehaviour
{
    private Animator ani;
    private TutorialTypeMonsterMove moveScript;


    const float resetPatternTime = 2f;
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
                ani.SetBool("Bool_Enemy_PatternClose", true);
                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime);
                break;

            case 1:
                ani.SetBool("Bool_Enemy_PatternFar", true);
                break;
        }
    }




    public void aniChoice (string aniType)
    {
        beforeStart();

        switch (aniType)
        {
            case "Wait":
                ani.SetBool("Bool_Enemy_Waiting", true);
                break;

            case "Run":
                ani.SetBool("Bool_Enemy_Waiting", false);
                ani.SetBool("is_Run", true);
                break;

            case "Dead":
                ani.SetBool("Bool_Enemy_Waiting", false);
                ani.SetBool("is_Enemy_Dead", true);
                break;
            case "PatternFar02":
                ani.SetBool("Bool_Enemy_Waiting", false);
                ani.SetBool("Bool_Enemy_PatternFar02", true);
                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime - 0.8f);
                break;
        }
    }


    void beforeStart()
    {
        ani.SetBool("Bool_Enemy_PatternClose", false);
        ani.SetBool("Bool_Enemy_PatternFar", false);
        ani.SetBool("is_Run", false);
        ani.SetBool("is_Enemy_Dead", false);
        ani.SetBool("Bool_Enemy_PatternFar02", false);
    }

    void resetPattern()
    {
        moveScript.enemyPatternStart();
        beforeStart();
        ani.SetBool("Bool_Enemy_Waiting", true);
    }
    void restartPattern()
    {
        moveScript.MakeEnemyPatternIdle();
        moveScript.weaponSwordOff();
        moveScript.enemyPatternStart();
    }
}
