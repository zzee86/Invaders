using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class PlayerManagerMultiplayer : MonoBehaviourPunCallbacks, IOnEventCallback
{

    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; //Send event to all clients

    PhotonView pv;
    GameObject character;

    int kills;
    int deaths;

    private const int WINNER = 3;
    private string winner;

    private string message = "";

    bool leaderboardCall = false;

    int numberOfPlayer;

    void Awake()
    {
        pv = GetComponent<PhotonView>();

    }
    void Start()
    {
        if (pv.IsMine)
        {
            CreateController();
        }


        Debug.Log("Player names: " + PlayerPrefs.GetString("PlayerName"));
    }

    void CreateController()
    {
        Transform spawnPoints = SpawnManager.current.getSpawnPoint();
        character = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPoints.position, spawnPoints.rotation, 0, new object[] { pv.ViewID });
    }

    public void playerDeath()
    {
        PhotonNetwork.Destroy(character);
        CreateController();

        deaths++;

        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    public void GetKill()
    {
        pv.RPC(nameof(RPC_GetKill), pv.Owner);
    }
    [PunRPC]
    void RPC_GetKill()
    {
        kills++;
        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    public static PlayerManagerMultiplayer Find(Player player)
    {
        return FindObjectsOfType<PlayerManagerMultiplayer>().SingleOrDefault(x => x.pv.Owner == player);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == WINNER)
        {
            // Previous version not working
            // object[] data = (object[])photonEvent.CustomData;
            // winner = data[1].ToString().Remove(0, 4).Replace("'", "");

            var data = photonEvent.CustomData;

            winner = (string)data;
            Debug.Log("should be corrent player " + winner.ToString());



            if (winner == PhotonNetwork.NickName)
            {
                Debug.Log("It matches! " + "winner = " + winner);
                Victory(winner);
                //      MultiplayerGameManager.instance.updateLeaderboard();

                if (!pv.IsMine)
                    return;

                CheckLeaderboardCall();
            }
        }
    }

    // Did try with RPC
    void CheckLeaderboardCall()
    {
        if (!leaderboardCall)
        {
            leaderboardCall = true;
            MultiplayerGameManager.instance.updateLeaderboard();
            Debug.Log("player manager: leaderboard sent");
        }
        else if (leaderboardCall)
        {
            Debug.Log("update leaderboard already called");
        }
    }


    private void Victory(string name)
    {
        pv.RPC(nameof(RPC_Victory), RpcTarget.All, name);
        //  MainMenuButton.SetActive(true);
        //  winSound.Play();
        // QuitButton.SetActive(true);
    }

    [PunRPC]
    void RPC_Victory(string name)
    {
        MultiplayerGameManager.instance.DisplayerWinCanvas(name);
    }
    //Needed for event calls
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        numberOfPlayer--;
        Debug.Log("display screen");
        Victory(PhotonNetwork.LocalPlayer.NickName);

        if (!pv.IsMine)
            return;

        CheckLeaderboardCall();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        numberOfPlayer++;
    }

}
