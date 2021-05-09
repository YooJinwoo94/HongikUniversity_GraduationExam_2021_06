using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenAniEndCoinsSetActiveTrue : MonoBehaviour
{
    [SerializeField]
    GameObject goldSet;

    private void Start()
    {
        Invoke("goldObjCon", 1f);
    }

    void goldObjCon()
    {
        goldSet.SetActive(true);
    }
}
