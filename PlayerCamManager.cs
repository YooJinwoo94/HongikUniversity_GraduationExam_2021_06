using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum CamState
{
   playerFollow,
   bossStageCam,
}


public class PlayerCamManager : MonoBehaviour
{
    
    [SerializeField]
    Transform target;
    [SerializeField]
     Vector3 offSet;
     Animator PlayerCamAni;

   CamState CamState;

    float shakeAmount = 0 ;
    Vector3 orginPos;
    


    private static PlayerCamManager instance = null;

    public static PlayerCamManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
    private void Awake()
    {
        PlayerCamAni = GetComponent<Animator>();
        CamState = CamState.playerFollow;
        PlayerCamAni.enabled = false;

        instance = this;
        if (null == instance) instance = this;
    }


    // Update is called once per frame
    void Update()
    {      
        
        if (CamState != CamState.playerFollow) return;
        transform.position = target.position + offSet;
        
    }


 
















    /*
    public void shack(float amt = 0.05f , float length = 0.15f )
    {
        orginPos = this.transform.position;
        shakeAmount = amt;
        InvokeRepeating("beginShack", 0, 0.01f);
        Invoke("stopShack", length);
    }
   void beginShack()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = this.transform.position;

            float offSetX = Random.value * shakeAmount * 2 - shakeAmount;
           // float offSetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x = offSetX;
           // camPos.y = offSetY;

            this.transform.position = camPos;
        }
    }


    void stopShack()
    {
        CancelInvoke("beginShack");
        this.transform.position = orginPos;
    }
  */
}
