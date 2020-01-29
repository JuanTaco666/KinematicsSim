using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainCameraSettings : MonoBehaviour
{
    public Camera camera;
    public GameObject ballPrefab;
    public GameObject PreBall;
    public Button ballButton;

    private bool needBall = false;
    private GameObject preball;
    private double cameraHeight;
    private double cameraWidth;
    
    
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
        if (needBall)
        {

            float x = (Input.mousePosition.x * 0.01258765f * 2);
            float y = (Input.mousePosition.y * 0.025f) - 5;
            preball.transform.position = new Vector3(x, y, 0);
        }
    }
    //when Ball Button Is Clicked
    void TaskOnClick()
    {
        needBall = !needBall;
        if (needBall)
        {
            preball = createPreBall(0, 0);
        } else 
        {
            Destroy(preball);
        }
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

    public GameObject createBall(float x,float y)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        ball.transform.position = new Vector3(x, y, 0);
        return ball;
    }
    public GameObject createPreBall(float x,float y)
    {
        GameObject preball = Instantiate(PreBall);
        preball.transform.SetParent(camera.transform);
        preball.transform.position = new Vector3(x, y, 0);
        return preball;
    }

}
