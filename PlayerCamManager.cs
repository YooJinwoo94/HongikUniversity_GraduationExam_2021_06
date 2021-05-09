using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum CamState
{
   playerFollow,
   shake
}


public class PlayerCamManager : MonoBehaviour
{ 
    [SerializeField]
    Transform target;
    [SerializeField]
     Vector3 offSet;
    [HideInInspector]
    public Animator playerCamAni;

   CamState CamState;


    float shakeAmount = 2f;
    float shakeDuration = 0.2f;

    Vector3 camPos;
    Camera cam;

    private static PlayerCamManager instance = null;

    public static PlayerCamManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
    private void Start()
    {
        cam = GetComponent<Camera>();
        playerCamAni = GetComponent<Animator>();

        playerCamAni.enabled = false;
        CamState = CamState.playerFollow;
        camPos = cam.transform.position;

        instance = this;
        if (null == instance) instance = this;
    }




    public void shake()
    {
        shakeDuration = 0.2f;
        StartCoroutine(ShakeCam());
    }

    IEnumerator ShakeCam()
    {
        Vector3 startRotation = transform.eulerAngles;

        while (shakeDuration > 0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);

            cam.transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeAmount * 2);

            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(37, 0, 0));
        StopCoroutine(ShakeCam());
    }
}
