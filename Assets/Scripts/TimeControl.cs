using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private static bool isPaused = false;
    public static float time = 0;

    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    void Update()
    {
         time += Time.deltaTime;
    }

    public static void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }

    }

    public static void Play()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    //getters
    public static bool IsPaused()
    {
        return isPaused;
    }

    public static float GetTime(){
        return time;
    }
    public static void ResetTime(){
        time = 0;
    }
}
