using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CheckPatternForEnemy : Conditional
{
    public SharedInt numOfPattern;
    public SharedTransform target;
    public SharedString thisGameObjName;
    public SharedBool attackStart;

    const float isFarOrCloseDistance = 9f;

    EnemyHpPostionScript hpPostionScript;
    DistanceAttackTypeNormalAni distanceAttackEnemyAniScript01;
    CloseAttackTypeNormalAni closeAttackEnemyAniScript01;
    BossAniScript bossAniScript01;

   public float[] bossAttackDistancePattern = new float[11];





    public override void OnStart()
    {
        hpPostionScript = GetComponent<EnemyHpPostionScript>();

        switch (thisGameObjName.Value)
        {
            case "CloseAttackEnemy01":
                closeAttackEnemyAniScript01 = GetComponent<CloseAttackTypeNormalAni>();
                break;

            case "DistanceAttackEnemy01":
                distanceAttackEnemyAniScript01 = GetComponent<DistanceAttackTypeNormalAni>();
                break;

            case "Boss_01(Clone)":
                bossAniScript01 = GetComponent<BossAniScript>();
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
                if (Vector3.Distance(transform.position, target.Value.position) <= isFarOrCloseDistance) numOfPattern.Value = 1;
                if (Vector3.Distance(transform.position, target.Value.position) > isFarOrCloseDistance)  numOfPattern.Value = 2;

                closeAttackEnemyAniScript01 = GetComponent<CloseAttackTypeNormalAni>();
                closeAttackEnemyAniScript01.enemyPattern = CloseAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "DistanceAttackEnemy01":
                //  따라가기  
                if (Vector3.Distance(transform.position, target.Value.position) > isFarOrCloseDistance + 3) numOfPattern.Value = 1;
                //   공격
                if (Vector3.Distance(transform.position, target.Value.position) <= isFarOrCloseDistance + 3) numOfPattern.Value = 2;

                distanceAttackEnemyAniScript01.enemyPattern = DistanceAttackEnemyType01AtkPattern.patternIdle;
                break;

            case "Boss_01(Clone)":

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
                break;
        }

        if (attackStart.Value == true) return TaskStatus.Success;

        return TaskStatus.Failure;
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
