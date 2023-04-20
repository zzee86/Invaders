using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountTimerMultiplayer : MonoBehaviour
{
    float totalGameTime;
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI startPrompt;
    private float timeRemaining = 5;

    void Start()
    {
        startPrompt.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;

        int minutes = (int)(totalGameTime / 60);
        float seconds = totalGameTime % 60f;

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            startPrompt.enabled = false;
        }
    }
}
