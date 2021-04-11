using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CloseAttackTypeNormalState
{
    idle,
    attacked,
}
enum CloseAttackTypeNormalPattern
{
    patternZero,
    patternIdle,
    patternClose,
    patternFar,
}

public class CloseAttackTypeNormalMove : MonoBehaviour
{
    [SerializeField]
    BoxCollider weaponCollider;
    [SerializeField]
    EnemyHpPostionScript hpPostionScript;

    Transform playerTransform;
    Transform enemyTransform;
    CloseAttackTypeNormalState closeAttackTypeNormalState;
    CloseAttackTypeNormalPattern enemyPattern;

    bool enemyDistanceCheck;
    const float enemyPatternCloseDistance = 1f;
    const float enemyPatternFarDistance = 3f;
    const float enemyAttackSpeedPatternClose = 0.02f;
    const float enemyAttackSpeedPatternFar = 0.04f;
    const float enemyAttackCheckAreaDistance = 12f;
    const float isFarOrCloseDistance = 7f;

    private CloseAttackTypeNormalAni aniScript;

    bool trap01;




    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aniScript = GetComponent<CloseAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();

        enemyPattern = CloseAttackTypeNormalPattern.patternZero;
        closeAttackTypeNormalState = CloseAttackTypeNormalState.idle;

        trap01 = false;
        weaponCollider.enabled = false;
        enemyDistanceCheck = false;
        StartCoroutine("WaitForPlayer");
    }

    private void Update()
    {
        if (hpPostionScript.deadOrLive == 1) return;
        if (enemyPattern == CloseAttackTypeNormalPattern.patternZero) return;
        switch (enemyPattern)
         {
            case CloseAttackTypeNormalPattern.patternClose:
                resetNowStateToStopFollowing(enemyPatternCloseDistance, enemyAttackSpeedPatternClose);
                break;

            case CloseAttackTypeNormalPattern.patternFar:
                resetNowStateToStopFollowing(enemyPatternCloseDistance, enemyAttackSpeedPatternFar);
                break;
         }
        rotateBoss();       
    }




    void checkDistanceFromPlayer()
    {
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < isFarOrCloseDistance) isClose();
        else isFar();
    }

    //플레이어를 바라봐유
    void rotateBoss()
    {
        Vector3 vec = playerTransform.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
    void isClose()
    {
        enemyPattern = CloseAttackTypeNormalPattern.patternClose;
        bossWeaponSwordOn();
        aniScript.patternChoice(0);
    }
    void isFar()
    {
        enemyPattern = CloseAttackTypeNormalPattern.patternFar;
        bossWeaponSwordOn();
        aniScript.patternChoice(1);
    }
    void patternEnd()
    { 
        StartCoroutine("CloseAttackTypeNormalController");
    }
    void bossWeaponSwordOn()
    {
        weaponCollider.enabled = true;
    }
    public void bossWeaponSwordOff()
    {
        weaponCollider.enabled = false;
    }
    public void MakeEnemyPatternIdle()
    {
        enemyPattern = CloseAttackTypeNormalPattern.patternIdle;
        closeAttackTypeNormalState = CloseAttackTypeNormalState.idle;
    }


    // 기달리는 행동이에유
    IEnumerator WaitForPlayer()
    {
        yield return null;
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < enemyAttackCheckAreaDistance)
        {
            StartCoroutine("CloseAttackTypeNormalController");
            StopCoroutine("WaitForPlayer");
        }
        StartCoroutine("WaitForPlayer");
    }

    // 행동이에유
    IEnumerator CloseAttackTypeNormalController()
    {
        yield return null;

        if (hpPostionScript.deadOrLive == 1)
        {
            StopCoroutine("CloseAttackTypeNormalController");
        }

        yield return new WaitForSeconds(3.7f);
        checkDistanceFromPlayer();

        StopCoroutine("CloseAttackTypeNormalController");
    }


    public void enemyPatternStart()
    {
        StartCoroutine("CloseAttackTypeNormalController");
    }


    // 일정거리이내까지만 따라온다음 멈춰야 공격시 플레이어가 피할 수 있다.
    void resetNowStateToStopFollowing(float distance ,float enemyAttackSpeed)
    {
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) >= distance) transform.position = Vector3.Lerp(transform.position, playerTransform.position, enemyAttackSpeed);
    }


    void stateChange()
    {
        closeAttackTypeNormalState = CloseAttackTypeNormalState.idle;
    }
    void isTrap01CoolTimeOn()
    {
        trap01 = false;
    }


    private void OnTriggerExit(Collider other)
    {
        if (closeAttackTypeNormalState == CloseAttackTypeNormalState.attacked) return;

        if(other.gameObject.tag == "PlayerSword01")
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
                closeAttackTypeNormalState = CloseAttackTypeNormalState.attacked;
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
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                closeAttackTypeNormalState = CloseAttackTypeNormalState.attacked;
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
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }
            else
            {
                closeAttackTypeNormalState = CloseAttackTypeNormalState.attacked;
                Invoke("stateChange", 0.3f);
            }
            return;
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
                closeAttackTypeNormalState = CloseAttackTypeNormalState.attacked;
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
                aniScript.deadAniOn();
                Destroy(this.gameObject, 3f);
            }

            else Invoke("isTrap01CoolTimeOn", 2f);

            return;
        } 
    }
}
