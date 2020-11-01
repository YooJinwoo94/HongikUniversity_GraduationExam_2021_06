using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapType3BoomAttack : MonoBehaviour
{
    BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    
    }

    void boxColliderOff()
    {
        boxCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxCollider.enabled = true;
            Invoke("boxColliderOff", 0.2f);
            Destroy(gameObject, 2f);
        }
    }
}
