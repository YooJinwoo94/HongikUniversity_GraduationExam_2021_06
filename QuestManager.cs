using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   public bool[,] isQuestEnd = new bool[10, 10];
   private static QuestManager instance = null;



    void Start()
    {
        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }
    public static QuestManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
}



