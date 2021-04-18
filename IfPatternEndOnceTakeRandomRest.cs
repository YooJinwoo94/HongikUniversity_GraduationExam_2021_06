using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class IfPatternEndOnceTakeRandomRest : Conditional
{
    bool waitForSecondAttack = false;
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
    public SharedBool attackStart;

    Vector3 velo = Vector3.zero;







    public override void OnStart()
    {
        velo = Vector3.zero;

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
        if (attackStart.Value == false) return TaskStatus.Failure;

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                if (waitForSecondAttack== false)
                {
                    waitForSecondAttack = true;
                    return TaskStatus.Success;
                }
                if (startTime + randomTime < Time.time) return TaskStatus.Success;
                if (Vector3.Distance(transform.position, target.Value.position) >= 4f) transform.position = Vector3.SmoothDamp(transform.position, target.Value.position, ref velo, 2f);
                break;

            case "DistanceAttackEnemy01":
                if (waitForSecondAttack == false)
                {
                    waitForSecondAttack = true;
                    return TaskStatus.Success;
                }
                if (startTime + randomTime < Time.time) return TaskStatus.Success;
                if (Vector3.Distance(transform.position, target.Value.position) >= 10f) transform.position = Vector3.SmoothDamp(transform.position, target.Value.position, ref velo, 10f);
                break;

            case "Boss_01(Clone)":
                if (waitForSecondAttack == false)
                {
                    waitForSecondAttack = true;
                    return TaskStatus.Success;
                }
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
