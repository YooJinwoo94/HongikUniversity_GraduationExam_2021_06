using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum MakePlayerWait
{
   wait,
   play
}

public enum TutorialStateVer2
{ 
    step00,
    step01_1, step01_2, step01_3, step01_3_1, step01_4,
    step02_1, step02_2, step02_3,
    step03_1, step03_2, step03_3, step03_4, step03_5, step03_6, step03_7, step03_8, step03_9,
    step04_1, step04_2, step04_2_1,step04_3, step04_4, step04_5, step04_6,
    step05_1, step05_2, step05_3,
}



public class TutorialManagerVer2 : MonoBehaviour
{
    TypingTextCon typingTextConScript;

    public MakePlayerWait makePlayerWait = MakePlayerWait.wait;
    public TutorialStateVer2 tutorial = TutorialStateVer2.step00;


    int buttonAniNum;
    [SerializeField]
    GameObject tutorialMonster;
    [SerializeField]
    Transform tutorialMonsterSpwanPos;
    [SerializeField]
    Animator[] doorAni;
    [SerializeField]
    Animator[] spaceButtonAni;
    [SerializeField]
    Animator[] wsadButtonAni;
    [SerializeField]
    Animator[] cButtonAni;
    [SerializeField]
    Animator[] invenButtonAni;
    [SerializeField]
    Animator[] mouseClickButtonAni;

    [SerializeField]
    GameObject[] forIngameUiTutorial;
    [SerializeField]
    GameObject[] ingameUISet;
    [SerializeField]
    GameObject[] typingUiSet;
    [SerializeField]
    GameObject[] uiSet;

    [SerializeField]
    GameObject[] forNextFloorObj;

    [SerializeField]
    GameObject[] camObj;


    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;
    PlayerPowerGetUINo2 playerPowerGetUiNo2Script;

    bool startOnce = false;
    int textCount = 0;
    int detailTextCount = 0;


    private void Start()
    {
        playerPowerGetUiNo2Script = GameObject.Find("PlayerUIManager").GetComponent<PlayerPowerGetUINo2>();
        playerGetWeaponUINo5Script = GameObject.Find("PlayerUIManager").GetComponent<PlayerGetWeaponUINNo5>();
        buttonAniNum = 0;
        typingTextConScript = GetComponent<TypingTextCon>();

        uiSet[0].SetActive(true);
        StartCoroutine("ButtonAni");
    }








    IEnumerator ButtonAni()
    {
        if ((tutorial == TutorialStateVer2.step02_3) ||
            (tutorial == TutorialStateVer2.step03_1) ||  
            (tutorial == TutorialStateVer2.step01_3) ||
            (tutorial == TutorialStateVer2.step01_3_1) ||
            (tutorial == TutorialStateVer2.step03_7) ||
            (tutorial == TutorialStateVer2.step03_8) ||
            (tutorial == TutorialStateVer2.step04_4) ||
            (tutorial == TutorialStateVer2.step04_4) ||
            (tutorial == TutorialStateVer2.step03_9) || 
            (tutorial == TutorialStateVer2.step04_5) ||
            (tutorial == TutorialStateVer2.step04_3))
        {
            spaceButtonAni[0].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            spaceButtonAni[0].SetBool("Start", false);
            yield return new WaitForSeconds(1f);
        }
        if ((tutorial == TutorialStateVer2.step02_2))
        {
            spaceButtonAni[0].SetBool("Start", true);
            spaceButtonAni[1].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            spaceButtonAni[0].SetBool("Start", false);
            spaceButtonAni[1].SetBool("Start", false);
            yield return new WaitForSeconds(1f);
        }
        if ((tutorial == TutorialStateVer2.step03_3))
        {
            wsadButtonAni[buttonAniNum].SetBool("Start", true);
            spaceButtonAni[0].SetBool("Start", true);
            spaceButtonAni[2].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            wsadButtonAni[buttonAniNum].SetBool("Start", false);
            spaceButtonAni[0].SetBool("Start", false);
            spaceButtonAni[2].SetBool("Start", false);
            buttonAniNum++;
            if (buttonAniNum == 8) buttonAniNum = 4;
            yield return new WaitForSeconds(0.2f);
        }
        if ((tutorial == TutorialStateVer2.step03_5))
        {
            cButtonAni[0].SetBool("Start", true);
            spaceButtonAni[0].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            cButtonAni[0].SetBool("Start", false);
            spaceButtonAni[0].SetBool("Start", false);
            yield return new WaitForSeconds(1f);
        }
        if (tutorial == TutorialStateVer2.step00)
        {
            wsadButtonAni[buttonAniNum].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            wsadButtonAni[buttonAniNum].SetBool("Start", false);
            buttonAniNum++;
            if (buttonAniNum == 4) buttonAniNum = 0;
        }
        if (tutorial == TutorialStateVer2.step03_1)
        {
            mouseClickButtonAni[0].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            mouseClickButtonAni[0].SetBool("Start", false);
            yield return null;
        }
        if (tutorial == TutorialStateVer2.step04_2)
        {
            spaceButtonAni[1].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            spaceButtonAni[1].SetBool("Start", false);
            yield return new WaitForSeconds(1f);
        }
        if (tutorial == TutorialStateVer2.step04_3)
        {
            invenButtonAni[0].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
            invenButtonAni[0].SetBool("Start", false);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("ButtonAni");
    }
    void camCon(int num , bool isReset = false)
    {
        switch (isReset)
        {
            case true:
                camObj[num].SetActive(false);
                break;

            case false:
                for (int i = 0; i < 10; i++)
                {
                    if (i == num) camObj[i].SetActive(true);
                    else camObj[i].SetActive(false);
                }
                break;
        }
    }



    void takeTimeForNextLevel()
    {
        switch (tutorial)
        {
            case TutorialStateVer2.step00:
                startOnce = false;
                //¸ØÃç
                makePlayerWait = MakePlayerWait.wait;
                tutorial = TutorialStateVer2.step01_1;
                break;

            case TutorialStateVer2.step01_1:
                camCon(0, true);
                makePlayerWait = MakePlayerWait.play;
                tutorial = TutorialStateVer2.step01_2;
                startOnce = false;
                break;

            case TutorialStateVer2.step01_2:
                break;

            case TutorialStateVer2.step01_3:
                uiSet[1].SetActive(true);
                typingUiSet[0].SetActive(true);
                typingTextConScript.typingTextStart(textCount);

                tutorial = TutorialStateVer2.step01_3_1;
                break;

            case TutorialStateVer2.step01_4:
                camCon(1, true);
                startOnce = false;
                tutorial = TutorialStateVer2.step02_1;
                break;

            case TutorialStateVer2.step03_4:
                TutorialTypeMonsterMove tutorialMoveScript = GameObject.Find("TutorialEnemy01(Clone)").GetComponent<TutorialTypeMonsterMove>();
                tutorialMoveScript.state = TutorialEnemyState.getWait;
                tutorial = TutorialStateVer2.step03_5;
                startOnce = false;
                break;
        }
    }



    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial_Scene_Ver2") return;


        if ((playerGetWeaponUINo5Script.bgUiNo5Obj.activeInHierarchy == true)
          && (tutorial == TutorialStateVer2.step04_1)) tutorial = TutorialStateVer2.step04_2;

        if ((playerGetWeaponUINo5Script.bgUiNo5Obj.activeInHierarchy == false)
  && (tutorial == TutorialStateVer2.step04_2_1)) tutorial = TutorialStateVer2.step04_3;

        if ((playerPowerGetUiNo2Script.bgUiNo2Obj.activeInHierarchy == false)
            && (tutorial == TutorialStateVer2.step05_2)) tutorial = TutorialStateVer2.step05_3;

        switch (tutorial)
        {    
            case TutorialStateVer2.step00:
                if (((Input.GetKeyDown(KeyCode.W))
                    || Input.GetKeyDown(KeyCode.A)
                    || Input.GetKeyDown(KeyCode.S)
                    || Input.GetKeyDown(KeyCode.D))
                    && startOnce == false)
                {      
                    startOnce = true;
                    StopAllCoroutines();
                    uiSet[0].SetActive(false);
                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.play;
                    Invoke("takeTimeForNextLevel", 0.5f);
                }
                break;

            case TutorialStateVer2.step01_1:
                if (startOnce == false)
                {
                    startOnce = true;
                    camCon(0);
                    Invoke("takeTimeForNextLevel", 3f);
                }
                break;
            case TutorialStateVer2.step01_2:
                break;

            case TutorialStateVer2.step01_3:
                if (startOnce == false)
                {
                    startOnce = true;
                    camCon(0);

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    ingameUISet[0].SetActive(false);
                    ingameUISet[1].SetActive(false);

                    StartCoroutine("ButtonAni");
                    Invoke("takeTimeForNextLevel", 2f);
                }
                break;

                // 1¹ø¤Š ¹®ÀÌ ¿­¸²
            case TutorialStateVer2.step01_3_1:
                if (typingTextConScript.textState == TextState.textStart) return;
                if ((Input.GetKeyDown(KeyCode.Space)))
                {
                    switch (textCount)
                    {
                        case 1:
                            startOnce = false;
                            StopAllCoroutines();
                            //Ä· Á¶Àý
                            camCon(0, true);
                            camCon(1);
                            //¹® ¿­·Á!
                            doorAni[0].SetTrigger("OpenDoor");

                            tutorial = TutorialStateVer2.step01_4;
                            return;
                    }

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                break;

            case TutorialStateVer2.step01_4:
                uiSet[1].SetActive(false);
                typingUiSet[0].SetActive(false);
                Invoke("takeTimeForNextLevel", 3f);
                break;

            case TutorialStateVer2.step02_1:
                if (startOnce == false)
                {
                    startOnce = true;
                    //¿òÁ÷¿©!
                    makePlayerWait = MakePlayerWait.play;
                    // ÀÎ°ÔÀÓ ui È­¸é Á¶Àý
                    ingameUISet[0].SetActive(true);
                }
                break;

            case TutorialStateVer2.step02_2:
                if (startOnce == false)
                {
                    startOnce = true;
                    //¹® ´Ý¾Æ!
                    doorAni[0].SetBool("Start",true);
                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;
                    typingUiSet[0].SetActive(true);

                    StartCoroutine("ButtonAni");

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }

                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (detailTextCount >= 7)
                    {
                        StopAllCoroutines();

                        uiSet[2].SetActive(false);
                        forIngameUiTutorial[detailTextCount - 1].SetActive(false);

                        startOnce = false;
                        tutorial = TutorialStateVer2.step02_3;
                    }
                    else
                    {
                        typingUiSet[0].SetActive(false);
                        uiSet[2].SetActive(true);
                        
                        if (detailTextCount != 0) forIngameUiTutorial[detailTextCount - 1].SetActive(false);
                        forIngameUiTutorial[detailTextCount].SetActive(true);
                        typingTextConScript.tutorialDetail(detailTextCount);
                        detailTextCount++;
                    }
                }
                break;

            
            case TutorialStateVer2.step02_3:
                if (startOnce == false)
                {
                    StartCoroutine("ButtonAni");

                    startOnce = true;

                    typingUiSet[0].SetActive(true);

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopAllCoroutines();

                    startOnce = false;
                    typingUiSet[0].SetActive(false);
                    tutorial = TutorialStateVer2.step03_1;
                }
                break;

            //¸ðÀÇ ÀüÅõ ½ÃÀÛ!
            // ‹š·ÁºÁ!
            case TutorialStateVer2.step03_1:
                if (startOnce == false)
                {
                    startOnce = true;
                    Instantiate(tutorialMonster, tutorialMonsterSpwanPos);

                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);
                    typingUiSet[2].SetActive(true);
                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;
                    //¿òÁ÷¿©
                    makePlayerWait = MakePlayerWait.play;
                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);
                    typingUiSet[2].SetActive(false);
                    tutorial = TutorialStateVer2.step03_2;
                }
                break;

            case TutorialStateVer2.step03_2:
                break;

            // À¸¾Ç ¸Â¾Ò¾î!
            case TutorialStateVer2.step03_3:
                if (startOnce == false)
                {
                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    startOnce = true;
                    buttonAniNum = 4;
                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);
                    typingUiSet[1].SetActive(true);
                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;
                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);
                    typingUiSet[1].SetActive(false);
                    TutorialTypeMonsterMove tutorialMoveScript = GameObject.Find("TutorialEnemy01(Clone)").GetComponent<TutorialTypeMonsterMove>();
                    tutorialMoveScript.state = TutorialEnemyState.getPlayerDogged;
                    tutorial = TutorialStateVer2.step03_4;
                    makePlayerWait = MakePlayerWait.play;
                }
                break;

                // ÀÌÁ¦ ±¸¸£±â ½ÃÀÛ!
            case TutorialStateVer2.step03_4:
                if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A) && startOnce == false)
                {
                    startOnce = true;
                    Invoke("takeTimeForNextLevel", 0.5f);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && startOnce == false)
                {
                    startOnce = true;
                    Invoke("takeTimeForNextLevel", 0.5f);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && startOnce == false)
                {
                    startOnce = true;
                    Invoke("takeTimeForNextLevel", 0.5f);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W) && startOnce == false)
                {
                    startOnce = true;
                    Invoke("takeTimeForNextLevel", 0.5f);
                }
                break;

            // ±¸¸£±â ¼º°ø ! ÆÐ¸µ ÇØ¾ßÇØ!
            case TutorialStateVer2.step03_5:
                if (startOnce == false)
                {
                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    startOnce = true;
                    buttonAniNum = 4;
                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);
                    typingUiSet[3].SetActive(true);

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;
                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);
                    typingUiSet[3].SetActive(false);

                    TutorialTypeMonsterMove tutorialMoveScript = GameObject.Find("TutorialEnemy01(Clone)").GetComponent<TutorialTypeMonsterMove>();
                    tutorialMoveScript.state = TutorialEnemyState.getParring;
                    tutorial = TutorialStateVer2.step03_6;
                    makePlayerWait = MakePlayerWait.play;
                }
                break;

             // ÆÐ¸µÁß
            case TutorialStateVer2.step03_6:
                break;

            // ÀÌÁ¦ Á×´Â ¸ð½ÀÀ» º¸¿©ÁÙ²¨¾ß
            case TutorialStateVer2.step03_7:
                if (startOnce == false)
                {
                    TutorialTypeMonsterMove tutorialMoveScript = GameObject.Find("TutorialEnemy01(Clone)").GetComponent<TutorialTypeMonsterMove>();
                    tutorialMoveScript.state = TutorialEnemyState.getWait;

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    startOnce = true;
                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;
                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);

                    TutorialTypeMonsterMove tutorialMoveScript = GameObject.Find("TutorialEnemy01(Clone)").GetComponent<TutorialTypeMonsterMove>();
                    tutorialMoveScript.state = TutorialEnemyState.getAfterDead;
                    tutorial = TutorialStateVer2.step03_8;
                    makePlayerWait = MakePlayerWait.play;
                }
                break;

           // ½Î¿ö!
            case TutorialStateVer2.step03_8:
                break;

            // Á×¾ú¾î! ¹«±â¸¦ ¸Ô¾î!
            case TutorialStateVer2.step03_9:
                if (startOnce == false)
                {
                    startOnce = true;

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;

                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);

                    makePlayerWait = MakePlayerWait.play;
                    tutorial = TutorialStateVer2.step04_1;
                }
                break;

            // ¹«±â¿¡°Ô ´Ù°¡°¡±â±îÁö ±â´Þ¸²
            case TutorialStateVer2.step04_1:
                break;

            // ¹«±â °ü·Ã ¼³¸í ui on!
            case TutorialStateVer2.step04_2:
                if (startOnce == false)
                {
                    startOnce = true;

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    uiSet[2].SetActive(true);
                    forIngameUiTutorial[detailTextCount].SetActive(true);
                    typingTextConScript.tutorialDetail(detailTextCount);
                    detailTextCount++;
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (detailTextCount >= 11)
                    {
                        StopAllCoroutines();
                        forIngameUiTutorial[detailTextCount - 1].SetActive(false);
                        startOnce = false;
                        uiSet[2].SetActive(false);
                        tutorial = TutorialStateVer2.step04_2_1;
                    }
                    else
                    {
                        if (detailTextCount != 0) forIngameUiTutorial[detailTextCount - 1].SetActive(false);
                        forIngameUiTutorial[detailTextCount].SetActive(true);
                        typingTextConScript.tutorialDetail(detailTextCount);
                        detailTextCount++;
                    }
                }
                break;

                // Àá½Ã ±â´Þ¸² + ¹«±âÃ¢ UI¸¦ ³»¸®¸é UPDATE¿¡¼­ Ã³¸®ÇÔ
            case TutorialStateVer2.step04_2_1:
                break;

            // i¸¦ ´­·¯ºÁ¶ó°í ¼³¸íÇÔ
            case TutorialStateVer2.step04_3:
                if (startOnce == false)
                {
                    startOnce = true;

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    typingUiSet[0].SetActive(true);
                    typingUiSet[4].SetActive(true);
                    StartCoroutine("ButtonAni");
                   

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;

                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);
                    typingUiSet[4].SetActive(false);

                    makePlayerWait = MakePlayerWait.play;
                    tutorial = TutorialStateVer2.step04_4;
                }
                break;

           // i¸¦ ´©¸¦°æ¿ì
            case TutorialStateVer2.step04_4:
                if (Input.GetKeyDown(KeyCode.I) && startOnce == false)
                {
                    startOnce = true;
                    //¿òÁ÷¿©!
                    makePlayerWait = MakePlayerWait.play;
                }
                if (Input.GetKeyDown(KeyCode.Escape) && startOnce == true)
                {
                    startOnce = false;
                    tutorial = TutorialStateVer2.step04_5;
                }
                break;

            // ÀÌÁ¦ ¼®»ó¿¡ ´ëÇØ ¾Ë·ÁÁÜ
            case TutorialStateVer2.step04_5:
                if (startOnce == false)
                {
                    startOnce = true;

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.wait;

                    StartCoroutine("ButtonAni");
                    typingUiSet[0].SetActive(true);

                    textCount++;
                    typingTextConScript.typingTextStart(textCount);
                }
                if (typingTextConScript.textState == TextState.textStart) return;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startOnce = false;

                    //¹® ¿­¾î!
                    doorAni[1].SetBool("openDoor", true);

                    //¸ØÃç
                    makePlayerWait = MakePlayerWait.play;

                    StopAllCoroutines();
                    typingUiSet[0].SetActive(false);
                    tutorial = TutorialStateVer2.step05_1;
                }
                break;

            //
            case TutorialStateVer2.step05_1:
                if(startOnce == false) makePlayerWait = MakePlayerWait.play; startOnce = true;
                if(playerPowerGetUiNo2Script.bgUiNo2Obj.activeInHierarchy == true)
                {
                    startOnce = false;
                    tutorial = TutorialStateVer2.step05_2;
                }
                break;

            case TutorialStateVer2.step05_2:
                //¸ØÃç
                makePlayerWait = MakePlayerWait.wait;
                break;

                //Æ©Åä¸®¾ó ³¡
            case TutorialStateVer2.step05_3:
                if (startOnce == false)
                {
                    startOnce = true;
                    //¿òÁ÷¿©
                    makePlayerWait = MakePlayerWait.play;

                    //¹® ¿­¾î!
                    doorAni[2].SetBool("openDoor", true);
                }
                break;
        }
    }












    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BoxCollider thisCol = GetComponent<BoxCollider>();
            thisCol.enabled = false;

            startOnce = false;
            tutorial = TutorialStateVer2.step02_2;     
        }
    }
}
