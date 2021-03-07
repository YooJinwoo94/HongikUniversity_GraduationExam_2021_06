using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerUISeletManger : MonoBehaviour
{
    [SerializeField]
    TimeManager timerManagerScript;


    [Header("블러 + 어두운 화면입니다, ui창 뒤에 나옴.")]
    [SerializeField]
    GameObject playerUIBackGround;


    [SerializeField]
    Player_Move_Script Player_Move_Script;


    // i를 누르면 나오는 인벤토리 ui용
    [SerializeField]
    GameObject[] playerUiSetInputI;
    int invenPageCount;


    // 무기를 얻었을떄 뜨는 ui용
    [SerializeField]
    Image playerGetWeaponImage;
    [SerializeField]
    playerGetWeaponUINNo5 playerGetWeaponUINNo5;
    [SerializeField]
    GameObject[] playerUiSetGetWeapon;
    [SerializeField]
    Button[] playerUiSetGetWeaponLeftRightButton;
    [SerializeField]
    Animator[] playerUiSetGetWeaponAnimator;
    [SerializeField]
    public GameObject imageE;


    // 내가 가진 무기 Ui용
    [SerializeField]
    Image[] playerHaveWeapon;
    [SerializeField]
    Sprite[] weaponImage;


    private void Awake()
    {
        invenPageCount = 0;
    }



    //I를 눌렀을때 이미지 컨트롤
    public void playerInputI(int buttonCleck = 0)
    {
        if (playerUIBackGround.activeInHierarchy == false)
        {
            playerUIBackGround.SetActive(true);
            playerUiSetInputI[0].SetActive(true);
            timerManagerScript.playerUITimeOn();
        }

        if (Input.GetKeyDown(KeyCode.A) || buttonCleck == -1)
        {
            invenPageCount -= 1;

            if (invenPageCount == -2) invenPageCount = 1;
            invenPageOnOff();
        }
        else if (Input.GetKeyDown(KeyCode.D) || buttonCleck == 1)
        {
            invenPageCount += 1;

            if (invenPageCount == 2) invenPageCount = -1;

            invenPageOnOff();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            timerManagerScript.playerUITimeOff();
            Player_Move_Script.PlayerUI = PlayerUI.invenOff;
            playerUIBackGround.SetActive(false);

            playerUiSetInputI[0].SetActive(false);
            playerUiSetInputI[1].SetActive(false);
            playerUiSetInputI[2].SetActive(false);
        }
    }
    void invenPageOnOff()
    {
        switch (invenPageCount)
        {
            case -1:
                playerUiSetInputI[0].SetActive(false);
                playerUiSetInputI[1].SetActive(true);
                playerUiSetInputI[2].SetActive(false);
                break;

            case 0:
                playerUiSetInputI[0].SetActive(true);
                playerUiSetInputI[1].SetActive(false);
                playerUiSetInputI[2].SetActive(false);
                break;

            case 1:
                playerUiSetInputI[0].SetActive(false);
                playerUiSetInputI[1].SetActive(false);
                playerUiSetInputI[2].SetActive(true);
                break;
        }
    }


    //무기에 다가갈경우 E이미지 컨트롤
    public void turnOnImageE()
    {
        if (imageE.activeInHierarchy == false)
        {
            imageE.SetActive(true);

            playerUiSetGetWeaponLeftRightButton[0].interactable = true;
            playerUiSetGetWeaponLeftRightButton[1].interactable = true;

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
    }
    public void turnOffImageE()
    {
        if (imageE.activeInHierarchy == true) imageE.SetActive(false);
    }
    //무기를 얻었을때 이미지 컨트롤
    public void playerGetWeaponUiOn(int buttonCleck = 0)
    {
        if (Player_Move_Script.PlayerUI == PlayerUI.getWeaponUiOn)
        {
            timerManagerScript.playerUITimeOn();
            playerUIBackGround.SetActive(true);
            playerUiSetGetWeapon[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timerManagerScript.playerUITimeOff();
            Player_Move_Script.PlayerUI = PlayerUI.getWeaponUiOff;
            playerUIBackGround.SetActive(false);
            playerUiSetGetWeapon[0].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.A)  || buttonCleck == -1)
        {
            playerUiSetGetWeaponLeftRightButton[0].interactable = false;
            playerUiSetGetWeaponLeftRightButton[1].interactable = true;

            playerUiSetGetWeaponAnimator[0].SetBool("LeftMove_0", true);
            playerUiSetGetWeaponAnimator[1].SetBool("LeftMove_0", true);
            playerUiSetGetWeaponAnimator[2].SetBool("LeftMove_0", true);

            playerUiSetGetWeaponAnimator[0].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("RightMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("RightMove_0", false);
        }
        else if (Input.GetKeyDown(KeyCode.D) ||  buttonCleck == 1)
        {
            playerUiSetGetWeaponLeftRightButton[0].interactable = true;
            playerUiSetGetWeaponLeftRightButton[1].interactable = false;

            playerUiSetGetWeaponAnimator[0].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[1].SetBool("LeftMove_0", false);
            playerUiSetGetWeaponAnimator[2].SetBool("LeftMove_0", false);

            playerUiSetGetWeaponAnimator[0].SetBool("RightMove_0", true);
            playerUiSetGetWeaponAnimator[1].SetBool("RightMove_0", true);
            playerUiSetGetWeaponAnimator[2].SetBool("RightMove_0", true);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerUiSetGetWeaponLeftRightButton[0].interactable == true && playerUiSetGetWeaponLeftRightButton[1].interactable == true) return;

            // 현재 위치는 왼쪽에 있음 
            if (playerUiSetGetWeaponLeftRightButton[0].interactable == false)
            {
                playerGetWeaponUINNo5.getWeaponAndChangeIT(1, playerGetWeaponImage.sprite.name);

                // 플레이어의 무기이미지 교체
                switch (playerGetWeaponImage.sprite.name)
                {
                    case "weapon1":
                        playerHaveWeapon[0].sprite = weaponImage[0];
                        break;
                    case "weapon2":
                        playerHaveWeapon[0].sprite = weaponImage[1];
                        break;
                    case "weapon3":
                        playerHaveWeapon[0].sprite = weaponImage[2];
                        break;
                }
            }
            // 현재 위치는 오른쪽에 있음 
            else if (playerUiSetGetWeaponLeftRightButton[1].interactable == false)
            {              
                playerGetWeaponUINNo5.getWeaponAndChangeIT(2, playerGetWeaponImage.sprite.name);

                // 플레이어의 무기이미지 교체
                switch (playerGetWeaponImage.sprite.name)
                {
                    case "weapon1":
                        playerHaveWeapon[1].sprite = weaponImage[0];
                        break;
                    case "weapon2":
                        playerHaveWeapon[1].sprite = weaponImage[1];
                        break;
                    case "weapon3":
                        playerHaveWeapon[1].sprite = weaponImage[2];
                        break;
                }                         
            }
           
            timerManagerScript.playerUITimeOff();
            Player_Move_Script.PlayerUI = PlayerUI.getWeaponUiOff;
            playerUIBackGround.SetActive(false);
            playerUiSetGetWeapon[0].SetActive(false);
        }

    }
}
