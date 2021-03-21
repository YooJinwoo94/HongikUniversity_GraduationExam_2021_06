using System.Collections;
using System.Collections.Generic;

using UnityEngine;




enum BossState
{
    idle,
    attacked,
}

// 기존에 했었던 패턴 기억
enum BossPatternStorageToCheckLastOne
{
    bossWait = 0,

    pattern01 = 1,
    pattern02 = 2,
    pattern03 = 3,
    pattern04 = 4,
    pattern05 = 5,
    pattern06 = 6,
    pattern07 = 7,
    pattern08 = 8,
    pattern09 = 9,
}




public class BossMoveScript : MonoBehaviour
{
    [SerializeField]
    BoxCollider bossWeaponSword;
    [SerializeField]
    BoxCollider bossWeaponShield;

    Transform playerTransform;
    Transform bossTransform;
    [SerializeField]
    GameObject pattern02BossAttackAreaTransform;


    private BossAniScript bossAnimationScript;

    BossState BossState;
    BossPatternStorageToCheckLastOne bossPatternStorageToCheckLastOneState ;
    int bossPatternNow;

    bool ischaseStart;
    bool bossDistanceCheck;
    bool coroutineBossOncePattern;
    int bossPatternRandomStorage;

    [SerializeField]
    int bossHp = 50;

    [SerializeField]
    GameObject weaponSword;

    [SerializeField]
    Boss01HpPostionScript hpPostionScript;
    GameObject Health;



    //=======================================================

    float[] bossAttackDistancePattern  = new float[11] ;
    float[] bossAttackSpeedPattern = new float[11];






    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = GetComponent<Transform>();
        bossAnimationScript = GetComponent<BossAniScript>();
        hpPostionScript = GetComponent<Boss01HpPostionScript>();

        BossState = BossState.idle;
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.bossWait;

        for (int i = 1; i < 10; i++)
        {
            float num = 0;
            switch (i)
            {
                case 1:
                    num = 5.5f;
                    break;
                case 2:
                    num = 2.5f;
                    break;
                case 3:
                    num = 1f;
                    break;
                case 4:
                    num = 1f;
                    break;
                case 5:
                    num = 1f;
                    break;
                case 6:
                    num = 1f;
                    break;
                case 7:
                    num = 1.5f;
                    break;
                case 8:
                    num = 0.6f;
                    break;
                case 9:
                    num = 1f;
                    break;
            }

            bossAttackDistancePattern[i] = num;
        }    
        for (int i = 2; i < 10; i++)
        {
            float num = 0.05f;
            switch (i)
            {
                case 2:
                    num = 0.08f;
                    break;
            }
            bossAttackSpeedPattern[i] = num;
        }

        ischaseStart = false;
        bossDistanceCheck = false;
        coroutineBossOncePattern = false;
        bossPatternRandomStorage = 0;
        bossPatternNow = 0;

        pattern02BossAttackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;

        bossWeaponSword.enabled = false;
        bossWeaponShield.enabled = false;

        StartCoroutine("BossController");
    }


    private void FixedUpdate()
    {
        rotateBoss();

        if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) return;
        switch (bossPatternStorageToCheckLastOneState)
        {
            case (BossPatternStorageToCheckLastOne.pattern02):
                resetNowStateToStopFollowing(bossAttackDistancePattern[2]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[2]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern03):            
                resetNowStateToStopFollowing(bossAttackDistancePattern[3]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[3]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern04):
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[4]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern05):
                resetNowStateToStopFollowing(bossAttackDistancePattern[5]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[5]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern06):        
                resetNowStateToStopFollowing(bossAttackDistancePattern[6]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[6]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern07):
                resetNowStateToStopFollowing(bossAttackDistancePattern[7]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[7]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern08):
                resetNowStateToStopFollowing(bossAttackDistancePattern[8]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[8]);
                break;

            case (BossPatternStorageToCheckLastOne.pattern09):           
                resetNowStateToStopFollowing(bossAttackDistancePattern[9]);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern[9]);
                break;
        }
    }
    // Restart bossPattern
    public void bossConRestartBossPattern()
    {
        StartCoroutine("BossController");
    }

    IEnumerator BossController()
    {
        yield return null;
        if (bossHp <= 0) StopCoroutine("BossController");

        // 첫 행동 싸이클 
        if (coroutineBossOncePattern == false )
        {
            coroutineBossOncePattern = true;

            ifBossPatternEnd();
            yield return new WaitForSeconds(6f);
        } 
        yield return new WaitForSeconds(2f);
         checkDistanceFromPlayer();
        
        StopCoroutine("BossController");
    }


    //철자
    //거리를 계산해보겠어유
    void checkDistanceFromPlayer()
    {
        //만약 거리가 가까우면
        if (Vector3.Distance(bossTransform.position, playerTransform.position) < bossAttackDistancePattern[1]) ifClosePatternChoice(); 
        else  ifFarPatternChoice(); 
    }
    //먼가유?
    void ifFarPatternChoice()
    {
        bossPatternRandomStorage = Random.Range(2, 3);
        patternChoice(bossPatternRandomStorage);
    }
    //가까운가유?
    void ifClosePatternChoice()
    {
        bossPatternRandomStorage = Random.Range(3, 10);

        checkPatternAgain(bossPatternRandomStorage);
        patternChoice(bossPatternRandomStorage);
    }
   //이전에 했었던 패턴이면 다시 골라욧!
    void checkPatternAgain(int bossPatternRandomStorage)
    {       
       switch (bossPatternRandomStorage)
        {
            case 3:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern03) ifClosePatternChoice();
                break;
            case 4:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern04) ifClosePatternChoice();
                break;
            case 5:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern05) ifClosePatternChoice();
                break;
            case 6:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern06) ifClosePatternChoice();
                break;
            case 7:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern07) ifClosePatternChoice();
                break;
            case 8:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern08) ifClosePatternChoice();
                break;
            case 9:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern09) ifClosePatternChoice();
                break;
        }     
    }
    // 패턴 선택이에유 
    void patternChoice( int bossPatternRandomStorage)
    {
        if (bossPatternRandomStorage !=2)
        {
            bossPatternNow = bossPatternRandomStorage;
            colliderOn();
            bossAnimationScript.bossPatternChoice(bossPatternRandomStorage);
        }
        if (bossPatternRandomStorage != 2 && bossPatternRandomStorage != 1)
        {
            ischaseStart = true;
        }

        switch (bossPatternRandomStorage)
        {
            case 1:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern01;
                break;
            case 2:
                pattern02BossAttackAreaTransform.GetComponent<SpriteRenderer>().enabled = true;
                Invoke("offpattern02AttackAreaSprite", 1.7f);
                Invoke("pattern02", 2f);
                break;
            case 3:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern03;
                Invoke("stopAttackTracking", 0.8f);
                break;
            case 4:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern04;
                Invoke("stopAttackTracking", 0.8f);
                break;
            case 5:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern05;
                Invoke("stopAttackTracking", 0.8f);
                break;
            case 6:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern06;
                Invoke("stopAttackTracking", 0.4f);
                break;
            case 7:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern07;
                Invoke("stopAttackTracking", 1f);
                break;
            case 8:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern08;
                weaponSword.tag = "pattern08";
                Invoke("stopAttackTracking", 0.4f);
                break;
            case 9:
                bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern09;
                Invoke("stopAttackTracking", 1f);
                break;
        }           
    }






    //보스가 플레이어를 바라봐유
    void rotateBoss()
    {
        if (bossPatternNow == 8) return;

        Vector3 vec = playerTransform.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
    //========================================================= 패턴들 
    //피격용  ( 시간차 0 ) 
    public void ifBossPatternEnd()
    {
        if (bossPatternNow != 8)
        {
            bossPatternNow = 0;
            bossDistanceCheck = false;

            bossWeaponSword.enabled = false;
            bossWeaponShield.enabled = false;
            weaponSword.tag = "enemyWeapon";

            bossAnimationScript.bossPatternChoice(0);
            return;
        }
        if (bossPatternNow == 8)
        {
            Invoke("bossPatternNowCoolTime", 0.3f);

            bossWeaponSword.enabled = false;
            bossWeaponShield.enabled = false;
            weaponSword.tag = "enemyWeapon";

            bossAnimationScript.bossPatternChoice(0);
            return;
        }
    }
    void bossPatternNowCoolTime()
    {
            bossPatternNow = 0;
            bossDistanceCheck = false;
    }


    void colliderOff()
    {
        bossWeaponSword.enabled = false;
        bossWeaponShield.enabled = false;
    }
    void colliderOn()
    {
        switch (bossPatternNow)
        {
            case 1:
                break;

            case 2:
                bossWeaponSwordOn();
                break;

            case 3:
                bossWeaponShieldOn();
                break;

            case 4:
                bossWeaponSwordOn();
                break;

            case 5:
                bossWeaponSwordOn();
                Invoke("bossWeaponShieldOn", 0.5f);
                break;

            case 6:
                Invoke("bossWeaponSwordOn", 0.7f);
                break;

            case 7:
                Invoke("bossWeaponSwordOn", 0.7f);
                break;

            case 8:
                bossWeaponSwordOn();
                break;

            case 9:
                Invoke("bossWeaponShieldOn", 0.7f);
                break;
        }
    }
    void bossWeaponSwordOn()
    {
        bossWeaponSword.enabled = true;
    }
    void bossWeaponShieldOn()
    {
        bossWeaponShield.enabled = true;
    }

    







    // 돌진찌르기
    void pattern02()
    {
        ischaseStart = true;

        bossPatternNow = 2;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern02;
        bossAnimationScript.bossPatternChoice(2);

        Invoke("stopAttackTracking", 1f);
    }
    // 화살표 지워주기 
    void offpattern02AttackAreaSprite()
    {
        pattern02BossAttackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
    }














    // 일정거리이내까지만 따라온다음 멈춰야 공격시 플레이어가 피할 수 있다.
    void resetNowStateToStopFollowing(float distance)
    {
        if (Vector3.Distance(bossTransform.position, playerTransform.position) < distance) bossDistanceCheck = true;
        else bossDistanceCheck = false;
    }
    void stopAttackTracking()
    {
        ischaseStart = false;
    }
    void stateChange()
    {
        BossState = BossState.idle;
    }



    private void OnTriggerExit(Collider other)
    {
        if (BossState == BossState.attacked) return;

        if (other.gameObject.tag == "PlayerSword")
        {
            hpPostionScript.enemyDamagedAndImageChange(0.2f);
            BossState = BossState.attacked;
            Invoke("stateChange", 0.3f);
        }
    }
}
