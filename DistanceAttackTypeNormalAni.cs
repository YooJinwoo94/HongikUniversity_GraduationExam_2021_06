using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttackTypeNormalAni : MonoBehaviour
{
    [SerializeField]
    Animator enemyAni;


   public void fireBallAttack()
    {
        enemyAni.SetTrigger("is_Enemy_Attack_FireBall");
    }

    public void deadAniOn()
    {
        enemyAni.SetBool("is_Enemy_Dead", true);
    }
}
