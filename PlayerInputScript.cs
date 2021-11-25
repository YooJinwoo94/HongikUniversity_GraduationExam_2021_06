using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[HideInInspector]
public enum PlayerUI
{
    invenOn,
    invenOff,

    getShopUiOn,
    getShopUiOff,

    getWeaponUiOn,
    getWeaponUiOff,

    getPowerUiOn,
    getPowerUiOff,

    getSaveUiOn,
    getSaveUiOff,
}
[HideInInspector]
public enum PlayerState
{
    idle,
    parring,
    dodge,
    attack,
    waitForMoveNextStage,
    stopForCutSceen,
}

public enum PlayerHitted
{
    none,
    normalAttacked,
    airborneAttacked,
    airborneAttackedCoolTime,
    stunAttacked,
}

[HideInInspector]
public enum MousePlace
{
    top,
    bot,
    right,
    left
}








public class PlayerInputScript : MonoBehaviour
{
    #region
    TutorialManagerVer2 tutorialManagerVer2Script;
    [SerializeField]
    PlayerParringCon playerParringConScript;
    [SerializeField]
    TypingTextCon typingTextConScript;
    [SerializeField]
    DialogueManager dialogueManagerScript;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINo5Script;
    [SerializeField]
    PlayerUISeletManger playerUISeletMangerScript;
    [SerializeField]
    Camera cam;
    float speed = 5;

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    static PlayerInputScript instance = null;
    [Header("플레이어의 현재 소지하고 있는 물건을 보여주는 ui 변화를 위한 스크립트입니다.")]

    [SerializeField]
    PlayerWeaponInGameUI weaponInGameUIScript;
    PlayerColliderCon playerColliderConScript;
    PlayerDodgeCon dodgeConScript;
    PlayerAttackCon attackConScript;
    PlayerSpCon spConScript;
    PlayerAniScript animationScript;
    RectTransform rectTransform;
    Rigidbody rigid;

    // 상태 관련 
    [HideInInspector]
    public PlayerHitted playerHitted;
    [HideInInspector]
    public MousePlace mousePlace;
    [HideInInspector]
    public PlayerState state;
    [HideInInspector]
    public PlayerUI playerUIState;

    private float maxHP = 100;
    private float hP = 100;


    [HideInInspector]
    public bool isDodge;  // trapType2FireAttack을 피하기 위한것
    #endregion


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_Ver2")
        {
            tutorialManagerVer2Script = GameObject.Find("TutorialManagerVer2").GetComponent<TutorialManagerVer2>();
        }

        playerColliderConScript = GetComponent<PlayerColliderCon>();
        rectTransform = GetComponent<RectTransform>();
        rigid = GetComponent<Rigidbody>();
        dodgeConScript = GetComponent<PlayerDodgeCon>();
        animationScript = GetComponent<PlayerAniScript>();
        attackConScript = GetComponent<PlayerAttackCon>();
        spConScript = GetComponent<PlayerSpCon>();
        cam = FindObjectOfType<Camera>();

        isDodge = false;
        hP = maxHP;

        playerHitted = PlayerHitted.none;
        playerUIState = PlayerUI.invenOff;
        state = PlayerState.idle;
        mousePlace = MousePlace.top;

        instance = this;
        if (null == instance) instance = this;
    }



    public static PlayerInputScript Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }

    // update
    #region

    void FixedUpdate()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        //현재 에어본 상태는 패기되었습니다.
        if (playerHitted == PlayerHitted.airborneAttacked)
        {
            rigid.AddForce(Vector3.up * 0.35f, ForceMode.Impulse);
            return;
        }

        
        if ((state == PlayerState.idle || state == PlayerState.dodge ||
           state == PlayerState.attack)
           && playerHitted == PlayerHitted.none
           ) lookAtCam();




        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_Ver2")
        {
            switch (tutorialManagerVer2Script.makePlayerWait)
            {
                case MakePlayerWait.wait:
                    animationScript.playerAniWait();
                    inputProcessInven();
                    inputProcessE();
                    return;
            }
        }


        if (playerUIState == PlayerUI.invenOn || playerUIState == PlayerUI.getWeaponUiOn || playerUIState == PlayerUI.getShopUiOn
         || playerUIState == PlayerUI.getPowerUiOn || playerUIState == PlayerUI.getSaveUiOn)
        {
            animationScript.playerAniWait();
            inputProcessInven();
            inputProcessE();
            return;
        }
        else if (dialogueManagerScript.dialogueState == DialogueState.DialogueStart)
        {
            animationScript.playerAniWait();
            return;
        }


        switch (playerHitted)
        {
            //   case PlayerHitted.normalAttacked:
            //       return;
            case PlayerHitted.airborneAttacked:
                inputProcessInven();
                inputProcessE();
                return;
            case PlayerHitted.stunAttacked:
                inputProcessInven();
                inputProcessE();
                return;
        }

        switch (state)
        {
            case PlayerState.idle:
                inputProcessWhenPlayerIsIdle();

                inputProcessInven();
                inputProcessE();
                break;

            case PlayerState.dodge:
                isDodge = true;
                inputProcessDodge();

                inputProcessInven();
                inputProcessE();
                break;

            case PlayerState.attack:
                inputProcessAttack();

                inputProcessInven();
                inputProcessE();
                break;

            case PlayerState.parring:
                inputProcessInven();
                inputProcessE();
                break;

            //=========================================이동 불가 

            case PlayerState.waitForMoveNextStage:
                break;
            case PlayerState.stopForCutSceen:
                break;
        }
    }
    #endregion


    //  상태에 따라 입력 가능이 바뀜  
    #region
    void inputProcessWhenPlayerIsIdle()
    {
        checkRotationAndKeyDownToAniCon();

        // 오른쪽 마우스 클릭시 무기 바꿈
        if (Input.GetMouseButtonDown(1))
        {
            if (weaponInGameUIScript.isWeaponChangeCoolTime == true) return;
            weaponInGameUIScript.playerWeaponUISelect();
        }


        // 가만히 있음? 
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) animationScript.playerAniWait();
        else
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
        }
        // 그냥 이동함? 
        if (Input.GetKey(KeyCode.A))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyA();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyD();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyS();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyW();
        }

        if ((Input.GetKey(KeyCode.W))
                && (Input.GetKey(KeyCode.A)))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyWA();
        }
        else if ((Input.GetKey(KeyCode.W))
                && (Input.GetKey(KeyCode.D)))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeyWD();
        }
        else if ((Input.GetKey(KeyCode.S))
                && (Input.GetKey(KeyCode.D)))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeySD();
        }
        else if ((Input.GetKey(KeyCode.S))
               && (Input.GetKey(KeyCode.A)))
        {
            SoundManager.Instance.playerWalkSound();
            walkAndGetKeySA();
        }



        // 기력은 충분하니? 
        if (spConScript.isPlayerSpZero == true) return;


        // 공격 함? 
        switch (Input.GetMouseButtonDown(0))
        {
            case true:
                if (attackConScript.isCool == true) return;

                attackConScript.whenAttackCheckWeapon();
                break;
        }

        // 패링함?
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (playerParringConScript.isCool == true) return;
            playerParringConScript.parringStart();
        }

        //구르기 위한 조건을 만족 하니? 
        if (dodgeConScript.playerDodgeCoolTime == true
            || playerHitted == PlayerHitted.airborneAttacked || playerHitted == PlayerHitted.stunAttacked || state == PlayerState.dodge) return;
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) { dodgeConScript.dodgeAndGetKeyA(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) { dodgeConScript.dodgeAndGetKeyD(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) { dodgeConScript.dodgeAndGetKeyS(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) { dodgeConScript.dodgeAndGetKeyW(); spConScript.spDown(); }
    }
    void inputProcessAttack()
    {
        // 기력은 충분하니?  
        if (spConScript.isPlayerSpZero == true || state == PlayerState.dodge) return;

        // 공격 함? 
        switch (Input.GetMouseButtonDown(0))
        {
            case true:
                if (attackConScript.isCool == true) return;

                attackConScript.whenAttackCheckWeapon();
                break;
        }

        //구르기 위한 조건을 만족 하니? 
        if (dodgeConScript.playerDodgeCoolTime == true
            || playerHitted == PlayerHitted.airborneAttacked || playerHitted == PlayerHitted.stunAttacked || state == PlayerState.dodge) return;
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) { dodgeConScript.dodgeAndGetKeyA(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) { dodgeConScript.dodgeAndGetKeyD(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) { dodgeConScript.dodgeAndGetKeyS(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) { dodgeConScript.dodgeAndGetKeyW(); spConScript.spDown(); }
    }

    void inputProcessDodge()
    {
        transform.position += new Vector3(hAxis, 0, vAxis) * 9 * Time.deltaTime;  
    }

    void inputProcessInven()
    {
        if (Input.GetKeyDown(KeyCode.I)) playerUIState = PlayerUI.invenOn;

        if (playerUIState != PlayerUI.invenOn) return;
        playerUISeletMangerScript.playerInputI();
    }
    void inputProcessE()
    {
        if (playerGetWeaponUINo5Script.imageWhenPlayerTouchTheWeapon.activeInHierarchy == false) return;


        switch (playerColliderConScript.checkWhatItis.tag)
        {
            case "Dwarf_ShopOwner":
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    GameObject cam = GameObject.Find("Dwarf_Set").transform.Find("CM vcam1").gameObject;
                    cam.SetActive(true);
                    playerUIState = PlayerUI.getShopUiOn;
                    playerUISeletMangerScript.whenPlayerTouchShop();
                }
                break;

            case "PlayerWeaponDroped":
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerUIState = PlayerUI.getWeaponUiOn;
                    playerUISeletMangerScript.whenGetWeaponConTheUISet();
                }
                break;

            case "PlayerPowerGetSet":
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerUIState = PlayerUI.getPowerUiOn;
                    playerUISeletMangerScript.whenPlayerTouchPower();
                }
                break;

            case  "Save_State" :
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerUIState = PlayerUI.getSaveUiOn;
                    playerUISeletMangerScript.whenPlayerTouchSave();
                }
                break;

        }
        if (playerUIState == PlayerUI.getPowerUiOn) playerUISeletMangerScript.whenPlayerTouchPower();
        if (playerUIState == PlayerUI.getWeaponUiOn) playerUISeletMangerScript.whenGetWeaponConTheUISet();
        if (playerUIState == PlayerUI.getShopUiOn) playerUISeletMangerScript.whenPlayerTouchShop();
        if (playerUIState == PlayerUI.getSaveUiOn) playerUISeletMangerScript.whenPlayerTouchSave();
    }


    //  필수 요소
    void lookAtCam()
    {

        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_Ver2")
        {
            switch(tutorialManagerVer2Script.makePlayerWait)
            {
                case MakePlayerWait.wait:
                    return;
            }
        }


        if (playerUIState == PlayerUI.invenOn || playerUIState == PlayerUI.getWeaponUiOn || playerUIState == PlayerUI.getShopUiOn
           || playerUIState == PlayerUI.getPowerUiOn || playerUIState == PlayerUI.getSaveUiOn
           || dialogueManagerScript.dialogueState == DialogueState.DialogueStart) return;

        Ray rayCam = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlae = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlae.Raycast(rayCam, out rayLength))
        {
            Vector3 pointToLook = rayCam.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }
    void checkRotationAndKeyDownToAniCon()

    {
        if (transform.rotation.y >= -0.4f && transform.rotation.y <= 0.4f) mousePlace = MousePlace.top;
        else if (transform.rotation.y >= 0.4f && transform.rotation.y <= 0.8f) mousePlace = MousePlace.right;
        else if ( (transform.rotation.y >= 0.8f && transform.rotation.y <= 1f) || (transform.rotation.y < -0.8f && transform.rotation.y < -1f) ) mousePlace = MousePlace.bot;
        else if (transform.rotation.y <= 0f && transform.rotation.y >= -0.8f) mousePlace = MousePlace.left;
    }
    #endregion










    //
    //=====================
    public void walkAndGetKeyW()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkFront();
                break;
            case MousePlace.bot:
                animationScript.playerAniWalkBack();
                break;
            case MousePlace.right:
                animationScript.playerAniWalkLeft();
                break;
            case MousePlace.left:
                animationScript.playerAniWalkRight();
                break;
        }
    }
    public void walkAndGetKeyS()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkBack();
                break;
            case MousePlace.bot:
                animationScript.playerAniWalkFront();
                break;
            case MousePlace.right:
                animationScript.playerAniWalkRight();
                break;
            case MousePlace.left:
                animationScript.playerAniWalkLeft();
                break;
        }
    }
    public void walkAndGetKeyD()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkRight();
                break;
            case MousePlace.bot:
                animationScript.playerAniWalkLeft();
                break;
            case MousePlace.right:
                animationScript.playerAniWalkFront();
                break;
            case MousePlace.left:
                animationScript.playerAniWalkBack();
                break;
        }
    }
    public void walkAndGetKeyA()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkLeft();
                break;
            case MousePlace.bot:
                animationScript.playerAniWalkRight();
                break;
            case MousePlace.right:
                animationScript.playerAniWalkBack();
                break;
            case MousePlace.left:
                animationScript.playerAniWalkFront();
                break;
        }
    }

    public void walkAndGetKeyWA()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkFrontLeft();
                break;
        }
    }
    public void walkAndGetKeyWD()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                animationScript.playerAniWalkFrontRight();
                break;
        }
    }

    public void walkAndGetKeySA()
    {
        switch (mousePlace)
        {
            case MousePlace.bot:
                animationScript.playerAniWalkFrontRight();
                break;
        }
    }
    public void walkAndGetKeySD()
    {
        switch (mousePlace)
        {
            case MousePlace.bot:
                animationScript.playerAniWalkFrontLeft();
                break;
        }
    }
}

