using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAniScript : MonoBehaviour
{
    private Animator ani;
    private BossMoveScript moveScript;



    const float resetPatternTime = 0.5f;
    const float restartPatternTime = 2f;

    [SerializeField]
    BossMoveScript bossMoveScript;
    Transform particlePos;

    // Start is called before the first frame update
    void Start()
    {
        particlePos = GameObject.Find("BossParticleParent").transform;
        ani = GetComponent<Animator>();
        moveScript = GetComponent<BossMoveScript>();
    }






    public void bossPatternChoice(int patternCount)
    {
        switch (patternCount)
        {
            case 0:
                Invoke("resetPattern", resetPatternTime);
                break;
            case 1:
                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime);
                break;
            case 2:
                invokeParticle00();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern02", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime);
                break;
            case 3:            
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern03", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime - 0.4f);
                break;
            case 4:
                invokeParticle01();
                Invoke("invokeParticle02", 0.8f);
                Invoke("invokeParticle01", 1.4f);

                bossMoveScript.bossAttackParticleSet[2].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern04", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime);
                break;
            case 5:
                invokeParticle01();
                Invoke("invokeParticle02", 2f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern05", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime + 0.6f);
                break;
            case 6:
                Invoke("invokeParticle03", 1f);
               

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern06", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime);
                break;
            case 7:
                Invoke("invokeParticle01", 0.8f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern07", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime - 0.7f);
                break;
            case 8:
                bossMoveScript.bossAttackParticleSet[4].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern08", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime - 1f);
                break;
            case 9:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern09", true);

                Invoke("resetPattern", resetPatternTime);
                Invoke("restartPattern", restartPatternTime - 1f);
                break;
        }      
    }


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


    void invokeParticle00()
    {
        bossMoveScript.bossAttackParticleSet[0].Play();
    }
    void invokeParticle01()
    {
        bossMoveScript.bossAttackParticleSet[1].Play();
    }
    void invokeParticle02()
    {
        bossMoveScript.bossAttackParticleSet[2].Play();
    }
    void invokeParticle03()
    {
        bossMoveScript.bossAttackParticleSet[3].Play();

        var Object = Instantiate(bossMoveScript.whenBossAttackGroundMakeCrackObj, bossMoveScript.crackPos);
        Object.transform.SetParent(particlePos.transform);
    }
    public void deadAniOn()
    {
        ani.SetBool("is_Enemy_Dead", true);
    }
}
