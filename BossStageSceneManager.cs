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
    GameObject bossCamSet1;
    [SerializeField]
    GameObject bossCamSet2;
    [SerializeField]
    GameObject bossCamSet3;

    [SerializeField]
    Transform bossSwapnArea;

    [SerializeField]
    GameObject bossGameObj;



    /*
     [SerializeField]
     Transform playerDoorTransform;
     [SerializeField]
     Transform playerDoorCloseTransform;
     */
    //  Vector3 bossDoorOpenTransform_1 = new Vector3(0, 0, 0);
    //  Vector3 bossDoorCloseTransform_1 = new Vector3(0, -5.17f, 0);
    //   Vector3 playerDoorCloseTransform_1 = new Vector3(0, -4f, 0); 

    bool doorClose;
    bool stageFinsh;

    Vector3 velo = Vector3.zero;

    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        bossCamSet1.SetActive (false);
        bossCamSet2.SetActive(false);

        doorClose = false;
        stageFinsh = false;
       // StartCoroutine("DoorCoroutine");
    }




    IEnumerator DoorCoroutine()
    {
        yield return new WaitForSeconds(3f);
        doorClose = true;
        StopCoroutine("DoorCoroutine");
    }


    void setBossDoorAniStop()
    {
        bossDoorAni.SetBool("isDoorClose", false);
    }


    IEnumerator CamSetting()
    {
       // bossCamSet3.SetActive(true);
        bossCamSet1.SetActive(true);
        bossCamSet2.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Instantiate(bossGameObj, bossSwapnArea);
     
        yield return new WaitForSeconds(1f);
        bossDoorAni.SetBool("isDoorClose", true);
        yield return new WaitForSeconds(0.5f);
        bossCamSet1.SetActive(false);
        bossCamSet2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        bossCamSet2.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        bossCamSet3.SetActive(false);
        StopCoroutine("CamSetting");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDoorAni.SetBool("isPlayerDoorClose", true);

            boxCollider.enabled = false;

            StartCoroutine("CamSetting");
            Invoke("setBossDoorAniStop", 0.1f);
        }
    }
}
