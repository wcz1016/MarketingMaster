using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundEffectAudioSource;
    public AudioSource BGMAudioSource;
    public AudioClip cardAppear, cardSel;
    public AudioClip cheer;
    public AudioClip gameBegin, winGame;
    public AudioClip BGM1, BGM2;
    public static SoundManager Instance;

    private void Awake() {
        if(Instance == null)
            Instance = this;
    }

    public void CardAppearPlay()
    {
        SoundEffectAudioSource.PlayOneShot(cardAppear);
    }

    public void CardSelPlay()
    {
        SoundEffectAudioSource.PlayOneShot(cardSel);
    }

    public void CheerPlay()
    {
        SoundEffectAudioSource.PlayOneShot(cheer);
    }

    public void GameBeginPlay()
    {
        SoundEffectAudioSource.PlayOneShot(gameBegin);
    }

    public void WinGamePlay()
    {
        SoundEffectAudioSource.PlayOneShot(winGame);
    }

    public void ChangeBGM(){
        BGMAudioSource.Stop();
        BGMAudioSource.clip = BGM2;
        BGMAudioSource.Play();
    }
}
