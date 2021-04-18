using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class MonsterStateIsIdle : Action
{
    public SharedBool attackStart;





    public override TaskStatus OnUpdate()
    {
        if (attackStart.Value == false) return TaskStatus.Success;
        return TaskStatus.Success;
    }
}
