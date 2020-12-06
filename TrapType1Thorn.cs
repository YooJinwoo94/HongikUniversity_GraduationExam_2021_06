using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TrapType1State{
    normal,
    readyForAttack,
    attack
}



public class TrapType1Thorn : MonoBehaviour
{

    [SerializeField]
    Animator trapType1AttackAni;

    Renderer rendererColor;
    TrapType1State TrapType1State;
   // BoxCollider trapType1ThornCollider;

    // Start is called before the first frame update
    void Awake()
    {
        //  trapType1ThornCollider = GetComponent<BoxCollider>();
        //trapType1ThornCollider.enabled = true;
        rendererColor = gameObject.GetComponent<Renderer>();
        TrapType1State = TrapType1State.normal;
        gameObject.tag = "Untagged";
    }


    void attack()
    {
        //rendererColor.material.color = Color.red;

        gameObject.tag = "TrapType1Thorn";
        TrapType1State = TrapType1State.attack;
    }

      void reset()
    {
        //rendererColor.material.color = Color.white;

        trapType1AttackAni.SetBool("trapType1Attack", false);
        TrapType1State = TrapType1State.normal;
        gameObject.tag = "Untagged";
    }




    IEnumerator StartTrapType1Thorn()
    {
        yield return null;
        // 애니메이션 작동 
        yield return new WaitForSeconds(0.5f);
        trapType1AttackAni.SetBool("trapType1Attack", true);
        yield return new WaitForSeconds(1f);
        attack();

        yield return new WaitForSeconds(0.6f);
        reset();
        StopCoroutine("StartTrapType1Thorn");
    }


    private void OnTriggerStay(Collider other)
    {
        if (TrapType1State != TrapType1State.normal) return;

        if (other.gameObject.tag == "Player" 
            || other.gameObject.tag == "CloseAttackEnemy01"
            || other.gameObject.tag == "CloseAttackEnemy02"
            || other.gameObject.tag == "DistanceAttackEnemy01"
            || other.gameObject.tag == "DistanceAttackEnemy02")
        {
            TrapType1State = TrapType1State.readyForAttack;
            StartCoroutine("StartTrapType1Thorn");
        }
    }
}
