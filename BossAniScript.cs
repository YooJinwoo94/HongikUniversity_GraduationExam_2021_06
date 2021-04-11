using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public enum BossPatternStorageToCheckLastOne
{
    bossWait = 0,

    pattern01 = 1,
    pattern02 = 2,
    pattern03 = 3,
    pattern04 = 4,
    pattern05 = 5,
    pattern06 = 6,
    pattern07 = 7,
    pattern08 = 8,
    pattern09 = 9,

    patternZero,
}





public class BossAniScript : MonoBehaviour
{
    [SerializeField]
    GameObject crackParticle;

   public  Animator ani;

    const float resetPatternTime = 0.5f;
    const float restartPatternTime = 2f;

    EnemyParticleCon enemyParticleConScript;
    Transform particlePos;

    public BossPatternStorageToCheckLastOne bossPatternStorageToCheckLastOneState;




    void Start()
    {
        particlePos = GameObject.Find("BossParticleParent").transform;
        ani = GetComponent<Animator>();
        enemyParticleConScript = GetComponent<EnemyParticleCon>();

        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.patternZero;
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
                break;
            case 2:
                invokeParticle00();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern02", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 3:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern03", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 4:
                invokeParticle01();
                Invoke("invokeParticle02", 0.8f);
                Invoke("invokeParticle01", 1.4f);

                enemyParticleConScript.bossAttackParticle[2].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern04", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 5:
                invokeParticle01();
                Invoke("invokeParticle02", 2f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern05", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 6:
                Invoke("invokeParticle03", 1f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern06", true);

                
                Invoke("resetPattern", resetPatternTime);
                break;
            case 7:
                Invoke("invokeParticle01", 0.8f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern07", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 8:
                enemyParticleConScript.bossAttackParticle[4].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern08", true);

                Invoke("resetPattern", resetPatternTime);
                break;
            case 9:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern09", true);

                Invoke("resetPattern", resetPatternTime);
                break;
        }
    }


   public void resetPattern()
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



    void invokeParticle00()
    {
        enemyParticleConScript.bossAttackParticle[0].Play();
    }
    void invokeParticle01()
    {
        enemyParticleConScript.bossAttackParticle[1].Play();
    }
    void invokeParticle02()
    {
        enemyParticleConScript.bossAttackParticle[2].Play();
    }
    void invokeParticle03()
    {
        enemyParticleConScript.bossAttackParticle[3].Play();

        var Object = Instantiate(enemyParticleConScript.bossAttackParticle[5], enemyParticleConScript.bossParticlePos[0]);
        Object.transform.SetParent(particlePos.transform);
    }
    public void deadAniOn()
    {
        ani.SetBool("is_Enemy_Dead", true);
    }

    public void enemyHitted()
    {
        ani.SetTrigger("is_Enemy_Damaged");
    }
}


