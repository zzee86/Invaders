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
    PhotonView pv;
    GameObject character;

    int kills;

    private const int WINNER = 3;
    private string winner;

    private string message = "";

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
    }
    public void GetKill()
    {
        Debug.Log("get kill method");

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

        //WINNER EVENT - SENT BY LOSING PLAYER
        if (photonEvent.Code == WINNER)
        {
            object[] data = (object[])photonEvent.CustomData;
            winner = data[1].ToString().Remove(0, 4).Replace("'", "");
            message = data[0].ToString() + " King has been killed!";

            for (int i = 0; i < data.Length; i++)
            {
                Debug.Log("Data values: " + data.GetValue(i));
            }
            Debug.Log("onevent Winner = " + winner);
            Debug.Log("message = " + message);
            Debug.Log("PhotonNetwork.Nickname = " + PhotonNetwork.NickName);

            if (winner == PhotonNetwork.NickName)
            {
                Debug.Log("It matches!");
                Victory();
            }
            else
            {
                Debug.Log(winner + " has lost");

            }
        }
    }
    private void Victory()
    {
        Debug.Log(winner + " is the winner!");
        //  MainMenuButton.SetActive(true);
        //  winSound.Play();
        // QuitButton.SetActive(true);
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
}
