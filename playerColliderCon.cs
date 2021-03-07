using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColliderCon : MonoBehaviour
{
    [SerializeField]
    playerUISeletManger playerUISeletMangerScript;
    [SerializeField]
    TimeManager timeManagerScript;
    playerSpCon playerSpConScript;
    Player_Move_Script playerMoveScript;
    Player_Animation_Script playerAniConScript;
    playerDodgeCon playerDodgeConScript;

    [SerializeField]
    GameObject stunParticleObj;




    private void Awake()
    {
        playerSpConScript = GetComponent<playerSpCon>();
        playerAniConScript = GetComponent<Player_Animation_Script>();
        playerDodgeConScript = GetComponent<playerDodgeCon>();
        playerMoveScript = GetComponent<Player_Move_Script>();
    }

 


    //  공격 받은 경우
    #region
    void playerAttacked(PlayerState state)
    {
        StartCoroutine("PlayerAttackedCoroutine");
    }

    void resetStateToidle()
    {
        playerMoveScript.state = PlayerState.idle;
    }
    void turnOnStunParticle()
    {
        stunParticleObj.SetActive(true);
    }
    void turnOffStunParticle()
    {
        stunParticleObj.SetActive(false);
    }
    IEnumerator PlayerAttackedCoroutine()
    {
        switch (playerMoveScript.state)
        {
            case PlayerState.normalAttacked:
                playerAniConScript.attackedAni(1);
                yield return new WaitForSeconds(0.3f);
                playerAniConScript.attackedAniReset();
                yield return new WaitForSeconds(1.7f);
                resetStateToidle();
                break;

            case PlayerState.airborneAttacked:
                playerAniConScript.attackedAni(2);
                yield return new WaitForSeconds(0.3f);
                playerAniConScript.attackedAniReset();
                yield return new WaitForSeconds(1.7f);
                resetStateToidle();
                break;

            case PlayerState.stunAttacked:
                playerAniConScript.attackedAni(3);
                turnOnStunParticle();
                yield return new WaitForSeconds(0.3f);
                playerAniConScript.attackedAniReset();
                yield return new WaitForSeconds(1.7f);
                resetStateToidle();
                yield return new WaitForSeconds(0.3f);
                turnOffStunParticle();
                break;
        }
        StopCoroutine("PlayerAttackedCoroutine");
    }
    #endregion






    IEnumerator dodgeSuccess()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.21f);
         yield return waitForSeconds;

        playerSpConScript.dodgeSpUp();
        StopCoroutine("dodgeSuccess");
    }


    private void OnTriggerEnter(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (playerMoveScript.state == PlayerState.normalAttacked
            || playerMoveScript.state == PlayerState.airborneAttacked
            || playerMoveScript.state == PlayerState.airborneAttackedCoolTime
            ) return;
        // 구르기로 도피시 슬로우 모션
        if (playerMoveScript.state == PlayerState.dodge && playerDodgeConScript.playerDodgeCoolTime == true)
        {
            if (other.gameObject.tag == "TrapType2FireAttack" || other.gameObject.tag == "enemyWeapon"
            || other.gameObject.tag == "TrapType3BoomAttack" || other.gameObject.tag == "pattern08"
            || other.gameObject.tag == "enemyStun")
            {
                timeManagerScript.playerDodgeTime();
                playerSpConScript.isPlayerDodgeSucess = true;
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
            //  PlayerCamManager.Instance.shack();
            //  CamState CamState = CamState.playerFollow;
            // Damage(20);
            playerMoveScript.state = PlayerState.normalAttacked;
        }
        else if (other.gameObject.tag == "pattern08")
        {
            // Damage(20);
            playerMoveScript.state = PlayerState.airborneAttacked;
        }
        else if (other.gameObject.tag == "enemyStun")
        {
            playerMoveScript.state = PlayerState.stunAttacked;
        }
        else if (other.gameObject.tag == "NextStageDoor")
        {
            playerMoveScript.state = PlayerState.waitForMoveNextStage;
            playerAniConScript.playerDodgeAniReset();
            playerAniConScript.playerAniWait();
            StageManager.Instance.playerStageMapUI();
        }
        else if (other.gameObject.tag == "BossStageSceneManager")
        {
            playerMoveScript.state = PlayerState.stopForCutSceen;
            Invoke("stateChageToIdle", 5f);
        }
        else if (other.gameObject.tag == "PlayerWeaponDroped")
        {
            playerUISeletMangerScript.turnOnImageE();
        }
            playerAttacked(playerMoveScript.state);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NextStageDoor")
        {
            playerMoveScript.state = PlayerState.idle;
        }
        else if (other.gameObject.tag == "PlayerWeaponDroped")
        {
            playerUISeletMangerScript.turnOffImageE();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // 죽었을떄도 넣기 // 
        if (playerMoveScript.state == PlayerState.normalAttacked || playerMoveScript.state == PlayerState.dodge
            || playerMoveScript.state == PlayerState.airborneAttacked || playerMoveScript.state == PlayerState.airborneAttackedCoolTime
            ) return;

        if (other.gameObject.tag == "TrapType1Thorn") playerMoveScript.state = PlayerState.normalAttacked;
        playerAttacked(playerMoveScript.state);
    }
}
