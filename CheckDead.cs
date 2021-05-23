using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckDead : Conditional
{
    public string targetTag;
    public SharedTransform target;
    public float distance = 14f;
    private Transform targetTransform;

    Boss01HpPostionScript boss01HpPostionScript;
    EnemyHpPostionScript enemyHpPosionScript;
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

        if (targetTag == null) targetTag = "Player";
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        target.Value = targetTransform;

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                enemyHpPosionScript = GetComponent<EnemyHpPostionScript>();
                closeAttackEnemyaniScript01 = GetComponent<CloseAttackTypeNormalAni>();
                closeAttackEnemyaniScript01.enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "DistanceAttackEnemy01":
                enemyHpPosionScript = GetComponent<EnemyHpPostionScript>();
                distanceAttackEnemyaniScript01 = GetComponent<DistanceAttackTypeNormalAni>();
                distanceAttackEnemyaniScript01.enemyPattern = DistanceAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "Boss_01(Clone)":
                boss01HpPostionScript = GetComponent<Boss01HpPostionScript>();
                bossAniScript = GetComponent<BossAniScript>();
                //closeAttackEnemyaniScript01.enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;        
                break;
        }
    }


    public override TaskStatus OnUpdate()
    {
        switch (thisGameObjName.Value)
        {
            case "Boss_01(Clone)":
                if (startTime + 6.9f < Time.time)
                {
                    bossAniScript.resetAttackPattern();
                    return TaskStatus.Success;
                }
                break;

            case "CloseAttackEnemy01":
                if (enemyHpPosionScript.deadOrLive == 1) return TaskStatus.Failure;
                if (isTargetCome(targetTransform)) return TaskStatus.Success;
                break;

            case "DistanceAttackEnemy01":
                if (enemyHpPosionScript.deadOrLive == 1) return TaskStatus.Failure;
                if (isTargetCome(targetTransform)) return TaskStatus.Success;
                break;
        }
        return TaskStatus.Success;
    }


    public bool isTargetCome(Transform targetTransform)
    {
        return (Vector3.Distance(transform.position, targetTransform.position) < distance);
    }
}
