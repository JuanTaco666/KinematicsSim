using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
    public static readonly float yScale = 0.013f;
    public static readonly float xScaleFactor = 0.003f;

    public GameObject vector;

    private GameObject parentObject;
    private Vector2 vectorValue;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = vector.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;

    }

    // Update is called once per frame
    void Update()
    {
        vector.transform.rotation = Quaternion.Euler(0, 0, GetAngle());
        vector.transform.localScale = new Vector3(vectorValue.magnitude * xScaleFactor, yScale, 0);
    }

    public void Add(Vector2 v)
    {
        vectorValue += v;
    }

    public float GetAngle()
    {
        float angle = (float)(Math.Atan2(vectorValue.y, vectorValue.x) / Math.PI * 180); ;

        return angle;
    }

    public float GetMagintude()
    {
        return vectorValue.magnitude;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
    public void SetVector2(Vector2 vector)
    {
        vectorValue = vector;
    }
    public void SetObject(GameObject gObject)
    {
        vector.transform.parent = gObject.transform;
        this.parentObject = gObject;
    }

}
