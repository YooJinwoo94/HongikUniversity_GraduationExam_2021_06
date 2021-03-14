using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAniScript : MonoBehaviour
{
    private Animator ani;
    private BossMoveScript moveScript;



    const float resetPatternTime = 0.5f;
    const float restartPatternTime = 2f;




    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        moveScript = GetComponent<BossMoveScript>();
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
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern02", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern03()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern03", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime - 0.4f);
    }
     void bossAniPattern04()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern04", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern05()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern05", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime + 0.6f);
    }
     void bossAniPattern06()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern06", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern07()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern07", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime - 0.7f);
    }
     void bossAniPattern08()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern08", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime);
    }
     void bossAniPattern09()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("boolBossPattern09", true);

        Invoke("resetPattern", resetPatternTime);
        Invoke("restartPattern", restartPatternTime-1f);
    }




    // 작업을 해야할때는 
    // 패턴 1~2개로 하고 넘어가서 남은 부분을 해서 전체적인 완성도를 
    // 올려야 한다.
    void resetPattern()
    {
        ani.SetBool("Bool_Boss_Waiting", true);

        ani.SetBool("boolBossPattern02", false);
        ani.SetBool("boolBossPattern03", false);
        ani.SetBool("boolBossPattern04", false);
        ani.SetBool("boolBossPattern05", false);
        ani.SetBool("boolBossPattern06", false);
        ani.SetBool("boolBossPattern07", false);
        ani.SetBool("boolBossPattern08", false);
        ani.SetBool("boolBossPattern09", false);
    }
    void restartPattern()
    {
        moveScript.ifBossPatternEnd();
        moveScript.bossConRestartBossPattern();
    }

}
