using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixer audioMixer;

    [SerializeField] AudioSource song;
    [SerializeField] AudioSource sfx;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart") != 1)
        {
            SetMasterVolume(1f);
            SetSFXVolume(0.72f);
            SetMusicVolume(0.6f);

            PlayerPrefs.SetInt("FirstStart", 1);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("Master"), 0.05f)));
            audioMixer.SetFloat("SFXVolume", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("Sfx"), 0.05f)));
            audioMixer.SetFloat("SongVolume", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("Song"), 0.05f)));
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfx.PlayOneShot(clip);
    }

    public void PlaySong(AudioClip clip, bool isLoop)
    {
        song.Stop();

        if (clip == null)
            return;

        if (isLoop)
        {
            song.clip = clip;
            song.loop = isLoop;
            song.Play();
        }
        else
        {
            song.PlayOneShot(clip);
        }
    }

    public void SetMasterVolume(float value)
    {
        float volumeDB = 60 * Mathf.Log10(Mathf.Max(value, 0.05f));
        audioMixer.SetFloat("MasterVolume", volumeDB);
        PlayerPrefs.SetFloat("Master", value);
    }

    public void SetSFXVolume(float value)
    {
        float volumeDB = 60 * Mathf.Log10(Mathf.Max(value, 0.05f));
        audioMixer.SetFloat("SFXVolume", volumeDB);
        PlayerPrefs.SetFloat("Sfx", value);
    }

    public void SetMusicVolume(float value)
    {

        float volumeDB = 60 * Mathf.Log10(Mathf.Max(value, 0.05f));
        audioMixer.SetFloat("SongVolume", volumeDB);
        PlayerPrefs.SetFloat("Song", value);
    }
}
