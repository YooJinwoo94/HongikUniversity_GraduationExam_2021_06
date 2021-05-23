using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurseUI : MonoBehaviour
{
    [SerializeField]
    public Animator[] curseSkullAni;
    [SerializeField]
    public Image[] playerCurseImage;

    [SerializeField]
    public Image playerCurseBar;

    [HideInInspector]
    public bool isCurseUp;
    [HideInInspector]
    public int playerCurseCount;
    //float curseBarFloat = 0f;



    private void Start()
    {
        playerCurseBar.fillAmount = 0;
        playerCurseCount = 0;
    }


    public void isplayerCursed(float num)
    {
      if (playerCurseCount >= 5) return;

        isCurseUp = true;
        playerCurseBar.fillAmount += num;

        if (playerCurseBar.fillAmount >= 0.9f)
        {
            playerCurseBar.fillAmount = 0;
            curseSkullAni[playerCurseCount].SetBool("curseStart", true);
            playerCurseImage[playerCurseCount].color = new Color(123 / 255f, 58 / 255f, 214 / 255f);
            playerCurseCount++;
            Mathf.Clamp(playerCurseCount, 0, 4);
        }
      
       if (playerCurseCount >= 5) playerCurseBar.fillAmount = 1;
    }

    // 창고에 박혀있음
    public void playerCurseBarDown(float num)
    {
        playerCurseBar.fillAmount -= num;
    }

    public void ifPlayerDrinkBluePosion()
    {
        playerCurseCount--;
        playerCurseBar.fillAmount = 0;
        curseSkullAni[playerCurseCount].SetBool("curseStart", true);
        playerCurseImage[playerCurseCount].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        Mathf.Clamp(playerCurseCount, 0, 4);
    }
}
