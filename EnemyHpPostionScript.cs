using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHpPostionScript : MonoBehaviour
{
    [SerializeField]
    CapsuleCollider enemyCollider;
    [HideInInspector]
   public int deadOrLive = 0;
    int maxHealth = 100;
    int currentHealth;

    [SerializeField]
    Image prefabUi;
    Camera playerCamera;
    [SerializeField]
    GameObject[] weaponDropSet;
    int randomNumForWeaponDrop = 0;

    private Transform headPos;
    Vector3 offSet = new Vector3(0, 2f, 0);

    StageClearCheckManager StageClearCheckManager;
    StageManager StageManager;

    Image uiUse;
    GameObject cam;
    bool deadIsOnce = false;







    private void Start()
    {
        uiUse = Instantiate(prefabUi, GameObject.FindGameObjectWithTag("EnemyHpUI").transform);

        cam = GameObject.FindWithTag("Camera");
        playerCamera = cam.GetComponent<Camera>();

        currentHealth = maxHealth;

        uiUse.fillAmount = 1;
        uiUse.enabled = true;
    }

    private void FixedUpdate()
    {
        uiUse.transform.position = playerCamera.WorldToScreenPoint(transform.position + offSet);
    }



    public void healthUp(int amount)
    {
        uiUse.fillAmount += amount;
    }

    public void enemyDamagedAndImageChange(float amount)
    {
        uiUse.fillAmount -= amount;
    }


    public int enemyHpDeadCheck()
    {
        if (uiUse.fillAmount <= 0.01f && deadIsOnce == false)
        {
            deadIsOnce = true;
            randomNumForWeaponDrop = Random.Range(0, 5);


            if (randomNumForWeaponDrop <= 2)
            {
                Instantiate(weaponDropSet[randomNumForWeaponDrop], transform.position, transform.rotation);
            }

            //  enemyCollider.enabled = false; 
            // 현재 스테이지 값을 가져온다음 스테이지 클리어 여부를 확인 하기위해 값을 전달함.
            StageClearCheckManager.Instance.numMosterCount(StageManager.Instance.returnNowStageCount());           
            deadOrLive = 1;
            Destroy(uiUse.gameObject,3f);
        }
        return deadOrLive;
    }
}
