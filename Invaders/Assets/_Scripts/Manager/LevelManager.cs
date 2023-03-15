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
    }
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMultiplayer()
    {
        if (PlayerPrefs.GetString("PlayerName") == "")
        {
            SceneManager.LoadScene("Login");
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}



/*
    public static LevelManager instance;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider slider;
    private float _target;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        _target = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingScene.SetActive(true);

        do
        {
            _target = scene.progress;
        }
        while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        loadingScene.SetActive(false);
    }

    void Update()
    {
        slider.value = Mathf.MoveTowards(slider.maxValue, _target, 3 * Time.deltaTime);
        Debug.Log(slider.value);
        WaitForSeconds.Equals("20");
    }
}
*/
