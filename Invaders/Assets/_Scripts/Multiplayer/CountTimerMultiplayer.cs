using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountTimerMultiplayer : MonoBehaviour
{
    float totalGameTime;
    public TextMeshProUGUI timeText;

    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;

        int minutes = (int)(totalGameTime / 60);
        float seconds = totalGameTime % 60f;

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
