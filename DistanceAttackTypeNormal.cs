using System.Collections;
using System.Collections.Generic;
using UnityEngine;





enum DistanceAttackTypeNormalState
{
    idle,
    attacked,
}
enum DistanceAttackTypeNormalPattern
{
    patternZero,
    patternIdle,
    pattern00,
}


public class DistanceAttackTypeNormal : MonoBehaviour
{
    [SerializeField]
    GameObject fireAttack;
    [SerializeField]
    Transform firePos;
    [SerializeField]
    Transform attackAreaTransform;
    Transform playerTransform;
    Transform enemyTransform;


    bool enemyDistanceCheck;
    DistanceAttackTypeNormalState  distanceAttackTypeNormalState;
    DistanceAttackTypeNormalPattern distanceAttackTypeNormalPattern;
    const float enemyAttackCheckAreaDistance = 7f;
    const float enemyAttackSpeedPatternFar = 0.005f;
    const float isFarOrCloseDistance = 11f;
    [SerializeField]
    EnemyHpPostionScript hpPostionScript;
    [SerializeField]
    DistanceAttackTypeNormalAni distanceAttackTypeNormalAniScript;
    bool trap01;






    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        distanceAttackTypeNormalState = DistanceAttackTypeNormalState.idle;
        distanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.patternZero;

        enemyDistanceCheck = false;
        attackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
        trap01 = false;
        StartCoroutine("WaitForPlayer");
    }




    private void Update()
    {
        if (hpPostionScript.deadOrLive == 1) return;
        if (distanceAttackTypeNormalPattern == DistanceAttackTypeNormalPattern.patternZero) return;
        rotateBoss();

        if (enemyDistanceCheck == true) transform.position = Vector3.Lerp(transform.position, playerTransform.position, enemyAttackSpeedPatternFar);
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < isFarOrCloseDistance) enemyDistanceCheck = false;
    }



    void rotateBoss()
    {
        Vector3 vec = playerTransform.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }





    // 기달리는 행동이에유
    IEnumerator WaitForPlayer()
    {
        yield return null;
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < enemyAttackCheckAreaDistance)
        {
            distanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.patternIdle;

            StartCoroutine("DistanceAttackTypeNormalController");
            StopCoroutine("WaitForPlayer");
        }
        else StartCoroutine("WaitForPlayer");
    }

    // 행동이에유
    IEnumerator DistanceAttackTypeNormalController()
    {
        if (hpPostionScript.deadOrLive == 1) StopCoroutine("DistanceAttackTypeNormalController");
        yield return null;

        yield return new WaitForSeconds(3f);
        if (distanceAttackTypeNormalPattern != DistanceAttackTypeNormalPattern.pattern00 && Vector3.Distance(enemyTransform.position, playerTransform.position) > isFarOrCloseDistance) enemyDistanceCheck = true;
        else
        {
            if (hpPostionScript.deadOrLive == 1) StopCoroutine("DistanceAttackTypeNormalController");
            //attackAreaTransform.GetComponent<SpriteRenderer>().enabled = true;
            // yield return new WaitForSeconds(1f);
            // attackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
            enemyDistanceCheck = false; 
            distanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.pattern00;

            distanceAttackTypeNormalAniScript.fireBallAttack();
            Instantiate(fireAttack, firePos.position, firePos.rotation);
        }
        distanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.patternIdle;
        StartCoroutine("DistanceAttackTypeNormalController");
    }


    void stateChange()
    {
        distanceAttackTypeNormalState = DistanceAttackTypeNormalState.idle;
    }
    void isTrap01CoolTimeOn()
    {
        trap01 = false;
    }




    private void OnTriggerExit(Collider other)
    {
        if (distanceAttackTypeNormalState == DistanceAttackTypeNormalState.attacked) return;

        if (other.gameObject.tag == "PlayerSword01")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.2f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                distanceAttackTypeNormalAniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                distanceAttackTypeNormalState = DistanceAttackTypeNormalState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword02")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.5f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                distanceAttackTypeNormalAniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                distanceAttackTypeNormalState = DistanceAttackTypeNormalState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
        if (other.gameObject.tag == "PlayerSword03")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.8f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                distanceAttackTypeNormalAniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                distanceAttackTypeNormalState = DistanceAttackTypeNormalState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trap01 == true) return;

       else if (other.gameObject.tag == "TrapType2FireAttack"
      || other.gameObject.tag == "TrapType3BoomAttack")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.2f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                distanceAttackTypeNormalAniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                distanceAttackTypeNormalState = DistanceAttackTypeNormalState.attacked;
                Invoke("stateChange", 0.3f);
            }    
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
                distanceAttackTypeNormalAniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else Invoke("isTrap01CoolTimeOn", 2f);

            return;
        }
    }
}
