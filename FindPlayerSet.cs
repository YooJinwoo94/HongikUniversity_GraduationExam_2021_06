 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindPlayerSet : MonoBehaviour
{
    [SerializeField]
    GameObject playerSet;

    bool once;

    private void OnEnable()
    {

        if (SceneManager.GetActiveScene().name == "TitleScene" ||
            SceneManager.GetActiveScene().name == "LoadingScene")
        {
            return;
        }
        else Instantiate(playerSet);
    }

}
