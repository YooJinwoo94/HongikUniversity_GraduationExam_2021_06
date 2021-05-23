using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHpManager : MonoBehaviour
{
    [SerializeField]
   public Image hp;

    public void isPlayerDamaged(float damage)
    {
        hp.fillAmount -= damage;
    }
}
