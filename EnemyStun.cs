using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class EnemyStun : Action
{
    public SharedString thisGameObjName;
    private CloseAttackTypeNormalColliderCon closeAttackTypeNormalColliderCon;





    public override void OnStart()
    {
        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAttackTypeNormalColliderCon = GetComponent<CloseAttackTypeNormalColliderCon>();
                break;

            case "DistanceAttackEnemy01":
                break;

            case "Boss_01(Clone)":
                break;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (closeAttackTypeNormalColliderCon.IsAttackedState == CloseAttackTypeNormalColliderCon.CloseAttackEnemy01IsAttacked.stuned) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}
