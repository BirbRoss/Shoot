using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{

    public float TimeLeft = 3.0f;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerTxt;

    public PlayerShoot shootScript;

    // Start is called before the first frame update
    void Start()
    {
        //Disable later this is just for testing
        TimerOn = true;
        shootScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimerOn = false;
                TimeLeft = 0;
                shootScript.enabled = true;
                TimerTxt.text = "DRAW!";
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        TimerTxt.text = currentTime.ToString("0");
    }
}
