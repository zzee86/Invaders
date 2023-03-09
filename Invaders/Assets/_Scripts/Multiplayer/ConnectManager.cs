using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ConnectManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject connectedPage;
    [SerializeField] private GameObject disconnectedPage;

    public void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        connectedPage.SetActive(true);
    }
}
