using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2, audioSource3;
    public AudioClip cardAppear,cardDesc,cardSel,allcardSeled;
    public AudioClip cheer, laugh;
    public AudioClip gameBegin, winGame;
    public AudioClip BGM1, BGM2;
    public static SoundManager instance;

    private void Awake() {
        if(instance == null)
            instance = this;
    }
    public void CardAppearPlay()
    {
        audioSource1.PlayOneShot(cardAppear);
    }
    public void CardDescPlay()
    {
        audioSource1.PlayOneShot(cardDesc);
    }
    public void CardSelPlay()
    {
        audioSource1.PlayOneShot(cardSel);
    }
    public void CardSeledPlay()
    {
        audioSource1.PlayOneShot(allcardSeled);
    }
    public void CheerPlay()
    {
        audioSource1.PlayOneShot(cheer);
    }
    public void laughPlay()
    {
        audioSource1.PlayOneShot(laugh);
    }
    public void gameBeginPlay()
    {
        audioSource1.PlayOneShot(gameBegin);
    }
    public void winGamePlay()
    {
        audioSource1.PlayOneShot(winGame);
    }
    public void ChangeBGM(){

        audioSource2.Stop();
        audioSource3.Play();
    }
}
