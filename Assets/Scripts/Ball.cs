using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    private SpriteRenderer ballRender;
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
        ballRender = GetComponent<SpriteRenderer>();
        xPos = ball.transform.position.x;
        yPos = ball.transform.position.y;
        mass = 1;
        radius = 5;
        color = new Color(0, 1, 0, 1);
        velocity = new Vector2(0,0);
        force = new Vector2(0, 0);
        ballRender.color = color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //getter methods
    public double getX()
    {
        return (xPos);
    }
    public double getY()
    {
        return (yPos);
    }
    public double getMass()
    {
        return (mass);
    }
    public double getRadius()
    {
        return (radius);
    }
    public Vector2 getVelocity()
    {
        return (velocity);
    }
    public Vector2 getForce()
    {
        return (force);
    }
    public Color getColor()
    {
        return (color);
    }
}
