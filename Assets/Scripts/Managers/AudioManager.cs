using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SFXType { JUMP, DAMAGE, DEATH };
public class AudioManager : MonoBehaviour
{
    public static AudioManager instanceAudioManager;
    public AudioSource musica, SFX, uiSource;

    public AudioClip[] musicCollection;
    public AudioClip[] sfxCollection;
    public AudioClip[] uiCollection;
    public AudioMixer mixer;
    public Slider sliderVolMusic, sliderVolSFX;
    // Start is called before the first frame update
    void Awake()
    {
        instanceAudioManager = this;
    }
    public void Start()
    {
        PlayMusic(0);
    }

    public void PlayUI(int tipo)
    {
        uiSource.Pause();
        switch (tipo)
        {
            case 0:
                uiSource.clip = uiCollection[0];
                break;
            case 1:
                uiSource.clip = uiCollection[1];
                break;
            case 2:
                uiSource.clip = uiCollection[2];
                break;

        }
        uiSource.Play();
    }

    public void PlaySFX(int tipo)
    {
        switch (tipo)
        {
            case 0:
                SFX.PlayOneShot(sfxCollection[0]);
                break;
            case 1:
                SFX.PlayOneShot(sfxCollection[1]);
                break;
            case 2:
                SFX.PlayOneShot(sfxCollection[2]);
                break;
        }
    }

    public void PlayMusic(int musicClip)
    {
        musica.Pause();
        musica.clip = musicCollection[musicClip];
        musica.Play();
    }

    public void SetVolume()
    {
        mixer.SetFloat("VolMusic", sliderVolMusic.value);
        mixer.SetFloat("VolSFX", sliderVolSFX.value);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Z))
    //    //{
    //    //    musica.Play();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.X))
    //    //{
    //    //    musica.Pause();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.C))
    //    //{
    //    //    musica.UnPause();
    //    //}

        
    //}
}
