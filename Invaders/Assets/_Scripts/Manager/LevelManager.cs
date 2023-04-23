using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI welcome;

    void Start()
    {
        welcome.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
        Time.timeScale = 1;
    }
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMultiplayer()
    {
    //    if (PlayerPrefs.GetString("PlayerName") == "")
    //    {
            SceneManager.LoadScene("Login");
        // }
        // else
        // {
        //     SceneManager.LoadScene("Lobby");
        // }
    }
}
