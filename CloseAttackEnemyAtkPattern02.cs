using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CloseAttackEnemyAtkPattern02 : Action
{
    public SharedInt numOfPattern;
    public SharedTransform target;
    public EnemyHpPostionScript hpPostionScript;
    public CloseAttackTypeNormalAni aniScript;
    public CloseAttackTypeNormalColliderCon colliderConScript;

    const float isFarOrCloseDistance = 8.5f;

    const float enemyPatternFarDistance = 4f;
    const float enemyAttackSpeedPatternFar = 0.03f;






    public override void OnStart()
    {
        aniScript = GetComponent<CloseAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
        colliderConScript = GetComponent<CloseAttackTypeNormalColliderCon>();
    }

    public override TaskStatus OnUpdate()
    {
        if (hpPostionScript.deadOrLive == 1) return TaskStatus.Failure;
        if (aniScript.enemyPattern == CloseAttackEnemyType01AtkPattern.patternZero) return TaskStatus.Failure;
        if (numOfPattern.Value != 2) return TaskStatus.Success;

        if (colliderConScript.IsAttackedState == CloseAttackTypeNormalColliderCon.CloseAttackEnemy01IsAttacked.attacked) return TaskStatus.Failure;

        if (aniScript.enemyPattern == CloseAttackEnemyType01AtkPattern.patternIdle) patternStart();

        resetNowStateToStopFollowing(enemyPatternFarDistance, enemyAttackSpeedPatternFar);
        if (Vector3.Distance(transform.position, target.Value.position) < enemyPatternFarDistance)
        {
            aniScript.patternFar02();
            return TaskStatus.Failure;
        }

        rotateBoss();
        return TaskStatus.Running;
    }





    void rotateBoss()
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
        aniScript.enemyPattern = CloseAttackEnemyType01AtkPattern.patternFar;
        aniScript.patternChoice(1);
    }
}
