using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AudioController : MonoBehaviour
{
    public Slider sliderSong;
    public Slider sliderSFX;
    public Slider sliderMaster;


    private void OnEnable()
    {
        UpdateSliders();
    }

    public void UpdateSliders()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume");
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume");
        sliderSong.value = PlayerPrefs.GetFloat("SongVolume");
    }

    public void UpdateSongVolume(float vol)
    {
        AudioManager.instance.SetMusicVolume(vol);
    }

    public void UpdateSFXVolume(float vol)
    {
        AudioManager.instance.SetSFXVolume(vol);
    }

    public void UpdateMasterVolume(float vol)
    {
        AudioManager.instance.SetMasterVolume(vol);
    }
}
