﻿using System.Collections;
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
    public GameObject PreBall;

    //buttons
    public Button ballButton;
    public Button pauseButton;
    public Button resetButton;
    public Button DeleteBallButton;

    //top ui panel
    public InputField YGravText;
    public InputField XGravText;
    public Slider YGravSlider;
    public Slider XGravSlider;
    public Text TimeDisplay;

    //side panel
    public GameObject ballPanel;
    public GameObject PlaceholderPanel;
    public InputField MassInput;
    public InputField XInput;
    public InputField YInput;
    public InputField RadiusInput;
    public InputField ElasticityInput;
    public InputField FrictionInput;
    public InputField ForceXInput;
    public InputField ForceYInput;
    public InputField BallNameInput;
    public GameObject ColorPicker;

    private Ball currentBall;
    private GameObject preball;
    private List<GameObject> balls;
    private bool needBall;
    private bool isPaused;
    private bool isColorShown;
    private float cameraHeight;
    private float cameraWidth;
    private float YGravValue;
    private float XGravValue;

    public float XScale;
    public float YScale;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = camera.aspect * cameraHeight;
        camera.enabled = true;
        YGravValue = -9.81f;
        XGravValue = 0f;
        Debug.Log(cameraWidth + "    " + cameraHeight);



        balls = new List<GameObject>();
        needBall = false;
        isPaused = false;
        isColorShown = false;
       
        ballButton.onClick.AddListener(creatingBalls);
        pauseButton.onClick.AddListener(Pause);
        resetButton.onClick.AddListener(Reset);
        DeleteBallButton.onClick.AddListener(DeleteBall);

        YGravSlider.onValueChanged.AddListener(delegate { UpdateYGravText(); });
        YGravText.onEndEdit.AddListener(delegate { UpdateYGravSlider(); });
        UpdateYGravText();
        XGravSlider.onValueChanged.AddListener(delegate { UpdateXGravText(); });
        XGravText.onEndEdit.AddListener(delegate { UpdateXGravSlider(); });
        UpdateXGravText();

        MassInput.onEndEdit.AddListener(delegate { UpdateMass(); });
        RadiusInput.onEndEdit.AddListener(delegate { UpdateRadius(); });
        FrictionInput.onEndEdit.AddListener(delegate { UpdateFriction(); });
        ElasticityInput.onEndEdit.AddListener(delegate { UpdateElasticity(); });
        XInput.onEndEdit.AddListener(delegate { UpdatePosition(); });
        YInput.onEndEdit.AddListener(delegate { UpdatePosition(); });
        ForceXInput.onEndEdit.AddListener(delegate { UpdateForce(); });
        ForceYInput.onEndEdit.AddListener(delegate { UpdateForce(); });
        BallNameInput.onEndEdit.AddListener(delegate { UpdateName(); });

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
        
    }

    //when Ball Button Is Clicked
    void creatingBalls()
    {
        needBall = !needBall;
        if (needBall)
        {
            preball = CreatePreBall(0, 0);
        } else 
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
        UpdateYGravText();
        XGravSlider.value = XGravValue;
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
       /*  foreach (GameObject ball in balls)
        {
            if(currentBall == ball.GetComponent<Ball>()){
                Debug.Log(balls.IndexOf(ball));
            }
        }*/
        Destroy(currentBall.ball);
        HideUIPanel();
        ShowPlaceholderPanel();
    }

    private void UpdateName()
    {
        currentBall.name = BallNameInput.text;
    }
     private void UpdateTime(){
        TimeDisplay.text = TimeControl.Time.ToString();
        
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
            ColorPicker.SetActive(true);
            isColorShown = true;
            currentBall = ball;

            MassInput.text = (currentBall.GetMass()).ToString();
            RadiusInput.text = (currentBall.GetRadius()).ToString();
            FrictionInput.text = (currentBall.GetFriction()).ToString();
            ElasticityInput.text = (currentBall.GetElasticity()).ToString();
            XInput.text = (currentBall.GetX()).ToString();
            YInput.text = (currentBall.GetY()).ToString();
            BallNameInput.text = currentBall.name;

            ColorPicker.GetComponent<ColorPicker>().CurrentColor = currentBall.GetColor();
            HidePlaceholderPanel();
        }
    }
    
    public void HideUIPanel()
    { 
        ballPanel.SetActive(false);
        ColorPicker.SetActive(false);
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
        currentBall.SetMass(float.Parse(MassInput.text));
        MassInput.text = (currentBall.GetMass()).ToString();
    }
    private void UpdateRadius()
    {
        currentBall.SetRadius(float.Parse(RadiusInput.text));
        RadiusInput.text = (currentBall.GetRadius()).ToString();
    }
    private void UpdateFriction()
    {
        currentBall.SetFriction(float.Parse(FrictionInput.text));
        FrictionInput.text = (currentBall.GetFriction()).ToString();
    }
    private void UpdateElasticity()
    {
        currentBall.SetElasticity(float.Parse(ElasticityInput.text));
        if (currentBall.GetElasticity() > 1){
            currentBall.SetElasticity(1);
        }else if (currentBall.GetElasticity() < 0){
            currentBall.SetElasticity(0);
        }
        ElasticityInput.text = (currentBall.GetElasticity()).ToString();
    }
    private void UpdatePosition()
    {
        currentBall.SetPosition(float.Parse(XInput.text),float.Parse(YInput.text));
        XInput.text = (currentBall.GetX()).ToString();
        YInput.text = (currentBall.GetY()).ToString();
    }
    private void UpdateForce()
    {

    }
    private void UpdateColor()
    { 
        currentBall.SetColor(ColorPicker.GetComponent<ColorPicker>().CurrentColor);
    }
    public Ball GetCurrentBall()
    {
        return (currentBall);
    }

    //-----------------------------------------------
    private void UpdateYGravSlider()
    {
        YGravSlider.value = float.Parse(YGravText.text);
        YGravValue = YGravSlider.value;
        BallYGravUpdate();

    }
    private void UpdateYGravText()
    {
        YGravText.text = YGravSlider.value.ToString();
        YGravValue = float.Parse(YGravText.text);
        BallYGravUpdate();
    }
    private void BallYGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().UpdateGrav(XGravValue,YGravValue);
        }
    }
    private void UpdateXGravSlider()
    {
        XGravSlider.value = float.Parse(XGravText.text);
        XGravValue = XGravSlider.value;
        BallXGravUpdate();

    }
    private void UpdateXGravText()
    {
        XGravText.text = XGravSlider.value.ToString();
        XGravValue = float.Parse(XGravText.text);
        BallXGravUpdate();
    }
    private void BallXGravUpdate()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().UpdateGrav(XGravValue, YGravValue);
        }
    }

    //-----------------------------------------------
    private GameObject CreateBall(float x,float y)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        ball.transform.position = new Vector3(x, y, -9);
        ball.GetComponent<Ball>().camera = camera;
        return ball;
    }
    private GameObject CreatePreBall(float x,float y)
    {
        GameObject preball = Instantiate(PreBall);
        preball.transform.SetParent(camera.transform);
        preball.transform.position = new Vector3(x, y, 0);
        return preball;
    }

}
