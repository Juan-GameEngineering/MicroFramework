using UnityEngine;
using UnityEngine.Audio;

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
            SetSFXVolume(0.7f);
            SetMusicVolume(0.5f);

            PlayerPrefs.SetInt("FirstStart", 1);
        }
        else
        {
            audioMixer.SetFloat("Master", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("MasterVolume"), 0.05f)));
            audioMixer.SetFloat("SFX", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("SfxVolume"), 0.05f)));
            audioMixer.SetFloat("Song", 60 * Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat("SongVolume"), 0.05f)));
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
        audioMixer.SetFloat("Master", volumeDB);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        float volumeDB = 60 * Mathf.Log10(Mathf.Max(value, 0.05f));
        audioMixer.SetFloat("SFX", volumeDB);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        float volumeDB = 60 * Mathf.Log10(Mathf.Max(value, 0.05f));
        audioMixer.SetFloat("Song", volumeDB);
        PlayerPrefs.SetFloat("SongVolume", value);
    }
}
