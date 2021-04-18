using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CloseAttackEnemyAtkPattern01 : Action
{
    public SharedBool attackStart;
    public SharedInt numOfPattern;
    public SharedTransform target;
    public EnemyHpPostionScript hpPostionScript;
    public CloseAttackTypeNormalAni aniScript;
    public CloseAttackTypeNormalColliderCon colliderConScript;


    const float isFarOrCloseDistance = 8.5f;
    const float enemyPatternCloseDistance = 3f;
    const float enemyPatternFarDistance = 5f;
    const float enemyAttackSpeedPatternClose = 0.005f;


    float endTime =1f;
    float startTime;






    public override void OnStart()
    {
        aniScript = GetComponent<CloseAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
        colliderConScript = GetComponent<CloseAttackTypeNormalColliderCon>();
    }




    public override TaskStatus OnUpdate()
    {
        if (attackStart.Value == false ||
            hpPostionScript.deadOrLive == 1 ||
            numOfPattern.Value != 1 ||
            colliderConScript.IsAttackedState == CloseAttackTypeNormalColliderCon.CloseAttackEnemy01IsAttacked.attacked
            ) return TaskStatus.Failure;

    
        if (aniScript.enemyPattern == CloseAttackEnemyType01AtkPattern.patternIdle) patternStart();
        switch (aniScript.enemyPattern)
        {
            case CloseAttackEnemyType01AtkPattern.patternClose:
                resetNowStateToStopFollowing(enemyPatternCloseDistance, enemyAttackSpeedPatternClose);
                break;
        }

        if (startTime + endTime < Time.time) return TaskStatus.Failure;
        if (hpPostionScript.deadOrLive != 1) rotate();

        return TaskStatus.Running;
    }





    void rotate()
    {
        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }









    // 일정거리이내까지만 따라온다음 멈춰야 공격시 플레이어가 피할 수 있다.
    void resetNowStateToStopFollowing(float distance, float enemyAttackSpeed)
    {
        if (Vector3.Distance(transform.position, target.Value.position) >= distance) transform.position = Vector3.Lerp(transform.position, target.Value.position, enemyAttackSpeed);
    }

    void patternStart()
    {
        startTime = Time.time;
        aniScript.enemyPattern = CloseAttackEnemyType01AtkPattern.patternClose;
        aniScript.patternChoice(0);
    }
}
