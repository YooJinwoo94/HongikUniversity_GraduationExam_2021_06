using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryThisGameObject : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

