using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathText;

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
            if (changedProps.ContainsKey("kills"))
            {
                UpdateStats();
            }
        }
    }
    void UpdateStats()
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Debug.Log("player name: " + player.NickName + "kills: " + kills.ToString());
                Debug.Log("other player name: " + player.NickName + "kills: " + player.CustomProperties.TryGetValue("kills", out object kills2));


                if (killsText.text.Equals("3"))
                {
                    Debug.Log("winner: " + player.NickName + " kills: " + kills.ToString());
                    Debug.Log("other winner : " + player.NickName + "kills: " + player.CustomProperties.Values);

                }
            }
        }
    }
}
