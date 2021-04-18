using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class ThisIsDead : Action
{
    public SharedString thisGameObjName;
    CloseAttackTypeNormalAni closeAtkEnemy01AniScript;
    DistanceAttackTypeNormalAni disAtkEnemy01AniScript;
    BossAniScript bossAniScript;




    public override TaskStatus OnUpdate()
    {
        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAtkEnemy01AniScript = GetComponent<CloseAttackTypeNormalAni>();
                closeAtkEnemy01AniScript.deadAniOn();
                break;

            case "DistanceAttackEnemy01":
                disAtkEnemy01AniScript = GetComponent<DistanceAttackTypeNormalAni>();
                disAtkEnemy01AniScript.deadAniOn();
                break;

            case "Boss_01(Clone)":
                bossAniScript = GetComponent<BossAniScript>();
                bossAniScript.deadAniOn();
                break;
        }

        return TaskStatus.Failure;
    }
}
