using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenPlayerGetGoldAniCon : MonoBehaviour
{
    //Rigidbody rid;
    [HideInInspector]
   // public bool getGold = false;
    [SerializeField]
    GameObject[] goldGetObj;
    [SerializeField]
    Transform[] pos;
    [SerializeField]
    GameObject goldSet;
    [SerializeField]
    GameObject goldSingle;
   // GameObject player;
    float timeStamp;

    private void Start()
    {
       // rid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
      //  if (getGold == false) return;
       // Vector3 dir = -(transform.position - player.transform.position).normalized;

       // rid.velocity = new Vector3(dir.x, dir.y, dir.z) * 2f * (Time.time/timeStamp);

        //if (Vector3.Distance(transform.position, player.transform.position) < 0.7f) goldSet.SetActive(false);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //timeStamp = Time.time;
          //  player = GameObject.Find("Player");
           // getGold = true;
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
