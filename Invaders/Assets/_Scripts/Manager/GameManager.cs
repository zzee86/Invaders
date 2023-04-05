// Game logic is held and controlled with this manager (register gameobjects
// and reference other managers)
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class GameManager : MonoBehaviour
{
    // Public instance to call any method
    public static GameManager current;

    public float deathSequenceDuration = 1.5f;

    List<Orb> orbs;
    Door lockedDoor;
    SceneFader sceneFader;
    public PauseMenu pauseMenu;

    int numberOfDeaths;
    float totalGameTime;
    bool isGameOver;
    bool isPaused;

    public Vector2 lastCheckPoint;

    // Create Singleton
    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        // List of orbs to add and remove them easily
        orbs = new List<Orb>();

        // Persist the object between scene reloads and new scenes
        // And OnSceneLoaded() destroys it when the main menu scene is active
        DontDestroyOnLoad(gameObject);
        // Mute all audio
        AudioListener.pause = true;
    }

    void Update()
    {
        if (isGameOver)
            return;

        totalGameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(totalGameTime);

    }

    public static bool IsGameOver()
    {
        if (current == null)
            return false;

        return current.isGameOver;
    }

    public static void RegisterSceneFader(SceneFader fader)
    {
        if (current == null)
            return;

        // Record the reference
        current.sceneFader = fader;
    }
    public static void RegisterPauseMenu(PauseMenu pause)
    {
        if (current == null)
            return;

        current.pauseMenu = pause;

    }

    public static void RegisterDoor(Door door)
    {
        if (current == null)
            return;

        current.lockedDoor = door;
    }

    public static void RegisterOrb(Orb orb)
    {
        if (current == null)
            return;

        if (!current.orbs.Contains(orb))
            current.orbs.Add(orb);

        UIManager.UpdateOrbUI(current.orbs.Count);
    }


    public static void PlayerGrabbedOrb(Orb orb)
    {
        if (current == null)
            return;

        if (!current.orbs.Contains(orb))
            return;

        current.orbs.Remove(orb);

        if (current.orbs.Count == 0)
            current.lockedDoor.Open();

        UIManager.UpdateOrbUI(current.orbs.Count);
    }

    public static void PlayerDied()
    {
        if (current == null)
            return;

        current.numberOfDeaths++;
        UIManager.UpdateDeathUI(current.numberOfDeaths);

        if (current.sceneFader != null)
            current.sceneFader.FadeSceneOut();

        //Invoke the method after a delay
        current.Invoke("RestartScene", current.deathSequenceDuration);
    }

    public static void PlayerWon()
    {
        if (current == null)
            return;

        current.isGameOver = true;

        // Reference other methods from other game managers
        UIManager.DisplayGameOverText();
        AudioManager.PlayWonAudio();
    }

    void RestartScene()
    {
        // Clear the current number of orbs
        orbs.Clear();

        // Play the scene restart audio
        AudioManager.PlaySceneRestartAudio();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        // Destroy the managers first then load new scene
        StartCoroutine(deleteManagers());
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator deleteManagers()
    {
        Destroy(gameObject);
        UIManager.current.DestroyManager();
        yield return null;

    }
    // OnEnable called everytime the gameobject is called
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Time.timeScale = 1;

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
}