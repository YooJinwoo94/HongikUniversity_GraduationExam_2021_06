using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurseUI : MonoBehaviour
{
    [SerializeField]
    Animator[] curseSkullAni;
    [SerializeField]
    Image[] playerCurseImage;

    [SerializeField]
    Image playerCurseBar;

    [HideInInspector]
    public bool isCurseUp;
    int playerCurseCount;
    //float curseBarFloat = 0f;



    private void Start()
    {
        playerCurseBar.fillAmount = 0;
        playerCurseCount = 0;
    }


    /*
    private void Update()
    {
        if (isCurseUp == false) return;

         playerCurseBar.fillAmount += 0.001f;

        if (playerCurseBar.fillAmount >= curseBarFloat) isCurseUp = false;
    }
    */

    public void isplayerCursed(float num)
    {
      if (playerCurseCount >= 5) return;

        isCurseUp = true;
        //curseBarFloat += num;
        playerCurseBar.fillAmount += num;
        //Debug.Log(playerCurseBar.fillAmount);

        if (playerCurseBar.fillAmount >= 0.9f)
        {
            //curseBarFloat = 0;
            playerCurseBar.fillAmount = 0;
            curseSkullAni[playerCurseCount].SetBool("curseStart", true);
            playerCurseImage[playerCurseCount].color = new Color(123 / 255f, 58 / 255f, 214 / 255f);
            playerCurseCount++;
            Mathf.Clamp(playerCurseCount, 0, 4);
        }
       

       if (playerCurseCount >= 5) playerCurseBar.fillAmount = 1;
    }
    public void playerCurseBarDown(float num)
    {
        playerCurseBar.fillAmount -= num;
    }
}
