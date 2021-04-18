using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckIsAttackStartRange : Conditional
{
    public SharedTransform target;
    public SharedBool attackStart;
    public SharedString thisGameObjName;

    const float checkDistance = 12f;






    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, target.Value.position) <= checkDistance)
        {
            attackStart.Value = true;
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }
}
