using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public int levelIndex;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerPrefs.SetInt("Lv" + levelIndex, levelIndex);
            Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, levelIndex));
        }
    }
}

