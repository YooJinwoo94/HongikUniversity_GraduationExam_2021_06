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

    // [SerializeField]
    // GameObject bossCamSet1;
    //  [SerializeField]
    //  GameObject bossCamSet2;
    //  [SerializeField]
    //  GameObject bossCamSet3;
    //  [SerializeField]
    //  GameObject bossCamSet4;

      [SerializeField]
      GameObject[] bossCamSet;

    [SerializeField]
     GameObject playerCam;

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
      //  playerCam = GameObject.FindWithTag("Camera");
        // bossCamSet1.SetActive (false);
        //  bossCamSet2.SetActive(false);
        //  bossCamSet3.SetActive(false);
        //  bossCamSet4.SetActive(false);

        for (int i =0; i< 4; i++) bossCamSet[i].SetActive(false);

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
        playerCam.SetActive(false);
        bossCamSet[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        bossCamSet[0].SetActive(false);
        bossCamSet[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(bossGameObj, bossSwapnArea);
        yield return new WaitForSeconds(2f);
        bossDoorAni.SetBool("isDoorClose", true);
        bossCamSet[1].SetActive(false);
        bossCamSet[2].SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        bossCamSet[2].SetActive(false);
        bossCamSet[3].SetActive(true);
        yield return new WaitForSeconds(2f);
        bossCamSet[3].SetActive(false);

       
       // playerCam.SetActive(false);
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
