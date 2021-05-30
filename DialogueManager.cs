using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    GameObject[] cinemachineCam;


    GameObject lightObj;
    int count = 0;


    void uiOnCamCoolTime()
    {
        cinemachineCam[0].SetActive(true);
        CinemachineVirtualCamera virCam = cinemachineCam[0].GetComponent<CinemachineVirtualCamera>();
        virCam.Priority = 20;
    }

    public void uiOn(int num = 0)
    {      
        cinemachineCam[0] = GameObject.Find("Start_Stage_Cam").transform.Find("CMvcam1").gameObject;
        Invoke("uiOnCamCoolTime", 0.1f);

        playerAni.playerAniWait();
        dialogueState = DialogueState.DialogueStart;

        typingTextConScript.typingTextStart(count);

        dialogueUISet[0].SetActive(true);
        dialogueUISet[1].SetActive(true);
        dialogueUISet[2].SetActive(true);

        playerKeyButtonObj[4].SetActive(true);
        playerKeyButtonObj[5].SetActive(true);
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
                        playerKeyButtonObj[5].SetActive(false);
                        count = 0;

                        GameObject camObj = GameObject.Find("Start_Stage_Cam");
                        camObj.SetActive(false);

                        lightObj = GameObject.Find("Door_Light").transform.Find("Door_Open_Light").gameObject;
                        Invoke("lightOnTime", 1f);
                        break;

                    default:
                        typingTextConScript.typingTextStart(count);
                        count++;
                        break;
                    }
                    return;
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
                        playerKeyButtonObj[5].SetActive(false);
                        count = 0;

                        GameObject camObj = GameObject.Find("Start_Stage_Cam");
                        camObj.SetActive(false);

                        GameObject dwaf = GameObject.Find("Dwarf_ShopOwner");
                        dwaf.tag = "Dwarf_ShopOwner";
                        break;

                    default:
                        Debug.Log("aa");
                        typingTextConScript.typingTextStart(count);
                        count++;
                        break;
                }
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (questManagerScript.isQuestEnd[1, 0] == false)
            {
                dialogueState = DialogueState.DialogueEnd;

                dialogueUISet[0].SetActive(false);
                dialogueUISet[1].SetActive(false);
                dialogueUISet[2].SetActive(false);

                playerKeyButtonObj[4].SetActive(false);
                playerKeyButtonAni[4].enabled = false;
                playerKeyButtonObj[5].SetActive(false);
                count = 0;

                GameObject camObj = GameObject.Find("Start_Stage_Cam");
                camObj.SetActive(false);

                lightObj = GameObject.Find("Door_Light").transform.Find("Door_Open_Light").gameObject;
                Invoke("lightOnTime", 1f);
                return;
            }

            if (questManagerScript.isQuestEnd[1, 0] == true)
            {
                dialogueState = DialogueState.DialogueEnd;

                dialogueUISet[0].SetActive(false);
                dialogueUISet[1].SetActive(false);
                dialogueUISet[2].SetActive(false);

                playerKeyButtonObj[4].SetActive(false);
                playerKeyButtonAni[4].enabled = false;
                playerKeyButtonObj[5].SetActive(false);
                count = 0;

                GameObject camObj = GameObject.Find("Start_Stage_Cam");
                camObj.SetActive(false);

                GameObject dwaf = GameObject.Find("Dwarf_ShopOwner");
                dwaf.tag = "Dwarf_ShopOwner";
                return;
            }
        }
    }


    void lightOnTime()
    {
        lightObj.SetActive(true);
    }
}
