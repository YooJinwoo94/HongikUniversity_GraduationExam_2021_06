using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CloseAttackTypeNormalState
{
    idle,
    attacked,
    attack,
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
    BoxCollider bossWeaponSword;

    Transform playerTransform;
    Transform enemyTransform;
    CloseAttackTypeNormalPattern EnemyPattern;

    bool enemyDistanceCheck;
    const float enemyPatternCloseDistance = 1f;
    const float enemyPatternFarDistance = 3f;
    const float enemyAttackSpeedPatternClose = 0.02f;
    const float enemyAttackSpeedPatternFar = 0.04f;
    const float enemyAttackCheckAreaDistance = 6f;
    const float isFarOrCloseDistance = 7f;
    int enemyHp;

    private CloseAttackTypeNormalAni closeAttackTypeNormalAniScript;





    private void Awake()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        closeAttackTypeNormalAniScript = GetComponent<CloseAttackTypeNormalAni>();

        EnemyPattern = CloseAttackTypeNormalPattern.patternZero;
       
        enemyHp = 5;
        bossWeaponSword.enabled = false;
        enemyDistanceCheck = false;
      //  stopChase = false;
        StartCoroutine("WaitForPlayer");   
    }

    private void FixedUpdate()
    {
        if (EnemyPattern == CloseAttackTypeNormalPattern.patternZero) return;
        switch (EnemyPattern)
         {
            case CloseAttackTypeNormalPattern.patternClose:
                resetNowStateToStopFollowing(enemyPatternCloseDistance, enemyAttackSpeedPatternClose);
                break;

            case CloseAttackTypeNormalPattern.patternFar:
                resetNowStateToStopFollowing(enemyPatternCloseDistance, enemyAttackSpeedPatternFar);
                if (Vector3.Distance(enemyTransform.position, playerTransform.position) < enemyPatternFarDistance)
                {
                    closeAttackTypeNormalAniScript.patternFar02();
                    EnemyPattern = CloseAttackTypeNormalPattern.patternIdle;
                }
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
        EnemyPattern = CloseAttackTypeNormalPattern.patternClose;
        bossWeaponSwordOn();
        closeAttackTypeNormalAniScript.patternChoice(0);
    }
    void isFar()
    {
        EnemyPattern = CloseAttackTypeNormalPattern.patternFar;
        bossWeaponSwordOn();
        closeAttackTypeNormalAniScript.patternChoice(1);
    }
    void patternEnd()
    { 
        StartCoroutine("CloseAttackTypeNormalController");
    }
    void bossWeaponSwordOn()
    {
        bossWeaponSword.enabled = true;
    }
    public void bossWeaponSwordOff()
    {
        bossWeaponSword.enabled = false;
    }
    public void MakeEnemyPatternIdle()
    {
        EnemyPattern = CloseAttackTypeNormalPattern.patternIdle;
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

        if (enemyHp <= 0) StopCoroutine("CloseAttackTypeNormalController");

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
}
