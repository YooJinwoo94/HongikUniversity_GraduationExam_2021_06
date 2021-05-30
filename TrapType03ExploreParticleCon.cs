using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapType03ExploreParticleCon : MonoBehaviour
{  // Start is called before the first frame update

    [SerializeField]
    CapsuleCollider particleCol;

    void Start()
    {
        Invoke("turnOffBoxCol", 0.2f);
        Destroy(this.gameObject, 1f);
    }
  
    void turnOffBoxCol()
    {
        particleCol.enabled = false;
    }

}
