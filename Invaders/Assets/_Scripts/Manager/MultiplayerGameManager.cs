using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;



public class MultiplayerGameManager : MonoBehaviourPunCallbacks
{

    public static MultiplayerGameManager instance;
    Transform spawnPoint;

    PhotonView pv;
    int kills;
    PauseMenuMultiplayer pauseMenuMultiplayer;


    public TextMeshProUGUI winText;


    [Header("Leaderbaord")]
    [SerializeField] private GameObject SendLeaderboard;
    private PlayfabLeaderboardManager LeaderboardManager;
    private void Awake()
    {
        //Singleton - If RoomManager exists, delete it
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //If only one, make this the manager
        instance = this;
        pv = GetComponent<PhotonView>();
        LeaderboardManager = SendLeaderboard.GetComponent<PlayfabLeaderboardManager>();

    }
    public void updateLeaderboard()
    {
            LeaderboardManager.SendLeaderboard(1);        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    //If scene has loaded, Create a new playerManager object for each player - sceneIndex 2 refers to map 1 (sceneIndex is the scene order in build settings)
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManagerMultiplayer"), Vector3.zero, Quaternion.identity, 0);
    }
    public static void RegisterPauseMenu(PauseMenuMultiplayer pause)
    {
        //If there is no current Game Manager, exit
        if (instance == null)
            return;

        instance.pauseMenuMultiplayer = pause;

    }
        public void DisplayerWinCanvas(string name)
    {
        winText.SetText(name + "has won");
        Debug.Log(name + "has won");

    }

}