using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDataBase : MonoBehaviour
{
    [HideInInspector]
   public static string[] tutorialDetailText = new string[]
        {
          "���׹̳��Դϴ�." + "\n" + "���ݹ� ������� �Ҹ�Ǹ� �� ����ϸ� ���̻� ���� �� ������ �ൿ�� �� �� �����ϴ�. ���� �ð��� ������ ȸ���� �˴ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "�÷��̾��� ü�¹��Դϴ�." + "\n" + " ������ ������ �����ϸ�, 0�� �Ǹ� Ž�� ���а� �˴ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "�÷��̾��� ���ֹ��Դϴ�." + "\n" + " ������ �ްų� ���������� �̵��ϰų� Ȥ�� �繰�� ��ĥ ��� ���ְ� �����մϴ�" + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "�÷��̾��� ���� �����Դϴ�. " + "\n" + "���ֹٰ� �� ä������ ������ �ö󰡸� �÷��̾�� ������� ����ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "�÷��̾��� ��ȭ�����Դϴ�." + "\n" + " ���󿡼� �÷��̾��� ������ �ø� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          //===========================================================================================================================================
          "�÷��̾��� ��ȭ�Դϴ�." + "\n" + "���͸� ���� ���� ���� �����մϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "���� �÷��̾��� �����̹����Դϴ�." + "\n" + "���콺 ������Ŭ������ �ٲ� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "����� ���� ���� ���⿡ ���� �����Դϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "�ϴ��� ���� �÷��̾��� �����Դϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
          "A,D ��ư�� ���� ���� �÷��̾��� ����� �ٲ� ��Ҹ� ���� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
           //===========================================================================================================================================
           "ENTER ��ư�� ���� ���⸦ ���� �� �ֽ��ϴ�." + "\n" + "ESC ��ư�� ������ �ش� â�� ���� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
        };
    [HideInInspector]
    public static string[] tutorialText  = new string[]
        {
         "�ȳ��ϼ���, Ʃ�丮���� �����ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
         "�켱 UIâ�� ���� ������ �ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
         "�̵��� ���� ������ �ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
         "W ��ư�� ������ ������ �̵��մϴ�." + "\n" + "W ��ư�� �Է��� �ּ���.",
         "A ��ư�� ������ �������� �̵��մϴ�." + "\n" + "A ��ư�� �Է��� �ּ���.",
         //===========================================================================================================================================
         "D ��ư�� ������ ���������� �̵��մϴ�." + "\n" + "D ��ư�� �Է��� �ּ���.",
         "S ��ư�� ������ �ڷ� �̵��մϴ�." + "\n" + "S ��ư�� �Է��� �ּ���.",
         "W,A,S,D ��ư�� Space ��ư�� ���� ������ ȸ�ǰ� ������ �����⸦ �����մϴ�." + "\n" + "ȸ�Ǹ� �����ϸ� ���ο� ����� ������ ���¹̳��� ȸ���մϴ�." + "\n" + "�����⸦ ���ּ���",
         "���콺 ���� Ŭ���� �ϸ� ������ �����մϴ�." + "\n" + "������ �ִ� 3�� ���� ��� �� �� �ֽ��ϴ�." + "\n" + "���콺 ���� Ŭ���� ���ּ���",
         "���� ���� ������ �ǽ��ϰڽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
         //===========================================================================================================================================
         "���� ���̸� ���� Ȯ���� ���Ⱑ ��ӵ˴ϴ�." + "\n" + "���⿡ �ٰ����� E�� ������ ���⸦ ���� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
         "I��ư�� ������ �÷��̾��� �κ��丮â�� �� �� �ֽ��ϴ�." + "\n" + "I��ư�� �Է��� �ּ���.",
         "���� �տ��� E��ư�� ������ �÷��̾��� ��ȭ����â�� �� �� �ֽ��ϴ�." + "\n" + "SpaceŰ�� �Է��� �ּ���.",
        }
        ;

    [HideInInspector]
    public static string[] startStageTextQuest01Start = new string[]
       {
        "����ð�... ���⿡ ����� ���°� �������̱�...",
        "������ ���� ã�� �ִٰ�?",
        "�� �˿����� ������ �˰� ������... �� ��Ź�� �� ����ָ� ��� �ִ��� �˷� �� �� �� ����",
        "�ٸ��� �ƴ϶� �����ֺ��� ������ ���� ���͵��� �����Ѱ� ����...",
        "�׳�� ������ ��ó ���꿡 ���ϴ� �� ģ���鵵 ���ƾ�",
        "�׳���� ó���� �ָ� �˿� ���� ������ �˷�����..."
       };

    [HideInInspector]
    public static string[] startStageTextQuest02Start = new string[]
   {
       "��! ���ƿԱ���!",
       "�ȿ� �־��� �༮�� ó���߳�?",
       "�׷��׷� �׷� ������ �˿� ���� ������ �˷�����...",
       "�� ������ ���� ���� ���ʿ� �ִ� ������ ������ ��� �ִٰ� �ϴ����",
       "���� ���� �̸� ��������� ���� ã�ƺ���",
       "�� �׸��� �������� ���� ���� ������ ������ �� �� ������ �������� ���� �ϰ� ������ ���� �ɾ���"
   };
}