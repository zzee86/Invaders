using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class ConnectManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private TextMeshProUGUI StatusText;
    [SerializeField] private TextMeshProUGUI WelcomeMessage;
    [SerializeField] private GameObject buttons;

    private const string gameVersion = "1.0";

    private string[] Maps = new string[] { "Multiplayer1", "Multiplayer2" };

   [SerializeField] private PlayfabLeaderboardManager leaderboardManager;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        ShowStatus("Connecting to Photon Servers...");

        leaderboardManager.GetLeaderboard();
    }

    private void Update()
    {
        WelcomeMessage.text = "Welcome " + PlayerPrefs.GetString("PlayerName");

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
            return; //do nothing
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
        Debug.Log(PhotonNetwork.NickName);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        ShowStatus("Creating a new room...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ShowStatus("Created room - waiting for another player.");
    }

    //Once 2 players in a room, master client changes everyone to the game scene
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            // Select random level
            string scene = Maps[Random.Range(0, 2)].ToString().Replace("'", "");
            Debug.Log("Map name " + Maps[1].ToString().Replace("'", ""));

            PhotonNetwork.LoadLevel(scene);
        }
    }

    // Disconnect servers
    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("MainMenu");
    }
}
