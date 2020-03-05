using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : SubWindowComponent
{
    public Sprite dotSprite;
    public GameObject graphContent;
    private List<GameObject> dots;
    private DataList datas;

    [SerializeField] private int xScale = 100;
    [SerializeField] private int yScale = 20;

    // Update is called once per frame
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
