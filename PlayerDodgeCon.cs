using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeCon : MonoBehaviour
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

    PlayerSpCon spConScript;
    PlayerInputScript inputScript;
    PlayerAniScript aniScript;





    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        aniScript = GetComponent<PlayerAniScript>();
        inputScript = GetComponent<PlayerInputScript>();
        spConScript = GetComponent<PlayerSpCon>();
    }





    //  구르는경우 
    //======================================================
    void dodgeStart()
    {
        if (inputScript.state == PlayerState.dodge) return;

        inputScript.state = PlayerState.dodge;
        playerDodgeCoolTime = true;
        motionTrailCon.startMotionTrail();

        StartCoroutine("resetDodgeCoolTime");
    }
    public  void dodgeAndGetKeyW()
    {
        switch (inputScript.mousePlace)
        {
            case MousePlace.top:
                aniScript.playerAniRollFront();
                break;
            case MousePlace.bot:
                aniScript.playerAniRollBack();
                break;
            case MousePlace.right:
                aniScript.playerAniRollLeft();
                break;
            case MousePlace.left:
                aniScript.playerAniRollRight();
                break;
        }
        dodgeStart();  
    }
    public void dodgeAndGetKeyS()
    {
        switch (inputScript.mousePlace)
        {
            case MousePlace.top:
                aniScript.playerAniRollBack();
                break;
            case MousePlace.bot:
                aniScript.playerAniRollFront();
                break;
            case MousePlace.right:
                aniScript.playerAniRollRight();
                break;
            case MousePlace.left:
                aniScript.playerAniRollLeft();
                break;
        }
        dodgeStart();
    }
    public void dodgeAndGetKeyD()
    {
        switch (inputScript.mousePlace)
        {
            case MousePlace.top:
                aniScript.playerAniRollRight();
                break;
            case MousePlace.bot:
                aniScript.playerAniRollLeft();
                break;
            case MousePlace.right:
                aniScript.playerAniRollFront();
                break;
            case MousePlace.left:
                aniScript.playerAniRollBack();
                break;
        }
        dodgeStart();


    }
    public void dodgeAndGetKeyA()
    {
        switch (inputScript.mousePlace)
        {
            case MousePlace.top:
                aniScript.playerAniRollLeft();
                break;
            case MousePlace.bot:
                aniScript.playerAniRollRight();
                break;
            case MousePlace.right:
                aniScript.playerAniRollBack();
                break;
            case MousePlace.left:
                aniScript.playerAniRollFront();
                break;
        }
        dodgeStart();
    }

    void dodgeOut()
    {
        inputScript.state = PlayerState.idle;
        aniScript.playerDodgeAniReset();
        aniScript.playerAniWait();
        inputScript.isDodge = false;
        spConScript.isPlayerDodgeSucess = false;
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
