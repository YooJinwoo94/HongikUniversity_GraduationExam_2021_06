using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPosCon : MonoBehaviour
{
    [SerializeField]
    CoinManager coinManagerScript;
    [SerializeField]
    Transform playerPos;

    [SerializeField]
    Vector3 offSet;
    Animator coinTextAni;

    int countForCoinAni = 0;




    void Update()
    {
        transform.position = new Vector3(playerPos.position.x + offSet.x, playerPos.position.y + offSet.y, playerPos.position.z + offSet.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Single_Gold(Clone)")
        {
            countForCoinAni++;
            switch(countForCoinAni)
            {
                case 5:
                    countForCoinAni = 0;
                    coinManagerScript.cointCountToUi(100);
                    coinTextAni = GameObject.Find("Coin_Count_Text").GetComponent<Animator>();
                    coinTextAni.SetTrigger("AniStart");
                    break;
            }        
        }
    }
}
