using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void playerAttackTime()
    {
        //StartCoroutine("AttackSlowTimeCheck");
    }




    public void playerDodgeTime()
    {
        StartCoroutine("DodgeSlowTimeCheck");
    }


    IEnumerator AttackSlowTimeCheck()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.04f);
        Time.timeScale = 1f;
        StopAllCoroutines();
    }



    IEnumerator DodgeSlowTimeCheck()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.07f);
        Time.timeScale = 1f;
        StopAllCoroutines();
    }


    public void playerUITimeOn ()
    {
        Time.timeScale = 0f;
    }
    public void playerUITimeOff()
    {
        Time.timeScale = 1f;
    }
}
