using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject pnl_Main;
    [SerializeField] GameObject pnl_Config;

    [SerializeField] AudioClip menuSong;
    [SerializeField] AudioClip btnSFX;


    void Start()
    {
        AudioManager.instance.PlaySong(menuSong, true);
        SetupLayouts();
    }

    private void SetupLayouts()
    {
        pnl_Main.SetActive(true);
        pnl_Config.SetActive(false);
    }


    public void PlayGame()
    {
        AudioManager.instance.PlaySFX(btnSFX);
        SceneController.instance.LoadLevel(1);
    }

    public void OpenSettings()
    {
        AudioManager.instance.PlaySFX(btnSFX);
        pnl_Main.SetActive(false);
        pnl_Config.SetActive(true);
    }

    public void CloseSettings()
    {
        AudioManager.instance.PlaySFX(btnSFX);
        pnl_Main.SetActive(true);
        pnl_Config.SetActive(false);
    }
}
