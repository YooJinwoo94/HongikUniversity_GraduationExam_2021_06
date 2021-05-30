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
    private static StageManager instance = null;

    List<int> nowStageCountList = new List<int>();

    [SerializeField]
    TutorialStageManger tutorialStageManagerScript;
    [SerializeField]
    StageClearCheckManager stageClearCheckManagerScript;
    [SerializeField]
    PlayerUISeletManger playerUiSelectMangerScript;
    [SerializeField]
    PlayerInputScript playerMoveScript;
    [SerializeField]
    Transform[] transformPos;
    [SerializeField]
    Transform playerPos;
    [SerializeField]
    Image[] uiButtonsImg;
    [SerializeField]
    Button[] uiButtons;
    [SerializeField]
    Image[] lineImg;
    [SerializeField]
    GameObject[] xImg;
    [SerializeField]
    GameObject []stageMoveMapUI;
    [SerializeField]
    GameObject blurUI;

    int[] count ;
    [HideInInspector]
    public int dungeonNum = 1;




    void Start()
    {
        switch(dungeonNum)
        {
            case 1:
                for (int i = 0; i < 3; i++) uiButtons[i].interactable = true;
                for (int i = 3; i < 6; i++) uiButtons[i].interactable = false;
                break;
        }
        stageMoveMapUI[dungeonNum].SetActive(false);
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

    //버튼 눌렀으면 적용
    //현재 위치값을 가져온다 스위치로비교를 하여 현재 내 상황을 비교한다.
    //버튼을 구별해야 한다.
    // 스택으로 현재 위치값을 구하고 넣어준다.
    public void makeStageCountList(int buttonPosCheck)
    {
        switch(dungeonNum)
        {
            case 1:
                stageClearCheckManagerScript.ifPlayerGoNextStageReset();
                tutorialStageManagerScript.enabled = false;

                playerPos.position = transformPos[0].position;
                nowStageCountList.Add(buttonPosCheck);
                for (int i = 0; i < nowStageCountList.Count; i++)
                {
                    switch (nowStageCountList[i])
                    {
                        case 1:
                            for (int a = 0; a < 6; a++)
                            {
                                if (a == 0) lineImg[a].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);
                                else lineImg[a].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                            }
                            uiButtons[0].enabled = false;
                            uiButtonsImg[0].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);

                            uiButtons[1].interactable = false;
                            uiButtons[2].interactable = false;
                            uiButtons[3].interactable = true;

                            xImg[1].SetActive(true);
                            xImg[2].SetActive(true);
                            xImg[4].SetActive(true);
                            LoadingManager.loadScene("Dungeon_Num01_Stage_01");
                            //SceneManager.LoadScene("Dungeon_Num01_Stage_01");
                            break;
                        case 2:
                            for (int a = 0; a < 6; a++)
                            {
                                if (a == 1 || a == 2) lineImg[a].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);
                                else lineImg[a].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                            }
                            uiButtons[1].enabled = false;
                            uiButtonsImg[1].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);

                            uiButtons[0].interactable = false;
                            uiButtons[2].interactable = false;
                            uiButtons[3].interactable = true;
                            uiButtons[4].interactable = true;

                            xImg[0].SetActive(true);
                            xImg[2].SetActive(true);
                            LoadingManager.loadScene("Dungeon_Num01_Stage_02");
                          //  SceneManager.LoadScene("Dungeon_Num01_Stage_02");
                            break;
                        case 3:
                            for (int a = 0; a < 6; a++)
                            {
                                if (a == 3) lineImg[a].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);
                                else lineImg[a].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                            }
                            uiButtons[2].enabled = false;
                            uiButtonsImg[2].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);

                            uiButtons[0].interactable = false;
                            uiButtons[1].interactable = false;
                            uiButtons[3].interactable = false;
                            uiButtons[4].interactable = true;

                            xImg[0].SetActive(true);
                            xImg[1].SetActive(true);
                            xImg[3].SetActive(true);
                            LoadingManager.loadScene("Dungeon_Num01_Stage_03");
                            //SceneManager.LoadScene("Dungeon_Num01_Stage_03");
                            break;
                        case 4:
                            for (int a = 4; a < 6; a++)
                            {
                                if (a == 4) lineImg[a].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);
                                else lineImg[a].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                            }

                            uiButtons[3].enabled = false;
                            uiButtonsImg[3].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);

                            uiButtons[2].interactable = false;
                            uiButtons[4].interactable = false;
                            uiButtons[5].interactable = true;

                            xImg[2].SetActive(true);
                            xImg[4].SetActive(true);
                            LoadingManager.loadScene("Dungeon_Num01_Stage_04");
                          //  SceneManager.LoadScene("Dungeon_Num01_Stage_04");
                            break;
                        case 5:
                            for (int a = 5; a < 6; a++)
                            {
                                if (a == 5) lineImg[a].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);
                                else lineImg[a].color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                            }
                            uiButtons[4].enabled = false;
                            uiButtonsImg[4].color = new Color(229 / 255f, 126 / 255f, 12 / 255f, 255 / 255f);

                            uiButtons[3].interactable = false;
                            uiButtons[5].interactable = true;

                            xImg[3].SetActive(true);
                            LoadingManager.loadScene("Dungeon_Num01_Stage_05");
                         //   SceneManager.LoadScene("Dungeon_Num01_Stage_05");
                            break;
                        case 6:
                            LoadingManager.loadScene("Dungeon_Num01_Stage_06");
                            //SceneManager.LoadScene("Dungeon_Num01_Stage_06");
                            break;
                    }
                }
                stageMoveMapUI[dungeonNum].SetActive(false);
                break;
            case 2:
                break;
        }
        blurUI.SetActive(false);
        playerMoveScript.state = PlayerState.idle;
    }

    public void playerStageMapUI()
    {
        stageMoveMapUI[dungeonNum].SetActive(true);
        blurUI.SetActive(true);
    }
    public int returnNowStageName()
    {
        int num = 0;
        if (dungeonNum == 1)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Dungeon_Num01_Stage_01":
                    num = 1;
                    break;
                case "Dungeon_Num01_Stage_02":
                    num = 2;
                    break;
                case "Dungeon_Num01_Stage_03":
                    num = 3;
                    break;
                case "Dungeon_Num01_Stage_04":
                    num = 4;
                    break;
                case "Dungeon_Num01_Stage_05":
                    num = 5;
                    break;
                case "Dungeon_Num01_Stage_06":
                    num = 6;
                    break;
            }
        }
        return num;
    }
}
