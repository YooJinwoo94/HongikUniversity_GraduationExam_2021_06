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
    TypingTextCon typingTextConScript;
    [SerializeField]
    GameObject[] dialogueUISet;
    [SerializeField]
    Animator[] playerKeyButtonAni;
    [SerializeField]
    GameObject[] playerKeyButtonObj;






    public void uiOn()
    {
        typingTextConScript.typingTextStart();

        dialogueUISet[0].SetActive(true);
        dialogueUISet[1].SetActive(true);

        playerKeyButtonObj[4].SetActive(true);
        playerKeyButtonAni[4].enabled = true;
    }
}
