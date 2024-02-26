using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] BGMSounds;
    public AudioSource[] SFXSounds;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void StopBGM()
    {
        for (int i = 0; i < BGMSounds.Length; i++)
        {
            BGMSounds[i].Stop();
        }
    }
    public void PlaySFX(int SFX)
    {
        if (SFXSounds[SFX] != null)
            SFXSounds[SFX].Play();
    }
}
