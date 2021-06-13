using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;




public class TutorialGetAttacked : Action
{
    TutorialTypeMonsterMove tutorialMoveScript;
    public SharedTransform target;



    public override void OnStart()
    {
        tutorialMoveScript = GetComponent<TutorialTypeMonsterMove>();
    }


    public override TaskStatus OnUpdate()
    {
        if (tutorialMoveScript.state != TutorialEnemyState.getAttacked) return TaskStatus.Failure;

        rotate();
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
