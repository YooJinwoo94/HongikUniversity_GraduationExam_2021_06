using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// _ -> 소문자 사용할떄에만   
// 카멜컨팬션 / 파스칼 (대소문자 사용이랑 _사용을 혼용 x )
// 카멜 : 소문자 시작  멤버변수나 함수들 사용시  
// 파스칼 : 대문자 시작 ( 특수한 경우 ex 클래스 이름 스태틱변수나 함수일경우 ) 


    // 플레이어 죽음
    // 기본적인 ui
    // 보스 죽음


    // 전체적인 게임느낌이 중요함.
    // 잡몹들 넣기                  

enum PlayerState
{
  idle,
  normalAttacked,
    airborneAttacked,
    airborneAttackedCoolTime,
    stunAttacked,
    dodge,
    attack,
    waitForMove
}
enum MousePlace
{
    top,
    bot,
    right,
    left
}

public class Player_Move_Script : MonoBehaviour
{
    float speed = 5;
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Animator playerAnimator;
    Player_Animation_Script playerAnimationScript;
    RectTransform playerRectTransform;
    Rigidbody playerRigid;

    PlayerState state;
    [SerializeField]
    MousePlace mousePlace;

    // 연속 공격
    int noOfClicks = 0;
    float lastClickedTime = 0;
    [SerializeField]
    float maxComboDelay = 2f;
    bool attackCoolTime = false;


    [SerializeField]
    Camera cam;
    Vector3 mousePos;
    Vector3 moveInput;


    bool playerDodgeCoolTime = false;


    [SerializeField]
    BoxCollider playerSwordBoxCollider;



    //======================================================
    //  const float resetStateToidleTime = 1f;
    const float dodgeOutTime = 0.7f;
    const float dodgeCoolTime = 1.5f;

    GameObject stunParticleObj;





    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRectTransform = GetComponent<RectTransform>();
        playerRigid = GetComponent<Rigidbody>();
        playerAnimationScript = GetComponent<Player_Animation_Script>();
        cam = FindObjectOfType<Camera>();

        state = PlayerState.idle;
        mousePlace = MousePlace.top;

        stunParticleObj = transform.Find("paticlePos").gameObject;
        stunParticleObj.SetActive(false);

        playerSwordBoxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //이건 항상 
        attackCoolTimeCount();

        switch (state)
        {
            // 인터넷으로 동작하는 방식을 보기 

            case PlayerState.idle:     
                inputProcessIdleAndNormalAttacked();
                lookAtCam();
                break;


            // 구르기 회피를 위해
            case PlayerState.dodge:
                lookAtCam();
                inputProcessDodge();          
                break;

            case PlayerState.attack:
                inputProcessAttack();
                lookAtCam();
                break;

        
            case PlayerState.normalAttacked:
                inputProcessIdleAndNormalAttacked();
                lookAtCam();
                break;

            //=========================================이동 불가 
            case PlayerState.airborneAttacked:
                // 공중에 떠야함 
                // 조정 불가 
                playerPosUp();
                break;

            case PlayerState.stunAttacked:
                break;

            case PlayerState.waitForMove:
                break;
        }
    }


    //  상태에 따라 입력 가능이 바뀜  
    //======================================================
    void inputProcessIdleAndNormalAttacked()
    {
        checkRotationAndKeyDownToAniCon();

        if (Input.GetMouseButtonDown(0)) attack();

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
            isPlayerAniWalk();
        }
        else isPlayerAniIdel();

        if (playerDodgeCoolTime != false) return;

        if(Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) dodgeAndGetKeyA();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) dodgeAndGetKeyD();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) dodgeAndGetKeyS();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) dodgeAndGetKeyW();
    }
    void inputProcessDodge()
    {
        transform.position += new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;  
    }
    void inputProcessAttack()
    {
        checkRotationAndKeyDownToAniCon();

        if (Input.GetMouseButtonDown(0)) attack();

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) dodgeAndGetKeyA();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)) dodgeAndGetKeyD();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S)) dodgeAndGetKeyS();
        else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)) dodgeAndGetKeyW();

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ||
                       Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }           
    }


    //  필수 요소
    //======================================================
    void lookAtCam()
    {
        Ray rayCam = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlae = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlae.Raycast(rayCam, out rayLength))
        {
            Vector3 pointToLook = rayCam.GetPoint(rayLength);
            // Debug.DrawLine(rayCam.origin, pointToLook, Color.blue);
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




    //  구르는경우 
    //======================================================
    void dodgeWayCon(int wayPoint)
    {
        if (state == PlayerState.dodge) return;

        state = PlayerState.dodge;
        playerDodgeCoolTime = true;
        speed = 8f;

        Invoke("dodgeOut", dodgeOutTime);
        StartCoroutine("resetDodgeCoolTime");
        switch (wayPoint)
        {
            case 1:
                playerAnimationScript.playerAniRollFront();
                break;

            case 2:
                playerAnimationScript.playerAniRollLeft();
                break;

            case 3:
                playerAnimationScript.playerAniRollRight();
                break;

            case 4:
                playerAnimationScript.playerAniRollBack();
                break;
        }       
    }
    void dodgeAndGetKeyW()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                dodgeWayCon(1);
                break;
            case MousePlace.bot:
                dodgeWayCon(4);
                break;
            case MousePlace.right:
                dodgeWayCon(2);
                break;
            case MousePlace.left:
                dodgeWayCon(3);
                break;
        }
    }
    void dodgeAndGetKeyS()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                dodgeWayCon(4);
                break;
            case MousePlace.bot:
                dodgeWayCon(1);
                break;
            case MousePlace.right:
                dodgeWayCon(3);
                break;
            case MousePlace.left:
                dodgeWayCon(2);
                break;
        }
    }
    void dodgeAndGetKeyD()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                dodgeWayCon(3);
                break;
            case MousePlace.bot:
                dodgeWayCon(2);
                break;
            case MousePlace.right:
                dodgeWayCon(1);
                break;
            case MousePlace.left:
                dodgeWayCon(4);
                break;
        }
    }
    void dodgeAndGetKeyA()
    {
        switch (mousePlace)
        {
            case MousePlace.top:
                dodgeWayCon(2);
                break;
            case MousePlace.bot:
                dodgeWayCon(3);
                break;
            case MousePlace.right:
                dodgeWayCon(4);
                break;
            case MousePlace.left:
                dodgeWayCon(1);
                break;
        }
    }
    void dodgeOut()
    {
        resetStateToidle();
        speed = 5 ;       
        playerAnimationScript.playerAniRollReset();
    }

    IEnumerator resetDodgeCoolTime()
    {
        yield return new WaitForSeconds(dodgeCoolTime);
        playerDodgeCoolTime = false;
        StopCoroutine("resetDodgeCoolTime");
    }




    //  공격 받은 경우 
    //======================================================
    void playerAttacked(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.normalAttacked  :
                playerAnimationScript.attackedAni(1);

                Invoke("resetStateToidle", 1.5f);
                Invoke("attackedAniReset", 0.3f);
                break;

            case PlayerState.airborneAttacked :
                playerAnimationScript.attackedAni(2);

                Invoke("resetStateToidle", 2f);
                Invoke("attackedAniReset", 0.3f);
                break;

            case PlayerState.stunAttacked:
                playerAnimationScript.attackedAni(3);

                turnOnStunParticle();
                Invoke("turnOffStunParticle",2.3f);

                Invoke("resetStateToidle", 2.1f);
                Invoke("attackedAniReset", 0.3f);
                break;
        }
    }

    void attackedAniReset()
    {
        playerAnimationScript.attackedAniReset();
    }
    void resetStateToidle()
    {
        state = PlayerState.idle;
    }
    void  turnOnStunParticle()
    {
        stunParticleObj.SetActive(true);
    }
    void turnOffStunParticle()
    {
        stunParticleObj.SetActive(false);
    }



    //  공격 한 경우
    //======================================================
    void attack()
    {
        state = PlayerState.attack;
        playerSwordBoxCollider.enabled = true;

        attackCoolTime = true;
        lastClickedTime = Time.time;

        noOfClicks++;
        if(noOfClicks ==1 ) playerAnimationScript.playerAniAttackLeftCombo(1);      

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }
    void attackCoolTimeCount()
    {
        if (state != PlayerState.attack)
        {
            noOfClicks = 0;
            playerSwordBoxCollider.enabled = false;
            attackCoolTime = false;
            playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;
        }
        else if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            playerSwordBoxCollider.enabled = false;
            attackCoolTime = false;
            playerAnimationScript.playerAniAttackLeftCombo(0);
            maxComboDelay = 2f;

            state = PlayerState.idle;
        }
        else if (Time.time - lastClickedTime < maxComboDelay)
        {
            checkCombo2();
            checkCombo3();
        }
    }
    void checkCombo2()
    {
        if (noOfClicks >= 2)
        {
            playerSwordBoxCollider.enabled = true;
            playerAnimationScript.playerAniAttackLeftCombo(2);
        }
    }
    void checkCombo3()
    {
        if (noOfClicks == 3)
        {
            playerSwordBoxCollider.enabled = true;
            playerAnimationScript.playerAniAttackLeftCombo(3);
        }
        maxComboDelay = 0.8f;
    }
    void onOffSwordCollider()
    {
        playerSwordBoxCollider.enabled = false;
    }




    // 따로 할 이유없음.
    // 에어본인 경우 
    //======================================================
    void playerPosUp()
    {
        playerRigid.AddForce(Vector3.up * 6.6f, ForceMode.Impulse);
        state = PlayerState.airborneAttackedCoolTime;
    }

    // 걷기 + 일반 상태 
    //======================================================
    void isPlayerAniIdel()
    {
        if (attackCoolTime == false) playerAnimationScript.playerAniIdel();
    }
    void isPlayerAniWalk()
    {
        playerAnimationScript.playerAniWalk();
    }



   
    private void OnTriggerEnter(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (state == PlayerState.normalAttacked || state == PlayerState.dodge
            || state == PlayerState.airborneAttacked || state == PlayerState.airborneAttackedCoolTime
            ) return;

        if (other.gameObject.tag == "enemyWeapon"|| other.gameObject.tag == "TrapType2FireAttack"
            || other.gameObject.tag == "TrapType3BoomAttack")
        {
            state = PlayerState.normalAttacked;
            Debug.Log("hitted");
        }
        else if(other.gameObject.tag == "pattern08")
        {
            state = PlayerState.airborneAttacked;
            Debug.Log("hit_pattern08");
        }
        else if (other.gameObject.tag == "enemyStun")
        {
            state = PlayerState.stunAttacked;
            Debug.Log("stuned");
        }
        else if (other.gameObject.tag == "NextStageDoor")
        {
            state = PlayerState.waitForMove;
            StageManager.Instance.playerStageMapUI();
        }
        playerAttacked(state);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NextStageDoor")
        {
            state = PlayerState.idle;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (state == PlayerState.normalAttacked || state == PlayerState.dodge
            || state == PlayerState.airborneAttacked || state == PlayerState.airborneAttackedCoolTime
            ) return;

        if (other.gameObject.tag == "TrapType1Thorn" )
        {
            state = PlayerState.normalAttacked;
            Debug.Log("TrapType1Thornhitted");
        }
        playerAttacked(state);
    }
}

