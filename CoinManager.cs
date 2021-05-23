using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinManager : MonoBehaviour
{
    [SerializeField]
    public Text coinText;
    [SerializeField]
    PlayerShopUINo7 playerShopUINo7Script;

    bool isPlus = false;

    [HideInInspector]
    public int cost = 0;
    [HideInInspector]
    public int coinCount = 0;
    
    public void  coinCountToUi( int count , bool m_isPlus)
    {
        isPlus = m_isPlus;      
        cost = count;      
    }

    public void costCountUpDown()
    {
        StartCoroutine(Count(coinCount, coinCount, cost));
    }

    public void loadCost()
    {
        StartCoroutine(load(coinCount, coinCount, cost));
    }

    IEnumerator Count(float futurePlayersCoinCount, float nowPlayersCoinCount,int inputCount)

    {
        float duration = 0.5f;
      
        switch (isPlus)
        {
            case true:
                futurePlayersCoinCount += inputCount;

                float offset = (futurePlayersCoinCount - nowPlayersCoinCount) / duration;
                while (nowPlayersCoinCount < futurePlayersCoinCount)

                {
                    nowPlayersCoinCount += offset * Time.deltaTime;
                    coinText.text = ((int)nowPlayersCoinCount).ToString();

                    playerShopUINo7Script.coinText.text = ((int)nowPlayersCoinCount).ToString();
                    yield return null;
                }
                break;

            case false:
                if (coinCount - inputCount < 0) break;

                futurePlayersCoinCount -= inputCount;
                float offset1 = (futurePlayersCoinCount - nowPlayersCoinCount) / duration;
                while (nowPlayersCoinCount > futurePlayersCoinCount)

                {
                    nowPlayersCoinCount += offset1 * Time.deltaTime;
                    coinText.text = ((int)nowPlayersCoinCount).ToString();

                    playerShopUINo7Script.coinText.text = ((int)nowPlayersCoinCount).ToString();
                    yield return null;
                }
                break;
        }
       
        coinCount = (int)futurePlayersCoinCount;

        coinText.text = ((int)futurePlayersCoinCount).ToString();
        playerShopUINo7Script.coinText.text = coinText.text;
    }


    IEnumerator load(float futurePlayersCoinCount, float nowPlayersCoinCount, int inputCount)

    {
        float duration = 0.5f;

        float offset = (futurePlayersCoinCount - nowPlayersCoinCount) / duration;
        while (nowPlayersCoinCount < futurePlayersCoinCount)

        {
            nowPlayersCoinCount += offset * Time.deltaTime;
            coinText.text = ((int)nowPlayersCoinCount).ToString();

            playerShopUINo7Script.coinText.text = ((int)nowPlayersCoinCount).ToString();
            yield return null;
        }

        coinCount = (int)futurePlayersCoinCount;

        coinText.text = ((int)futurePlayersCoinCount).ToString();
        playerShopUINo7Script.coinText.text = coinText.text;
    }
}
