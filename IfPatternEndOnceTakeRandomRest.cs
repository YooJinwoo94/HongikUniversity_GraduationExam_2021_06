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

    private CloseAttackTypeNormalColliderCon closeAttackTypeNormalColliderCon;
  //private DistacneAttackTypeNormalColliderCon distacneAttackTypeNormalColliderCon;
    private Boss01ColliderCon boss01ColliderCon;

    public EnemyHpPostionScript hpPostionScript;
    private CloseAttackTypeNormalAni closeAttackEnemyaniScript;
    private DistanceAttackTypeNormalAni distanceAttackEnemyaniScript;
    BossAniScript bossAniScript01;
    WeaponColliderCon weaponColliderConScript;

    public SharedString thisGameObjName;
    public SharedInt numOfPattern;
    public SharedTransform target;
    public SharedBool waitForAttack;

    Vector3 velo = Vector3.zero;

    public float[] bossAttackDistancePattern = new float[11];





    public override void OnStart()
    {
        velo = Vector3.zero;
         
        startTime = Time.time;
        randomTime = Random.Range(minRandomTime, maxRandomTime);

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                hpPostionScript = GetComponent<EnemyHpPostionScript>();
                closeAttackTypeNormalColliderCon = GetComponent<CloseAttackTypeNormalColliderCon>();
                closeAttackEnemyaniScript = GetComponent<CloseAttackTypeNormalAni>();
                closeAttackEnemyaniScript.aniSet("Reset_Ani");
                break;

            case "DistanceAttackEnemy01":
                distanceAttackEnemyaniScript = GetComponent<DistanceAttackTypeNormalAni>();
                distanceAttackEnemyaniScript.aniSet("Reset_Ani");
                break;

            case "Boss_01(Clone)":
                boss01ColliderCon = GetComponent<Boss01ColliderCon>();
                bossAniScript01 = GetComponent<BossAniScript>();
                weaponColliderConScript = GetComponent<WeaponColliderCon>();

                weaponColliderConScript.weaponColliderOff();

                for (int i = 1; i < 10; i++)
                {
                    float num = 0;
                    switch (i)
                    {
                        case 1:
                            num = 8f;
                            break;
                        case 2:
                            num = 3f;
                            break;
                        case 3:
                            num = 2f;
                            break;
                        case 4:
                            num = 2f;
                            break;
                        case 5:
                            num = 2f;
                            break;
                        case 6:
                            num = 2f;
                            break;
                        case 7:
                            num = 1.5f;
                            break;
                        case 8:
                            num = 1.6f;
                            break;
                        case 9:
                            num = 2f;
                            break;
                    }

                    bossAttackDistancePattern[i] = num;
                }
                break;
        }
    }


    public override TaskStatus OnUpdate()
    {
        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                if ((hpPostionScript.deadOrLive != 1) 
                || (closeAttackTypeNormalColliderCon.IsAttackedState != CloseAttackTypeNormalColliderCon.CloseAttackEnemy01IsAttacked.stuned)) rotate();

                if (startTime + randomTime < Time.time)
                {
                    waitForAttack.Value = true;
                    closeAttackEnemyaniScript.resetAni();
                    return TaskStatus.Success;
                }
                if (Vector3.Distance(transform.position, target.Value.position) >= 4f)
                {
                    closeAttackEnemyaniScript.aniSet("Run");
                    transform.position = Vector3.SmoothDamp(transform.position, target.Value.position, ref velo, 5f);
                }
                else
                {
                    closeAttackEnemyaniScript.forIdle();
                }
                break;

            case "DistanceAttackEnemy01":
                if (startTime + randomTime < Time.time)
                {
                    distanceAttackEnemyaniScript.aniSet("Reset_Ani");
                    waitForAttack.Value = true;
                    return TaskStatus.Success;
                }
                if (Vector3.Distance(transform.position, target.Value.position) >= 8f)
                {
                    distanceAttackEnemyaniScript.aniSet("Run");
                    transform.position = Vector3.SmoothDamp(transform.position, target.Value.position, ref velo, 5f);
                }
                else 
                {
                    distanceAttackEnemyaniScript.aniSet("Reset_Ani");
                    waitForAttack.Value = true;
                }
                break;

            case "Boss_01(Clone)":
                if (startTime + randomTime < Time.time)
                {
                    bossAniScript01.resetAttackPattern();
                    waitForAttack.Value = true;

                    if (Vector3.Distance(transform.position, target.Value.position) < 7f) ifClosePatternChoiceBoss01();
                    else ifFarPatternChoiceBoss01();

                    switch (numOfPattern.Value)
                    {
                        case 2:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern02;
                            break;

                        case 3:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern03;
                            break;

                        case 4:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern04;
                            break;

                        case 5:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern05;
                            break;

                        case 6:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern06;
                            break;

                        case 7:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern07;
                            break;

                        case 8:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern08;
                            break;

                        case 9:
                            bossAniScript01.bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern09;
                            break;
                    }

                    return TaskStatus.Success;
                }
                if (Vector3.Distance(transform.position, target.Value.position) >= 5f)
                {
                    bossAniScript01.run();
                    transform.position = Vector3.SmoothDamp(transform.position, target.Value.position, ref velo, 5f);
                }
                else
                {
                    bossAniScript01.resetAttackPattern();
                }
                break;                                                                                              
        }

        rotate();      
        return TaskStatus.Running;
    }




    void rotate()
    {
        Vector3 vec = target.Value.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    //먼가유?
    void ifFarPatternChoiceBoss01()
    {
        numOfPattern.Value = Random.Range(2, 3);
    }
    //가까운가유?
    void ifClosePatternChoiceBoss01()
    {
        numOfPattern.Value = Random.Range(3, 10);

        checkPatternAgain(numOfPattern.Value);
    }
    //이전에 했었던 패턴이면 다시 골라욧!
    void checkPatternAgain(int bossPatternRandomStorage)
    {
        switch (bossPatternRandomStorage)
        {
            case 3:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern03) ifClosePatternChoiceBoss01();
                break;
            case 4:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern04) ifClosePatternChoiceBoss01();
                break;
            case 5:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern05) ifClosePatternChoiceBoss01();
                break;
            case 6:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern06) ifClosePatternChoiceBoss01();
                break;
            case 7:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern07) ifClosePatternChoiceBoss01();
                break;
            case 8:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern08) ifClosePatternChoiceBoss01();
                break;
            case 9:
                if (bossAniScript01.bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern09) ifClosePatternChoiceBoss01();
                break;
        }
    }
}
