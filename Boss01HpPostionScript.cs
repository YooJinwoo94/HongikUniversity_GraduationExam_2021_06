using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss01HpPostionScript : MonoBehaviour
{
    [SerializeField]
    Image[] bossHpBarImage;
    Image uiUse;





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
}
