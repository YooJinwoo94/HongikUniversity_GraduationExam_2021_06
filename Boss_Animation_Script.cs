using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Animation_Script : MonoBehaviour
{
    private Animator bossAnimator;
    private Boss_Move_Script bossMoveScript;


    const float restartPatternTime = 2f;
    const float resetPatternTime = 0.5f;





    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        bossMoveScript = GetComponent<Boss_Move_Script>();
    }






    public void bossPatternChoice(int patternCount)
    {
        switch (patternCount)
        {
            case 0:
                bossAniPattern00();
                break;
            case 1:
                bossAniPattern01();
                break;
            case 2:
                bossAniPattern02();
                break;
            case 3:
                bossAniPattern03();
                break;
            case 4:
                bossAniPattern04();
                break;
            case 5:
                bossAniPattern05();
                break;
            case 6:
                bossAniPattern06();
                break;
            case 7:
                bossAniPattern07();
                break;
            case 8:
                bossAniPattern08();
                break;
            case 9:
                bossAniPattern09();
                break;
        }      
    }

     void bossAniPattern00()
    {
        Invoke("resetPattern", resetPatternTime);

    }

     void bossAniPattern01()
    {
        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern02()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern02", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern03()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern03", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime - 0.4f);
    }
     void bossAniPattern04()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern04", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern05()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern05", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime + 0.6f);
    }
     void bossAniPattern06()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern06", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern07()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern07", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime - 0.7f);
    }
     void bossAniPattern08()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern08", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
    void bossAniPattern09()
    {
        bossAnimator.SetBool("Bool_Boss_Waiting", false);
        bossAnimator.SetBool("boolBossPattern09", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime-1f);
    }





     void restartPattern()
    {
        bossMoveScript.ifBossPatternEnd();
        bossMoveScript.bossConRestartBossPattern();
    }
    void resetPattern()
    { 
        bossAnimator.SetBool("Bool_Boss_Waiting", true);

        bossAnimator.SetBool("boolBossPattern02", false);
        bossAnimator.SetBool("boolBossPattern03", false);
        bossAnimator.SetBool("boolBossPattern04", false);
        bossAnimator.SetBool("boolBossPattern05", false);
        bossAnimator.SetBool("boolBossPattern06", false);
        bossAnimator.SetBool("boolBossPattern07", false);
        bossAnimator.SetBool("boolBossPattern08", false);
        bossAnimator.SetBool("boolBossPattern09", false);
    }
}
