using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject vector;

    private GameObject velocity;
    private SpriteRenderer ballRender;
    private Color color;
    private Vector2 force;
    private double mass;
    private double radius;
    private float xPos;
    private float yPos;

    // Start is called before the first frame update
    void Start()
    {
        ballRender = GetComponent<SpriteRenderer>();
        xPos = ball.transform.position.x;
        yPos = ball.transform.position.y;
        mass = 1;
        radius = 5;
        color = new Color(0, 1, 0, 1);
        velocity = instantiateVector(new Vector2(1, 1), "velocity");
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
    public GameObject getVelocity()
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

    GameObject instantiateVector(Vector2 vector, string description) 
    {
        float angle = Vector2.Angle(new Vector2(1, 0), vector);
        GameObject v = (GameObject)Instantiate(this.vector, new Vector3(xPos, yPos, 0), new Quaternion(0, 0, (float)1, 1));
        v.GetComponent<Vector>().setVector2(vector);
        v.GetComponent<Vector>().setObject(ball);

        return v;
    }
}
