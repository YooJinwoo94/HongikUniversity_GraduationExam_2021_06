using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum StageState
{
    state01,
    state02,
    state03,
    state04,
    state05,
    state06,
    state07,
    state08,
    state09,
    state10,
}



public class StageManager : MonoBehaviour
{
    //한 던전내  1개의 스태이지 매니저를 가진다 
    // enum으로 각각의 스태이지를 구분한다.
    // 

    StageState StageState;

    [SerializeField]
    BoxCollider[] nextStageCollider;
    [SerializeField]
    Transform[] transformPos;
    [SerializeField]
    Transform playerPos;

    //[SerializeField]
    // 싱글턴 형식
    private static StageManager instance = null ;




    void Start()
    {
        StageState = StageState.state01;
        instance = this;

        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public static StageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }







    void moveTo(int StageState)
    {     
        playerPos.position = transformPos[StageState].position;
        Debug.Log("stagemove");
    }



    public void checkPlayerStageAndWrap()
    {
        switch (StageState)
        {
            case StageState.state01:
                moveTo(0);
                break;
            case StageState.state02:
                moveTo(1);
                break;
            case StageState.state03:
                moveTo(2);
                break;
            case StageState.state04:
                break;
            case StageState.state05:
                break;
            case StageState.state06:
                break;
            case StageState.state07:
                break;
            case StageState.state08:
                break;
            case StageState.state09:
                break;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

       switch (StageState)
        {
            case StageState.state01 :
                moveTo(0);
                Debug.Log("ad");
                    break;
            case StageState.state02:
                moveTo(1);
                break;
            case StageState.state03:
                moveTo(2);
                break;
            case StageState.state04:
                break;
            case StageState.state05:
                break;
            case StageState.state06:
                break;
            case StageState.state07:
                break;
            case StageState.state08:
                break;
            case StageState.state09:
                break;
        }
    }
}
