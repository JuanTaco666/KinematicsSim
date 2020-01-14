using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
    public GameObject vector;
    private GameObject gObject;
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

    }

    public void setColor(Color color)
    {
        spriteRenderer.color = color;
    }
    public void setVector2(Vector2 vector)
    {
        vectorValue = vector;
    }
    public void setObject(GameObject gObject) 
    {
        this.gObject = gObject;
    }

}
