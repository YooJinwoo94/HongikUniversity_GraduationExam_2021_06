using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TrapType2State
{
    normal,
    readyForAttack,
    attack
}





public class TrapType2Fire : MonoBehaviour
{
    TrapType2State TrapType2State;

    [SerializeField]
    Transform fireBallPos;
    [SerializeField]
    GameObject fireBall;
    [SerializeField]
    Renderer rendererColor;






    // Start is called before the first frame update
    void Start()
    {
        TrapType2State = TrapType2State.normal;
    }

    void attack()
    {
        TrapType2State = TrapType2State.attack;
         Instantiate(fireBall, fireBallPos.position, fireBallPos.rotation);
    }


    IEnumerator StartTrapType2Thorn()
    {
        yield return null;
        // 애니메이션 작동 
        yield return new WaitForSeconds(1f);
        rendererColor.material.color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        rendererColor.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        attack();
     
        rendererColor.material.color = Color.white;  
        yield return new WaitForSeconds(3f);
        TrapType2State = TrapType2State.normal;
        StopCoroutine("StartTrapType2Thorn");
    }




    private void OnTriggerStay(Collider other)
    {
        if (TrapType2State != TrapType2State.normal) return;

        if (other.gameObject.tag == "Player")
        { 
            TrapType2State = TrapType2State.readyForAttack;
            StartCoroutine("StartTrapType2Thorn");
        }
    }
}
