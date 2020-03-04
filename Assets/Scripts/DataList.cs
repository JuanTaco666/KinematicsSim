using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList
{
    public class Data{

        public readonly float data;
        public readonly float time;

        public Data(float data, float time)
        {
            this.data = data;
            this.time = time;
        }

    }

    public List<Data> datas;
    public string name;

    public DataList(string name)
    {
        datas = new List<Data>();
        this.name = name;
    }

    public void Add(Vector2 data)
    {
        datas.Add(new Data(data.magnitude, TimeControl.Time));    
    }

    public void Add(float data)
    {
        datas.Add(new Data(data, TimeControl.Time));    
    }

    public float InstantaneousChange(int index)
    {
        if(index < 0 || index >= datas.Count)
        {
            return 0;
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
