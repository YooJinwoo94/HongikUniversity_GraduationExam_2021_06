using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerColliderCon : MonoBehaviour
{
    [SerializeField]
    GameObject[] hitParticle;
    [SerializeField]
    PlayerParringCon playerParringConScript;
    [SerializeField]
    Animator camAni;
    [SerializeField]
    Transform[] transformPos;
    [SerializeField]
    Transform playerPos;
    [SerializeField]
    DialogueManager dialogueManagerScript;
    [SerializeField]
    CoinManager coinManagerScript;
    [SerializeField]
    PlayerCamManager camShackManagerScript;
    [SerializeField]
    PlayerPowerGetUINo2 playerPowerGetUINo2Script;
    [SerializeField]
    PlayerGetWeaponUINNo5 playerGetWeaponUINNo5;
    [SerializeField]
    PlayerCurseUI playerCurseScript;
    [SerializeField]
    PlayerHpManager playerHpManagerScript;
    [SerializeField]
    PlayerUISeletManger playerUISeletMangerScript;
    [SerializeField]
    TimeManager timeManagerScript;
    PlayerSpCon spConScript;
    PlayerInputScript inputScript;
    PlayerAniScript aniConScript;
    PlayerDodgeCon dodgeConScript;

    [SerializeField]
    GameObject stunParticle;

    [HideInInspector]
    public GameObject checkWhatItis = null;

    private void Start()
    {
        spConScript = GetComponent<PlayerSpCon>();
        aniConScript = GetComponent<PlayerAniScript>();
        dodgeConScript = GetComponent<PlayerDodgeCon>();
        inputScript = GetComponent<PlayerInputScript>();
    }



    //  공격 받은 경우
    #region
    void resetStateToidle()
    {
        inputScript.state = PlayerState.idle;
        inputScript.playerHitted = PlayerHitted.none;
    }
    void turnOnStunParticle()
    {
        stunParticle.SetActive(true);
    }
    void turnOffStunParticle()
    {
        stunParticle.SetActive(false);
    }
    IEnumerator PlayerAttackedCoroutine()
    {
        switch (inputScript.playerHitted)
        {
            case PlayerHitted.normalAttacked:
             //   aniConScript.attackedAni(1);
                yield return new WaitForSeconds(0.3f);
              //  aniConScript.attackedAniReset();
                yield return new WaitForSeconds(0.7f);
                resetStateToidle();
                break;

            case PlayerHitted.airborneAttacked:
              //  aniConScript.attackedAni(2);
                yield return new WaitForSeconds(0.3f);
             //   aniConScript.attackedAniReset();
                yield return new WaitForSeconds(0.3f);
                resetStateToidle();
                break;

            case PlayerHitted.stunAttacked:
                aniConScript.attackedAni(3);
                turnOnStunParticle();
                yield return new WaitForSeconds(0.3f);
                aniConScript.attackedAniReset();
                yield return new WaitForSeconds(1.7f);
                resetStateToidle();
                yield return new WaitForSeconds(0.3f);
                turnOffStunParticle();
                break;
        }
        StopCoroutine(PlayerAttackedCoroutine());
    }
    #endregion


    void waitForBossStage ()
    {
        inputScript.state = PlayerState.idle;
        camAni.enabled = false;
    }



    IEnumerator dodgeSuccess()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.21f);
         yield return waitForSeconds;

        spConScript.dodgeSpUp();
        StopCoroutine("dodgeSuccess");
    }






    void hitParticleOn(string tagName = "" )
    {
        switch (tagName)
        {
            case "":
                Debug.Log("Err");
                break;

            case "enemyWeapon":
                hitParticle[0].SetActive(true);
                break;

            case "pattern08":
                hitParticle[0].SetActive(true);
                break;
//========================================================
            case "TrapType2FireAttack":
                hitParticle[1].SetActive(true);
                break;

            case "TrapType3BoomAttack":
                hitParticle[1].SetActive(true);
                break;

            case "DistanceAttackTypeFireAttack01":
                hitParticle[1].SetActive(true);
                break;
            //========================================================
            case "TrapType1Thorn":
                hitParticle[2].SetActive(true);
                break;
        }
    }

    void hitParticleOff()
    {
       for (int i =0; i<3; i++)
        {
            if (hitParticle[i].activeInHierarchy == true) hitParticle[i].SetActive(false);
        }
    }


    // 골드 획득만 예외적으로 다른 스크립트에서 처리한다.
    private void OnTriggerEnter(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (
            playerParringConScript.isSucess == true
            || inputScript.playerHitted == PlayerHitted.normalAttacked
            || inputScript.playerHitted == PlayerHitted.airborneAttacked
            || inputScript.playerHitted == PlayerHitted.airborneAttackedCoolTime
            ) return;
        // 구르기로 도피시 슬로우 모션
        if (inputScript.state == PlayerState.dodge && dodgeConScript.playerDodgeCoolTime == true)
        {
            if (other.gameObject.tag == "TrapType2FireAttack" || other.gameObject.tag == "enemyWeapon"
            || other.gameObject.tag == "TrapType3BoomAttack" || other.gameObject.tag == "pattern08"
            || other.gameObject.tag == "enemyStun")
            {
                timeManagerScript.playerDodgeTime();
                spConScript.isPlayerDodgeSucess = true;
                StartCoroutine("dodgeSuccess");
                return;
            }
        }

        //공격당함 1
        if (other.gameObject.tag == "enemyWeapon"
            || other.gameObject.tag == "DistanceAttackTypeFireAttack01"
            || other.gameObject.tag == "TrapType2FireAttack"
            || other.gameObject.tag == "TrapType3BoomAttack"
            || other.gameObject.tag == "pattern08"
            || other.gameObject.tag == "TrapType1Thorn")
        {
            hitParticleOn(other.gameObject.tag);
            Invoke("hitParticleOff", 1f);

            inputScript.playerHitted = PlayerHitted.normalAttacked;

            camShackManagerScript.shake();

            playerHpManagerScript.isPlayerDamaged(0.1f);
            playerCurseScript.isplayerCursed(0.2f);

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }

        /*
        if (other.gameObject.tag == "pattern08")
        {
            inputScript.playerHitted = PlayerHitted.airborneAttacked;

            playerCamManagerScript.shake();
            playerCurseScript.isplayerCursed(0.2f);
            playerHpManagerScript.isPlayerDamaged(0.1f);

           StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
        */
        if (other.gameObject.tag == "enemyStun")
        {
            inputScript.playerHitted = PlayerHitted.stunAttacked;
            camShackManagerScript.shake();
            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
      
        if (other.gameObject.tag == "DoorOfDungeon" + 1.ToString())
        {
            GameObject cam = GameObject.Find("DoorOfDungeon1_Set").transform.Find("CM vcam1").gameObject;
            cam.SetActive(true);

            inputScript.state = PlayerState.waitForMoveNextStage;

            aniConScript.playerDodgeAniReset();
            aniConScript.playerAniWait();
            StageManager.Instance.playerStageMapUI();
            StageManager.Instance.dungeonNum = 1;
            return;
        }

        if (other.gameObject.name == "GoToStartStage")
        {
            inputScript.state = PlayerState.idle;
            playerPos.position = transformPos[0].position;
            aniConScript.playerDodgeAniReset();
            aniConScript.playerAniWait();
            LoadingManager.loadScene("Start_Stage");
            return;
        }
        if (other.gameObject.name == "GoToStartStage_ForSecondPlayer")
        {
            inputScript.state = PlayerState.idle;
            playerPos.position = transformPos[0].position;
            aniConScript.playerDodgeAniReset();
            aniConScript.playerAniWait();
            LoadingManager.loadScene("Start_Stage_ForSecondPlayer");
            return;
        }
        if (other.gameObject.name == "DialogueStart")
        {
            BoxCollider box = other.GetComponent<BoxCollider>();
            box.enabled = false;
            dialogueManagerScript.uiOn();
            return;
        }
        if (other.gameObject.tag == "BossStageSceneManager")
        {
            inputScript.state = PlayerState.stopForCutSceen;

            aniConScript.playerAniWait();
            playerUISeletMangerScript.turnOnOffIngameUi();
            Invoke("waitForBossStage", 8.2f);
            return;
        }
        if (other.gameObject.tag == "Dwarf_ShopOwner")
        {
            checkWhatItis = other.gameObject;
            playerUISeletMangerScript.turnOnOffImageE(true);
            return;
        }
        if (other.gameObject.tag == "PlayerWeaponDroped")
        {
            checkWhatItis = other.gameObject;

            playerGetWeaponUINNo5.dropWeaponObj = other.gameObject;
            playerUISeletMangerScript.turnOnOffImageE(true);
            return;
        }
        if(other.gameObject.tag == "PlayerPowerGetSet")
        {
            playerPowerGetUINo2Script.stateObj = other.gameObject;
            playerUISeletMangerScript.turnOnOffImageE(true);
            checkWhatItis = other.gameObject;
            return;
        }
        if (other.gameObject.name == "Save_State")
        {
            checkWhatItis = other.gameObject;
            playerUISeletMangerScript.turnOnOffImageE(true);           
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NextStageDoor")
        {
            inputScript.state = PlayerState.idle;
            return;
        }

        if (other.gameObject.tag == "Dwarf_ShopOwner" ||
            other.gameObject.tag == "PlayerWeaponDroped" ||
            other.gameObject.tag == "PlayerPowerGetSet" ||
            other.gameObject.name == "Save_State")
        {
            playerUISeletMangerScript.turnOnOffImageE(false);
            return;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (inputScript.playerHitted == PlayerHitted.normalAttacked 
            || inputScript.state == PlayerState.dodge
            || inputScript.playerHitted == PlayerHitted.airborneAttacked 
            || inputScript.playerHitted == PlayerHitted.airborneAttackedCoolTime
            ) return;

        if (other.gameObject.tag == "TrapType1Thorn")
        {
            inputScript.playerHitted = PlayerHitted.normalAttacked;

            camShackManagerScript.shake();

            playerCurseScript.isplayerCursed(0.2f);
            playerHpManagerScript.isPlayerDamaged(0.1f);

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
    }
}
