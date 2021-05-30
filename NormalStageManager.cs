using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalStageManager : MonoBehaviour
{
    [SerializeField]
    Animator[] doorAni;
    [SerializeField]
    GameObject[] lightObj;




    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Dungeon_Num01_Stage_04" ||
            SceneManager.GetActiveScene().name == "Dungeon_Num01_Stage_05")
        {
            doorAni[0].SetBool("isPlayerDoorClose", true);
        }
        else
        {
            doorAni[0].SetBool("isPlayerDoorClose", true);

            lightObj[0].SetActive(true);
            lightObj[1].SetActive(true);
        }
      
    }
}
