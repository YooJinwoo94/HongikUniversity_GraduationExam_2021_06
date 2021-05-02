using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DialogueState
{
    DialogueStart,
    DialogueEnd
}

public class DialogueManager : MonoBehaviour
{
   public DialogueState dialogueState = DialogueState.DialogueEnd;

    [SerializeField]
    QuestManager questManagerScript;
    [SerializeField]
    TypingTextCon typingTextConScript;
    [SerializeField]
    GameObject[] dialogueUISet;
    [SerializeField]
    Animator[] playerKeyButtonAni;
    [SerializeField]
    GameObject[] playerKeyButtonObj;
    [SerializeField]
    PlayerAniScript playerAni;
    [SerializeField]
    GameObject playerCam;
    Animator playerCamAni;

    GameObject lightObj;
    int count = 0;




    public void uiOn(int num = 0)
    {
        playerCamAni = playerCam.GetComponent<Animator>();
        playerCamAni.enabled = true;
        playerCamAni.SetBool("is_StartStage_Talk_01", true);

        playerAni.playerAniWait();
        dialogueState = DialogueState.DialogueStart;

        typingTextConScript.typingTextStart(count);

        dialogueUISet[0].SetActive(true);
        dialogueUISet[1].SetActive(true);
        dialogueUISet[2].SetActive(true);

        playerKeyButtonObj[4].SetActive(true);
        playerKeyButtonAni[4].enabled = true;
        count++;
    }



    private void Update()
    {
        if (dialogueState == DialogueState.DialogueEnd || typingTextConScript.textState == TextState.textStart ) return;
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (questManagerScript.isQuestEnd[1, 0] == false)
                {
                     switch (count)
                  {
                    case 6:
                        dialogueState = DialogueState.DialogueEnd;

                        dialogueUISet[0].SetActive(false);
                        dialogueUISet[1].SetActive(false);
                        dialogueUISet[2].SetActive(false);

                        playerKeyButtonObj[4].SetActive(false);
                        playerKeyButtonAni[4].enabled = false;

                        count = 0;

                        playerCamAni.SetBool("is_StartStage_Talk_01", false);
                        Invoke("resetCamAni", 0.3f);

                        lightObj = GameObject.Find("Door_Light").transform.Find("Door_Open_Light").gameObject;
                        Invoke("lightOnTime", 1f);
                        break;

                    default:
                        typingTextConScript.typingTextStart(count);
                        count++;
                        break;
                    }
                }      
            
            if (questManagerScript.isQuestEnd[1, 0] == true)
            {
                switch (count)
                {
                    case 6:
                        dialogueState = DialogueState.DialogueEnd;

                        dialogueUISet[0].SetActive(false);
                        dialogueUISet[1].SetActive(false);
                        dialogueUISet[2].SetActive(false);

                        playerKeyButtonObj[4].SetActive(false);
                        playerKeyButtonAni[4].enabled = false;

                        count = 0;

                        playerCamAni.SetBool("is_StartStage_Talk_01", false);
                        Invoke("resetCamAni", 0.3f);
                        break;

                    default:
                        typingTextConScript.typingTextStart(count);
                        count++;
                        break;
                }
            }
        }
    }


    void lightOnTime()
    {
        lightObj.SetActive(true);
    }
    void resetCamAni()
    {
        playerCamAni.enabled = false;
    }
}
