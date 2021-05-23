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
    const float isFarOrCloseDistance = 7f;

    public override void OnStart()
    {
        if (Vector3.Distance(transform.position, target.Value.position) > isFarOrCloseDistance) numOfPattern.Value = 1;

        colliderConScript = GetComponent<DistacneAttackTypeNormalColliderCon>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
        aniScript = GetComponent<DistanceAttackTypeNormalAni>();
    }



    public override TaskStatus OnUpdate()
    {
        if (hpPostionScript.deadOrLive == 1 ||
           colliderConScript.IsAttackedState == DistacneAttackTypeNormalColliderCon.DistacneAttackEnemy01IsAttacked.attacked ||
           numOfPattern.Value != 1) return TaskStatus.Failure;

        rotate();

        transform.position = Vector3.Lerp(transform.position, target.Value.position, ifEnemyGoCloserToPlayerControllTheSpeed);
        if ((Vector3.Distance(transform.position, target.Value.position) <= distanceCheckForStop + 4)) return TaskStatus.Success;
        return TaskStatus.Running;
    }




    void rotate()
    {
        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
}
