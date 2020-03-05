using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : SubWindowComponent
{
    public Sprite dotSprite;
    public Text horizontalTemplate;
    public Text verticalTemplate;
    public GameObject graphContent;

    private List<GameObject> dots;
    private DataList datas;

    [SerializeField] private int xScale = 100;
    [SerializeField] private int yScale = 30;
    
    void Start()
    {
        Debug.Log(rectTransform.sizeDelta.x);
        for(int xPos = 30; xPos <= rectTransform.rect.width; xPos += xScale)
        {
            Text label = Instantiate(horizontalTemplate, transform);
            label.gameObject.SetActive(true);
            label.text = "" + (xPos - 30) / xScale;
            label.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, 10);
        }

        Debug.Log(rectTransform.sizeDelta.y);
        for(int yPos = 20; yPos <= rectTransform.rect.height - 15; yPos += yScale)
        {
            Text label = Instantiate(verticalTemplate, transform);
            label.gameObject.SetActive(true);
            label.text = "" + (yPos - 20) / yScale;
            label.GetComponent<RectTransform>().anchoredPosition = new Vector2(15, yPos + 7.5f);
        }
    }

    void Update()
    {
        for(int i = dots.Count; i < datas.datas.Count; i++)
        {
            dots.Add(CreateDot(datas.datas[i]));
        }
    }

    public void SetData(DataList datas)
    {
        dots = new List<GameObject>();
        this.datas = datas;
    }

    public GameObject CreateDot(DataList.Data data)
    {
        GameObject dot = new GameObject("dot", typeof(Image));
        dot.transform.SetParent(graphContent.transform, false);
        dot.GetComponent<Image>().sprite = dotSprite;

        RectTransform dotRT = dot.GetComponent<RectTransform>();
        float xPos = data.time * xScale;
        float yPos = data.data * yScale;
        dotRT.anchoredPosition = new Vector2(xPos, yPos);
        
        dotRT.sizeDelta = new Vector2(2.5f, 2.5f);
        dotRT.anchorMin = new Vector2(0, 0);
        dotRT.anchorMax = new Vector2(0, 0); 

        return dot;
    }
}
