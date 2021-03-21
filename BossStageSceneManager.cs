using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageSceneManager : MonoBehaviour
{
    [SerializeField]
    Transform bossDoorTransform;

    [SerializeField]
    Animator bossDoorAni;
    [SerializeField]
    Animator playerDoorAni;

    [SerializeField]
    Transform bossSwapnArea;

    [SerializeField]
    GameObject bossGameObj;



    // 페이드 인 아웃
    //=========================================
    [SerializeField]
    Image blackPanel;
    float time = 0f;
    float finTime = 0.15f;








    bool stageFinsh;
    BoxCollider boxCollider;





    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        stageFinsh = false;
        blackPanel.enabled = false;
    }



    IEnumerator FadeStart()
    {
        blackPanel.enabled = true;
        Color color = blackPanel.color;
        while (color.a <1f)
        {
            time += Time.deltaTime / finTime;
            color.a = Mathf.Lerp(0, 1, time);
            blackPanel.color = color;
            yield return null;
        }
        time = 0;
        yield return new WaitForSeconds(0.5f);

        while (color.a > 0f)
        {
            time += Time.deltaTime / finTime;
            color.a = Mathf.Lerp(1, 0, time);
            blackPanel.color = color;
            yield return null;
        }
        blackPanel.enabled = false;
        PlayerCamManager.Instance.playerCamAni.enabled = false;
        PlayerCamManager.Instance.playerCamAni.SetBool("bossStage01Start", false);
        StopCoroutine(FadeStart());
    }




    IEnumerator stageBoss01Opening()
    {
        yield return new WaitForSeconds(0.5f);
        bossDoorAni.SetBool("isDoorClose", true);
        yield return new WaitForSeconds(2.5f);
        Instantiate(bossGameObj, bossSwapnArea);
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeStart());
        StopCoroutine("stageBoss01Opening");
    }
    // 보스전이 끝나면 문이 열림
    void setBossDoorAniStop()
    {
        bossDoorAni.SetBool("isDoorClose", false);
    }

 




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerDoorAni.SetBool("isPlayerDoorClose", true);
            PlayerCamManager.Instance.playerCamAni.enabled = true;
            PlayerCamManager.Instance.playerCamAni.SetBool("bossStage01Start", true);
            
            boxCollider.enabled = false;
            StartCoroutine("stageBoss01Opening");
        }
    }
}
