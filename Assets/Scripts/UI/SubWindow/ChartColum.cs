using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartColum : MonoBehaviour
{
    public GameObject rowPrefab;
    private List<GameObject> rows;
    public Text text;


    public void SetData(DataList datas)
    {
        rows = new List<GameObject>();
     
        text.text = datas.name;

        int yPos = -32;
        foreach(DataList.Data data in datas.datas)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            RectTransform rowRt = row.GetComponent<RectTransform>();
            rowRt.offsetMax = new Vector2(2, rowRt.offsetMax.y);
            rowRt.anchoredPosition = new Vector2(rowRt.anchoredPosition.x, yPos);
            yPos -= 16;

            Text text = row.GetComponentInChildren<Text>();
            text.text = data.data.ToString("0.000");

            rows.Add(row);
        }
    }

}
