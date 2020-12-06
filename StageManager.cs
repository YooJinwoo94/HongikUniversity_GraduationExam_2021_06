using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    void Awake()
    {
        StageMoveMapUI.SetActive(false);
        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }
    public static StageManager Instance
    {
        get
        {
            if (null == instance) return null;
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
        moveTo(buttonPosCheck);
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

                    StageMoveMapUI.SetActive(false);
                    SceneManager.LoadScene("Stage_01");
                    break;
                case 2:
                    uiButtons[0].interactable = false;
                    uiButtons[1].enabled = false;
                    uiButtons[3].interactable = true;
                    uiButtons[4].interactable = true;

                    StageMoveMapUI.SetActive(false);
                    SceneManager.LoadScene("Stage_02");
                    break;
                case 3:
                    uiButtons[2].enabled = false;
                    uiButtons[3].interactable = false;
                    uiButtons[5].interactable = true;

                    StageMoveMapUI.SetActive(false);                
                    SceneManager.LoadScene("Stage_03");
                    break;
                case 4:
                    uiButtons[2].interactable = false;
                    uiButtons[3].enabled = false;
                    uiButtons[4].interactable = false;
                    uiButtons[5].interactable = true;

                    StageMoveMapUI.SetActive(false);
                    SceneManager.LoadScene("Stage_04");
                    break;
                case 5:
                    uiButtons[3].interactable = false;
                    uiButtons[4].enabled = false;
                    uiButtons[5].interactable = true;

                    StageMoveMapUI.SetActive(false);
                    SceneManager.LoadScene("Stage_05");
                    break;
                case 6:
                    StageMoveMapUI.SetActive(false);
        
                //    PlayerCamManager.Instance.bossStageCam();
                    SceneManager.LoadScene("Stage_06");
                    break;
            }
        }
        Player_Move_Script.Instance.playerStateChageFromStageManger();
    }

    public void playerStageMapUI()
    {
        StageMoveMapUI.SetActive(true);
    }

    public int returnNowStageCount()
    {
        return nowStageCountList[nowStageCountList.Count-1];
    }
}
