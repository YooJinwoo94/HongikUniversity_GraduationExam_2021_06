using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DistanceAttackEnemyType01AtkPattern
{
    patternZero,
    patternIdle,
    patternFireBallAttack,
    patternFar,
}




public class DistanceAttackTypeNormalAni : MonoBehaviour
{
    [SerializeField]
    Animator enemyAni;

    [HideInInspector]
    public DistanceAttackEnemyType01AtkPattern enemyPattern = DistanceAttackEnemyType01AtkPattern.patternZero;

    EnemyParticleCon enemyParticleConScript;
    [SerializeField]
    Transform fireAttackPos;

    private void Start()
    {
        enemyParticleConScript = GetComponent<EnemyParticleCon>();
    }


    public void aniSet(string name)
    {
        switch (name)
        {
            case "Reset_Ani":
                enemyAni.SetBool("is_Run", false);
                enemyAni.SetBool("is_Enemy_Rest", false);

                enemyAni.SetBool("Bool_Enemy_Waiting", true);
                break;

            case "Run":
                enemyAni.SetBool("is_Enemy_Rest", false);
                enemyAni.SetBool("is_Run", true);
                break;

            case "Hitted":
                enemyAni.SetBool("is_Enemy_Rest", false);
                enemyAni.SetTrigger("is_Enemy_Damaged");
                break;

            case "Dead":
                enemyAni.SetBool("is_Enemy_Rest", false);
                enemyAni.SetBool("is_Enemy_Dead", true);
                break;

            case "Rest":
                enemyAni.SetBool("is_Enemy_Rest", false);
                enemyAni.SetBool("is_Enemy_Rest", true);
                break;

            case "FireBallAttack":
                Instantiate(enemyParticleConScript.fireBall, fireAttackPos.position, transform.rotation);
                enemyAni.SetTrigger("is_Enemy_Attack_FireBall");
                break;
        }
    }
}
