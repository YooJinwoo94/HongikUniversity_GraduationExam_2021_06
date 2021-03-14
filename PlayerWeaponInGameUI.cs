using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponInGameUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerWeapon;
    [Header("0 == 왼쪽 , 1 == 오른쪽.")]
    int checkNowWeaponUISelected;
    [HideInInspector]
    public bool isWeaponChangeCoolTime;







    private void Start()
    {
        isWeaponChangeCoolTime = false;
        checkNowWeaponUISelected = 0;
    }



    public void playerWeaponUISelect()
    {
        if (checkNowWeaponUISelected == 0)
        {
            playerWeapon[0].SetActive(false);
            playerWeapon[1].SetActive(true);
             checkNowWeaponUISelected = 1;
        }
        else
        {
            playerWeapon[0].SetActive(true);
            playerWeapon[1].SetActive(false);
            checkNowWeaponUISelected = 0;
        }

        isWeaponChangeCoolTime = true;

        StartCoroutine("resetCoolTime");
    }
   


    IEnumerator resetCoolTime()
    {
        yield return new WaitForSeconds(0.2f);
        isWeaponChangeCoolTime = false;
        StopCoroutine("resetCoolTime");
    }
}
