using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderCon : MonoBehaviour
{
    [SerializeField]
    Animator camAni;

    [SerializeField]
    PlayerCamManager playerCamManagerScript;
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
        switch (inputScript.state)
        {
            case PlayerState.normalAttacked:
                aniConScript.attackedAni(1);
                yield return new WaitForSeconds(0.3f);
                aniConScript.attackedAniReset();
                yield return new WaitForSeconds(0.7f);
                resetStateToidle();
                break;

            case PlayerState.airborneAttacked:
                aniConScript.attackedAni(2);
                yield return new WaitForSeconds(0.3f);
                aniConScript.attackedAniReset();
                yield return new WaitForSeconds(0.3f);
                resetStateToidle();
                break;

            case PlayerState.stunAttacked:
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


    private void OnTriggerEnter(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (inputScript.state == PlayerState.normalAttacked
            || inputScript.state == PlayerState.airborneAttacked
            || inputScript.state == PlayerState.airborneAttackedCoolTime
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
            || other.gameObject.tag == "TrapType1Thorn")
        {
            inputScript.state = PlayerState.normalAttacked;

            playerCamManagerScript.shake();
            playerHpManagerScript.isPlayerDamaged(0.1f);
            playerCurseScript.isplayerCursed(0.2f);

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
        if (other.gameObject.tag == "pattern08")
        {
            inputScript.state = PlayerState.airborneAttacked;

            playerCamManagerScript.shake();
            playerCurseScript.isplayerCursed(0.2f);
            playerHpManagerScript.isPlayerDamaged(0.1f);

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
        if (other.gameObject.tag == "enemyStun")
        {
            inputScript.state = PlayerState.stunAttacked;

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
        if (other.gameObject.tag == "NextStageDoor")
        {
            inputScript.state = PlayerState.waitForMoveNextStage;

            aniConScript.playerDodgeAniReset();
            aniConScript.playerAniWait();
            StageManager.Instance.playerStageMapUI();
            return;
        }
        if (other.gameObject.tag == "BossStageSceneManager")
        {
            inputScript.state = PlayerState.stopForCutSceen;

            aniConScript.playerAniWait();
            playerUISeletMangerScript.turnOnOffIngameUi();
            Invoke("waitForBossStage", 6.5f);
            return;
        }
        if (other.gameObject.tag == "PlayerWeaponDroped")
        {
            checkWhatItis = other.gameObject;
            playerGetWeaponUINNo5.dropWeaponObj = other.gameObject;
            playerUISeletMangerScript.turnOnOffImageE();
            return;
        }
        if(other.gameObject.tag == "PlayerPowerGetSet")
        {
            playerUISeletMangerScript.turnOnOffImageE();
            checkWhatItis = other.gameObject;
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
        if (other.gameObject.tag == "PlayerWeaponDroped")
        {
            checkWhatItis = null;
            playerGetWeaponUINNo5.dropWeaponObj = null;

            playerUISeletMangerScript.turnOnOffImageE();
            return;
        }
        if (other.gameObject.tag == "PlayerPowerGetSet")
        {
            checkWhatItis = null;
            playerGetWeaponUINNo5.dropWeaponObj = null;

            playerUISeletMangerScript.turnOnOffImageE();
            return;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (inputScript.state == PlayerState.normalAttacked || inputScript.state == PlayerState.dodge
            || inputScript.state == PlayerState.airborneAttacked || inputScript.state == PlayerState.airborneAttackedCoolTime
            ) return;

        if (other.gameObject.tag == "TrapType1Thorn")
        {
            inputScript.state = PlayerState.normalAttacked;

            playerCamManagerScript.shake();
            playerCurseScript.isplayerCursed(0.2f);
            playerHpManagerScript.isPlayerDamaged(0.1f);

            StartCoroutine(PlayerAttackedCoroutine());
            return;
        }
    }
}
