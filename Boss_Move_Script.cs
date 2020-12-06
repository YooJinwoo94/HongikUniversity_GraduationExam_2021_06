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

    pattern01,
    pattern02,
    pattern03,
    pattern04,
    pattern05,
    pattern06,
    pattern07,
    pattern08,
    pattern09
}




public class Boss_Move_Script : MonoBehaviour
{
    [SerializeField]
    BoxCollider bossWeaponSword;
    [SerializeField]
    BoxCollider bossWeaponShield;

    Transform playerTransform;
    Transform bossTransform;
    Transform pattern02BossAttackAreaTransform;

    private Boss_Animation_Script bossAnimationScript;

    BossState BossState;
    BossPatternStorageToCheckLastOne bossPatternStorageToCheckLastOneState;
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
    Boss01HpPostionScript HpPostionScript;
    GameObject Health;
    //EnemyHealthScript EnemyHealthScript;


    //=======================================================
    const float bossCheckAreaDistance = 5.5f;

    const float bossPattern02Distance = 2.5f;
    const float bossAttackSpeedPattern02 = 0.08f;

    const float bossPattern03Distance = 1f;
    const float bossAttackSpeedPattern03 = 0.05f;

    const float bossPattern04Distance = 1f;
    const float bossAttackSpeedPattern04 = 0.05f;

    const float bossPattern05Distance = 1f;
    const float bossAttackSpeedPattern05 = 0.05f;

    const float bossPattern06Distance = 1f;
    const float bossAttackSpeedPattern06 = 0.06f;

    const float bossPattern07Distance = 1.5f;
    const float bossAttackSpeedPattern07 = 0.05f;

    const float bossPattern08Distance = 0.6f;
    const float bossAttackSpeedPattern08 = 0.05f;

    const float bossPattern09Distance = 1f;
    const float bossAttackSpeedPattern09 = 0.05f;


    void Awake()
    {
         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
         bossTransform = GetComponent<Transform>();
         bossAnimationScript = GetComponent<Boss_Animation_Script>();
         pattern02BossAttackAreaTransform = GameObject.FindGameObjectWithTag("pattern02BossAttackAreaSprite").transform;


        HpPostionScript = GetComponent<Boss01HpPostionScript>();

        BossState = BossState.idle;
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.bossWait;

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
        switch (bossPatternStorageToCheckLastOneState)
        {
            case (BossPatternStorageToCheckLastOne.pattern02):
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;
                resetNowStateToStopFollowing(bossPattern02Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern02);
                break;

            case (BossPatternStorageToCheckLastOne.pattern03):            
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                resetNowStateToStopFollowing(bossPattern03Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern03);
                break;

            case (BossPatternStorageToCheckLastOne.pattern04):
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern04);
                break;

            case (BossPatternStorageToCheckLastOne.pattern05):
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;
                resetNowStateToStopFollowing(bossPattern06Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern05);
                break;

            case (BossPatternStorageToCheckLastOne.pattern06):        
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                resetNowStateToStopFollowing(bossPattern06Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern06);
                break;

            case (BossPatternStorageToCheckLastOne.pattern07):
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                resetNowStateToStopFollowing(bossPattern07Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern07);
                break;

            case (BossPatternStorageToCheckLastOne.pattern08):
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                resetNowStateToStopFollowing(bossPattern08Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern08);
                break;

            case (BossPatternStorageToCheckLastOne.pattern09):           
                if (bossDistanceCheck == true || bossPatternNow == 0 || ischaseStart == false) break;

                resetNowStateToStopFollowing(bossPattern09Distance);
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, bossAttackSpeedPattern09);
                break;
        }
    }
    // Restart bossPattern
    public void bossConRestartBossPattern()
    {
        StartCoroutine("BossController");
    }
    // 행동이에유
    IEnumerator BossController()
    {
        yield return null;

        if (bossHp <= 0) StopCoroutine("BossController");

        // 첫 행동 싸이클 
        if (coroutineBossOncePattern == false )
        {
            coroutineBossOncePattern = true;

            ifBossPatternEnd();
            yield return new WaitForSeconds(3f);
            pattern01();
            yield return new WaitForSeconds(3f);
            pattern03();       
        }  
        yield return new WaitForSeconds(3.5f);
         checkDistanceFromPlayer();
        //pattern03();

        StopCoroutine("BossController");
    }


    //철자
    //거리를 계산해보겠어유
    void checkDistanceFromPlayer()
    {
        //만약 거리가 가까우면
        if (Vector3.Distance(bossTransform.position, playerTransform.position) < bossCheckAreaDistance) ifClosePatternChoice(); 
        else  ifFarPatternChoice(); 
    }
    //먼가유?
    void ifFarPatternChoice()
    {
        bossPatternRandomStorage = Random.Range(2, 3);
        patternChoice(bossPatternRandomStorage);

        Debug.Log(bossPatternRandomStorage);
    }
    //가까운가유?
    void ifClosePatternChoice()
    {
        //   bossPatternRandomStorage = Random.Range(3, 10);
         bossPatternRandomStorage = Random.Range(3, 7);

        checkPatternAgain(bossPatternRandomStorage);
        patternChoice(bossPatternRandomStorage);
    }
   //이전에 했었던 패턴이면 다시 골라욧!
    void checkPatternAgain(int bossPatternRandomStorage)
    {       
       switch (bossPatternRandomStorage)
        {
          /*
            case 1:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern01) iffarPatternChoice();
                break;
            case 2:
                if (bossPatternStorageToCheckLastOneState == BossPatternStorageToCheckLastOne.pattern02) iffarPatternChoice();
                break;
           */
           //=======================================================================

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
       switch (bossPatternRandomStorage)
        {
            case 1:
                pattern01();
              break;
            case 2:
                pattern02AttackAreaSprite();
                Invoke("pattern02", 2f);
                break;
            case 3:
                pattern03();
                break;
            case 4:
                pattern04();
                break;
            case 5:
                pattern05();
                break;
            case 6:
                pattern06();
                break;
            case 7:
                pattern07();
                break;
            case 8:
                pattern08();
                break;
            case 9:
                pattern09();
                break;
        }           
    }






    //보스가 플레이어를 바라봐유
    void rotateBoss()
    {
        if (bossPatternNow == 8) return;

        Debug.Log(bossPatternNow);
        Vector3 vec = playerTransform.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }
    //========================================================= 패턴들 
    //피격용  ( 시간차 0 ) 
    public void ifBossPatternEnd()
    {
        switch (bossPatternNow)
            {
            case 0:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 1:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 2:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 3:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 4:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 5:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 6:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 7:
                bossDistanceCheck = false;
                bossPatternNow = 0;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 8:
                Invoke("bossPatternNowCoolTime", 0.3f);

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
            case 9:
                bossPatternNow = 0;
                bossDistanceCheck = false;

                bossWeaponSword.enabled = false;
                bossWeaponShield.enabled = false;
                weaponSword.tag = "enemyWeapon";

                bossAnimationScript.bossPatternChoice(0);
                break;
        }
    }
    void bossPatternNowCoolTime()
    {
            bossPatternNow = 0;
            bossDistanceCheck = false;
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




    // 돌진베기
    void pattern01()
    {
        bossPatternNow = 1;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern01;
        bossAnimationScript.bossPatternChoice(1);
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
    // 화살표 그려주기 
    void pattern02AttackAreaSprite()
    {
        pattern02BossAttackAreaTransform.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("offpattern02AttackAreaSprite", 1.7f);
    }
    // 화살표 지워주기 
    void offpattern02AttackAreaSprite()
    {
        pattern02BossAttackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
    }

    // 2번 방패로 때리기
    void pattern03()
    {
        ischaseStart = true;

        bossPatternNow = 3;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern03;
        bossAnimationScript.bossPatternChoice(3);

        Invoke("stopAttackTracking", 0.8f);
        Debug.Log("ad");
    }

    // 3연격 (1)
    void pattern04()
    {
        ischaseStart = true;

        bossPatternNow = 4;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern04;
        bossAnimationScript.bossPatternChoice(4);

        Invoke("stopAttackTracking", 0.8f);
    }

    // 3연격 (2)
    void pattern05()
    {
        ischaseStart = true;

        bossPatternNow = 5;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern05;
        bossAnimationScript.bossPatternChoice(5);

        Invoke("stopAttackTracking", 0.8f);
    }

    // 점프후 내려찍기 
    void pattern06()
    {
        ischaseStart = true;

        bossPatternNow = 6;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern06;
        bossAnimationScript.bossPatternChoice(6);

        Invoke("stopAttackTracking", 0.4f);
    }

    // 플레이어 방향으로 구르고 내려찍기
    void pattern07()
    {
        ischaseStart = true;

        bossPatternNow = 7;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern07;
        bossAnimationScript.bossPatternChoice(7);

        Invoke("stopAttackTracking", 1f);
    }

    // 올려치기 
    void pattern08()
    {
        ischaseStart = true;

        bossPatternNow = 8;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern08;
        bossAnimationScript.bossPatternChoice(8);
        weaponSword.tag = "pattern08";

        Invoke("stopAttackTracking", 0.4f);
    }

    // 구르고 방패로 2번 떄리기 
    void pattern09()
    {
        ischaseStart = true;

        bossPatternNow = 9;
        colliderOn();
        bossPatternStorageToCheckLastOneState = BossPatternStorageToCheckLastOne.pattern09;
        bossAnimationScript.bossPatternChoice(9);

        Invoke("stopAttackTracking", 1f);
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
            HpPostionScript.enemyDamagedAndImageChange(0.2f);
            BossState = BossState.attacked;
            Invoke("stateChange", 0.4f);
        }
    }
}
