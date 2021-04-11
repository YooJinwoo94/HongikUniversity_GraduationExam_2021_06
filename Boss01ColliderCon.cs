using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    idle,
    attacked,
}

public class Boss01ColliderCon : MonoBehaviour
{
    public BossState bossState;

    Boss01HpPostionScript hpPostionScript;
    BossAniScript aniScript;

    bool trap01 = false;







    private void Start()
    {
        hpPostionScript = GetComponent<Boss01HpPostionScript>();
        aniScript = GetComponent<BossAniScript>();

        bossState = BossState.idle;
    }






    void stateChange()
    {
        bossState = BossState.idle;
    }
    void isTrap01CoolTimeOn()
    {
        trap01 = false;
        bossState = BossState.idle;
    }


    private void OnTriggerExit(Collider other)
    {
        if (bossState == BossState.attacked) return;


        if (other.gameObject.tag == "PlayerSword01")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.1f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                aniScript.enemyHitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword02")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.3f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                aniScript.enemyHitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword03")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.6f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                aniScript.enemyHitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (trap01 == true) return;

        if (other.gameObject.tag == "TrapType2FireAttack"
         || other.gameObject.tag == "TrapType3BoomAttack")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.2f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }

            else
            {
                aniScript.enemyHitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (trap01 == true) return;

        switch (other.gameObject.tag)
        {
            case "TrapType1Thorn":
                trap01 = true;
                hpPostionScript.enemyDamagedAndImageChange(0.2f);
                hpPostionScript.enemyHpDeadCheck();

                if (hpPostionScript.deadOrLive == 1)
                {
                    aniScript.deadAniOn();
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    aniScript.enemyHitted();
                    bossState = BossState.attacked;
                    Invoke("stateChange", 0.3f);
                    Invoke("isTrap01CoolTimeOn", 2f);
                }
                break;
        }


    }
}
