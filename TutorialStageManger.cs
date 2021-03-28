using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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
    tutorialEnd,
    wait
}

public class TutorialStageManger : MonoBehaviour
{
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

    [SerializeField]
    TypingTextCon typingTextConScript;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;

    [SerializeField]
    GameObject[] uiPanel;
    [HideInInspector]
    public int nowTutorialNum = 0;

    BoxCollider boxCollider;
    int count = 0;
    int tutorial02Phase = 0;
    [SerializeField]
    Animator[] boorAni;

    [SerializeField]
    Image[] forIngameUiTutorialVer;
    [SerializeField]
    Image[] forWeaponGetUiTutorialVer;


    public TutorialState tutorialState = TutorialState.tutorialReady;





    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
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
        }   
        tutorial();    
    }











    void tutorialFin()
    {
        boorAni[0].SetBool("OpenDoor", true);
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
                break;
            case 5:
                tutorialState = TutorialState.tutorial02_6;
                break;
            case 6:
                tutorialState = TutorialState.tutorial03;
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
        }
        uiPanel[0].SetActive(true);

        startOnce = false;
    }




    void tutorial()
    {
        switch (tutorialState)
        {
            case TutorialState.tutorialReady:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    uiPanel[0].SetActive(false);
                    uiPanel[1].SetActive(false);
                    tutorialState = TutorialState.wait;
                }
                break;

            case TutorialState.tutorial01:
                if (startOnce == false)
                {
                    typingTextConScript.typingTextStart(1);
                    startOnce = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (count >= 7)
                    {
                        tutorialState = TutorialState.tutorial02_0;

                        tutorialBasicObj.SetActive(false);
                        forIngameUiTutorialVer[count - 1].enabled = false;
                        startOnce = false;
                    }
                    else
                    {
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
                    uiPanel[0].SetActive(true);
                    uiPanel[1].SetActive(true);
                    typingTextConScript.typingTextStart(2);
                    startOnce = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    uiPanel[1].SetActive(false);
                    typingTextConScript.typingTextStart(3);
                    nowTutorialNum = 0;
                    tutorialState = TutorialState.wait;

                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_1:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    typingTextConScript.typingTextStart(4);
                    nowTutorialNum = 1;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_2:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    typingTextConScript.typingTextStart(5);
                    nowTutorialNum = 2;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_3:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    typingTextConScript.typingTextStart(6);
                    nowTutorialNum = 3;
                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }
                break;

            case TutorialState.tutorial02_4:
                if (Input.GetKeyDown(KeyCode.S))
                {
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
                    typingTextConScript.typingTextStart(8);
                    // 다음꺼 tutorial02_6 입니다 라고 알림
                    nowTutorialNum = 5;

                    uiPanel[0].SetActive(false);
                    uiPanel[1].SetActive(false);
                    tutorialState = TutorialState.wait;

                    Invoke("resetState", 0.5f);
                }
                break;
            
            case TutorialState.tutorial02_6:
                if (Input.GetMouseButtonDown(0))
                {
                    typingTextConScript.typingTextStart(9);

                    // 다음꺼 tutorial03 입니다 라고 알림
                    nowTutorialNum = 6;                    
                    uiPanel[0].SetActive(false);           
                    uiPanel[1].SetActive(false);            

                    tutorialState = TutorialState.wait;
                    Invoke("resetState", 0.5f);
                }          
                break;

                // 전투
            case TutorialState.tutorial03:
               
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (startOnce != false) return;

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
                    return;
                }
                if (Input.anyKeyDown)
                {
                    uiPanel[0].SetActive(false);
                    tutorialState =  TutorialState.wait;
                    nowTutorialNum = 8;
                }
                break;
             
                // 3_1 => update => 4_0
            case TutorialState.tutorial04_0:

                if (startOnce == false)
                {
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
                    startOnce = true;
                    uiPanel[0].SetActive(true);
                    typingTextConScript.typingTextStart(12);
                    return;
                }
                if (Input.anyKeyDown)
                {
                    tutorialStageClearManagerScript.openDoor();
                    uiPanel[0].SetActive(false);
                    tutorialState = TutorialState.wait;
                }
                break;
        }
    }

















    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tutorialState = TutorialState.tutorial01;

            uiPanel[0].SetActive(true);
            uiPanel[1].SetActive(true);

            boorAni[0].SetBool("Start", true);
            boxCollider.enabled = false;
        }
    }
}
