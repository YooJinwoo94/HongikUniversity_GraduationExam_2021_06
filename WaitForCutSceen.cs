using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


public class WaitForCutSceen : Conditional
{
    bool cutSceen ;
    private float startTime;
    public float waitTime;


    public override void OnStart()
    {
        startTime = Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        if (cutSceen == true)
        {
            return TaskStatus.Success;
        }
        if ((startTime + waitTime < Time.time) && cutSceen == false)
        {
            cutSceen = true;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
