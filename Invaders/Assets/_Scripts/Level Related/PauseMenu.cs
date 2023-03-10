using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.RegisterPauseMenu(this);
        panel.SetActive(false);


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            panel.SetActive(isPaused);
        }
    }
    public void ResumeScene()
    {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            panel.SetActive(isPaused);
    }
}
