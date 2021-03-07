using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCurseUI : MonoBehaviour
{
    [SerializeField]
    Image[] playerCurseImage;

    [SerializeField]
    Image playerCurseBar;

    int playerCurseCount;


    private void Awake()
    {
        playerCurseBar.fillAmount = 0;

        playerCurseCount = 0;
     //   StartCoroutine("dodgeSuccess");
    }



    public void playerCurseBarUp(float num)
    {
        if (playerCurseCount >= 5) return;
        playerCurseBar.fillAmount += num;


        if (playerCurseBar.fillAmount < 1) return;

        playerCurseImage[playerCurseCount].color = new Color(123 / 255f, 58 / 255f, 214 / 255f);
        playerCurseCount++;
        Mathf.Clamp(playerCurseCount, 0, 4);

        if (playerCurseCount >= 5) return;
        playerCurseBar.fillAmount = 0;
    }
    public void playerCurseBarDown(float num)
    {
        playerCurseBar.fillAmount -= num;
    }



    IEnumerator dodgeSuccess()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        while(true)
        {
            yield return waitForSeconds;
            playerCurseBarUp(0.1f);
        }
    }
}
