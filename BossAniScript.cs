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
                aniSet("ResetPattern", resetPatternTime);
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 1:
                aniSet("ResetPattern", resetPatternTime);
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 2:
                aniSet("InvokeParticle00");
                aniSet("ResetPattern", resetPatternTime);
                         
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern02", true);
                //invokeParticle00();    
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 3:
                aniSet("ResetPattern", resetPatternTime);
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern03", true);

                //Invoke("resetPattern", resetPatternTime);
                break;
            case 4:
                aniSet("InvokeParticle01");
                aniSet("InvokeParticle02", 0.8f);
                aniSet("InvokeParticle01", 1.4f);
                aniSet("InvokeParticle02");

                // invokeParticle01();
                //Invoke("invokeParticle02", 0.8f);
                //Invoke("invokeParticle01", 1.4f);

                //enemyParticleConScript.bossAttackParticle[2].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern04", true);

                aniSet("ResetPattern", resetPatternTime);
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 5:
                aniSet("InvokeParticle01");
                aniSet("InvokeParticle02",2f);

                //invokeParticle01();
                //Invoke("invokeParticle02", 2f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern05", true);

                aniSet("ResetPattern", resetPatternTime);
                // Invoke("resetPattern", resetPatternTime);
                break;
            case 6:
                aniSet("InvokeParticle03", 1f);
               // Invoke("invokeParticle03", 1f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern06", true);

                aniSet("ResetPattern", resetPatternTime);
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 7:
                aniSet("InvokeParticle01", 0.8f);
                // Invoke("invokeParticle01", 0.8f);

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern07", true);

                aniSet("ResetPattern", resetPatternTime);
                //Invoke("resetPattern", resetPatternTime);
                break;
            case 8:
                aniSet("InvokeParticle04");
               // enemyParticleConScript.bossAttackParticle[4].Play();

                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern08", true);

                aniSet("ResetPattern", resetPatternTime);
                // Invoke("resetPattern", resetPatternTime);
                break;
            case 9:
                ani.SetBool("Bool_Boss_Waiting", false);
                ani.SetBool("boolBossPattern09", true);

                aniSet("ResetPattern", resetPatternTime);
                // Invoke("resetPattern", resetPatternTime);
                break;
        }
    }




    public void aniSet (string name,float time = 0)
    {
        StartCoroutine(patternTime(name, time));
    }



  IEnumerator patternTime(string name ,float time)
    {
        yield return new WaitForSeconds(time);

        switch (name)
        {
            case "Run":
                ani.SetBool("is_Enemy_Run", true);
                break;

            case "ResetPattern":
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
                break;

            case "Hitted":
                ani.SetBool("is_Enemy_Run", false);
                ani.SetTrigger("is_Enemy_Damaged");
                break;

            case "Dead":
                ani.SetBool("is_Enemy_Run", false);
                ani.SetBool("is_Enemy_Dead", true);
                break;

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

        StopCoroutine(patternTime(name,time));
    }
}


