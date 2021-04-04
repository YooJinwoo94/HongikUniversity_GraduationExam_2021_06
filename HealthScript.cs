using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    Image foreImage;
    float updateSpeed = 0.5f;

    int maxHealth = 100;
    int currentHealth;


 


    private void Awake()
    {
        currentHealth = maxHealth;


    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
    }

    public void healthUp(int amount)
    {
        currentHealth += amount;
    }

    public void enemyDamagedAndImageChange(float amount)
    {
        StartCoroutine(HpCon(amount));
    }

    IEnumerator HpCon(float amount)
    {
        yield return null;

        float nowHp = foreImage.fillAmount;
       
        foreImage.fillAmount -= amount;
        StopCoroutine("HpCon");
    }
}
