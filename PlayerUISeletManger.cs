using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUISeletManger : MonoBehaviour
{
    [SerializeField]
    GameObject playerIngameUiSet;
    [SerializeField]
    PlayerAniScript playerAniScript;

    [SerializeField]
    TimeManager timeManagerScript;


    [Header("블러 + 어두운 화면입니다, ui창 뒤에 나옴.")]
    [SerializeField]
    GameObject playerUIBackGround;


    [SerializeField]
    PlayerInputScript playerInputScript;


    // i를 누르면 나오는 인벤토리 ui용
    [SerializeField]
    GameObject[] playerUiSetWhenInputI;
    int numOfInvenPage;


    // 무기를 얻었을떄 뜨는 ui용
    [SerializeField]
    TutorialStageManger tutorialStageMangerScript;
    [SerializeField]
    PlayerWeaponInGameUI playerWeaponInGameUiScript;
    [SerializeField]
    PlayerPowerGetUINo2 playerPowerGetUiNo2Script;
    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUINo4;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;
    [SerializeField]
    PlayerShopUINo7 playerShopUINo7Script;

    [SerializeField]
    PlayerPowerDataBase playerPowerDataBaseScript;

    [SerializeField]
    public Button[] leftRightButtonToSwitchTheBoxWhenGetWeapon;
    [SerializeField]
    Animator[] playerUiSetGetWeaponAnimator;


    int playerChoose = 99;






    private void Start()
    {
        numOfInvenPage = 0;
    }










    public void turnOnOffIngameUi()
    { 
        if (playerIngameUiSet.activeInHierarchy == true) 
        {
            playerIngameUiSet.transform.localScale = new Vector3(0f, 0f, 0f);
            StartCoroutine("ifTurnOnIngameUiStayPlz");
        }
    }
    IEnumerator ifTurnOnIngameUiStayPlz()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(6.5f);

        yield return waitForSeconds;

        playerIngameUiSet.transform.localScale = new Vector3(1f, 1f, 1f);
        StopCoroutine("ifTurnOnIngameUiStayPlz");
    }







    //I를 눌렀을때 이미지 컨트롤
    public void playerInputI(int buttonCleck = 0)
    {
        if (playerUIBackGround.activeInHierarchy == false)
        {
            playerUIBackGround.SetActive(true);
            playerUiSetWhenInputI[0].SetActive(true);
            timeManagerScript.playerUITimeOn();
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || buttonCleck == -1)
        {
            numOfInvenPage -= 1;

            if (numOfInvenPage == -2) numOfInvenPage = 1;
            invenPageOnOff();
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) || buttonCleck == 1)
        {
            numOfInvenPage += 1;

            if (numOfInvenPage == 2) numOfInvenPage = -1;

            invenPageOnOff();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            numOfInvenPage = 0;

            timeManagerScript.playerUITimeOff();
            playerInputScript.playerUIState = PlayerUI.invenOff;
            playerUIBackGround.SetActive(false);

            playerUiSetWhenInputI[0].SetActive(false);
            playerUiSetWhenInputI[1].SetActive(false);
            playerUiSetWhenInputI[2].SetActive(false);
            return;
        }
    }
    void invenPageOnOff()
    {
        switch (numOfInvenPage)
        {
            case -1:
                playerUiSetWhenInputI[0].SetActive(false);
                playerUiSetWhenInputI[1].SetActive(true);
                playerUiSetWhenInputI[2].SetActive(false);
                break;

            case 0:
                playerUiSetWhenInputI[0].SetActive(true);
                playerUiSetWhenInputI[1].SetActive(false);
                playerUiSetWhenInputI[2].SetActive(false);
                break;

            case 1:
                playerUiSetWhenInputI[0].SetActive(false);
                playerUiSetWhenInputI[1].SetActive(false);
                playerUiSetWhenInputI[2].SetActive(true);
                break;
        }
    }








    //무기에 다가갈경우 혹은 강화내용에 다가갈 경우 E이미지 컨트롤
    public void turnOnOffImageE()
    {
        if (playerGetWeaponUINo5Script.imageWhenPlayerTouchTheWeapon.activeInHierarchy == false)
        {
            playerGetWeaponUINo5Script.imageWhenPlayerTouchTheWeapon.SetActive(true);

            leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable = true;
            leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable = true;

            playerUiSetGetWeaponAnimator[0].SetBool("Reset", true);
            playerUiSetGetWeaponAnimator[1].SetBool("Reset", true);
            playerUiSetGetWeaponAnimator[2].SetBool("Reset", true);

            playerUiSetGetWeaponAnimator[0].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[0].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("RightMove_0", false);
        }

        else playerGetWeaponUINo5Script.imageWhenPlayerTouchTheWeapon.SetActive(false);
    }
    //무기를 얻었을때 이미지 컨트롤
    public void whenGetWeaponConTheUISet(int buttonClick = 0)
    {
        timeManagerScript.playerUITimeOn();
        playerUIBackGround.SetActive(true);
        playerGetWeaponUINo5Script.bgUiNo5Obj.SetActive(true);
        playerGetWeaponUINo5Script.settingNameAndSprite();
        playerGetWeaponUINo5Script.whatIsThisWeapon();

        if (tutorialStageMangerScript.tutorialState == TutorialState.tutorial04_0) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timeManagerScript.playerUITimeOff();
            playerInputScript.playerUIState = PlayerUI.getWeaponUiOff;
            playerUIBackGround.SetActive(false);
            playerGetWeaponUINo5Script.bgUiNo5Obj.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)  || buttonClick == -1)
        {
            leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable = false;
            leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable = true;

            playerUiSetGetWeaponAnimator[0].SetBool("LeftMove_0", true);
            playerUiSetGetWeaponAnimator[1].SetBool("LeftMove_0", true);
            playerUiSetGetWeaponAnimator[2].SetBool("LeftMove_0", true);

            playerUiSetGetWeaponAnimator[0].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("RightMove_0", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) ||  buttonClick == 1)
        {
            leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable = true;
            leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable = false;

            playerUiSetGetWeaponAnimator[0].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("LeftMove_0", false);

            playerUiSetGetWeaponAnimator[0].SetBool("RightMove_0", true);
            playerUiSetGetWeaponAnimator[1].SetBool("RightMove_0", true);
            playerUiSetGetWeaponAnimator[2].SetBool("RightMove_0", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {    
            if (leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable == true && leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable == true) return;




            if (leftRightButtonToSwitchTheBoxWhenGetWeapon[0].interactable == false) playerChoose = 0;
            if (leftRightButtonToSwitchTheBoxWhenGetWeapon[1].interactable == false)
            {
                playerChoose = 1;
                playerGetWeaponUINo5Script.playersWeaponImage[playerChoose].enabled = true;
            }

            // 플레이어의 무기이미지 및 텍스트 교체
            switch (playerGetWeaponUINo5Script.dropedWeaponName.text)
            {
                case "저주받은 진소의 검":
                    playerHaveWeaponUINo4.changePlayersWeapon(playerChoose, 0);
                    playerWeaponInGameUiScript.ifPlayerGetWeaponChangeIngameWeaponImage(0);
                    break;
                case "삼천호검":
                    playerHaveWeaponUINo4.changePlayersWeapon(playerChoose, 1);
                    playerWeaponInGameUiScript.ifPlayerGetWeaponChangeIngameWeaponImage(1);
                    break;
                case "최후의 섬광":
                    playerHaveWeaponUINo4.changePlayersWeapon(playerChoose, 2);
                    playerWeaponInGameUiScript.ifPlayerGetWeaponChangeIngameWeaponImage(2);
                    break;
            }
           
            timeManagerScript.playerUITimeOff();
            playerInputScript.playerUIState = PlayerUI.getWeaponUiOff;
            playerUIBackGround.SetActive(false);
            playerGetWeaponUINo5Script.bgUiNo5Obj.SetActive(false);

            return;
        }

        playerAniScript.playerAniWait();
    }







    //강화내용을 눌렀을때 
    public void whenPlayerTouchPower()
    {
        playerPowerDataBaseScript.changeTextToMakeSameWithInven();
      
        playerUIBackGround.SetActive(true);
        playerPowerGetUiNo2Script.bg.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
            playerInputScript.playerUIState = PlayerUI.getPowerUiOff;
            playerUIBackGround.SetActive(false);
            playerPowerGetUiNo2Script.bg.SetActive(false);
            return;
        }

        playerAniScript.playerAniWait();
    }
    public void resetPlayerPowerGetUiSet()
    {
        timeManagerScript.playerUITimeOff();
        playerInputScript.playerUIState = PlayerUI.getPowerUiOff;
        playerUIBackGround.SetActive(false);
        playerPowerGetUiNo2Script.bg.SetActive(false);
    }




    public void whenPlayerTouchShop()
    {
        playerPowerDataBaseScript.changeTextToMakeSameWithInven();
        
        playerUIBackGround.SetActive(true);
        playerShopUINo7Script.bg.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject cam = GameObject.Find("Dwarf_Set").transform.Find("CM vcam1").gameObject;
            cam.SetActive(false);

            playerInputScript.playerUIState = PlayerUI.getShopUiOff;
            playerUIBackGround.SetActive(false);
            playerShopUINo7Script.bg.SetActive(false);
            return;
        }

        playerAniScript.playerAniWait();
    }
}
