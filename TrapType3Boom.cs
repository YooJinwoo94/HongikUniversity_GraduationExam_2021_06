using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TrapType3State
{
    normal,
    boomReady,
    boom
}

public class TrapType3Boom : MonoBehaviour
{
    [SerializeField]
    Renderer rendererColor;
    [SerializeField]
    GameObject boomParticleAndBoomArea;
    [SerializeField]
    Transform boomParticleAndBoomAreaPos;
    [SerializeField]
    GameObject boomAreaGameObject;



    TrapType3State TrapType3State;

    // Start is called before the first frame update
    void Start()
    {
        rendererColor = GetComponent<Renderer>();
        boomAreaGameObject.SetActive(false);
    }



    void boom()
    {
        rendererColor.enabled = false;

        Instantiate(boomParticleAndBoomArea, boomParticleAndBoomAreaPos.position, boomParticleAndBoomAreaPos.rotation);
    }

 


    IEnumerator StartTrapType3Thorn()
    {
        yield return null;
        // 애니메이션 작동 
        yield return new WaitForSeconds(0.5f);
        rendererColor.material.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        rendererColor.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        boom();
      //  Instantiate(track_03_PartObj);
        boomAreaGameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
       
        Destroy(gameObject);

        StopCoroutine("StartTrapType3Thorn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TrapType3State != TrapType3State.normal) return;

        if (other.gameObject.tag == "PlayerSword" 
            || other.gameObject.tag == "enemyWeapon")
        {
            boomAreaGameObject.SetActive(true);
            TrapType3State = TrapType3State.boomReady;
            StartCoroutine("StartTrapType3Thorn");
        }
    }
}
