using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList
{
    public class Data{

        public readonly Vector2 data;
        public readonly float time;

        public Data(Vector2 data, float time)
        {
            this.data = data;
            this.time = time;
        }

    }

    List<Data> datas;

    public DataList()
    {
        datas = new List<Data>();
    }

    public void Add(Vector2 data)
    {
        datas.Add(new Data(data + new Vector2(0, 0), TimeControl.Time));
    }

    public void Add(float data)
    {
        datas.Add(new Data(new Vector2(data, 0), TimeControl.Time));    
    }

    public Vector2 InstantaneousChange(int index)
    {
        if(index < 0 || index >= datas.Count)
        {
            return new Vector2(0, 0);
        }
        
        Data data1 = Get((index == 0) ? 0 : index - 1);
        Data data2 = Get((index == datas.Count - 1) ? index : index + 1);
        
        return (data2.data - data1.data)/(data2.time - data1.time);
    }

    public Data Get(int index)
    {
        return datas[index];
    }

}
