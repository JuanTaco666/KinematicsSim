using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    private static Color randColor;
    public static Color randomColor
    {
        get
        {
            return randColor;
        }
    }

    public static void RandomizeColor()
    {

        randColor = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 0.90f, 0.90f);

    }

}
