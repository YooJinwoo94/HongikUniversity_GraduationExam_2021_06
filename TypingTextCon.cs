using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[HideInInspector]
public enum TextState
{
    textReady,
    textStart,
    textFin
}


public class TypingTextCon : MonoBehaviour
{
    [SerializeField]
    QuestManager questManagerScript;
    [SerializeField]
    TutorialStageManger tutorialStageMangerScript;
    [SerializeField]
   public Text text;
    [SerializeField]
    Text detailText;
    [HideInInspector]
    public TextState textState = TextState.textReady;






    public void tutorialDetail(int num)
    {
        detailText.text = TextDataBase.tutorialDetailText[num];
    }



    public void typingTextStart(int num = 0)
    {
        StartCoroutine(typingText(num));
    }

    IEnumerator typingText(int num = 0)
    {
        textState = TextState.textStart;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Tutorial_Scene":
                for (int i = 0; i <= TextDataBase.tutorialText[num].Length; i++)
                {
                    text.text = null;
                    text.text = TextDataBase.tutorialText[num].Substring(0, i);

                    yield return new WaitForSeconds(0.05f);
                }
                break;

            case "Start_Stage":

                if (questManagerScript.isQuestEnd[1, 0] == false)
                {
                    for (int i = 0; i <= TextDataBase.startStageTextQuest01Start[num].Length; i++)
                    {
                        text.text = null;
                        text.text = TextDataBase.startStageTextQuest01Start[num].Substring(0, i);

                        yield return new WaitForSeconds(0.05f);
                    }
                }
                if (questManagerScript.isQuestEnd[1, 0] == true && questManagerScript.isQuestEnd[2, 0] == false)
                {
                    for (int i = 0; i <= TextDataBase.startStageTextQuest02Start[num].Length; i++)
                    {
                        text.text = null;
                        text.text = TextDataBase.startStageTextQuest02Start[num].Substring(0, i);

                        yield return new WaitForSeconds(0.05f);
                    }
                }
                break;
        }
       

        textState = TextState.textFin;
        StopCoroutine(typingText());
    }
}
