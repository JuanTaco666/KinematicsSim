using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{

    public GameObject ball;
    public GameObject vector;
    public Rigidbody2D rb;
    public PhysicsMaterial2D ballMaterial;
    public Camera camera;

    private static int ballNum = 0;

    private GameObject force;
    private GameObject velocity;
    private CircleCollider2D coll;
    private SpriteRenderer ballRender;
    private Color color;
    private float mass;
    private float radius;
    private float elasticity;
    private float friction;
    
    // Start is called before the first frame update
    void Start()
    {
   
        elasticity = 0.9f;
        friction = 0;
        mass = 1;
        radius = 5;

        name = "ball " + ++ballNum;

        ballRender = GetComponent<SpriteRenderer>();
        color = new Color(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 1);
        ballRender.color = color;

        velocity = InstantiateVector(rb.velocity, "velocity");
        force = InstantiateVector(new Vector2(0, 0), "force");

        coll = GetComponent<CircleCollider2D>();
        MakeMaterial(elasticity,friction);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();
    }

    private void MakeMaterial(float elasticity,float friction)
    {
        PhysicsMaterial2D thiccy = Instantiate(ballMaterial);
        thiccy.bounciness = elasticity;
        thiccy.friction = friction;
        coll.sharedMaterial = thiccy;
    }

    //getter methods
    public float GetX()
    {
        return (ball.transform.position.x);
    }
    public float GetY()
    {
        return (ball.transform.position.y);
    }
    public float GetMass()
    {
        return (mass);
    }
    public float GetRadius()
    {
        return (radius);
    }
    public float GetElasticity()
    {
        return (elasticity);
    }
    public float GetFriction()
    {
        return (friction);
    }
    public GameObject GetVelocity()
    {
        return (velocity);
    }
    public GameObject GetForce()
    {
        return (force);
    }
    public Color GetColor()
    {
        return (color);
    }

    //setter methods 
    public void SetPosition(float x, float y){
        ball.transform.position = new Vector3(x, y, 0);
    }
    public void SetMass(float mass)
    {
        this.mass = mass;
        rb.mass = mass;
    }
    public void SetElasticity(float elasticity)
    {
        this.elasticity = elasticity;
        MakeMaterial(elasticity, friction);
    }
    public void SetFriction(float friction)
    {
        this.friction = friction;
        MakeMaterial(elasticity, friction);
    }
    public void SetRadius(float radius)
    {
        this.radius = radius;
        transform.localScale = new Vector3(radius, radius, 1);
    }
    public void SetVelocity(GameObject velocity)
    {
        this.velocity = velocity;
    }
    public void SetForce(GameObject force)
    {
        this.force = force;
    }
    public void SetColor(Color color)
    {
        this.color = color;
        ballRender.color = color;
    }
    

    //other methods
    void OnMouseDown()
    {
        if (camera == null)
            return;
        if (camera.GetComponent<UI>().GetCurrentBall() != this )
        {
            camera.GetComponent<UI>().OpenUIPanel(this);
           // Debug.Log("Sprite Clicked");   
        }
    }

    private void UpdateVelocity()
    {
        velocity.GetComponent<Vector>().SetVector2(rb.velocity);
    }
    public void UpdateGrav(float XGrav, float YGrav)
    {
        Physics2D.gravity = new Vector2(XGrav, YGrav);
    }

    GameObject InstantiateVector(Vector2 vector, string description) 
    {
        GameObject v = (GameObject)Instantiate(this.vector, new Vector3(GetX(), GetY(), 1), Quaternion.identity);
     
        float angle = Vector2.Angle(new Vector2(1, 0), vector);
        v.transform.Rotate(0, 0, angle, Space.World);
        v.GetComponent<Vector>().SetVector2(vector);
        v.GetComponent<Vector>().SetObject(ball);
        return v;
    }
}
