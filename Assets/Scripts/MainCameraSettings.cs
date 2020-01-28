using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainCameraSettings : MonoBehaviour
{
    public Camera camera;
    public GameObject ballPrefab;
    public GameObject preBall;
    public Button ballButton;

    private GameObject onlyPreball;
    private bool ballPlacing = false;
    private bool preBallPlacing = false;
    private double cameraHeight;
    private double cameraWidth;
    private Transform preballTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = camera.aspect * cameraHeight;
        camera.enabled = true;

        Button btnB = ballButton.GetComponent<Button>();
        btnB.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (preBallPlacing)
        {
            onlyPreball = createPreball();
            preballTransform = onlyPreball.GetComponent<Transform>();
            preBallPlacing = !preBallPlacing;
        }
        if (ballPlacing)
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            Vector2 followthecursor = new Vector3(mouseX, mouseY,0);
            preballTransform.position = followthecursor;//Vector3(mouseX, mouseY, 0);


        }
        if (!ballPlacing)
        {
            Destroy(onlyPreball);
        }

        
    }
    //when Ball Button Is Clicked
    void TaskOnClick()
    {
        ballPlacing = !ballPlacing;
        preBallPlacing = !preBallPlacing;
    }
    //getters
    public double getCamHeight()
    {
        return (cameraHeight);
    }
    public double getCamWidth()
    {
        return (cameraWidth);
    }

    public GameObject createBall()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        return ball;
    }
    public GameObject createPreball()
    {
        GameObject preball = Instantiate(preBall);
        preball.transform.SetParent(camera.transform);
        return preball;
    }

}
