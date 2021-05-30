using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackTypeNormalColliderCon : MonoBehaviour
{
    public enum CloseAttackEnemy01IsAttacked
    {
        idle,
        attacked,
        stuned
    }
    public CloseAttackEnemy01IsAttacked IsAttackedState;

    [HideInInspector]
    public bool isStun;
    [HideInInspector]
    public bool isAttack;

    [SerializeField]
    ParticleSystem stunParicle;


    [SerializeField]
    SkinnedMeshRenderer skinMeshRender;
    [SerializeField]
    GameObject[] hitParticle;
    PlayerCamManager camShackManagerScript;
    TimeManager timeManagerScript;



    WeaponColliderCon weaponColConScript;
    EnemyHpPostionScript hpPostionScript;
    CloseAttackTypeNormalAni aniScript;



    bool trap01;



    private void Start()
    {
        isStun = false;
        isAttack = false;
        trap01 = false;

        aniScript = GetComponent<CloseAttackTypeNormalAni>();
        hpPostionScript = GetComponent<EnemyHpPostionScript>();
        weaponColConScript = GetComponent<WeaponColliderCon>();
        IsAttackedState = CloseAttackEnemy01IsAttacked.idle;

        camShackManagerScript = GameObject.Find("PlayerCamManager").GetComponent<PlayerCamManager>();
        timeManagerScript = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }







    void stateChange()
    {
        if (stunParicle.isPlaying) stunParicle.Stop();
        IsAttackedState = CloseAttackEnemy01IsAttacked.idle;

        isStun = false;
        isAttack = false;
    }
    void isTrap01CoolTimeOn()
    {
        trap01 = false;
    }


    void parringObjOnOff()
    {
        Animator parringoAni = GameObject.Find("ParringParticle").GetComponent<Animator>();

        if (parringoAni.enabled == true) parringoAni.enabled = false;
        else
        {
            parringoAni.enabled = true;
            parringoAni.SetTrigger("ParringOn");
        }
    }

    void hitObjOnOff()
    {
        if (hitParticle[0].activeInHierarchy == false )
        {
            hitParticle[0].SetActive(true);
            hitParticle[1].SetActive(false);
        }
        else
        {
            hitParticle[0].SetActive(false);
            hitParticle[1].SetActive(true);
        }
    }
    
    
    
    
    
    private void OnTriggerExit(Collider other)
    {
        if (IsAttackedState == CloseAttackEnemy01IsAttacked.attacked) return;
        switch(other.gameObject.tag)
        {
            case "PlayerSword01":
                hpPostionScript.enemyDamagedAndImageChange(0.2f);
                hpPostionScript.enemyHpDeadCheck();

                camShackManagerScript.shake();
                timeManagerScript.playerAttackTime();

                hitObjOnOff();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {     
                    if (isAttack == true) return;

                    aniScript.aniSet("Hitted");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }

                if (stunParicle.isPlaying) stunParicle.Stop();
                break;

            case "PlayerSword02":
                hpPostionScript.enemyDamagedAndImageChange(0.5f);
                hpPostionScript.enemyHpDeadCheck();

                camShackManagerScript.shake();
                timeManagerScript.playerAttackTime();

                hitObjOnOff();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    Invoke("hitObjOnOff", 1f);

                    if (isAttack == true) return;

                    aniScript.aniSet("Hitted");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }

                if (stunParicle.isPlaying) stunParicle.Stop();
                break;

            case "PlayerSword03":
                hpPostionScript.enemyDamagedAndImageChange(0.8f);
                hpPostionScript.enemyHpDeadCheck();

                camShackManagerScript.shake();
                timeManagerScript.playerAttackTime();

                hitObjOnOff();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    Invoke("hitObjOnOff", 1f);

                    if (isAttack == true) return;

                    aniScript.aniSet("Hitted");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }

                if (stunParicle.isPlaying) stunParicle.Stop();
                break;
        }

        if ((IsAttackedState == CloseAttackEnemy01IsAttacked.stuned)
            || (PlayerInputScript.Instance.state != PlayerState.parring)
            || (isAttack == false)) return;

        switch (other.gameObject.name)
        {
            case "Player_Weapon_Shield":             
                hpPostionScript.enemyDamagedAndImageChange(0.2f);
                hpPostionScript.enemyHpDeadCheck();

                camShackManagerScript.shake();
                timeManagerScript.playerAttackTime();

                weaponColConScript.weaponColliderOff(0);

                parringObjOnOff();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    stunParicle.Play();
                    isStun = true;

                    aniScript.aniSet("Stuned");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.stuned;
                    Invoke("parringObjOnOff", 3f);
                    Invoke("stateChange", 1.5f);
                }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsAttackedState == CloseAttackEnemy01IsAttacked.attacked) return;

        switch (other.gameObject.tag)
        {
            case "TrapType2FireAttack":
                hpPostionScript.enemyDamagedAndImageChange(0.2f);
                hpPostionScript.enemyHpDeadCheck();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    aniScript.aniSet("Hitted");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;

            case "TrapType3BoomAttack":
                hpPostionScript.enemyDamagedAndImageChange(0.2f);
                hpPostionScript.enemyHpDeadCheck();

                if (hpPostionScript.deadOrLive == 1)
                {
                    skinMeshRender.materials[0].color = Color.white;
                    aniScript.aniSet("Dead");
                    Destroy(this.gameObject, 3f);
                }
                else
                {
                    aniScript.aniSet("Hitted");
                    IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                    Invoke("stateChange", 0.3f);
                }
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (trap01 == true) return;

        if (other.gameObject.tag == "TrapType1Thorn")
        {
            trap01 = true;
            hpPostionScript.enemyDamagedAndImageChange(0.2f);
            hpPostionScript.enemyHpDeadCheck();

            if (hpPostionScript.deadOrLive == 1)
            {
                skinMeshRender.materials[0].color = Color.white;
                aniScript.aniSet("Dead");
                Destroy(this.gameObject, 3f);
            }

            else
            {
                aniScript.aniSet("Hitted");
                IsAttackedState = CloseAttackEnemy01IsAttacked.attacked;
                Invoke("stateChange", 0.3f);
                Invoke("isTrap01CoolTimeOn", 2f);
            }
            return;
        }
    }
}
