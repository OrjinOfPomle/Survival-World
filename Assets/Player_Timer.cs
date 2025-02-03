using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public static float timeFloat;
    public static bool stopTime = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTime)
        {
            float t = Time.time - startTime;
            timeFloat = t;
            string min = ((int)t / 60).ToString();
            int sec = ((int)(t % 60));
            string milSec = ((int)(((t % 60) - sec) * 100)).ToString();
            timerText.text = "Time: " + min + ":" + sec + ":" + milSec;
        }
    }
}
