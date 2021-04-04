using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageManager : MonoBehaviour
{
    [SerializeField]
    Animator[] doorAni;
    [SerializeField]
    GameObject[] lightObj;




    private void OnTriggerEnter(Collider other)
    {
        doorAni[0].SetBool("isPlayerDoorClose", true);
        lightObj[0].SetActive(true);
        lightObj[1].SetActive(true);
    }
}
