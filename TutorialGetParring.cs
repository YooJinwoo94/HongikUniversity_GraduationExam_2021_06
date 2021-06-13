using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


public class TutorialGetParring : Action
{
    TutorialTypeMonsterMove tutorialMoveScript;

    public override void OnStart()
    {
        tutorialMoveScript = GetComponent<TutorialTypeMonsterMove>();
    }


    public override TaskStatus OnUpdate()
    {
        if (tutorialMoveScript.state != TutorialEnemyState.getParring) return TaskStatus.Failure;
        else return TaskStatus.Success;
    }
}
