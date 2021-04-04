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
    Image uiUse;

    [SerializeField]
    CapsuleCollider enemyCollider;
    bool deadIsOnce = false;










    private void Start()
    {
        Instantiate(bossHpBarImage[0], FindObjectOfType<Canvas>().transform);
        uiUse = Instantiate(bossHpBarImage[1], FindObjectOfType<Canvas>().transform).GetComponent<Image>();

        uiUse.fillAmount = 1;
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
            //enemyCollider.enabled = false; 

            // 현재 스테이지 값을 가져온다음 스테이지 클리어 여부를 확인 하기위해 값을 전달함.
            //StageClearCheckManager.Instance.numMosterCount(StageManager.Instance.returnNowStageCount());
            deadOrLive = 1;
            Destroy(uiUse.gameObject, 3f);
        }
        return deadOrLive;
    }
}
