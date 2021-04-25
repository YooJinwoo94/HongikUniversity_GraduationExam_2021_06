using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGetAni_02 : MonoBehaviour
{
    Rigidbody rid;

    GameObject player;
    GameObject coinPos;
    float timeStamp;

    private void Start()
    {
        timeStamp = Time.time;
        rid = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        coinPos = player.transform.GetChild(0).gameObject;

       
    }


    private void Update()
    {
        Vector3 dir = -(transform.position - coinPos.transform.position).normalized;

        rid.velocity = new Vector3(dir.x, dir.y, dir.z) * 4f * (Time.time / timeStamp);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player") Destroy(this.gameObject);
    }
}
