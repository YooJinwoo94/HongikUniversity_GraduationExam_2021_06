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




    public void enemyHitted()
    {
        enemyAni.SetTrigger("is_Enemy_Damaged");
    }
    public void fireBallAttack()
    {
        Instantiate(enemyParticleConScript.fireBall, fireAttackPos.position,transform.rotation);
        enemyAni.SetTrigger("is_Enemy_Attack_FireBall");
    }
    public void deadAniOn()
    {
        enemyAni.SetBool("is_Enemy_Dead", true);
    }
}
