using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageManager : MonoBehaviour
{
    int stageNum;
    bool[] StageBoolSetting;

    [SerializeField]
    Animator[] doorAni;

    private void Awake()
    {
        stageNum = 0;
    }


    public void checkStageAndSettingStart(int stageCount)
    {
        switch (stageCount)
        {
            case 1:
                for (int i = 0; i < 1; i++) StageBoolSetting[i] = false;
                break;

            case 2:
                for (int i = 0; i < 1; i++) StageBoolSetting[i] = false;
                break;

            case 3:
                for (int i = 0; i < 1; i++) StageBoolSetting[i] = false;
                break;

            case 4:
                for (int i = 0; i < 1; i++) StageBoolSetting[i] = false;
                break;

            case 5:
                for (int i = 0; i < 1; i++) StageBoolSetting[i] = false;
                break;

           // boss stage Pass
            case 6:
                break;
        }
        stageNum = stageCount;
    }

    void doorOpenAndNextRoom()
    {
        switch (stageNum)
        {
            case 1:
                doorAni[1].SetBool("isPlayerDoorOpen", true);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            // boss stage Pass
            case 6:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorAni[0].SetBool("isPlayerDoorClose", true);
    }
}
