using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    private List<SubWindowComponent> components;
    public Title title;

    private int width;
    private int height;
    private float componentY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        components = new List<SubWindowComponent>();

        componentY = -40; //default title height
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SetName(string name)
    {
        this.name = name;
        title.title.text = name;
    }

    void SetSize(float width, float height)
    {
        rectTransform.sizeDelta  = new Vector2(width, height);
    }

    public void AddComponent(SubWindowComponent component)
    {
        components.Add(component);

        if(!component.transform.parent.Equals(transform))
            component.gameObject.transform.SetParent(transform);

        component.rectTransform.anchoredPosition = new Vector2(0, -component.height / 2 + componentY);
        component.rectTransform.sizeDelta = new Vector2(-10, component.height);
        componentY -= component.height;
    }
}
