using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class track_03_PartObj : MonoBehaviour
{
    private void Awake()
    {
        Invoke("destroy", 1.5f);
    }

    private void destroy()
    {
        Destroy(this);
    }
}
