using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinManager : MonoBehaviour
{
    [SerializeField]
    Text coinText;

    int coinCount = 0;

    
    public void  cointCountToUi(int count)
    {
        coinCount += count;

        coinText.text = coinCount.ToString();
    }
}
