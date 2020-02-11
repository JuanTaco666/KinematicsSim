using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private static bool isPaused = false;
    private static float time = 0;
    public static float Time
    {
        get{
            return time;
        }
    }

    void Start()
    {
        UnityEngine.Time.timeScale = 1;
        isPaused = false;
    }

    void Update()
    {
         time += UnityEngine.Time.deltaTime;
    }

    public static void TogglePause()
    {
        if (isPaused)
        {
            UnityEngine.Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            UnityEngine.Time.timeScale = 0;
            isPaused = true;
        }

    }

    public static void Play()
    {
        UnityEngine.Time.timeScale = 1;
        isPaused = false;
    }

    public static void Pause()
    {
        UnityEngine.Time.timeScale = 0;
        isPaused = true;
    }

    //getters
    public static bool IsPaused()
    {
        return isPaused;
    }

    public static void ResetTime(){
        time = 0;
    }
}
