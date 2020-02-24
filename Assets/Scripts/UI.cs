using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public Camera camera;

    //prefabs
    public GameObject ballPrefab;
    public GameObject preBall;

    //top ui panel
    public Button timeResetButton;
    public Button pauseButton;
    public Button resetButton;
    public Button ballButton;
    public Button stopAfterTimeButton;
    public Slider yGravSlider;
    public Slider xGravSlider;
    public InputField yGravText;
    public InputField xGravText;
    public InputField stopAfterTimeInput;
    public Text timeDisplay;
    

    //side panel
    public GameObject ballPanel;
    public GameObject PlaceholderPanel;
    public InputField ballNameInput;
    public InputField xInput;
    public InputField yInput;
    public InputField massInput;
    public InputField radiusInput;
    public InputField elasticityInput;
    public InputField frictionInput;
    public InputField forceXInput;
    public InputField forceYInput;
    public Button deleteBallButton;
    public GameObject colorPicker;

    private Ball currentBall;
    private GameObject preball;
    private List<GameObject> balls;
    private bool needBall;
    private bool isPaused;
    private bool isColorShown;
    private float cameraHeight;
    private float cameraWidth;
    private float yGravValue;
    private float xGravValue;
    private float stopTime;
    private bool gettingTime;
    private float timeStopTime;

    public float xScale;
    public float yScale;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = camera.aspect * cameraHeight;
        camera.enabled = true;
        stopTime = 0;
        yGravValue = -9.81f;
        xGravValue = 0f;


        balls = new List<GameObject>();
        needBall = false;
        isPaused = false;
        isColorShown = false;
        gettingTime = true;
        timeStopTime = 0;
       
        ballButton.onClick.AddListener(CreatingBalls);
        pauseButton.onClick.AddListener(Pause);
        resetButton.onClick.AddListener(Reset);
        deleteBallButton.onClick.AddListener(DeleteBall);
        timeResetButton.onClick.AddListener(ResetTime);
        stopAfterTimeButton.onClick.AddListener(StopTimeButton);

        yGravSlider.onValueChanged.AddListener(delegate { UpdateYGravText(); });
        yGravText.onEndEdit.AddListener(delegate { UpdateYGravSlider(); });
        UpdateYGravText();
        xGravSlider.onValueChanged.AddListener(delegate { UpdateXGravText(); });
        xGravText.onEndEdit.AddListener(delegate { UpdateXGravSlider(); });
        UpdateXGravText();
        xGravText.onEndEdit.AddListener(delegate { UpdateXGravSlider(); });
        UpdateXGravText();
        stopAfterTimeInput.onEndEdit.AddListener(delegate { updateStopTime(); });

        massInput.onEndEdit.AddListener(delegate { UpdateMass(); });
        radiusInput.onEndEdit.AddListener(delegate { UpdateRadius(); });
        frictionInput.onEndEdit.AddListener(delegate { UpdateFriction(); });
        elasticityInput.onEndEdit.AddListener(delegate { UpdateElasticity(); });
        xInput.onEndEdit.AddListener(delegate { UpdatePosition(); });
        yInput.onEndEdit.AddListener(delegate { UpdatePosition(); });
        forceXInput.onEndEdit.AddListener(delegate { UpdateForce(); });
        forceYInput.onEndEdit.AddListener(delegate { UpdateForce(); });
        ballNameInput.onEndEdit.AddListener(delegate { UpdateName(); });

        HideUIPanel();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        if (needBall)
        {
            float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            preball.transform.position = new Vector3(x, y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                balls.Add(CreateBall(x, y));
            }
        }
        if (isColorShown)
        {
            UpdateColor();
        }
        StopTimeImplement();
        
    }

    //when Ball Button Is Clicked
    void CreatingBalls()
    {

        needBall = !needBall;
        if (needBall)
        {
            preball = CreatePreBall(0, 0);
            Util.RandomizeColor();
            preball.GetComponent<SpriteRenderer>().color = Util.randomColor;
        } 
        else 
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
            Destroy(preball);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    //when Pause Button Is Clicked
    void Pause()
    {
        TimeControl.TogglePause();

        if (needBall)
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    //when Reset Button Is Clicked
    void Reset()
    {
        yGravValue = -9.81f;
        xGravValue = 0f;
        if (needBall)
        {
            Destroy(balls[balls.Count - 1]);
            balls.RemoveAt(balls.Count - 1);
            Destroy(preball);
        }
        
        needBall = false;
        isPaused = false;
        yGravSlider.value = yGravValue;
        UpdateYGravText();
        xGravSlider.value = xGravValue;
        UpdateXGravText();
        //HideUIPanel(false);
        TimeControl.Pause();
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
        HideUIPanel();
        ShowPlaceholderPanel();
        EventSystem.current.SetSelectedGameObject(null);
        TimeControl.ResetTime();
    }

    void DeleteBall()
    {
        Debug.Log(balls.IndexOf(currentBall.ball));
        balls.Remove(currentBall.ball);
        Destroy(currentBall.ball);
        HideUIPanel();
        ShowPlaceholderPanel();
    }
    void ResetTime(){
        TimeControl.ResetTime();
    }
    private void StopTimeButton(){
        stopAfterTimeInput.text = null;
        stopTime = 0;
    }
    private void updateStopTime(){
        stopTime = float.Parse(stopAfterTimeInput.text);
        //Debug.Log("this is stoptime: " + stopTime);
        if(stopTime == 0){
            stopAfterTimeInput.text = null;
            return;
        }
    }
    private void StopTimeImplement(){
        if (stopTime != 0){
            if(gettingTime){
                gettingTime = false;
                timeStopTime = TimeControl.Time;
            } else {
                if(TimeControl.Time >= (timeStopTime + stopTime)){
                    TimeControl.Time = (timeStopTime + stopTime);
                    StopTimeButton();
                    gettingTime = true;
                    Pause();
                }
            }
        }
    }
    private void UpdateName()
    {
        currentBall.name = ballNameInput.text;
    }
     private void UpdateTime(){
        timeDisplay.text = TimeControl.Time.ToString();
        
        if(TimeControl.IsPaused())
        {
           pauseButton.GetComponentInChildren<Text>().text = "Play";
        }
        else
        {
           pauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
     }


    //getters
    public double GetCamHeight()
    {
        return (cameraHeight);
    }
    public double GetCamWidth()
    {
        return (cameraWidth);
    }

    //-----------------------------------------------
    public void OpenUIPanel(Ball ball)
    {
        if (!needBall)
        {
            ballPanel.SetActive(true);
            colorPicker.SetActive(true);
            isColorShown = true;
            currentBall = ball;

            massInput.text = (currentBall.GetMass()).ToString();
            radiusInput.text = (currentBall.GetRadius()).ToString();
            frictionInput.text = (currentBall.GetFriction()).ToString();
            elasticityInput.text = (currentBall.GetElasticity()).ToString();
            xInput.text = (currentBall.GetX()).ToString();
            yInput.text = (currentBall.GetY()).ToString();
            ballNameInput.text = currentBall.name;

            colorPicker.GetComponent<ColorPicker>().CurrentColor = currentBall.GetColor();
            HidePlaceholderPanel();
        }
    }
    
    public void HideUIPanel()
    { 
        ballPanel.SetActive(false);
        colorPicker.SetActive(false);
        isColorShown = false;
        
    }
    private void ShowPlaceholderPanel()
    { 
        PlaceholderPanel.SetActive(true);
    }
    private void HidePlaceholderPanel()
    { 
        PlaceholderPanel.SetActive(false);
    }
   
    private void UpdateMass()
    {
        currentBall.SetMass(float.Parse(massInput.text));
        massInput.text = (currentBall.GetMass()).ToString();
    }
    private void UpdateRadius()
    {
        currentBall.SetRadius(float.Parse(radiusInput.text));
        radiusInput.text = (currentBall.GetRadius()).ToString();
    }
    private void UpdateFriction()
    {
        currentBall.SetFriction(float.Parse(frictionInput.text));
        frictionInput.text = (currentBall.GetFriction()).ToString();
    }
    private void UpdateElasticity()
    {
        currentBall.SetElasticity(float.Parse(elasticityInput.text));
        if (currentBall.GetElasticity() > 1){
            currentBall.SetElasticity(1);
        }else if (currentBall.GetElasticity() < 0){
            currentBall.SetElasticity(0);
        }
        elasticityInput.text = (currentBall.GetElasticity()).ToString();
    }
    private void UpdatePosition()
    {
        currentBall.SetPosition(float.Parse(xInput.text),float.Parse(yInput.text));
        xInput.text = (currentBall.GetX()).ToString();
        yInput.text = (currentBall.GetY()).ToString();
    }
    private void UpdateForce()
    {

    }
    private void UpdateColor()
    { 
        currentBall.SetColor(colorPicker.GetComponent<ColorPicker>().CurrentColor);
    }
    public Ball GetCurrentBall()
    {
        return (currentBall);
    }

    //-----------------------------------------------
    private void UpdateYGravSlider()
    {
        yGravSlider.value = float.Parse(yGravText.text);
        yGravValue = yGravSlider.value;
        BallYGravUpdate();

    }
    private void UpdateYGravText()
    {
        yGravText.text = yGravSlider.value.ToString();
        yGravValue = float.Parse(yGravText.text);
        BallYGravUpdate();
    }
    private void BallYGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().UpdateGrav(xGravValue,yGravValue);
        }
    }
    private void UpdateXGravSlider()
    {
        xGravSlider.value = float.Parse(xGravText.text);
        xGravValue = xGravSlider.value;
        BallXGravUpdate();

    }
    private void UpdateXGravText()
    {
        xGravText.text = xGravSlider.value.ToString();
        xGravValue = float.Parse(xGravText.text);
        BallXGravUpdate();
    }
    private void BallXGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().UpdateGrav(xGravValue, yGravValue);
        }
    }

    //-----------------------------------------------
    private GameObject CreateBall(float x,float y)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        ball.transform.position = new Vector3(x, y, -9);
        ball.GetComponent<Ball>().camera = camera;

        ball.GetComponent<Ball>().SetColor(Util.randomColor);
        Util.RandomizeColor();
        preball.GetComponent<SpriteRenderer>().color = Util.randomColor;
        return ball;
    }
    private GameObject CreatePreBall(float x,float y)
    {
        GameObject preball = Instantiate(preBall);
        preball.transform.SetParent(camera.transform);
        preball.transform.position = new Vector3(x, y, 0);
        return preball;
    }

}
