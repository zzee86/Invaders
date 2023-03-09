using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public static LoginManager current;

    public GameObject loginPage;
    public GameObject registerPage;
    public TextMeshProUGUI message;

    void Awake()
    {
        //If an LoginManager exists
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
    }


    public void LoginPage()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        message.text = "";
    }

    public void RegisterPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        message.text = "";
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

