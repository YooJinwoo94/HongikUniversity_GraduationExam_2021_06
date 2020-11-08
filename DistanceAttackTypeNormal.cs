using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DistanceAttackTypeNormalPattern
{
    patternZero,
    patternIdle,
    pattern00,
}


public class DistanceAttackTypeNormal : MonoBehaviour
{
    [SerializeField]
    GameObject fireAttack;
    [SerializeField]
    Transform firePos;
    [SerializeField]
    Transform attackAreaTransform;
    Transform playerTransform;
    Transform enemyTransform;


    int enemyHp;
    bool enemyDistanceCheck;
    DistanceAttackTypeNormalPattern DistanceAttackTypeNormalPattern;
    const float enemyAttackCheckAreaDistance = 5f;
    const float enemyAttackSpeedPatternFar = 0.02f;
    const float isFarOrCloseDistance = 6f;


    private void Awake()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //closeAttackTypeNormalAniScript = GetComponent<CloseAttackTypeNormalAni>();

        DistanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.patternZero;
        enemyHp = 5;  
        enemyDistanceCheck = false;
        attackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
        //Instantiate(fireAttack, firePos.position, firePos.rotation);
        StartCoroutine("WaitForPlayer");
    }
    private void FixedUpdate()
    {
        if (DistanceAttackTypeNormalPattern == DistanceAttackTypeNormalPattern.patternZero) return;
        rotateBoss();

        if (enemyDistanceCheck == true) transform.position = Vector3.Lerp(transform.position, playerTransform.position, enemyAttackSpeedPatternFar);
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < isFarOrCloseDistance) enemyDistanceCheck = false;
    }



    void rotateBoss()
    {
        Vector3 vec = playerTransform.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }





    // 기달리는 행동이에유
    IEnumerator WaitForPlayer()
    {
        yield return null;
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) < enemyAttackCheckAreaDistance)
        {
            Debug.Log("AA");
            StartCoroutine("DistanceAttackTypeNormalController");
            StopCoroutine("WaitForPlayer");
        }
        else StartCoroutine("WaitForPlayer");
    }

    // 행동이에유
    IEnumerator DistanceAttackTypeNormalController()
    {
        yield return null;
        if (enemyHp <= 0) StopCoroutine("DistanceAttackTypeNormalController");
        yield return new WaitForSeconds(3f);
        if (DistanceAttackTypeNormalPattern != DistanceAttackTypeNormalPattern.pattern00 && Vector3.Distance(enemyTransform.position, playerTransform.position) > isFarOrCloseDistance) enemyDistanceCheck = true;
        else
        {
            attackAreaTransform.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1f);
            attackAreaTransform.GetComponent<SpriteRenderer>().enabled = false;
            enemyDistanceCheck = false; 
            DistanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.pattern00;
            Instantiate(fireAttack, transform.position, firePos.rotation);
        }
        DistanceAttackTypeNormalPattern = DistanceAttackTypeNormalPattern.patternIdle;
        StartCoroutine("DistanceAttackTypeNormalController");
    }
}
