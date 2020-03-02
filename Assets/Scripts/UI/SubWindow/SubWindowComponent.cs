using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWindowComponent : MonoBehaviour
{

    public RectTransform rectTransform;
    public float height 
    {
        get 
        {
            return rectTransform.rect.height;
        }
        set
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, value);
        }
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetWidth(float width)
    {
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void SetY(float y)
    {
        rectTransform.anchoredPosition = new Vector2(0, y);
    }
}
