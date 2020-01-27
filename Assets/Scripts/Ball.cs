using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject vector;
    public Rigidbody2D rb;
    public PhysicsMaterial2D ballMaterial;
    private CircleCollider2D coll;

    

    private GameObject velocity;
    private SpriteRenderer ballRender;
    private Color color;
    private GameObject force;
    private float mass;
    private float radius;
    private float xPos;
    private float yPos;
    private float bounciness;
    private float friction;


    // Start is called before the first frame update
    void Start()
    {
        ballRender = GetComponent<SpriteRenderer>();

        xPos = ball.transform.position.x;
        yPos = ball.transform.position.y;

        bounciness = 1f;
        friction = 0;
        mass = 4;
        radius = 5;
        color = new Color(0, 1, 0, 1);
        velocity = instantiateVector(rb.velocity, "velocity");
        force = instantiateVector(new Vector2(-1, 1), "force");
        ballRender.color = color;

        coll = GetComponent<CircleCollider2D>();
        PhysicsMaterial2D thiccy = Instantiate(ballMaterial);
        thiccy.bounciness = bounciness;
        coll.sharedMaterial = thiccy;

    }

    // Update is called once per frame
    void Update()
    {
        updateVelocity();
        updateRadius();
        updateMass();
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
    public float getMass()
    {
        return (mass);
    }
    public float getRadius()
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
    //setter methods 
    public void setX(float x)
    {
        xPos = x;
    }
    public void setY(float y)
    {
        yPos = yPos;
    }
    public void setMass(float mass)
    {
        this.mass = mass;
    }
    public void setRadius(float radius)
    {
        this.radius = radius;
    }
    public void setVelocity(GameObject velocity)
    {
        this.velocity = velocity;
    }
    public void setForce(GameObject force)
    {
        this.force = force;
    }
    public void setColor(Color color)
    {
        this.color = color;
    }
    //other methods
    private void updateVelocity()
    {
        velocity.GetComponent<Vector>().setVector2(rb.velocity);
    }
    private void updateMass()
    {
        rb.mass = mass;
    }
    private void updateRadius()
    {
        transform.localScale = new Vector3(radius, radius, 1);
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
