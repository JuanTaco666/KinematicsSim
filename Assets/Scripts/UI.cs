using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public Camera camera;
    public GameObject ballPrefab;
    public GameObject PreBall;
    public Button ballButton;
    public Button pauseButton;
    public Button resetButton;
    public Slider YGravSlider;
    public Slider XGravSlider;
    public GameObject ballPanel;
    public InputField YGravText;
    public InputField XGravText;
    public InputField massInput;
    

    private List<GameObject> balls;
    private bool needBall;
    private bool isPaused;
    private GameObject preball;
    private float cameraHeight;
    private float cameraWidth;
    private float YGravValue;
    private float XGravValue;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = camera.aspect * cameraHeight;
        camera.enabled = true;
        YGravValue = -9.81f;
        XGravValue = 0f;

        balls = new List<GameObject>();
        needBall = false;
        isPaused = false;
        

        Button btnB = ballButton.GetComponent<Button>();
        btnB.onClick.AddListener(creatingBalls);
        Button btnP = pauseButton.GetComponent<Button>();
        btnP.onClick.AddListener(Pause);
        Button btnR = resetButton.GetComponent<Button>();
        btnR.onClick.AddListener(Reset);

        YGravSlider.onValueChanged.AddListener(delegate { updateYGravText(); });
        YGravText.onEndEdit.AddListener(delegate { updateYGravSlider(); });
        updateYGravText();
        XGravSlider.onValueChanged.AddListener(delegate { updateXGravText(); });
        XGravText.onEndEdit.AddListener(delegate { updateXGravSlider(); });
        updateXGravText();

       // massInput.onEndEdit.AddListener(delegate { updateMass(); });
        Pause();

        //camera.aspect = 10;
        //Debug.Log(camera.aspect);

    }

    // Update is called once per frame
    void Update()
    {
        if (needBall)
        {

            float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            preball.transform.position = new Vector3(x, y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                balls.Add(createBall(x, y));
            }
           
        }
      
        
    }

    //when Ball Button Is Clicked
    void creatingBalls()
    {
        needBall = !needBall;
        if (needBall)
        {
            preball = createPreBall(0, 0);
        } else 
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
            Destroy(preball);
        }
    }

    //when Pause Button Is Clicked
    void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseButton.GetComponentInChildren<Text>().text = "Pause";    
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseButton.GetComponentInChildren<Text>().text = "Play";
            if (needBall) ;
        }
        if (needBall)
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
        }
    }

    //when Reset Button Is Clicked
    void Reset()
    {
        YGravValue = -9.81f;
        XGravValue = 0f;
        if (needBall)
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
            Destroy(preball);
        }
        needBall = false;
        isPaused = false;
        YGravSlider.value = YGravValue;
        updateYGravText();
        XGravSlider.value = XGravValue;
        updateXGravText();
        Pause();
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
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
    //-----------------------------------------------
    private void getClickedObject()
    {

    }

    private void updateMass(Ball ball)
    {

    }
    private void updateRadius(Ball ball)
    {

    }
    private void updateFriction(Ball ball)
    {

    }
    private void updateElasticity(Ball ball)
    {

    }
    private void updatePosition(Ball ball)
    {

    }
    private void updateForce(Ball ball)
    {

    }
    private void changeColor(Ball ball)
    {

    }
    //-----------------------------------------------
    private void updateYGravSlider()
    {
        YGravSlider.value = float.Parse(YGravText.text);
        YGravValue = YGravSlider.value;
        ballYGravUpdate();

    }
    private void updateYGravText()
    {
        YGravText.text = YGravSlider.value.ToString();
        YGravValue = float.Parse(YGravText.text);
        ballYGravUpdate();
    }
    private void ballYGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().updateYGrav(XGravValue,YGravValue);
        }
    }
    private void updateXGravSlider()
    {
        XGravSlider.value = float.Parse(XGravText.text);
        XGravValue = XGravSlider.value;
        ballXGravUpdate();

    }
    private void updateXGravText()
    {
        XGravText.text = XGravSlider.value.ToString();
        XGravValue = float.Parse(XGravText.text);
        ballXGravUpdate();
    }
    private void ballXGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().updateYGrav(XGravValue, YGravValue);
        }
    }
    //-----------------------------------------------
    private GameObject createBall(float x,float y)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        ball.transform.position = new Vector3(x, y, -9);
        return ball;
    }
    private GameObject createPreBall(float x,float y)
    {
        GameObject preball = Instantiate(PreBall);
        preball.transform.SetParent(camera.transform);
        preball.transform.position = new Vector3(x, y, 0);
        return preball;
    }

}
