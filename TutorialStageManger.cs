using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum TutorialState
{
    tutorialReady,
    tutorial01,
    tutorial02_0,
    tutorial02_1,
    tutorial02_2,
    tutorial02_3,
    tutorial02_4,
    tutorial02_5,
    tutorial02_6,
    tutorial03,
    tutorial03_1,
    tutorial04_0,
    tutorial05_0,
    tutorial06_0,
    tutorial07_0,
    tutorial08_0,
    tutorialEnd,
    wait
}

public class TutorialStageManger : MonoBehaviour
{
    [SerializeField]
    GameObject ui;
    [SerializeField]
    PlayerUISeletManger playerUiSelectManagerScript;
    [SerializeField]
    TutorialStageClearManager tutorialStageClearManagerScript;
    [SerializeField]
    GameObject tutorialMonsterPrefab;
    [SerializeField]
    Transform[] tutorialMonsterSpawnPos;

    [SerializeField]
    GameObject tutorialBasicObj;

    bool startOnce = false;
    bool startOnce_02 = false;
    bool startOnce_03 = false;

    [SerializeField]
    TypingTextCon typingTextConScript;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;
    [SerializeField]
    PlayerPowerGetUINo2 playerPowerGetUiNo2Script;

    [SerializeField]
    GameObject[] uiPanel;
    [HideInInspector]
    public int nowTutorialNum = 0;

    BoxCollider boxCollider;
    int count = 0;
    int tutorial02Phase = 0;
    [SerializeField]
    Animator[] doorAni;

    [SerializeField]
    Image[] forIngameUiTutorialVer;
    [SerializeField]
    Image[] forWeaponGetUiTutorialVer;

    //키보드 애니 작동
    [SerializeField]
    Animator[] playerKeyButtonAni;

    [SerializeField]
    GameObject[] playerKeyButtonObj;

    public TutorialState tutorialState = TutorialState.tutorialReady;


    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial_Scene")
        {
            ui.SetActive(true);
            typingTextConScript.typingTextStart(0);
        }
    }


   

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial_Scene") return;



        if   ( tutorialState == TutorialState.tutorialEnd || typingTextConScript.textState == TextState.textStart) return;

        if (playerGetWeaponUINo5Script.bgUiNo5Obj.activeInHierarchy == true &&
            startOnce_02 == false)
        {
            uiPanel[0].SetActive(false);
            startOnce_02 = true;
            startOnce = false;
            tutorialState = TutorialState.tutorial04_0;
            // 다음꺼 tutorial04_0 입니다 라고 알림
            nowTutorialNum = 8;
            count = 0;
            return;
        }
        if (playerPowerGetUiNo2Script.bgUiNo2Obj.activeInHierarchy == true && startOnce_03 == false)
        {
            startOnce_03 = true;
            tutorialState = TutorialState.tutorial08_0;
            return;
        }
        tutorial();    
    }








    // 인사말
    // 1번째 ui설명
    // 2번쨰 기본이동 
    // 3번쨰 기본 전투 
    // 무기 습득 및 인벤토리 설명
    public void resetState()
    {
        switch (nowTutorialNum)
        {
            // w 키에 대한 설명
            case 0:
                tutorialState = TutorialState.tutorial02_1;
                break;
            case 1:
                tutorialState = TutorialState.tutorial02_2;
                break;
            case 2:
                tutorialState = TutorialState.tutorial02_3;
                break;
            case 3:
                tutorialState = TutorialState.tutorial02_4;
                break;
            case 4:
                tutorialState = TutorialState.tutorial02_5;
                for (int i = 0; i < 5; i++)
                {
                    playerKeyButtonAni[i].SetBool("Start", true);
                }
                break;
            case 5:
                tutorialState = TutorialState.tutorial02_6;
                break;
            case 6:
                tutorialState = TutorialState.tutorial03;
                playerKeyButtonObj[4].SetActive(true);
                playerKeyButtonAni[4].enabled = true;
                break;
            case 7:
                tutorialState = TutorialState.tutorial03_1;
                break;
            case 8:
                tutorialState = TutorialState.tutorial04_0;
                break;
            case 9:
                tutorialState = TutorialState.tutorial05_0;
                break;
            case 10:
                tutorialState = TutorialState.tutorial06_0;
                break;
            case 11:
                tutorialState = TutorialState.tutorial07_0;
                break;
            case 12:
                tutorialState = TutorialState.tutorial08_0;
                break;
        }
        uiPanel[0].SetActive(true);

        startOnce = false;
    }


    public void isTutorial()
    {
        tutorialState = TutorialState.tutorialReady;
    }

    void tutorial()
    {
        switch (tutorialState)
        {
            case TutorialState.tutorialReady:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialState = TutorialState.tutorial01;

                    uiPanel[0].SetActive(true);
                    uiPanel[1].SetActive(true);
                }
                break;

            case TutorialState.tutorial01:
                if (startOnce == false)
                {
                    startOnce = true;

                    playerKeyButtonObj[4].SetActive(true);
                    playerKeyButtonObj[4].transform.localPosition = new Vector3(296.7f, -432, 0);
                    playerKeyButtonAni[4].enabled = true;

                    typingTextConScript.typingTextStart(1);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (count >= 7)
                    {
                        tutorialState = TutorialState.tutorial02_0;

                        tutorialBasicObj.SetActive(false);
                        forIngameUiTutorialVer[count - 1].enabled = false;

                        playerKeyButtonObj[4].SetActive(false);
                        playerKeyButtonAni[4].enabled = false;

                        startOnce = false;
                    }
                    else
                    {
                        playerKeyButtonObj[4].SetActive(true);
                        playerKeyButtonObj[4].transform.localPosition = new Vector3(193, -371.4f, 0);
                        playerKeyButtonAni[4].enabled = true;

                        tutorialBasicObj.SetActive(true);
                        uiPanel[0].SetActive(false);
                        uiPanel[1].SetActive(false);

                        if (count != 0) forIngameUiTutorialVer[count - 1].enabled = false;
                        forIngameUiTutorialVer[count].enabled = true;
                        typingTextConScript.tutorialDetail(count);
                        count++;
                    }
                }
                break;

            case TutorialState.tutorial02_0:
                if (startOnce == false)
                {
                    startOnce = true;

                    playerKeyButtonObj[4].SetActive(true);
                    playerKeyButtonObj[4].transform.localPosition = new Vector3(296.7f, -432, 0);
                    playerKeyButtonAni[4].enabled = true;

                    uiPanel[0].SetActive(true);
                    uiPanel[1].SetActive(true);

                    typingTextConScript.typingTextStart(2);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerKeyButtonObj[4].SetActive(false);

                    uiPanel[1].SetActive(false);
                    typingTextConScript.typingTextStart(3);
                    nowTutorialNum = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        playerKeyButtonObj[i].SetActive(true);
                        playerKeyButtonAni[i].SetBool("Start", false);
                    }
                    playerKeyButtonAni[0].SetBool("Start", true);

                    tutorialState = TutorialState.tutorial02_1;
                }
                break;

            case TutorialState.tutorial02_1:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    for (int i = 0; i < 5; i++) playerKeyButtonAni[i].SetBool("Start", false);
                    playerKeyButtonAni[1].SetBool("Start", true);

                    playerKeyButtonObj[1].SetActive(true);


                    typingTextConScript.typingTextStart(4);
                    nowTutorialNum = 1;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_2:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    for (int i = 0; i < 5; i++) playerKeyButtonAni[i].SetBool("Start", false);
                    playerKeyButtonAni[2].SetBool("Start", true);

                    playerKeyButtonObj[2].SetActive(true);
                    playerKeyButtonAni[2].enabled = true;


                    typingTextConScript.typingTextStart(5);
                    nowTutorialNum = 2;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_3:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    for (int i = 0; i < 5; i++) playerKeyButtonAni[i].SetBool("Start", false);
                    playerKeyButtonAni[3].SetBool("Start", true);

                    playerKeyButtonObj[3].SetActive(true);
                    playerKeyButtonAni[3].enabled = true;


                    typingTextConScript.typingTextStart(6);
                    nowTutorialNum = 3;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_4:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    playerKeyButtonObj[4].SetActive(true);

                    for (int i = 0; i < 5; i++)
                    {
                        playerKeyButtonAni[i].SetBool("Start", false);
                    }

                    typingTextConScript.typingTextStart(7);
                    nowTutorialNum = 4;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_5:
                if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)||
                    Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W))
                {
                    tutorialState = TutorialState.wait;

                    for (int i = 0; i < 5; i++)
                    {
                        playerKeyButtonObj[i].SetActive(false);
                        playerKeyButtonAni[i].enabled = false;
                    }

                    playerKeyButtonObj[6].SetActive(true);
                    playerKeyButtonAni[6].enabled = true;

                    typingTextConScript.typingTextStart(8);
                    // 다음꺼 tutorial02_6 입니다 라고 알림
                    nowTutorialNum = 5;

                    uiPanel[0].SetActive(false);
                    uiPanel[1].SetActive(false);
            
                    Invoke("resetState", 0.5f);
                }
                break;
            

                //마우스 클릭으로 공격
            case TutorialState.tutorial02_6:
                if (Input.GetMouseButtonDown(0))
                {
                    Animator door = GameObject.Find("TutorialStartDoor").GetComponent<Animator>();
                    door.SetBool("OpenDoor", true);

                    typingTextConScript.typingTextStart(9);

                    playerKeyButtonObj[6].SetActive(false);
                    playerKeyButtonAni[6].enabled = false;

                    // 다음꺼 tutorial03 입니다 라고 알림
                    nowTutorialNum = 6;                    
                    uiPanel[0].SetActive(false);           
                    uiPanel[1].SetActive(false);            

                    tutorialState = TutorialState.wait;
                }          
                break;

                // 전투
            case TutorialState.tutorial03:
               
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (startOnce != false) return;

                    playerKeyButtonObj[4].SetActive(false);
                    playerKeyButtonAni[4].enabled = false;

                    uiPanel[0].SetActive(false);
                    // 다음꺼 tutorial03_1 입니다 라고 알림
                    nowTutorialNum = 7;
                    tutorialState = TutorialState.wait;                
                    startOnce = true;    
                    
                    uiPanel[0].SetActive(false);
                    Instantiate(tutorialMonsterPrefab, tutorialMonsterSpawnPos[0]);
                }
                break;

                // 무기를 얻으면 나오는 설명
            case TutorialState.tutorial03_1:
                if (startOnce == false)
                {
                    startOnce = true;
                    typingTextConScript.typingTextStart(10);

                    playerKeyButtonObj[4].SetActive(true);
                    playerKeyButtonAni[4].enabled = true;
                    return;
                }
                if (Input.anyKeyDown)
                {
                    playerKeyButtonObj[4].SetActive(false);
                    playerKeyButtonAni[4].enabled = false;
                    uiPanel[0].SetActive(false);
                    tutorialState =  TutorialState.wait;
                    nowTutorialNum = 8;
                }
                break;
             
                // 3_1 => update => 4_0
            case TutorialState.tutorial04_0:

                if (startOnce == false)
                {
                    playerKeyButtonObj[4].transform.localPosition = new Vector3(193, -371.4f, 0);
                    playerKeyButtonObj[4].SetActive(true);
                    playerKeyButtonAni[4].enabled = true;


                    startOnce = true;
                    tutorialBasicObj.SetActive(true);

                    forWeaponGetUiTutorialVer[count].enabled = true;
                    typingTextConScript.tutorialDetail(count + 7);
                    count++;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (count >= 4)
                    {
                        playerKeyButtonObj[4].SetActive(false);
                        playerKeyButtonAni[4].enabled = false;

                        startOnce = false;
                        tutorialBasicObj.SetActive(false);
                        forWeaponGetUiTutorialVer[count - 1].enabled = false;

                        nowTutorialNum = 9;
                        tutorialState = TutorialState.tutorial05_0;
                    }
                    else
                    {
                        tutorialBasicObj.SetActive(true);
                        uiPanel[0].SetActive(false);
                        uiPanel[1].SetActive(false);

                        if (count != 0) forWeaponGetUiTutorialVer[count - 1].enabled = false;
                        forWeaponGetUiTutorialVer[count].enabled = true;
                        typingTextConScript.tutorialDetail(count+7);
                        count++;
                    }
                }

                break;

            case TutorialState.tutorial05_0:
                if ((startOnce == false && Input.GetKeyDown(KeyCode.Return) && 
                    (playerUiSelectManagerScript.leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable == false 
                    || playerUiSelectManagerScript.leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable == false))

               || (startOnce == false && Input.GetKeyDown(KeyCode.Escape)) )
                {
                    playerKeyButtonObj[5].SetActive(true);
                    playerKeyButtonAni[5].enabled = true;
                    playerKeyButtonObj[5].transform.localPosition = new Vector3(-18.6f, -432.6f, 0);

                    startOnce = true;
                    uiPanel[0].SetActive(true);
                    typingTextConScript.typingTextStart(11);
                    tutorialState = TutorialState.tutorial06_0;
                    nowTutorialNum = 10;
                }
                break;

            case TutorialState.tutorial06_0:
                if (Input.GetKeyDown(KeyCode.I))
                {
                    startOnce = false;

                    playerKeyButtonObj[5].SetActive(false);
                    playerKeyButtonAni[5].enabled = false;

                    uiPanel[0].SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Escape)&& startOnce == false)
                {
                    tutorialState = TutorialState.tutorial07_0;
                    nowTutorialNum = 11;
                }
                break;

            case TutorialState.tutorial07_0:

                if (startOnce == false)
                {
                    playerKeyButtonObj[4].transform.localPosition = new Vector3(296.7f, -432, 0);
                    playerKeyButtonObj[4].SetActive(true);
                    playerKeyButtonAni[4].enabled = true;

                    startOnce = true;
                    uiPanel[0].SetActive(true);
                    typingTextConScript.typingTextStart(12);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    nowTutorialNum = 12;
                    startOnce = false;

                    playerKeyButtonObj[4].SetActive(false);
                    playerKeyButtonAni[4].enabled = false;

                    tutorialStageClearManagerScript.openDoor(0);
                    uiPanel[0].SetActive(false);
                    tutorialState = TutorialState.wait;
                }
                break;

            case TutorialState.tutorial08_0:

                if (playerPowerGetUiNo2Script.bgUiNo2Obj.activeInHierarchy == false)
                {
                    tutorialStageClearManagerScript.openDoor(1);
                    tutorialState = TutorialState.wait;
                }
                break;
        }
    }
















    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name != "Tutorial_Scene") return;



        if (other.tag == "Player")
        {
            Animator door = GameObject.Find("TutorialStartDoor").GetComponent<Animator>();
            door.SetBool("Start", true);
            boxCollider.enabled = false;
            Invoke("resetState", 0.5f);
        }
    }
}
