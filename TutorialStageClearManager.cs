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




    //������
    public void openDoor()
    {
        doorOpenAni[0].SetBool("openDoor", true);
        /*
        killedMosterCount++;
        //���Ŀ� ���� �߰� �� �� ������ �ߺ����� ����
        if (killedMosterCount == tutoialStageMosterCount)
        {
            doorOpenAni[0].SetBool("openDoor", true);

            tutorialStageConManagerScript.resetState();
        }
        */
    }

    // ������ �� �Լ��� ȣ�� ��
    public void ifDeadChageUiState()
    {
        tutorialStageConManagerScript.resetState();
    }
}
