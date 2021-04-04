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
    public void openDoor(int doorNum)
    {
        doorOpenAni[doorNum].SetBool("openDoor", true);
    }

    // 죽으면 이 함수를 호출 함
    public void ifDeadChageUiState()
    {
        tutorialStageConManagerScript.resetState();
    }
}
