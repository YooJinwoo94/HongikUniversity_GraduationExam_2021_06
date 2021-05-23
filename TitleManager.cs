using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] uiSet;
    [SerializeField]
    GameObject[] player;
    [SerializeField]
    GameObject[] cam;
    [SerializeField]
    Animator[] ani;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiSet[0].SetActive(false);
            cam[0].SetActive(false);

            ani[0].SetBool("doorOpen", false);
            ani[1].SetBool("doorOpen", false);
        }
    }


    void doorOpen()
    {
        ani[0].SetBool("doorOpen", true);
        ani[1].SetBool("doorOpen", true);
    }


    public void firstStart()
    {
        uiSet[0].SetActive(true);
        cam[0].SetActive(true);

        Invoke("doorOpen", 1f);
    }

    public void firstStart_Yes()
    {
        //Transform pos = GameObject.Find("Player").GetComponent<Transform>();
        LoadingManager.loadScene("Tutorial_Scene");
    }
    public void firstStart_No()
    {   
        LoadingManager.loadScene("Start_Stage");
    }

    public void endGame()
    {
        Application.Quit();
    }
}
