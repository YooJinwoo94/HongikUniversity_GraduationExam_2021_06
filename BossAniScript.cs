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

    const float resetPatternTime = 2f;

    EnemyParticleCon enemyParticleConScript;
    Transform particlePos;

    public BossPatternStorageToCheckLastOne bossPatternStorageToCheckLastOneState;

    IEnumerator enumerator;
    string invokeName = null;

    void Start()
    {

        particlePos = GameObject.Find("BossParticleParent").transform;
        ani = GetComponent<Animator>();
        enemyParticleConScript = GetComponent<EnemyParticleCon>();

        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.patternZero;
    }






    public void bossPatternChoice(int patternCount)
    {
        Debug.Log(patternCount);
        switch (patternCount)
        {
            case 0:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 1:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 2:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern02", true);
                ani.SetBool("boolBossPattern02", true);

                invokeName = "InvokeParticle00";
                Invoke("particle", 0f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 3:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern03", true);
                ani.SetBool("boolBossPattern03", true);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 4:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern04", true);
                ani.SetBool("boolBossPattern04", true);

                invokeName = "InvokeParticle01";
                Invoke("particle", 0f);
                invokeName = "InvokeParticle02";
                Invoke("particle", 0.8f);
                invokeName = "InvokeParticle01";
                Invoke("particle", 1.4f);
                invokeName = "InvokeParticle02";
                Invoke("particle", 0f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 5:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern05", true);
                ani.SetBool("boolBossPattern05", true);

                invokeName = "InvokeParticle01";
                Invoke("particle", 0f);
                invokeName = "InvokeParticle02";
                Invoke("particle", 2f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 6:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern06", true);
                ani.SetBool("boolBossPattern06", true);

                invokeName = "InvokeParticle03";
                Invoke("particle", 1f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 7:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern07", true);
                ani.SetBool("boolBossPattern07", true);

                invokeName = "InvokeParticle01";
                Invoke("particle", 0.8f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 8:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern08", true);


                invokeName = "InvokeParticle04";
                Invoke("particle",0f);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
            case 9:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("boolBossPattern09", true);
                ani.SetBool("boolBossPattern09", true);

                Invoke("resetAttackPattern", resetPatternTime);
                break;
        }
    }






    void particle ()
    {
        switch(invokeName)
        {
            case "InvokeParticle00":
                enemyParticleConScript.bossAttackParticle[0].Play();
                break;

            case "InvokeParticle01":
                enemyParticleConScript.bossAttackParticle[1].Play();
                break;

            case "InvokeParticle02":
                enemyParticleConScript.bossAttackParticle[2].Play();
                break;

            case "InvokeParticle03":
                enemyParticleConScript.bossAttackParticle[3].Play();

                var Object = Instantiate(enemyParticleConScript.bossAttackParticle[5], enemyParticleConScript.bossParticlePos[0]);
                Object.transform.SetParent(particlePos.transform);
                break;

            case "InvokeParticle04":
                enemyParticleConScript.bossAttackParticle[4].Play();
                break;
        }
    }




    public void resetAttackPattern ()
    {
        ani.SetBool("boolBossPattern02", false);
        ani.SetBool("boolBossPattern03", false);
        ani.SetBool("boolBossPattern04", false);
        ani.SetBool("boolBossPattern05", false);
        ani.SetBool("boolBossPattern06", false);
        ani.SetBool("boolBossPattern07", false);
        ani.SetBool("boolBossPattern08", false);
        ani.SetBool("boolBossPattern09", false);
        ani.SetBool("is_Enemy_Run", false);

        ani.SetBool("Bool_Boss_Waiting", true);
    }

    public void run()
    {
        ani.SetBool("Bool_Boss_Waiting", false);
        ani.SetBool("is_Enemy_Run", true);
    }

   public void hitted()
    {
        ani.SetBool("is_Enemy_Run", false);
        ani.SetTrigger("is_Enemy_Damaged");
    }
    public void dead ()
    {
        ani.SetBool("is_Enemy_Run", false);
        ani.SetBool("is_Enemy_Dead", true);
    }
}


