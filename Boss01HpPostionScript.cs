using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss01HpPostionScript : MonoBehaviour
{
    [HideInInspector]
    public int deadOrLive = 0;
    [SerializeField]
    Image[] bossHpBarImage;
    GameObject bossHpBg;
    Image bossHpBar;

    [SerializeField]
    CapsuleCollider enemyCollider;
    bool deadIsOnce = false;
    Rigidbody rid;









    private void Start()
    {

        bossHpBg = Instantiate(bossHpBarImage[0], FindObjectOfType<Canvas>().transform).gameObject;
        bossHpBar = Instantiate(bossHpBarImage[1], FindObjectOfType<Canvas>().transform).GetComponent<Image>();


        rid = GetComponent<Rigidbody>();
        bossHpBar.fillAmount = 1;
    }



    public void healthUp(int amount)
    {
        bossHpBar.fillAmount += amount;
    }

    public void enemyDamagedAndImageChange(float amount)
    {
        bossHpBar.fillAmount -= amount;     
    }


    public int enemyHpDeadCheck()
    {
        if (bossHpBar.fillAmount <= 0.01f && deadIsOnce == false)
        {
            deadIsOnce = true;

            rid.useGravity = false;
            enemyCollider.enabled = false;
            // 현재 스테이지 값을 가져온다음 스테이지 클리어 여부를 확인 하기위해 값을 전달함.
            StageClearCheckManager.Instance.numMosterCount(StageManager.Instance.dungeonNum, StageManager.Instance.returnNowStageName());
            QuestManager.Instance.isQuestEnd[StageManager.Instance.dungeonNum, 0] = true;

            deadOrLive = 1;
            Destroy(bossHpBar.gameObject, 1f);
            Destroy(bossHpBg, 1f);
        }
        return deadOrLive;
    }
}
