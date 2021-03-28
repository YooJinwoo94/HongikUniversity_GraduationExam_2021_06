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
        tutorialDetailText[0] = "스테미너입니다."+ "\n" +"공격및 구르기시 소모되며 다 사용하면 더이상 공격 및 구르기 행동을 할 수 없습니다. 일정 시간이 지나면 회복이 됩니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[1] = "플레이어의 체력바입니다." + "\n" + " 공격을 받으면 감소하며, 0이 되면 탐험 실패가 됩니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[2] = "플레이어의 저주바입니다." + "\n" + " 공격을 받거나 다음맵으로 이동하거나 혹은 재물로 받칠 경우 저주가 증가합니다" + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[3] = "플레이어의 저주 스택입니다. " + "\n" + "저주바가 다 채워지면 스택이 올라가며 플레이어에게 디버프가 생깁니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[4] = "플레이어의 강화내역입니다." + "\n" + " 석상에서 플레이어의 스탯을 올릴 수 있습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[5] = "플레이어의 재화입니다." + "\n" + "몬스터를 죽일 수록 돈이 증가합니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[6] = "현재 플레이어의 무기이미지입니다." + "\n" + "마우스 오른쪽클릭으로 바꿀 수 있습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[7] = "상단은 현재 얻은 무기에 대한 설명입니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[8] = "하단은 현재 플레이어의 무기입니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[9] = "A,D 버튼을 통해 현재 플레이어의 무기와 바꿀 장소를 고를 수 있습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialDetailText[10] = "ENTER 버튼을 통해 무기를 얻을 수 있습니다." + "\n" + "ESC 버튼을 누르면 해당 창을 나올 수 있습니다." + "\n" + "Space키를 입력해 주세요.";
      
        tutorialText[0] = "안녕하세요, 듀토리얼을 시작하겠습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialText[1] = "우선 UI창에 대해 설명을 하겠습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialText[2] = "이동에 대해 설명을 하겠습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialText[3] = "W 버튼을 누르면 앞으로 이동합니다." + "\n" + "W 버튼을 입력해 주세요.";
        tutorialText[4] = "A 버튼을 누르면 왼쪽으로 이동합니다." + "\n" + "A 버튼을 입력해 주세요.";
        tutorialText[5] = "D 버튼을 누르면 오른쪽으로 이동합니다." + "\n" + "D 버튼을 입력해 주세요.";
        tutorialText[6] = "S 버튼을 누르면 뒤로 이동합니다." + "\n" + "S 버튼을 입력해 주세요.";
        tutorialText[7] = "W,A,S,D 버튼과 Space 버튼을 같이 누르면 회피가 가능한 구르기를 시전합니다." + "\n" + "회피를 성공하면 슬로우 모션이 나오며 스태미나를 회복합니다." + "\n" + "구르기를 해주세요";
        tutorialText[8] = "마우스 왼쪽 클릭을 하면 공격이 가능합니다." + "\n" + "공격은 최대 3번 연속 사용 할 수 있습니다." + "\n" + "마우스 왼쪽 클릭을 해주세요";
        tutorialText[9] = "이제 모의 전투를 실시하겠습니다." + "\n" + "Space키를 입력해 주세요.";
        tutorialText[10] = "적을 죽이면 일정 확률로 무기가 드롭됩니다." + "\n" + "무기에 다가가서 E를 누르면 무기를 얻을 수 있습니다." + "\n" + "아무키나 입력해 주세요.";
        tutorialText[11] = "I버튼을 누르면 플레이어의 인벤토리창을 열 수 있습니다." + "\n" + "I버튼을 입력해 주세요.";
        tutorialText[12] = "석상 앞에서 E버튼을 누르면 플레이어의 강화선택창을 열 수 있습니다." + "\n" + "아무키나 입력해 주세요.";


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
