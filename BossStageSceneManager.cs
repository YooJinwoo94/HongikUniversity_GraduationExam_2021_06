using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageSceneManager : MonoBehaviour
{
    [SerializeField]
    Transform bossDoorTransform;

    [SerializeField]
    Animator bossDoorAni;
    [SerializeField]
    Animator playerDoorAni;

    [SerializeField]
    Transform bossSwapnArea;

    [SerializeField]
    GameObject bossGameObj;






    bool doorClose;
    bool stageFinsh;

    Vector3 velo = Vector3.zero;

    BoxCollider boxCollider;





    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        doorClose = false;
        stageFinsh = false;
    }


    IEnumerator DoorCoroutine()
    {
        yield return new WaitForSeconds(3f);
        doorClose = true;
        StopCoroutine("DoorCoroutine");
    }






    IEnumerator stageBoss01Opening()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(bossGameObj, bossSwapnArea);

        yield return new WaitForSeconds(2f);
        setBossDoorAniStop();
        StopCoroutine("stageBoss01Opening");
    }


    void setBossDoorAniStop()
    {
        bossDoorAni.SetBool("isDoorClose", false);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDoorAni.SetBool("isPlayerDoorClose", true);
            
            boxCollider.enabled = false;

            //camAni.SetBool("bossStage01Start", true);
            //StartCoroutine("CamSetting");

            Invoke("setBossDoorAniStop", 0.1f);
        }
    }
}
