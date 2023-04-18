using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBtn : MonoBehaviour
{
  bool isVisible;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }


    public void TogglePanel()
    {
        isVisible = !isVisible;
        panel.SetActive(isVisible);
    }
}
