using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerGetGoldAniCon : MonoBehaviour
{
    [SerializeField]
    GameObject[] goldGetObj;
    [SerializeField]
    Transform[] pos;
    [SerializeField]
    GameObject goldSet;
    [SerializeField]
    GameObject goldSingle;
    float timeStamp;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goldSingle.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(goldGetObj[i],pos[i].position,transform.rotation);
            }
            gameObject.SetActive(false);
            Destroy(goldSet, 7f);
        }
    }
}
