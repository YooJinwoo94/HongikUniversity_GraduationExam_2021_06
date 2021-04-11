using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IfPatternEndOnceTakeRandomRest : Conditional
{
    private float startTime;
    private float randomTime;
    public float maxRandomTime;
    public float minRandomTime;

    private CloseAttackTypeNormalAni closeAttackEnemyaniScript;
    private DistanceAttackTypeNormalAni distanceAttackEnemyaniScript;
    BossAniScript bossAniScript;
    WeaponColliderCon weaponColliderConScript;

    public SharedString thisGameObjName;
    public SharedInt numOfPattern;
    public SharedTransform target;










    public override void OnStart()
    {
        startTime = Time.time;
        randomTime = Random.Range(minRandomTime, maxRandomTime);

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAttackEnemyaniScript = GetComponent<CloseAttackTypeNormalAni>();
                break;

            case "DistanceAttackEnemy01":
                distanceAttackEnemyaniScript = GetComponent<DistanceAttackTypeNormalAni>();
                break;

            case "Boss_01(Clone)":
                bossAniScript = GetComponent<BossAniScript>();
                weaponColliderConScript = GetComponent<WeaponColliderCon>();

                weaponColliderConScript.weaponColliderOff();
                break;
        }
    }


    // 처음에는 바로 패턴이 시작되지만
    // 2번쨰 부터는 딜래이를 가지고 시작한다.
    public override TaskStatus OnUpdate()
    {
        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                if (closeAttackEnemyaniScript.enemyPattern == CloseAttackEnemyType01AtkPattern.patternZero) return TaskStatus.Success;
                if (startTime + randomTime < Time.time) return TaskStatus.Success;
                break;

            case "DistanceAttackEnemy01":
                if (distanceAttackEnemyaniScript.enemyPattern == DistanceAttackEnemyType01AtkPattern.patternZero) return TaskStatus.Success;
                if (startTime + randomTime < Time.time) return TaskStatus.Success;
                break;

            case "Boss_01(Clone)":
                if (bossAniScript.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.patternZero) return TaskStatus.Success;
                if (startTime + randomTime < Time.time) return TaskStatus.Success;
                break;
        }

        rotateBoss();

        return TaskStatus.Running;
    }




    void rotateBoss()
    {
        if (numOfPattern.Value == 8) return;

        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
}
