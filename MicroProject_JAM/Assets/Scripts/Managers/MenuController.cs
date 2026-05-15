/*using Photon.Pun;*/
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*using UnityEngine.UIElements;*/

public class MenuController : MonoBehaviour
{

    //[SerializeField] GameObject pnlMainScene;
    //[SerializeField] GameObject pnlMainSettings;
    //[SerializeField] GameObject btn_Config;
    //[SerializeField] GameObject pnlAudioManager;

    [SerializeField] GameObject btn_StartGameMP;

    [SerializeField] GameObject pnl_Main;
    [SerializeField] GameObject pnl_Config;
    [SerializeField] GameObject pnl_Coop;
    [SerializeField] GameObject pnl_Connecting;
    [SerializeField] GameObject pnl_CreateJoinRoom;
    [SerializeField] GameObject pnl_PreGameRoom;
    [SerializeField] TMP_InputField txt_RoomName;
    [SerializeField] TMP_InputField txt_Nickname;
    [SerializeField] TMP_Text txt_PlayersList;
    /*[SerializeField] TMP_Text txt_Warning;*/


    [SerializeField] GameObject txt_WarningExtras;
    [SerializeField] GameObject pnl_alphaScene;
    [SerializeField] GameObject pnl_agilityMode;


    [SerializeField] AudioClip menuSong;


    private void OnEnable()
    {
        NetworkManager.LobbyJoined += OpenPnlCreateJoinRoom;
        NetworkManager.RoomJoined += OpenPnlPreGameRoom;
        NetworkManager.PlayersUpdated += UpdateListPlayer;
    }

    private void OnDisable()
    {
        NetworkManager.LobbyJoined -= OpenPnlCreateJoinRoom;
        NetworkManager.RoomJoined -= OpenPnlPreGameRoom;
        NetworkManager.PlayersUpdated -= UpdateListPlayer;
    }

    void Start()
    {
        GameManager.instance.SetStartGame(false);
        AudioManager.instance.PlaySong(menuSong, true);
        SetupLayouts();

        DisconnectFromNetwork();
    }

    private void SetupLayouts()
    {
        //pnlMainScene.SetActive(true);
        //pnlMainSettings.SetActive(false);
        //btn_Config.SetActive(true);
        pnl_Main.SetActive(true);
        pnl_Config.SetActive(false);
        pnl_Coop.SetActive(false);

        pnl_Connecting.SetActive(false);
        pnl_CreateJoinRoom.SetActive(false);
        pnl_PreGameRoom.SetActive(false);

        /*txt_WarningExtras.SetActive(true);*/
        /*pnl_alphaScene.SetActive(false);
        pnl_agilityMode.SetActive(false);*/
    }


    public void PlayGame()
    {
        /*AudioManager.instance.PlaySong(null, true);*/
        //SceneManager.LoadScene(1);

        /*SceneController.instance.NextLevel(1);*/

        PhotonNetwork.OfflineMode = true;
        GameManager.instance.SetSinglePlayer();
    }

    public void OpenSettings()
    {
        //pnlMainScene.SetActive(false);
        //pnlMainSettings.SetActive(true);
        //btn_Config.SetActive(false);

        pnl_Main.SetActive(false);
        pnl_Coop.SetActive(false);
        pnl_Config.SetActive(true);

        if (GameManager.instance.GetGameCompleted())
        {
            txt_WarningExtras.SetActive(false);
            pnl_alphaScene.SetActive(true);
            pnl_agilityMode.SetActive(true);
        }
        else
        {
            txt_WarningExtras.SetActive(true);
            pnl_alphaScene.SetActive(false);
            pnl_agilityMode.SetActive(false);
        }
    }

    public void Multiplayer()
    {
        pnl_Main.SetActive(false);
        pnl_Coop.SetActive(true);
        pnl_Config.SetActive(false);

        pnl_Connecting.SetActive(true);
        pnl_CreateJoinRoom.SetActive(false);
        pnl_PreGameRoom.SetActive(false);

        NetworkManager.Instance.ConnectToServer();
    }

    public void OpenPnlCreateJoinRoom()
    {
        pnl_CreateJoinRoom.SetActive(true);
        /*txt_Warning.gameObject.SetActive(false);*/
    }

    public void OpenPnlPreGameRoom()
    {
        pnl_PreGameRoom.SetActive(true);

        btn_StartGameMP.SetActive(NetworkManager.Instance.IsMasterClient());
    }

    public void UpdateListPlayer()
    {
        txt_PlayersList.text = NetworkManager.Instance.GetPlayersListRoom();
    }


    public void CreateRoom()
    {
        if (txt_RoomName.text != "" && txt_Nickname.text != "")
        {
            NetworkManager.Instance.CreateRoom(txt_RoomName.text);
            NetworkManager.Instance.SetNickname(txt_Nickname.text);
        }
        /* else
         {
             txt_Warning.gameObject.SetActive(true);
             txt_Warning.text = "Room name invalid or empty";
         }*/
    }

    public void JoinRoom()
    {
        if (txt_RoomName.text != "" && txt_Nickname.text != "")
        {
            NetworkManager.Instance.JoinRoom(txt_RoomName.text);
            NetworkManager.Instance.SetNickname(txt_Nickname.text);
        }
        /*else
        {
            txt_Warning.gameObject.SetActive(true);
            txt_Warning.text = "Room name invalid or empty";
        }*/
    }

    public void LeaveRoom()
    {
        NetworkManager.Instance.LeaveRoom();
        pnl_PreGameRoom.SetActive(false);
    }

    public void StartGameMP()
    {
        /*GameManager.instance.SetMultiplayer();*/
        NetworkManager.Instance.ChangeScene(1);
    }

    private void DisconnectFromNetwork()
    {
        if (NetworkManager.Instance.IsConnected())
            NetworkManager.Instance.DisconnectFromServer();
    }

    public void Extra_LoadAlphaScene()
    {
        /*AudioManager.instance.PlaySong(null, true);*/
        SceneController.instance.NextLevel(3);
    }

    public void Extra_LoadAgilityMode()
    {
        /*AudioManager.instance.PlaySong(null, true);*/
        SceneController.instance.NextLevel(4);
    }

    public void BackToBegin()
    {
        NetworkManager.Instance.DisconnectFromServer();
        SetupLayouts();
    }
}
