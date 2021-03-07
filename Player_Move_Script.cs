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

public class Player_Move_Script : MonoBehaviour
{
    #region
    [SerializeField]
    playerUISeletManger playerUISeletMangerScript;

   

    


    [SerializeField]
    Camera cam;
    float speed = 5;

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    static Player_Move_Script instance = null;
    [Header("플레이어의 현재 소지하고 있는 물건을 보여주는 ui 변화를 위한 스크립트입니다.")]
    [SerializeField]
    playerWeaponUI playerWeaponUIScript;
    playerDodgeCon playerDodgeConScript;
    playerAttackCon playerAttackConScript;
    playerSpCon playerSpConScript;
    Player_Animation_Script playerAnimationScript;
    RectTransform playerRectTransform;
    Rigidbody playerRigid;

    // 상태 관련 
    [HideInInspector]
    public MousePlace mousePlace;
    [HideInInspector]
    public PlayerState state;
    [HideInInspector]
    public PlayerUI PlayerUI;

    //스턴 관련 
    GameObject stunParticleObj;

    private float maxHP = 100;
    private float HP = 100;

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
    








    void Awake()
    {
        isDodge = false;
        HP = maxHP;

        playerRectTransform = GetComponent<RectTransform>();
        playerRigid = GetComponent<Rigidbody>();

        playerDodgeConScript = GetComponent<playerDodgeCon>();
        playerAnimationScript = GetComponent<Player_Animation_Script>();
        playerAttackConScript = GetComponent<playerAttackCon>();
        playerSpConScript = GetComponent<playerSpCon>();

        cam = FindObjectOfType<Camera>();

        PlayerUI = PlayerUI.invenOff;
        state = PlayerState.idle;
        mousePlace = MousePlace.top;

        stunParticleObj = transform.Find("paticlePos").gameObject;
        stunParticleObj.SetActive(false);

        instance = this;
        if (null == instance) instance = this;
    }



    public static Player_Move_Script Instance
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
        if (state == PlayerState.idle || state == PlayerState.dodge ||
           state == PlayerState.attack || state == PlayerState.normalAttacked) lookAtCam();

        inputProcessInven();
        inputProcessE();
       
        if (PlayerUI == PlayerUI.invenOn || PlayerUI == PlayerUI.getWeaponUiOn) return;

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
                playerPosUp();
                break;

            case PlayerState.stunAttacked:
                break;

            case PlayerState.waitForMoveNextStage:
                break;
            //stopForCutSceen
            case PlayerState.stopForCutSceen:
                break;              
        }
    }
    #endregion


    //  상태에 따라 입력 가능이 바뀜  
    #region
    void inputProcessIdleAndNormalAttacked()
    {
        if (PlayerUI == PlayerUI.invenOn) return;

        checkRotationAndKeyDownToAniCon();

        if (Input.GetMouseButtonDown(1))
        {
            if (playerWeaponUIScript.isWeaponChangeCoolTime == true) return;
            playerWeaponUIScript.playerWeaponUISelect();
        }

        // 그냥 이동함? 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
            playerAnimationScript.playerAniWalk();

            SoundManager.Instance.playerWalkSound();
        }
        // 가만히 있음? 
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) playerAnimationScript.playerAniWait();

        // 기력은 충분하니? 
        if (playerSpConScript.isPlayerSpZero == true) return;

        // 공격 함? 
        if (Input.GetMouseButtonDown(0))
        {
            if (playerAttackConScript.isCool == true) return;

            playerAttackConScript.whenAttackCheckWeapon();
        }
        //구르기 위한 조건을 만족 하니? 
        if (playerDodgeConScript.playerDodgeCoolTime == true || state == PlayerState.stunAttacked 
            || state == PlayerState.airborneAttacked || state == PlayerState.stunAttacked) return;
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) { playerDodgeConScript.dodgeAndGetKeyA(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) { playerDodgeConScript.dodgeAndGetKeyD(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) { playerDodgeConScript.dodgeAndGetKeyS(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) { playerDodgeConScript.dodgeAndGetKeyW(); playerSpConScript.spDown(); }
    }
    void inputProcessAttack()
    {
        if (PlayerUI == PlayerUI.invenOn) return;

        // 기력은 충분하니? 
        if (playerSpConScript.isPlayerSpZero == true) return;

        // 공격 함? 
        if (Input.GetMouseButtonDown(0))
        {
            if (playerAttackConScript.isCool == true) return;

            playerAttackConScript.whenAttackCheckWeapon();
        }
        //구르기 위한 조건을 만족 하니? 
        if (playerDodgeConScript.playerDodgeCoolTime == true || state == PlayerState.stunAttacked
            || state == PlayerState.airborneAttacked || state == PlayerState.stunAttacked) return;
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) { playerDodgeConScript.dodgeAndGetKeyA(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) { playerDodgeConScript.dodgeAndGetKeyD(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) { playerDodgeConScript.dodgeAndGetKeyS(); playerSpConScript.spDown(); }
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) { playerDodgeConScript.dodgeAndGetKeyW(); playerSpConScript.spDown(); }
    }

    void inputProcessDodge()
    {
        transform.position += new Vector3(hAxis, 0, vAxis) * 9 * Time.deltaTime;  
    }

    void inputProcessInven()
    {
        if (Input.GetKeyDown(KeyCode.I)) PlayerUI = PlayerUI.invenOn;

        if (PlayerUI != PlayerUI.invenOn) return;
        playerUISeletMangerScript.playerInputI();
    }
    void inputProcessE()
    {
       // if (playerUISeletMangerScript.imageE.activeInHierarchy == false) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerUISeletMangerScript.imageE.SetActive(false);
            PlayerUI = PlayerUI.getWeaponUiOn;           
        }

        if (PlayerUI != PlayerUI.getWeaponUiOn) return;
        playerUISeletMangerScript.playerGetWeaponUiOn();
    }


    //  필수 요소
    void lookAtCam()
    {
        if (PlayerUI == PlayerUI.invenOn || PlayerUI == PlayerUI.getWeaponUiOn) return;

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






    // 따로 할 이유없음.
    // 에어본인 경우 
    void playerPosUp()
    {
        playerRigid.AddForce(Vector3.up * 6.6f, ForceMode.Impulse);
        state = PlayerState.airborneAttackedCoolTime;
    }
}

