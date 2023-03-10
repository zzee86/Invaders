using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;


    [SerializeField] private TextMeshProUGUI welcome;

    void Start()
    {
        welcome.text = "Welcome " + PlayerPrefs.GetString("PlayerName");
    }
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    IEnumerator LoadAsynchronously(string sceneName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;

        }

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
