using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int levelMusicToPlay;

    public AudioMixerGroup musicMixer, sfxMixer;



    //private int currentTrack;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       PlayMusic(levelMusicToPlay);
    }

    
    void Update()
    {
        /*if(Input .GetKeyDown(KeyCode.M))
        {
            currentTrack++;
            PlayMusic(currentTrack);
        }*/

    }

    public void PlayMusic(int musicToPlay)
    {
        for(int i=0; i < music.Length; i++)
        {
            music[i].Stop();
        }
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }
}
