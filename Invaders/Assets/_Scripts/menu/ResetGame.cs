using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{

    public void FactoryGame()
    {
        PlayerPrefs.DeleteAll();
        gameObject.SetActive(false);
    }
}
