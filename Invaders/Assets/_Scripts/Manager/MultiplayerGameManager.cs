using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MultiplayerGameManager : MonoBehaviour
{

    public static MultiplayerGameManager instance;

    private Vector3 spawn1 = new Vector3(-19.38f, -4.8f, 0);
    private Vector3 spawn2 = new Vector3(-15.47f, -4.8f, 0);
    public GameObject playerMain;

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
    }

    void Start()
    {
        PhotonNetwork.Instantiate(playerMain.name, spawn1, Quaternion.identity);
    }

    //If scene has loaded, Create a new playerManager object for each player - sceneIndex 2 refers to map 1 (sceneIndex is the scene order in build settings)
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        /*   if (PhotonNetwork.IsMasterClient)
           {
               InstantiatePlayer("MasterPlayer1", spawn1, "Player");

               PhotonNetwork.Instantiate(player.name, spawn1, Quaternion.identity, 0);
           }// scene.buildIndex == 3 && 
           else if (!PhotonNetwork.IsMasterClient)
           {
               InstantiatePlayer("Player1", P2Spawn1, "Player");

               PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerSelector"), new Vector3(0, 0, 0), Quaternion.identity, 0);
           }
           */
    }

    private void InstantiatePlayer(string playerName, Vector3 spawn, string playerType)
    {
        GameObject player = PhotonNetwork.Instantiate(playerMain.name, spawn, Quaternion.identity);
        player.name = playerName;
    }

}