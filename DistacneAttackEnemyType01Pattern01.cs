using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;




public class DistacneAttackEnemyType01Pattern01 : Action
{
    public SharedInt numOfPattern;
    public SharedTransform target;
    public CheckPatternForEnemy CheckPatternForEnemy;

    EnemyHpPostionScript hpPostionScript;
    DistanceAttackTypeNormalAni aniScript;
    DistacneAttackTypeNormalColliderCon colliderConScript;

    const float ifEnemyGoCloserToPlayerControllTheSpeed = 0.004f;
    const float distanceCheckForStop = 8.5f;


    public override void OnStart()
    {
        colliderConScript = GetComponent<DistacneAttackTypeNormalColliderCon>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
        aniScript = GetComponent<DistanceAttackTypeNormalAni>();
    }



    public override TaskStatus OnUpdate()
    {
        if (hpPostionScript.deadOrLive == 1) return TaskStatus.Failure;
        if (colliderConScript.IsAttackedState == DistacneAttackTypeNormalColliderCon.DistacneAttackEnemy01IsAttacked.attacked) return TaskStatus.Failure;
        if (CheckPatternForEnemy.numOfPattern.Value != 1) return TaskStatus.Success;

        rotateBoss();

        transform.position = Vector3.Lerp(transform.position, target.Value.position, ifEnemyGoCloserToPlayerControllTheSpeed);
        if ((Vector3.Distance(transform.position, target.Value.position) <= distanceCheckForStop + 4)) return TaskStatus.Success;
        return TaskStatus.Running;
    }




    void rotateBoss()
    {
        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
}
