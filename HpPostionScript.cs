using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpPostionScript : MonoBehaviour
{
   //ws EnemyHealthScript enemyHealthScript;
    float updateSpeed = 0.5f;
    int maxHealth = 100;
    int currentHealth;


    [SerializeField]
    Image prefabUi;


    Image uiUse;

    GameObject cam;
    [SerializeField]
    Camera playerCamera;

    private Transform headPos;
    Vector3 offSet = new Vector3(0, 2f, 0);



    void instanceHpTime()
    {
        uiUse.enabled = true;
    }


    private void Awake()
    {


        uiUse = Instantiate(prefabUi, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        //enemyHealthScript = prefabUi.GetComponent<EnemyHealthScript>();

        cam = GameObject.FindWithTag("Camera");
        playerCamera = cam.GetComponent<Camera>();

        currentHealth = maxHealth;

        uiUse.fillAmount = 1;
        uiUse.enabled = false;

        Invoke("instanceHpTime", 3.5f);
    }

    private void Update()
    {
        uiUse.transform.position = playerCamera.WorldToScreenPoint(transform.position + offSet);
    }



    public void healthUp(int amount)
    {
        uiUse.fillAmount += amount;
    }

    public void enemyDamagedAndImageChange(float amount)
    {
        uiUse.fillAmount -= amount;
    }


    
    IEnumerator HpCon(float amount)
    {
        yield return null;

        //float nowHp = prefabUi.fillAmount;

        prefabUi.fillAmount -= amount;
        StopCoroutine("HpCon");
    }
    
}
