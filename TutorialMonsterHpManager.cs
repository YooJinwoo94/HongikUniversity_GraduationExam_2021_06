using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialMonsterHpManager : MonoBehaviour
{
    [HideInInspector]
   public int deadOrLive = 0;
    int maxHealth = 100;
    int currentHealth;
    [SerializeField]
    GameObject[] weaponDropSet;
    [SerializeField]
    Image prefabUi;
    Camera playerCamera;



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

            int count = Random.Range(0, 3);
            Instantiate(weaponDropSet[0], transform.position, transform.rotation);

            // 현재 스테이지 값을 가져온다음 스테이지 클리어 여부를 확인 하기위해 값을 전달함.
            TutorialStageClearManager.Instance.ifDeadChageUiState();
            deadOrLive = 1;
            Destroy(uiUse.gameObject,3f);
        }

        return deadOrLive;
    }
}
