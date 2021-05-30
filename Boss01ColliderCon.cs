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

    [SerializeField]
    SkinnedMeshRenderer bossSkinMeshRender;
    [SerializeField]
    GameObject[] hitParticle;
    PlayerCamManager camShackManagerScript;
    TimeManager timeManagerScript;




    private void Start()
    {
        hpPostionScript = GetComponent<Boss01HpPostionScript>();
        aniScript = GetComponent<BossAniScript>();

        bossState = BossState.idle;

        camShackManagerScript = GameObject.Find("PlayerCamManager").GetComponent<PlayerCamManager>();
        timeManagerScript = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }




    void hitObjOnOff()
    {
        if (hitParticle[0].activeInHierarchy == false)
        {
            hitParticle[0].SetActive(true);
            hitParticle[1].SetActive(false);
        }
        else
        {
            hitParticle[0].SetActive(false);
            hitParticle[1].SetActive(true);
        }
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

            StartCoroutine("onDamgeColorChange");

            camShackManagerScript.shake();
            timeManagerScript.playerAttackTime();

            hitObjOnOff();

            if (hpPostionScript.deadOrLive == 1)
            {
                bossSkinMeshRender.materials[0].color = Color.white;

                aniScript.resetAttackPattern();
                aniScript.dead();
                Destroy(this.gameObject, 3f);
                bossSkinMeshRender.materials[0].color = Color.white;
            }
            else
            {
                aniScript.hitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword02")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.3f);
            hpPostionScript.enemyHpDeadCheck();

            StartCoroutine("onDamgeColorChange");

            camShackManagerScript.shake();
            timeManagerScript.playerAttackTime();

            hitObjOnOff();

            if (hpPostionScript.deadOrLive == 1)
            {
                bossSkinMeshRender.materials[0].color = Color.white;

                aniScript.resetAttackPattern();
                aniScript.dead();
                Destroy(this.gameObject, 3f);
                bossSkinMeshRender.materials[0].color = Color.white;
            }
            else
            {
                aniScript.hitted();
                bossState = BossState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword03")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.6f);
            hpPostionScript.enemyHpDeadCheck();

            StartCoroutine("onDamgeColorChange");

            camShackManagerScript.shake();
            timeManagerScript.playerAttackTime();

            hitObjOnOff();

            if (hpPostionScript.deadOrLive == 1)
            {
                bossSkinMeshRender.materials[0].color = Color.white;

                aniScript.resetAttackPattern();
                aniScript.dead();
                Destroy(this.gameObject, 3f);
                bossSkinMeshRender.materials[0].color = Color.white;
            }
            else
            {
                aniScript.hitted();
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

            StartCoroutine("onDamgeColorChange");

            if (hpPostionScript.deadOrLive == 1)
            {
                bossSkinMeshRender.materials[0].color = Color.white;

                aniScript.resetAttackPattern();
                aniScript.dead();
                Destroy(this.gameObject, 3f);
                bossSkinMeshRender.materials[0].color = Color.white;
            }

            else
            {
                aniScript.hitted();
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

                StartCoroutine("onDamgeColorChange");

                if (hpPostionScript.deadOrLive == 1)
                {
                    bossSkinMeshRender.materials[0].color = Color.white;

                    aniScript.resetAttackPattern();
                    aniScript.dead();
                    Destroy(this.gameObject, 3f);
                    bossSkinMeshRender.materials[0].color = Color.white;
                }
                else
                {
                    aniScript.hitted();
                    bossState = BossState.attacked;
                    Invoke("stateChange", 0.3f);
                    Invoke("isTrap01CoolTimeOn", 2f);
                }
                break;
        }
    }




    IEnumerator onDamgeColorChange()
    {
        bossSkinMeshRender.materials[0].color = Color.red;
        yield return new WaitForSeconds(0.2f);

        if (hpPostionScript.deadOrLive != 1) bossSkinMeshRender.materials[0].color = Color.white;
        StopCoroutine("onDamgeColorChange");
    }
}
