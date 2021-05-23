using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckIsAttackStartRange : Conditional
{
    CloseAttackTypeNormalAni closeAttackEnemyAni;
    DistanceAttackTypeNormalAni distanceAttackAni;
    BossAniScript bossAni;

    public SharedTransform target;
    public SharedString thisGameObjName;

    const float checkDistance = 12f;





    public override TaskStatus OnUpdate()
    {

        if (Vector3.Distance(transform.position, target.Value.position) <= checkDistance)
        {
            return TaskStatus.Failure;
        }

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAttackEnemyAni = GetComponent<CloseAttackTypeNormalAni>();
                closeAttackEnemyAni.aniSet("Rest");
                break;

            case "DistanceAttackEnemy01":
                distanceAttackAni = GetComponent<DistanceAttackTypeNormalAni>();
                distanceAttackAni.aniSet("Rest");
                break;

            case "Boss_01(Clone)":
                bossAni = GetComponent<BossAniScript>();
                break;
        }

        return TaskStatus.Success;
    }
}
