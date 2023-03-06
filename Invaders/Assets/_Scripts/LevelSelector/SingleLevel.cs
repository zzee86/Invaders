using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
    private bool completed;
    public int levelIndex;
public void BackButton(){
    SceneManager.LoadScene("LevelSelection");
}

}
