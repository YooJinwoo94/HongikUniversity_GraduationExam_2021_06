using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class DistacneAttackEnemyType01Pattern02 : Action
{
    public SharedInt numOfPattern;
    public SharedTransform target;
    public CheckPatternForEnemy CheckPatternForEnemy;
    DistacneAttackTypeNormalColliderCon colliderConScript;
    DistanceAttackTypeNormalAni ani;
    EnemyHpPostionScript hpPostionScript;
  



    public override void OnStart()
    {
        colliderConScript = GetComponent<DistacneAttackTypeNormalColliderCon>();
        ani = GetComponent<DistanceAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
    }

    public override TaskStatus OnUpdate()
    {
        if (hpPostionScript.deadOrLive == 1 ||
            colliderConScript.IsAttackedState == DistacneAttackTypeNormalColliderCon.DistacneAttackEnemy01IsAttacked.attacked)
            return TaskStatus.Failure;

        rotate();
        if (ani.enemyPattern == DistanceAttackEnemyType01AtkPattern.patternIdle) patternStart();

        return TaskStatus.Failure;
    }






    void rotate()
    {
        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
    void patternStart()
    {
        ani.enemyPattern = DistanceAttackEnemyType01AtkPattern.patternFireBallAttack;
        rotate();
        ani.aniSet("FireBallAttack");
    }
}
