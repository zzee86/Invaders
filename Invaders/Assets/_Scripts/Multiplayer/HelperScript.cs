using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HelperScript : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
//    [SerializeField] double timer = 20;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
    }

    void Update()
    {
        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
    }
}