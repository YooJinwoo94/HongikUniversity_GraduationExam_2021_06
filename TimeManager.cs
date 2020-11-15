using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance = null;

    public static TimeManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }

    public void playerDodgeTime()
    {
        StartCoroutine("DodgeSlowTimeCheck");
    }

        IEnumerator DodgeSlowTimeCheck()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.07f);
        Time.timeScale = 1f;
        StopCoroutine("DodgeSlowTimeCheck");
    }
}
