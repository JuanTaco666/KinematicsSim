using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    private double xPos;
    private double yPos;
    private Vector2 velocity;
    private double mass;
    private Vector2 force;
    private double radius;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        xPos = ball.transform.position.x;
        yPos = ball.transform.position.y;
        mass = 1;
        radius = 5;
        color = new Color(0, 1, 0, 1);
        velocity = new Vector2(0,0);
        force = new Vector2(0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
