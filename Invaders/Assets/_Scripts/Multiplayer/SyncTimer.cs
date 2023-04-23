using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SyncTimer : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    ExitGames.Client.Photon.Hashtable timerValue;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            timerValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            timerValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(timerValue);
        }
        else
        {
            startTime = 0f;
            startTimer = true;
        }
    }

    void Update()
    {
        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
    }
    
}