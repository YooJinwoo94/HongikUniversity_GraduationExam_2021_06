using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHpPostionScript : MonoBehaviour
{
    //ws EnemyHealthScript enemyHealthScript;
    //float updateSpeed = 0.5f;
    int maxHealth = 100;
    int currentHealth;

    [SerializeField]
    Image prefabUi;

    [SerializeField]
    Camera playerCamera;



    private Transform headPos;
    Vector3 offSet = new Vector3(0, 2f, 0);

    StageClearCheckManager StageClearCheckManager;
    StageManager StageManager;

    Image uiUse;
    GameObject cam;



    //    FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    //.GetComponent<Image>()
    private void Awake()
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
        int deadOrLive = 0;

        if (uiUse.fillAmount <= 0.01f)
        {
            // return true;
            //StageClearCheckManager =  GameObject.Find("StageClearCheckManager").GetComponent<StageClearCheckManager>();
            //StageClearCheckManager.numMosterCount(1);

            // 현재 스테이지 값을 가져온다음 스테이지 클리어 여부를 확인 하기위해 값을 전달함.
            StageClearCheckManager.Instance.numMosterCount(StageManager.Instance.returnNowStageCount());

            deadOrLive = 1;

            Destroy(uiUse.gameObject);
        }

        return deadOrLive;
    }
 

    /*
    IEnumerator HpCon(float amount)
    {
        yield return null;

        //float nowHp = prefabUi.fillAmount;

        prefabUi.fillAmount -= amount;
        StopCoroutine("HpCon");
    }
    */
}
