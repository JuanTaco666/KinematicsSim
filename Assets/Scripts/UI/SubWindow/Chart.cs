using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : SubWindowComponent
{
    public GameObject columPrefab;
    public GameObject content;

    private List<GameObject> colums;

    void Start()
    {
        colums = new List<GameObject>();
    }

    public void AddData(DataList data)
    {
        GameObject colum = Instantiate(columPrefab, content.transform);
        
        int xPos = 30 + 60 * colums.Count;
        RectTransform columRT = colum.GetComponent<RectTransform>();
        columRT.anchoredPosition = new Vector2(xPos, columRT.anchoredPosition.y);

        colum.GetComponent<ChartColum>().SetData(data);
        colums.Add(colum);
    }

}
