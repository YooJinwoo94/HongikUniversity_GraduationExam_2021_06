using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDodgeCon : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    Vector3 mousePos;
    [HideInInspector]
    public  bool playerDodgeCoolTime = false;
    const float dodgeOutTime = 0.3f;
    const float dodgeCoolTime = 0.6f;
    float speed = 20;


    [Header("잔상효과")]
    [SerializeField]
    private MotionTrailCon motionTrailCon;

    playerSpCon playerSpConScript;
    Player_Move_Script player_Move_Script;
    Player_Animation_Script playerAnimationScript;


    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        playerAnimationScript = GetComponent<Player_Animation_Script>();
        player_Move_Script = GetComponent<Player_Move_Script>();
        playerSpConScript = GetComponent<playerSpCon>();
    }





    //  구르는경우 
    //======================================================
    void dodgeStart()
    {
        if (player_Move_Script.state == PlayerState.dodge) return;

        player_Move_Script.state = PlayerState.dodge;
        playerDodgeCoolTime = true;
        motionTrailCon.startMotionTrail();

        StartCoroutine("resetDodgeCoolTime");
    }
    public  void dodgeAndGetKeyW()
    {
        switch (player_Move_Script.mousePlace)
        {
            case MousePlace.top:
                playerAnimationScript.playerAniRollFront();
                break;
            case MousePlace.bot:
                playerAnimationScript.playerAniRollBack();
                break;
            case MousePlace.right:
                playerAnimationScript.playerAniRollLeft();
                break;
            case MousePlace.left:
                playerAnimationScript.playerAniRollRight();
                break;
        }
        dodgeStart();  
    }
    public void dodgeAndGetKeyS()
    {
        switch (player_Move_Script.mousePlace)
        {
            case MousePlace.top:
                playerAnimationScript.playerAniRollBack();
                break;
            case MousePlace.bot:
                playerAnimationScript.playerAniRollFront();
                break;
            case MousePlace.right:
                playerAnimationScript.playerAniRollRight();
                break;
            case MousePlace.left:
                playerAnimationScript.playerAniRollLeft();
                break;
        }
        dodgeStart();
    }
    public void dodgeAndGetKeyD()
    {
        switch (player_Move_Script.mousePlace)
        {
            case MousePlace.top:
                playerAnimationScript.playerAniRollRight();
                break;
            case MousePlace.bot:
                playerAnimationScript.playerAniRollLeft();
                break;
            case MousePlace.right:
                playerAnimationScript.playerAniRollFront();
                break;
            case MousePlace.left:
                playerAnimationScript.playerAniRollBack();
                break;
        }
        dodgeStart();


    }
    public void dodgeAndGetKeyA()
    {
        switch (player_Move_Script.mousePlace)
        {
            case MousePlace.top:
                playerAnimationScript.playerAniRollLeft();
                break;
            case MousePlace.bot:
                playerAnimationScript.playerAniRollRight();
                break;
            case MousePlace.right:
                playerAnimationScript.playerAniRollBack();
                break;
            case MousePlace.left:
                playerAnimationScript.playerAniRollFront();
                break;
        }
        dodgeStart();
    }

    void dodgeOut()
    {
        player_Move_Script.state = PlayerState.idle;
        playerAnimationScript.playerDodgeAniReset();
        playerAnimationScript.playerAniWait();
        player_Move_Script.isDodge = false;
        playerSpConScript.isPlayerDodgeSucess = false;
    }

    IEnumerator resetDodgeCoolTime()
    {
        yield return new WaitForSeconds(dodgeOutTime);
        dodgeOut();
        yield return new WaitForSeconds(dodgeCoolTime);
        playerDodgeCoolTime = false;
        StopCoroutine("resetDodgeCoolTime");
    }

}
