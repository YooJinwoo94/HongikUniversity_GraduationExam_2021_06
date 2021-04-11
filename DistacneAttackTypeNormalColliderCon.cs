using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistacneAttackTypeNormalColliderCon : MonoBehaviour
{
    public enum DistacneAttackEnemy01IsAttacked
    {
        idle,
        attacked,
    }
    public DistacneAttackEnemy01IsAttacked IsAttackedState;



    EnemyHpPostionScript hpPostionScript;
    DistanceAttackTypeNormalAni aniScript;
    bool trap01;



    private void Start()
    {
        trap01 = false;
        aniScript = GetComponent<DistanceAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();

        IsAttackedState = DistacneAttackEnemy01IsAttacked.idle;
    }





    void stateChange()
    {
        IsAttackedState = DistacneAttackEnemy01IsAttacked.idle;
    }
    void isTrap01CoolTimeOn()
    {
        trap01 = false;
    }


    private void OnTriggerExit(Collider other)
    {
        if (IsAttackedState == DistacneAttackEnemy01IsAttacked.attacked) return;

        switch (other.gameObject.tag)
        {
            case "PlayerSword01":
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
                    IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;

            case "PlayerSword02":
                hpPostionScript.enemyDamagedAndImageChange(0.5f);
                hpPostionScript.enemyHpDeadCheck();

                if (hpPostionScript.deadOrLive == 1)
                {
                    aniScript.deadAniOn();
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    aniScript.enemyHitted();
                    IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;

            case "PlayerSword03":
                hpPostionScript.enemyDamagedAndImageChange(0.8f);
                hpPostionScript.enemyHpDeadCheck();

                if (hpPostionScript.deadOrLive == 1)
                {
                    aniScript.deadAniOn();
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    aniScript.enemyHitted();
                    IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsAttackedState == DistacneAttackEnemy01IsAttacked.attacked) return;

        switch (other.gameObject.tag)
        {
            case "TrapType2FireAttack":
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
                    IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;

            case "TrapType3BoomAttack":
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
                    IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (trap01 == true) return;

        if (other.gameObject.tag == "TrapType1Thorn")
        {
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
                IsAttackedState = DistacneAttackEnemy01IsAttacked.attacked;
                Invoke("stateChange", 0.3f);
                Invoke("isTrap01CoolTimeOn", 2f);
            }
            return;
        }
    }
}
