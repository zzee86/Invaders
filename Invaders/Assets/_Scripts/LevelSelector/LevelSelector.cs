using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image unlockImage;

    void Start()
    {
        // PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
    }
    void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
        //   Debug.Log(PlayerPrefs.GetInt("Lv" + 1));
    }
    private void UpdateLevelStatus()
    {
        int previousLevel = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevel.ToString()) > 0)
        {
            unlocked = true;
        }
        //  Debug.Log(PlayerPrefs.GetInt("Lv" + PreviousLevel.ToString()));
    }
    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
        }
    }
    public void PressSelection(string sceneName)
    {
        if (unlocked)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
