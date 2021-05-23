using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public string sceneName;
    public float hp;
    public float curseBar;
    public int curseCount;

    public int coinCount;

    public float[] playerPos = new float[3];

    public bool[,] playerQuest = new bool [10,10];

    public string[] playersWeapon  = new string [2];

    public int[] playersPowerCount = new int[3];
}
