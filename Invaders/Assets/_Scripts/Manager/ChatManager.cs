using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    #region Setup
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username;

    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }

    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
    }

    #endregion Setup

    #region General

    [SerializeField] GameObject chatPanel;
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDisplay;
    string privateReceiver = "";
    string currentChat;

    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }
    }

    #endregion General

    #region PublicChat

    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "")
        {
            currentChat = username + ": " + currentChat;
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    #endregion PublicChat

    #region Callbacks

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnChatStateChange(ChatState state)
    {
        if (state == ChatState.Uninitialized)
        {
            isConnected = false;
            joinChatButton.SetActive(true);
            chatPanel.SetActive(false);
        }

    }

    public void OnConnected()
    {
        joinChatButton.SetActive(false);
        username = PhotonNetwork.NickName;
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        isConnected = false;
        chatPanel.SetActive(false);
        joinChatButton.SetActive(true);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string message = "";
        for (int i = 0; i < senders.Length; i++)
        {
            message = string.Format("{0}", messages[i]);
            chatDisplay.text += "\n" + message;
        }

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }
    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    #endregion Callbacks
}

