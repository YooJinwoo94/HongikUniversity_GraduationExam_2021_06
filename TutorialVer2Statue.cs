using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVer2Statue : MonoBehaviour
{
    [SerializeField]
    TutorialManagerVer2 tutorialManagerVer2Script;

    [SerializeField]
    BoxCollider StatueBoxCol;

    bool startOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (tutorialManagerVer2Script.tutorial != TutorialStateVer2.step01_2) return;

        switch (other.name)
        {
            case "Player":
                tutorialManagerVer2Script.tutorial = TutorialStateVer2.step01_3;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case "Player":
                this.tag = "Save_State";
                break;
        }
    }
}
