using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] playerAttackAudioClip;
    public AudioClip[] playerWalkAudioClip;
    AudioSource audioSource;
    int playerWalkCount;

    private static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
    private void Awake()
    {
        playerWalkCount = 0;
        audioSource = GetComponent<AudioSource>();
        instance = this;
        if (null == instance)
        {
            instance = this;
        }
    }

    public void playerAttackSound(int count)
    {
        audioSource.volume = 0.151f;
        audioSource.PlayOneShot(playerAttackAudioClip[count]);
    }

    

    public void playerWalkSound()
    {
        if (audioSource.isPlaying != false) return;
        switch (playerWalkCount)
        {
            case 0:
                audioSource.PlayOneShot(playerWalkAudioClip[playerWalkCount]);
                break;
            case 1:
                audioSource.PlayOneShot(playerWalkAudioClip[playerWalkCount]);
                break;
        }
        if (playerWalkCount >= 1) playerWalkCount = 0;
        playerWalkCount++;
    }
}
