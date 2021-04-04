using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HideInInspector]
public enum PlayerUI
{
    invenOn,
    invenOff,

    getWeaponUiOn,
    getWeaponUiOff,

    getPowerUiOn,
    getPowerUiOff,
}
[HideInInspector]
public enum PlayerState
{
    idle,
    normalAttacked,
    airborneAttacked,
    airborneAttackedCoolTime,
    stunAttacked,
    dodge,
    attack,
    waitForMoveNextStage,
    stopForCutSceen,
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
    [SerializeField]
    TypingTextCon typingTextConScript;
    [SerializeField]
    TutorialStageManger tutorialStageMangerScript;
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
    public MousePlace mousePlace;
    [HideInInspector]
    public PlayerState state;
    [HideInInspector]
    public PlayerUI playerUIState;

    private float maxHP = 100;
    private float hP = 100;

    /*
    [SerializeField]
     private float damageBloodAmount = 3;
    [SerializeField]
      private float maxBloodIndication = 0.5f; 

   [SerializeField]
     float recoverSpeed = 1;//HP per second
     */
    [HideInInspector]
    public bool isDodge;  // trapType2FireAttack을 피하기 위한것
    #endregion


    private void Start()
    {
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
    void Update()
    {
        if (state == PlayerState.airborneAttacked)
        {
            rigid.AddForce(Vector3.up * 0.35f, ForceMode.Impulse);
            return;
        }

        if (state == PlayerState.idle || state == PlayerState.dodge ||
           state == PlayerState.attack || state == PlayerState.normalAttacked) lookAtCam();

        inputProcessInven();
        inputProcessE();

        if (playerUIState == PlayerUI.invenOn || playerUIState == PlayerUI.getWeaponUiOn
       || playerUIState == PlayerUI.getPowerUiOn || tutorialStageMangerScript.tutorialState == TutorialState.tutorialReady
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial01
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_1
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_2 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_3
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_4 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_5
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_6
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial03   || tutorialStageMangerScript.tutorialState == TutorialState.tutorial03_1 
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial04_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial05_0
       || tutorialStageMangerScript.tutorialState == TutorialState.tutorial06_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial07_0)
        {
            animationScript.playerAniWait();
            return;
        }

        switch (state)
        {
            case PlayerState.idle:
                inputProcessIdleAndNormalAttacked();
                break;

            case PlayerState.dodge:
                isDodge = true;
                inputProcessDodge();          
                break;

            case PlayerState.attack:
                inputProcessAttack();
                break;
  
            case PlayerState.normalAttacked:
                inputProcessIdleAndNormalAttacked();
                break;

            //=========================================이동 불가 
            case PlayerState.airborneAttacked:
                break;
            case PlayerState.stunAttacked:
                break;
            case PlayerState.waitForMoveNextStage:
                break;
            case PlayerState.stopForCutSceen:
                break;              
        }
    }
    #endregion


    //  상태에 따라 입력 가능이 바뀜  
    #region
    void inputProcessIdleAndNormalAttacked()
    {
        checkRotationAndKeyDownToAniCon();

        if (Input.GetMouseButtonDown(1))
        {
            if (weaponInGameUIScript.isWeaponChangeCoolTime == true) return;
            weaponInGameUIScript.playerWeaponUISelect();
        }

        // 그냥 이동함? 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
            animationScript.playerAniWalk();

            SoundManager.Instance.playerWalkSound();
        }
        // 가만히 있음? 
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) animationScript.playerAniWait();

        // 기력은 충분하니? 
        if (spConScript.isPlayerSpZero == true) return;

        // 공격 함? 
        if (Input.GetMouseButtonDown(0))
        {
            if (attackConScript.isCool == true) return;

            attackConScript.whenAttackCheckWeapon();
        }

        //구르기 위한 조건을 만족 하니? 
        if (dodgeConScript.playerDodgeCoolTime == true || state == PlayerState.stunAttacked 
            || state == PlayerState.airborneAttacked || state == PlayerState.stunAttacked || state == PlayerState.dodge) return;
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
        if (Input.GetMouseButtonDown(0))
        {
            if (attackConScript.isCool == true) return;

            attackConScript.whenAttackCheckWeapon();
        }

        //구르기 위한 조건을 만족 하니? 
        if (dodgeConScript.playerDodgeCoolTime == true || state == PlayerState.stunAttacked
            || state == PlayerState.airborneAttacked || state == PlayerState.stunAttacked || state == PlayerState.dodge) return;
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) { dodgeConScript.dodgeAndGetKeyA(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) { dodgeConScript.dodgeAndGetKeyD(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) { dodgeConScript.dodgeAndGetKeyS(); spConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) { dodgeConScript.dodgeAndGetKeyW(); spConScript.spDown(); }
    }

    void inputProcessDodge()
    {
        //if (state != PlayerState.dodge || state == PlayerState.attack) return;
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
            case "PlayerWeaponDroped":
                if (Input.GetKeyDown(KeyCode.E)) playerUIState = PlayerUI.getWeaponUiOn;           
                break;

            case "PlayerPowerGetSet":
                if (Input.GetKeyDown(KeyCode.E)) playerUIState = PlayerUI.getPowerUiOn;
                break;
        }
        if (playerUIState == PlayerUI.getWeaponUiOn)
        {
            playerUISeletMangerScript.whenGetWeaponConTheUISet();
            return;
        }
        if (playerUIState == PlayerUI.getPowerUiOn)
        {
            playerUISeletMangerScript.whenPlayerTouchPower();
            return;
        }
    }


    //  필수 요소
    void lookAtCam()
    {
        if (playerUIState == PlayerUI.invenOn || playerUIState == PlayerUI.getWeaponUiOn
           || playerUIState == PlayerUI.getPowerUiOn || tutorialStageMangerScript.tutorialState == TutorialState.tutorialReady
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial01
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_1
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_2 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_3
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_4 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_5
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial02_6
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial03 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial03_1
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial04_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial05_0
           || tutorialStageMangerScript.tutorialState == TutorialState.tutorial06_0 || tutorialStageMangerScript.tutorialState == TutorialState.tutorial07_0) return;

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



    /*
    //  공격 받은 경우 
    //======================================================
    public void Damage(int amount)
    {
        BleedBehavior.BloodAmount += Mathf.Clamp01(damageBloodAmount * amount / HP);

        HP -= amount;

        if (HP <= 0) HP = maxHP;
    }
    */
}

