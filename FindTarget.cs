using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class FindTarget : Conditional
{
    public string targetTag;
    public SharedTransform target;
    public float distance = 14f;
    private Transform targetTransform;


    public override void OnStart()
    {
        if (targetTag == null) targetTag = "Player";
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        target.Value = targetTransform;
    }



    public override TaskStatus OnUpdate()
    {
        if (isTargetCome(targetTransform)) return TaskStatus.Success;
        return TaskStatus.Running;
    }


    public bool isTargetCome(Transform targetTransform)
    {
        return (Vector3.Distance(transform.position, targetTransform.position) < distance);
    }
}
