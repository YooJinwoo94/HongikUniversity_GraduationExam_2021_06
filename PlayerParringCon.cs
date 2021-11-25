using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerParringCon : MonoBehaviour
{
//    [SerializeField]
 //   BoxCollider playerSheildBoxCol;

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
        forTutorialMonsterCheckOnce = false;

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


    bool forTutorialMonsterCheckOnce;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "CloseAttackEnemy01")
        {
            CloseAttackTypeNormalColliderCon con = other.GetComponent<CloseAttackTypeNormalColliderCon>();

            isSucess  = con.isStun;
            return;
        }

        // 튜토리얼용
        if ((other.gameObject.name == "TutorialEnemy01(Clone)")
            && (SceneManager.GetActiveScene().name == "Tutorial_Scene_Ver2")
            && (forTutorialMonsterCheckOnce == false))
        {
            TutorialManagerVer2 tutorialManagerScript = GameObject.Find("TutorialManagerVer2").GetComponent<TutorialManagerVer2>();

            if (tutorialManagerScript.tutorial != TutorialStateVer2.step03_6) return;

            CloseAttackTypeNormalColliderCon con = other.GetComponent<CloseAttackTypeNormalColliderCon>();
            isSucess = con.isStun;
            switch (isSucess)
            {
                case true: 
                    tutorialManagerScript.tutorial = TutorialStateVer2.step03_7;

                    TutorialTypeMonsterMove tutorialMonsterMoveScript = other.GetComponent<TutorialTypeMonsterMove>();
                    tutorialMonsterMoveScript.state = TutorialEnemyState.getWait;

                    forTutorialMonsterCheckOnce = true;
                    break;
            }
            return;
        }

        // 튜토리얼 + 일반 전투용
        if ((other.gameObject.name == "TutorialEnemy01(Clone)")
          && (SceneManager.GetActiveScene().name == "Tutorial_Scene_Ver2"))
        {
            CloseAttackTypeNormalColliderCon con = other.GetComponent<CloseAttackTypeNormalColliderCon>();
            isSucess = con.isStun;

            return;
        }
    }
}
