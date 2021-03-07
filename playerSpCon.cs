using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSpCon : MonoBehaviour
{
    [SerializeField]
    Animator[] playerSpAni;
    playerAttackCon playerAttackConScript;
    [SerializeField]
    private Image[] playerSpImage;

    [Header("스테미나가 0임을 알려줌.")]
    [HideInInspector]
    public bool isPlayerSpZero ;
    [HideInInspector]
    public bool isPlayerDodgeSucess;
    [Header("spUpCoolTime은 스테미나 소비시 바로 카운드 안되도록 막기 위한 조치이다.")]
    [HideInInspector]
    public bool spUpCoolTime;
    [Header("usedSpCount는 0~5 , spCount는 현재 스태미너 갯수이다.")]
    int usedSpCount = 0;



    private void Awake()
    {
        playerAttackConScript = GetComponent<playerAttackCon>();

        isPlayerDodgeSucess = false;
        isPlayerSpZero = false;
        spUpCoolTime = false;

        StopAllCoroutines();
        StartCoroutine("spUp");
    }






    public void dodgeSpUp()
    {
        usedSpCount--;
        Mathf.Clamp(usedSpCount, 0, 5);
        if (usedSpCount == 4) isPlayerSpZero = false;

        playerSpImage[usedSpCount].enabled = true;
        playerSpImage[usedSpCount].color = new Color(243, 243, 243);
        playerSpAni[usedSpCount].SetBool("big", true);
        playerSpAni[usedSpCount].SetBool("small", false); 
    }


    public void spDown()
    {
        playerSpImage[usedSpCount].color = new Color(225, 0, 0);
        spUpCoolTime = true;
        playerSpAni[usedSpCount].SetBool("big", false);
        playerSpAni[usedSpCount].SetBool("small", true);

        StartCoroutine("spDownCount");
    }



    IEnumerator spDownCount()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.21f);

        yield return waitForSeconds;

        playerSpImage[usedSpCount].enabled = false;
        playerSpImage[usedSpCount].color = new Color(243, 243, 243);

        usedSpCount += 1;
        Mathf.Clamp(usedSpCount, 0, 5);
        if (usedSpCount == 5) isPlayerSpZero = true;

        StopCoroutine("spDownCount");
    }
    IEnumerator spUp()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);
        WaitForSeconds waitForTwoSeconds = new WaitForSeconds(2f);
        while (true)
        {
            yield return null;

            for (int i = 4; i > -1; i--)
            {
                //자 이제 회복할 시간이야
                if (spUpCoolTime == false && playerSpImage[0].enabled == false)
                {
                    usedSpCount--;
                    Mathf.Clamp(usedSpCount, 0, 5);

                    playerSpAni[usedSpCount].SetBool("big", true);
                    playerSpAni[usedSpCount].SetBool("small", false);

                    playerSpImage[usedSpCount].enabled = true;
                    if (usedSpCount == 4) isPlayerSpZero = false;
                    break;
                }
            }

            // 만약 공격을 한 경우라면 회복하지마
            if (spUpCoolTime == true)
            {
                spUpCoolTime = false;
                yield return waitForTwoSeconds;             
            }
            // pass
            else yield return waitForSeconds;
        }
    }
}
