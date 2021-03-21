using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArrowCon : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer arrow;

    float grow = 10f;

    bool finArrowMove =false;

    private void Update()
    {
        if (arrow.enabled == false )
        {
            finArrowMove = false;
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            return;
        }
        if (finArrowMove == true) return;
        
        float y = transform.localScale.y + grow * Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            finArrowMove = true;
        }
    }
}
