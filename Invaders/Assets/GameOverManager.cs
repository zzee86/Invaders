using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class GameOverManager : MonoBehaviourPunCallbacks
{
    public static GameOverManager current;
    public TextMeshProUGUI winText;

    PhotonView pv;
    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        DontDestroyOnLoad(gameObject);

        pv = GetComponent<PhotonView>();
    }

    public void DisplayerWinCanvas(string name)
    {
        winText.SetText(name + "has won");
        Debug.Log(name + "has won");

    }

}
