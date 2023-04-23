using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class ConnectManager : MonoBehaviourPunCallbacks
{
    private const string gameVersion = "1.0";

    private string[] mapSelection = new string[] { "Multiplayer1", "Multiplayer2" };

    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private TextMeshProUGUI StatusText;
    [SerializeField] private TextMeshProUGUI WelcomeMessage;
    [SerializeField] private GameObject buttons;

    [SerializeField] private PlayfabLeaderboardManager leaderboardManager;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        ShowStatus("Connecting to Photon Servers...");
        leaderboardManager.GetLeaderboard();
        WelcomeMessage.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
    }

    private void Update()
    {
        if (PhotonNetwork.NickName == "")
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
    }

    public void StartGame()
    {
        buttons.SetActive(false);
        ShowStatus("Connecting...");
        Time.timeScale = 1;
        if (PhotonNetwork.IsConnected)
        {
            ShowStatus("Joining Random Room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            ShowStatus("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    private void ShowStatus(string text)
    {
        if (StatusText == null)
        {
            return;
        }

        StatusText.gameObject.SetActive(true);
        StatusText.text = text;
    }

    public override void OnConnectedToMaster()
    {
        ShowStatus("Connected to Servers");
        StartGameButton.SetActive(true);
        WelcomeMessage.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
        PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ShowStatus("Created room - waiting for another player.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectPanel.SetActive(true);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ShowStatus("Creating a new room...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    // 2 players in a room, then the scene will change 
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            // Select random level from the options available
            string scene = mapSelection[Random.Range(0, 2)].ToString().Replace("'", "");
            PhotonNetwork.LoadLevel(scene);
        }
    }

    // Disconnect servers
    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("MainMenu");
    }
}
