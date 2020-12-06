using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearCheckManager : MonoBehaviour
{
    private static StageClearCheckManager instance = null;

    int stageMosterCount;
    int killedMosterCount;

    [SerializeField]
    Animator[] doorOpenAni;
    [SerializeField]
    GameObject[] camSet;
    [SerializeField]
    Transform[] camPos;
   // GameObject camObj;
    // Start is called before the first frame update
    void Awake()
    {
        stageMosterCount = 0;
        killedMosterCount = 0;


        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }

    public static StageClearCheckManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }






    
    IEnumerator CamSetting()
    {
       
        yield return new WaitForSeconds(2f);
        //camSet[0].SetActive(false);
        //DestroyImmediate(camSet[0], true);

        Destroy(GameObject.FindWithTag("StageClearCam"));
        StopCoroutine("CamSetting");
    }
    




    // 죽으면 이 함수를 호출 함
    // 함수 호출시 카운트를 함 
    public  void numMosterCount(int stage)
    {
        checkStageClear(stage);
        killedMosterCount++;

        //추후에 문이 추가 될 수 있으니 중복으로 놔둠
        if (killedMosterCount == stageMosterCount)

        {
            switch (stage)
            {
                case 1:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
                case 2:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
                case 3:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
                case 4:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
                case 5:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
                case 6:
                    doorOpenAni[0] = GameObject.FindWithTag("stageClearDoor").GetComponent<Animator>();
                    doorOpenAni[0].SetBool("openDoor", true);
                    break;
            }

            Instantiate(camSet[0], camPos[stage -1]);
           // Destroy(camSet[0], 2f);
            StartCoroutine("CamSetting");
        }
    }

    void checkStageClear(int nowStageCount)
    {
        switch (nowStageCount)
        {
            case 1:
                stageMosterCount = 3;
                break;
            case 2:
                stageMosterCount = 3;
                break;
            case 3:
                stageMosterCount = 3;
                break;
            case 4:
                stageMosterCount = 3;
                break;
            case 5:
                stageMosterCount = 3;
                break;
            case 6:
                stageMosterCount = 1;
                break;
        }
    }
}
