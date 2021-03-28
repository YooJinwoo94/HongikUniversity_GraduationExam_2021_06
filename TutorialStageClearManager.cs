using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStageClearManager : MonoBehaviour
{
    private static TutorialStageClearManager instance = null;

    [SerializeField]
    TutorialStageManger tutorialStageConManagerScript;
    [SerializeField]
    Animator[] doorOpenAni;

    int tutoialStageMosterCount;
    int killedMosterCount;


    void Start()
    {
        tutoialStageMosterCount = 1;
        killedMosterCount = 0;

        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }
    public static TutorialStageClearManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }




    //문열기
    public void openDoor()
    {
        doorOpenAni[0].SetBool("openDoor", true);
        /*
        killedMosterCount++;
        //추후에 문이 추가 될 수 있으니 중복으로 놔둠
        if (killedMosterCount == tutoialStageMosterCount)
        {
            doorOpenAni[0].SetBool("openDoor", true);

            tutorialStageConManagerScript.resetState();
        }
        */
    }

    // 죽으면 이 함수를 호출 함
    public void ifDeadChageUiState()
    {
        tutorialStageConManagerScript.resetState();
    }
}
