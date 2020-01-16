using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject vector;
    private Collider coll;

    private GameObject velocity;
    private SpriteRenderer ballRender;
    private Color color;
    private GameObject force;
    private double mass;
    private double radius;
    private float xPos;
    private float yPos;
    private float ballBounciness;
    private float ballFriction;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        ballRender = GetComponent<SpriteRenderer>();

        xPos = ball.transform.position.x;
        yPos = ball.transform.position.y;

        ballBounciness = 1;
        ballFriction = 0;
        mass = 1;
        radius = 5;
        color = new Color(0, 1, 0, 1);
        velocity = instantiateVector(new Vector2(1, 1), "velocity");
        force = instantiateVector(new Vector2(-1, 1), "force");
        ballRender.color = color;

        //coll.material.bounciness = ballBounciness;
        //coll.material.dynamicFriction = ballFriction;
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
    public GameObject getForce()
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
        GameObject v = (GameObject)Instantiate(this.vector, new Vector3(xPos, yPos, 1), Quaternion.identity);
        //GameObject v = (GameObject)Instantiate(this.vector);
        //v.transform.SetParent(ball.transform);
        //Debug.Log(ball.transform.position.x - v.transform.position.x);
        v.transform.Rotate(0, 0, angle, Space.World);
        v.GetComponent<Vector>().setVector2(vector);
        v.GetComponent<Vector>().setObject(ball);

        return v;
    }
}
