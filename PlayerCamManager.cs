using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamManager : MonoBehaviour
{ 
    float shakeAmount = 1f;
    float shakeDuration = 0.1f;

    [SerializeField]
    Transform cam;


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

        cam.rotation = Quaternion.Euler(new Vector3(34.055f, 0, 0));
        StopCoroutine(ShakeCam());
    }
}
