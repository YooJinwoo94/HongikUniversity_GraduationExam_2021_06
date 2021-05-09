using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WhenCoinFly : MonoBehaviour
{
    Vector3 coinGestPos = new Vector3(1f, 1f);
    Transform coinGetPos;
    Animator coinTextAni;



    private void Start()
    {
        coinGetPos = GameObject.Find("CoinGetPos").gameObject.transform;
    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, coinGetPos.position, Time.deltaTime * 18f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CoinGetPos") Destroy(this.gameObject);
    }
}

