// All UI is held and controlled with this manager
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Public instance to call any method
    public static UIManager current;
    public TextMeshProUGUI orbText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathText;


    public GameObject gameOverCanvas;

    // Create Singleton
    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        // Persist the object between scene reloads and new scenes
        // And OnSceneLoaded() destroys it when the main menu scene is active
        DontDestroyOnLoad(gameObject);
        current.gameOverCanvas.SetActive(false);

    }

    public static void UpdateOrbUI(int orbCount)
    {
        if (current == null)
            return;

        current.orbText.text = orbCount.ToString();
    }

    public static void UpdateTimeUI(float time)
    {
        if (current == null)
            return;

        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        current.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public static void UpdateDeathUI(int deathCount)
    {
        if (current == null)
            return;

        current.deathText.text = deathCount.ToString();
    }

    public static void DisplayGameOverText()
    {
        if (current == null)
            return;


        // Show gameover panel and pause the game
        current.gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }


    // OnEnable called everytime the gameobject is called
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Destroy GameManager On MainMenu To Replay Completed Level
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene.name to check which scene was loaded
        if (scene.name == "MainMenu")
        {

            // Destroy the GameManager object
            Destroy(gameObject);
        }
    }

    public void DestroyManager()
    {
        Destroy(gameObject);
    }
}
