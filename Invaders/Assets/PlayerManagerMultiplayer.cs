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

public class PlayerManagerMultiplayer : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    GameObject character;

    int kills;

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

}