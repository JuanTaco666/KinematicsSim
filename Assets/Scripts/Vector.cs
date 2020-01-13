using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
    public GameObject vector;
    private Vector2 vectorValue;
    private SpriteRenderer spriteRenderer;
    private Color color;

    public Vector(float x, float y) {

        vectorValue = new Vector2(x, y);
        color = Color.blue;

        Instantiate(vector, new Vector3(0, 0, 0), Quaternion.identity);
        spriteRenderer = vector.GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = color;
    }
}
