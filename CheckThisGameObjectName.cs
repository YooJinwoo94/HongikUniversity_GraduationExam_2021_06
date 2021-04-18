using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckThisGameObjectName : Conditional
{
      CloseAttackTypeNormalAni closeAttackEnemyaniScript01;
      DistanceAttackTypeNormalAni distanceAttackEnemyaniScript01;
      BossAniScript bossAniScript;

      GameObject Obj;
      public SharedString thisGameObjName;

       float endTime = 1.5f;
       float startTime;






    public override void OnStart()
    {
        startTime = Time.time;
        thisGameObjName.Value = this.gameObject.name;

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAttackEnemyaniScript01 = GetComponent<CloseAttackTypeNormalAni>();
                closeAttackEnemyaniScript01.enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "DistanceAttackEnemy01":
                distanceAttackEnemyaniScript01 = GetComponent<DistanceAttackTypeNormalAni>();
                distanceAttackEnemyaniScript01.enemyPattern = DistanceAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "Boss_01(Clone)":
                bossAniScript = GetComponent<BossAniScript>();
                //closeAttackEnemyaniScript01.enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;        
                break;
        }      
    }


    public override TaskStatus OnUpdate()
    {
        switch(thisGameObjName.Value)
        {
            case "Boss_01(Clone)" :
                if (startTime + 6.9f < Time.time)
                {
                    bossAniScript.resetPattern();
                    return TaskStatus.Success;
                }
                break;
            default:
                return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
