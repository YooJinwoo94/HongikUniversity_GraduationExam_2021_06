using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManger : MonoBehaviour
{
    [HideInInspector]
    public static int num = 0;

    [SerializeField]
    GameObject playerObj;

    [SerializeField]
    PlayerInputScript playerInputScript;
    [SerializeField]
    PlayerUISeletManger playerUISelectManagerScript;


    [SerializeField]
    Transform playerPos;

    [SerializeField]
    PlayerWeaponInGameUI weaponInGameUIScript;
    [SerializeField]
    QuestManager questManagerScript;
    [SerializeField]
    PlayerHpManager playerHpManagerScript;
    [SerializeField]
    PlayerCurseUI playerCurseUIScript;
    [SerializeField]
    PlayerHaveWeaponUINo4 playerHaveWeaponUINo4Script;
    [SerializeField]
    PlayerHavePowerUINo3 playerHavePowerUINo3Script;
    [SerializeField]
    PlayerPowerDataBase playerPowerDataBaseScript;
    [SerializeField]
    CoinManager coinMangerScript;
    PlayerWeaponObjCon playerWeaponObjConScript;
    string sceneName;






    public void saveThis()
    {
        SaveData save = new SaveData();
        getHierachy();
        save.hp = playerHpManagerScript.hp.fillAmount;
        save.curseBar = playerCurseUIScript.playerCurseBar.fillAmount;
        save.curseCount = playerCurseUIScript.playerCurseCount;
        save.coinCount = coinMangerScript.coinCount;
        save.playerPos[0] = playerPos.position.x;
        save.playerPos[1] = playerPos.position.y;
        save.playerPos[2] = playerPos.position.z;
        for (int i = 0; i< 2; i++) save.playersWeapon[i] = playerHaveWeaponUINo4Script.playerWeaponName[i].text;
        for (int i = 0; i < 3; i++) save.playersPowerCount[i] = playerPowerDataBaseScript.playersPowerNum[i];

        for (int i = 0; i< 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                save.playerQuest[i, j] = questManagerScript.isQuestEnd[i, j];
            }
        }

        save.sceneName = SceneManager.GetActiveScene().name;

        SaveDataManager.save(save);
    }

    public void loadThis()
    {
        SaveData load = SaveDataManager.load();
        getHierachy();

        playerHpManagerScript.hp.fillAmount = load.hp;
        playerCurseUIScript.playerCurseBar.fillAmount = load.curseBar;
        playerCurseUIScript.playerCurseCount = load.curseCount;

        for (int i = 0; i < playerCurseUIScript.playerCurseCount; i++)
        {
            playerCurseUIScript.curseSkullAni[i].SetBool("curseStart", true);
            playerCurseUIScript.playerCurseImage[i].color = new Color(123 / 255f, 58 / 255f, 214 / 255f);
        }

        playerPos.position = new Vector3(load.playerPos[0], load.playerPos[1], load.playerPos[2]);

        coinMangerScript.coinCount = load.coinCount;
        coinMangerScript.coinCountToUi(coinMangerScript.coinCount , true);
        coinMangerScript.loadCost();

        //인벤창에서의 내 무기를 가져옴
        for (int i = 0; i < 2; i++)
        {
            playerHaveWeaponUINo4Script.playerWeaponName[i].text = load.playersWeapon[i];
            playerHaveWeaponUINo4Script.ifLoadData();
        }

        //강화내역 가져오기
        for (int i = 0; i < 3; i++)
        {
            weaponInGameUIScript.playersPowerText[i].text = load.playersPowerCount[i].ToString();
            playerHavePowerUINo3Script.playersPowerCount[i].text = load.playersPowerCount[i].ToString();
        }
        
        //퀘스트 내용 가져오기
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                questManagerScript.isQuestEnd[i, j] = load.playerQuest[i, j];
            }
        }

        //플레이어의 무기 Obj를 변경
        playerWeaponObjConScript.changePlayerWeaponObj();
    }

    public void loadScene()
    {
        SaveData load = SaveDataManager.load();
        num = 1;   
        LoadingManager.loadScene("Start_Stage");
    }

    void getHierachy()
    {
        playerPos = GameObject.Find("Player").transform;

        weaponInGameUIScript = GameObject.Find("PlayerUIManager").GetComponent<PlayerWeaponInGameUI>();
        coinMangerScript = GameObject.Find("PlayerUIManager").GetComponent<CoinManager>();
        playerHavePowerUINo3Script = GameObject.Find("PlayerUIManager").GetComponent<PlayerHavePowerUINo3>();
        playerHaveWeaponUINo4Script = GameObject.Find("PlayerUIManager").GetComponent<PlayerHaveWeaponUINo4>();
        playerCurseUIScript = GameObject.Find("PlayerUIManager").GetComponent<PlayerCurseUI>();
        playerHpManagerScript = GameObject.Find("PlayerUIManager").GetComponent<PlayerHpManager>();

        questManagerScript = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        playerPowerDataBaseScript = GameObject.Find("PlayerDataBase").GetComponent<PlayerPowerDataBase>();
        playerWeaponObjConScript = GameObject.Find("Player").GetComponent<PlayerWeaponObjCon>();
    }

    public  void turnOffUI()
    {
        playerInputScript.playerUIState = PlayerUI.getSaveUiOff;
        playerUISelectManagerScript.playerSave.SetActive(false);
    }
 

    private void OnTriggerEnter(Collider other)
    {
        //공격당함 1
        if (other.gameObject.tag == "Player")
        {
            if (num == 1)
            {
                BoxCollider col = this.gameObject.GetComponent<BoxCollider>();
                col.enabled = false;
                loadThis();
                num = 0;

                this.enabled = false;
                return;
            }
            else return;
        }
    }
}



