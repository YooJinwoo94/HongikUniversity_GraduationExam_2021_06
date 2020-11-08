using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum StageState
{
    weaponStage,
    hpStage,
    moreGoldStage,
    playerStateChangeStage,
    bossStage,
}


public class StageManager : MonoBehaviour
{
    List<int> nowStageCountList = new List<int>();
    //Stack<int> nowStageCountStack = new Stack<int>();
    // List<StageCount> stageCountList = new List<StageCount>();
    // List<StageState> stageStateList = new List<StageState>();

    // StageCount StageCount;
    // StageState StageState;

    [SerializeField]
    Transform[] transformPos;
    [SerializeField]
    Transform playerPos;
    [SerializeField]
    Button[] uiButtons;
    [SerializeField]
    GameObject StageMoveMapUI;
    private static StageManager instance = null;
    int[] count ;



    void Start()
    {
        StageMoveMapUI.SetActive(false);
        uiButtons[2].interactable = false;
        uiButtons[3].interactable = false;
        uiButtons[4].interactable = false;
        uiButtons[5].interactable = false;

        instance = this;

        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public static StageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }







    void moveTo(int StageState)
    {
        playerPos.position = transformPos[StageState-1].position;
    }

    //버튼 눌렀으면 적용
    //현재 위치값을 가져온다 스위치로비교를 하여 현재 내 상황을 비교한다.
    //버튼을 구별해야 한다.
    // 스택으로 현재 위치값을 구하고 넣어준다.
    public void makeStageCountList(int buttonPosCheck)
    {
        /*
        switch (buttonPosCheck)
        {
            case 1:
                uiButtons[0].enabled = false;
                uiButtons[1].interactable = false;
                uiButtons[2].interactable = true;
                uiButtons[3].interactable = true;
                uiButtons[4].interactable = false;
                uiButtons[5].interactable = false;
                break;
            case 2:
                uiButtons[0].interactable = false;
                uiButtons[1].enabled = false;
                uiButtons[2].interactable = false;
                uiButtons[3].interactable = true;
                uiButtons[4].interactable = true;
                uiButtons[5].interactable = false;
                break;
            case 3:
                if (nowStageCountStack.Peek() == 1)
                {
                    uiButtons[0].enabled = false;
                    uiButtons[1].interactable = false;
                    uiButtons[2].enabled = false;
                    uiButtons[3].interactable = false;
                    uiButtons[4].interactable = false;
                    uiButtons[5].interactable = true;
                }
                break;
            case 4:
                if (nowStageCountStack.Peek() == 1)
                {
                    uiButtons[0].enabled = false;
                    uiButtons[1].interactable = false;
                    uiButtons[2].interactable = false;
                    uiButtons[3].enabled = false;
                    uiButtons[4].interactable = false;
                    uiButtons[5].interactable = true;
                }
                else if (nowStageCountStack.Peek() == 2)
                {
                    uiButtons[0].interactable = false;
                    uiButtons[1].enabled = false;
                    uiButtons[2].interactable = false;
                    uiButtons[3].enabled = false;
                    uiButtons[4].interactable = false;
                    uiButtons[5].interactable = true;
                }
                break;
            case 5:
                if (nowStageCountStack.Peek() == 2)
                {
                    uiButtons[0].interactable = false;
                    uiButtons[1].enabled = false;
                    uiButtons[2].interactable = false;
                    uiButtons[3].interactable = false;
                    uiButtons[4].enabled = false;
                    uiButtons[5].interactable = true;
                }
                break;
            case 6:
                break;          
        }
        */
        moveTo(buttonPosCheck);
        //nowStageCountStack.Push(buttonPosCheck);
        nowStageCountList.Add(buttonPosCheck);
        for (int i = 0; i < nowStageCountList.Count; i++)
        {
            switch (nowStageCountList[i])
            {
                case 1:
                    uiButtons[0].enabled = false;
                    uiButtons[1].interactable = false;
                    uiButtons[2].interactable = true;
                    uiButtons[3].interactable = true;
                    break;
                case 2:
                    uiButtons[0].interactable = false;
                    uiButtons[1].enabled = false;
                    uiButtons[3].interactable = true;
                    uiButtons[4].interactable = true;
                    break;
                case 3:
                    uiButtons[2].enabled = false;
                    uiButtons[3].interactable = false;
                    uiButtons[5].interactable = true;
                    break;
                case 4:
                    uiButtons[2].interactable = false;
                    uiButtons[3].enabled = false;
                    uiButtons[4].interactable = false;
                    uiButtons[5].interactable = true;
                    break;
                case 5:
                    uiButtons[3].interactable = false;
                    uiButtons[4].enabled = false;
                    uiButtons[5].interactable = true;
                    break;
                case 6:
                    break;
            }
           
        }
        StageMoveMapUI.SetActive(false);
    }



    public void playerStageMapUI()
    {
        StageMoveMapUI.SetActive(true);
    }
}
