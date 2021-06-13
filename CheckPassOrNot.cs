using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;










public class CheckPassOrNot : Conditional
{
    TutorialTypeMonsterMove tutorialMoveScript;

    public SharedString thisGameObjName;
    private Transform targetTransform;
    public SharedTransform target;
    string targetTag = "Player";


    public override void OnStart()
    {
        if (targetTag == null) targetTag = "Player";
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        target.Value = targetTransform;

        thisGameObjName.Value = this.gameObject.name;

        tutorialMoveScript = GetComponent<TutorialTypeMonsterMove>();
    }


    public override TaskStatus OnUpdate()
    {
        switch (tutorialMoveScript.state)
        {
            case TutorialEnemyState.getAttacked:
                return TaskStatus.Success;

            case TutorialEnemyState.getPlayerDogged:
                return TaskStatus.Success;

            case TutorialEnemyState.getParring:
                return TaskStatus.Success;

            case TutorialEnemyState.getAfterDead:
                return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
