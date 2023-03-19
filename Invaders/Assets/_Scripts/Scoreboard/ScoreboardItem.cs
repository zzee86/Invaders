using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathText;
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; //Send event to all clients

    private const int WINNER = 3;
    private string winner;
    Player player;
    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;
        UpdateStats();
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("kills") || changedProps.ContainsKey("deaths"))
            {
                UpdateStats();
                if (killsText.text.Equals("3"))
                {
                    object[] winner = new object[] { PhotonNetwork.NickName, PhotonNetwork.PlayerList[0].ToString() };
                    PhotonNetwork.RaiseEvent(WINNER, winner, raiseEventOptions, SendOptions.SendReliable);
                }
            }
        }
    }
    void UpdateStats()
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();
        }
        if (player.CustomProperties.TryGetValue("deaths", out object deaths))
        {
            deathText.text = deaths.ToString();
        }
    }
}
