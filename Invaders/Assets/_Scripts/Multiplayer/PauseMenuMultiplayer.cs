using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseMenuMultiplayer : MonoBehaviour
{
    bool isPaused;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        MultiplayerGameManager.RegisterPauseMenu(this);
        panel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CheckPaused();
        }
    }
    public void ResumeScene()
    {
        CheckPaused();
    }


    void CheckPaused()
    {
        isPaused = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        panel.SetActive(isPaused);
    }

}


