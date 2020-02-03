using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    static void TogglePause()
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

    public static void Pause()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public static void Play()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public static bool IsPaused()
    {
        return isPaused;
    }
}
