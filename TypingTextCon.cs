using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    TutorialStageManger tutorialStageMangerScript;
    [SerializeField]
    Text text;
    [SerializeField]
    Text detailText;
    [HideInInspector]
    public TextState textState = TextState.textReady;




     string[] tutorialText = new string[20];

     string[] tutorialDetailText = new string[20];


    private void Start()
    {
        tutorialDetailText[0] = "���׹̳��Դϴ�."+ "\n" +"���ݹ� ������� �Ҹ�Ǹ� �� ����ϸ� ���̻� ���� �� ������ �ൿ�� �� �� �����ϴ�. ���� �ð��� ������ ȸ���� �˴ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[1] = "�÷��̾��� ü�¹��Դϴ�." + "\n" + " ������ ������ �����ϸ�, 0�� �Ǹ� Ž�� ���а� �˴ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[2] = "�÷��̾��� ���ֹ��Դϴ�." + "\n" + " ������ �ްų� ���������� �̵��ϰų� Ȥ�� �繰�� ��ĥ ��� ���ְ� �����մϴ�" + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[3] = "�÷��̾��� ���� �����Դϴ�. " + "\n" + "���ֹٰ� �� ä������ ������ �ö󰡸� �÷��̾�� ������� ����ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[4] = "�÷��̾��� ��ȭ�����Դϴ�." + "\n" + " ���󿡼� �÷��̾��� ������ �ø� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[5] = "�÷��̾��� ��ȭ�Դϴ�." + "\n" + "���͸� ���� ���� ���� �����մϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[6] = "���� �÷��̾��� �����̹����Դϴ�." + "\n" + "���콺 ������Ŭ������ �ٲ� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[7] = "����� ���� ���� ���⿡ ���� �����Դϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[8] = "�ϴ��� ���� �÷��̾��� �����Դϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[9] = "A,D ��ư�� ���� ���� �÷��̾��� ����� �ٲ� ��Ҹ� �� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialDetailText[10] = "ENTER ��ư�� ���� ���⸦ ���� �� �ֽ��ϴ�." + "\n" + "ESC ��ư�� ������ �ش� â�� ���� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
      
        tutorialText[0] = "�ȳ��ϼ���, ���丮���� �����ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialText[1] = "�켱 UIâ�� ���� ������ �ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialText[2] = "�̵��� ���� ������ �ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialText[3] = "W ��ư�� ������ ������ �̵��մϴ�." + "\n" + "W ��ư�� �Է��� �ּ���.";
        tutorialText[4] = "A ��ư�� ������ �������� �̵��մϴ�." + "\n" + "A ��ư�� �Է��� �ּ���.";
        tutorialText[5] = "D ��ư�� ������ ���������� �̵��մϴ�." + "\n" + "D ��ư�� �Է��� �ּ���.";
        tutorialText[6] = "S ��ư�� ������ �ڷ� �̵��մϴ�." + "\n" + "S ��ư�� �Է��� �ּ���.";
        tutorialText[7] = "W,A,S,D ��ư�� Space ��ư�� ���� ������ ȸ�ǰ� ������ �����⸦ �����մϴ�." + "\n" + "ȸ�Ǹ� �����ϸ� ���ο� ����� ������ ���¹̳��� ȸ���մϴ�." + "\n" + "�����⸦ ���ּ���";
        tutorialText[8] = "���콺 ���� Ŭ���� �ϸ� ������ �����մϴ�." + "\n" + "������ �ִ� 3�� ���� ��� �� �� �ֽ��ϴ�." + "\n" + "���콺 ���� Ŭ���� ���ּ���";
        tutorialText[9] = "���� ���� ������ �ǽ��ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.";
        tutorialText[10] = "���� ���̸� ���� Ȯ���� ���Ⱑ ��ӵ˴ϴ�." + "\n" + "���⿡ �ٰ����� E�� ������ ���⸦ ���� �� �ֽ��ϴ�." + "\n" + "�ƹ�Ű�� �Է��� �ּ���.";
        tutorialText[11] = "I��ư�� ������ �÷��̾��� �κ��丮â�� �� �� �ֽ��ϴ�." + "\n" + "I��ư�� �Է��� �ּ���.";
        tutorialText[12] = "���� �տ��� E��ư�� ������ �÷��̾��� ��ȭ����â�� �� �� �ֽ��ϴ�." + "\n" + "�ƹ�Ű�� �Է��� �ּ���.";


        typingTextStart(0);
    }




   public void tutorialDetail(int num)
    {
        detailText.text = tutorialDetailText[num];
    }







    public void typingTextStart(int num = 0)
    {
        StartCoroutine(typingText(num));
    }

    IEnumerator typingText(int num = 0)
    {
        textState = TextState.textStart;

        for (int i = 0; i <= tutorialText[num].Length; i++)
        {
            text.text = tutorialText[num].Substring(0, i);

            yield return new WaitForSeconds(0.05f);
        }

        textState = TextState.textFin;
        StopCoroutine(typingText());
    }
}
