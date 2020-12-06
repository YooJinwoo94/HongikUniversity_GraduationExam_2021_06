using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenManager : MonoBehaviour
{
    [SerializeField]
     Animator doorOpenAnimator01;
    [SerializeField]
    Animator doorOpenAnimator02;
    [SerializeField]
    GameObject particleDoorEffect;


    private void Awake()
    {
        particleDoorEffect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
          //  particleDoorEffect.SetActive(true);
            doorOpenAnimator01.SetBool("doorOpen", true);
            doorOpenAnimator02.SetBool("doorOpen", true);
        }
    }
}
