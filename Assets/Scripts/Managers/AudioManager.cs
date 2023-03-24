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
        uiSource.clip = uiCollection[tipo];
        uiSource.Play();
    }

    public void PlaySFX(int tipo)
    {
        SFX.PlayOneShot(sfxCollection[tipo]);
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

}
