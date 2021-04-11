using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Boss01Pattern04 : Action
{
    public SharedTransform target;
    public SharedInt numOfPattern;

    WeaponColliderCon weaponColliderConScript;
    BossAniScript bossAniScript01;
    Boss01HpPostionScript hpPostionScript;
    public CheckPatternForEnemy checkPatternForEnemyScript;

    const float bossAttackSpeedPattern = 0.01f;

    float endTime = 2f;
    float startTime;
    bool ischaseStart;
    bool aniStart;














    public override void OnStart()
    {
        bossAniScript01 = GetComponent<BossAniScript>();
        hpPostionScript = GetComponent<Boss01HpPostionScript>();
        weaponColliderConScript = GetComponent<WeaponColliderCon>();

        startTime = Time.time;
        ischaseStart = true;
        aniStart = false;
    }














    public override TaskStatus OnUpdate()
    {
        if (numOfPattern.Value != 4) return TaskStatus.Success;

        if (hpPostionScript.deadOrLive == 1 ) return TaskStatus.Failure;
        if ((aniStart == false) && Vector3.Distance(transform.position, target.Value.position) < checkPatternForEnemyScript.bossAttackDistancePattern[4])
        {
            ischaseStart = false;
            bossAniScript01.bossPatternChoice(numOfPattern.Value);
            weaponColliderConScript.weaponColliderOn(0);
            aniStart = true;
        }

        if ((aniStart == true) && (startTime + endTime < Time.time)) return TaskStatus.Failure;
        if (ischaseStart == true) transform.position = Vector3.Lerp(transform.position, target.Value.position, bossAttackSpeedPattern);

  
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
