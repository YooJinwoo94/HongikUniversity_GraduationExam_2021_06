using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParringCon : MonoBehaviour
{
    [SerializeField]
    BoxCollider playerSheildBoxCol;

    PlayerSpCon playerSpConScript;
    PlayerInputScript inputScript;

    [SerializeField]
    PlayerAniScript aniScript;
    // 필요한지 확인.
    [HideInInspector]
    public bool isCool;
    public bool isSucess;
    float lastClickedTime = 0;
    const float delay = 1f;







    private void Start()
    {
        isCool = false;
        isSucess = false;

        inputScript = GetComponent<PlayerInputScript>();
        playerSpConScript = GetComponent<PlayerSpCon>();
    }

    private void Update()
    {
        if (lastClickedTime == 0 ) return;

        if (Time.time - lastClickedTime > delay)
        {
            lastClickedTime = 0;

            isCool = false;
            isSucess = false;

            inputScript.state = PlayerState.idle;
            aniScript.playerAniWait();
        }
    }




    public void parringStart()
    {
        lastClickedTime = Time.time;
        playerSpConScript.spDown();
        isCool = true;
        inputScript.state = PlayerState.parring;
        aniScript.playerAniParring();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CloseAttackEnemy01")
        {
            CloseAttackTypeNormalColliderCon con = other.GetComponent<CloseAttackTypeNormalColliderCon>();

            isSucess  = con.isStun;
        }
    }
}
