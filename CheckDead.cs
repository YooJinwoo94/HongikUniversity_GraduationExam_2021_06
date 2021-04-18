using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckDead : Conditional
{

    EnemyHpPostionScript enemyHpPosionScript;



    public override void OnStart()
    {
        enemyHpPosionScript = GetComponent<EnemyHpPostionScript>();
    }


    public override TaskStatus OnUpdate()
    {
        if(enemyHpPosionScript.deadOrLive == 1) return TaskStatus.Failure;

        return TaskStatus.Success;
    }
}
