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

    public InputField YGravText;
    public InputField XGravText;
    public Slider YGravSlider;
    public Slider XGravSlider;

    public Text TimeDisplay;
    public GameObject ballPanel;
    public InputField MassInput;
    public InputField XInput;
    public InputField YInput;
    public InputField RadiusInput;
    public InputField ElasticityInput;
    public InputField FrictionInput;
    public InputField ForceXInput;
    public InputField ForceYInput;
    public Button ColorButton;
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
        Debug.Log(cameraHeight + "    " + cameraHeight);



        balls = new List<GameObject>();
        needBall = false;
        isPaused = false;
        isColorShown = false;
       
        Button btnB = ballButton.GetComponent<Button>();
        btnB.onClick.AddListener(creatingBalls);
        Button btnP = pauseButton.GetComponent<Button>();
        btnP.onClick.AddListener(Pause);
        Button btnR = resetButton.GetComponent<Button>();
        btnR.onClick.AddListener(Reset);
        Button btnC = ColorButton.GetComponent<Button>();
        btnC.onClick.AddListener(ChangeColor);

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

        HideUIPanel(false);

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
        HideUIPanel(false);
        TimeControl.Pause();
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
        EventSystem.current.SetSelectedGameObject(null);
        TimeControl.ResetTime();
    }

     private void UpdateTime(){
         TimeDisplay.text = TimeControl.GetTime().ToString();
         
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
    public void OpenUIPanel(Ball Ball)
    {
        if (!needBall)
        {
            HideUIPanel(true);
            HideUIColorPanel(false);
            currentBall = Ball;
            MassInput.text = (currentBall.GetComponent<Ball>().GetMass()).ToString();
            RadiusInput.text = (currentBall.GetComponent<Ball>().GetRadius()).ToString();
            FrictionInput.text = (currentBall.GetComponent<Ball>().GetFriction()).ToString();
            ElasticityInput.text = (currentBall.GetComponent<Ball>().GetElasticity()).ToString();
            XInput.text = (currentBall.GetComponent<Ball>().GetX()).ToString();
            YInput.text = (currentBall.GetComponent<Ball>().GetY()).ToString();
        }
    }
    
    public void HideUIPanel(bool isShown)
    { 
        ballPanel.SetActive(isShown);
        isColorShown = false;
        HideUIColorPanel(isShown);
    }
    private void HideUIColorPanel(bool isShown)
    {
        ColorPicker.SetActive(isShown);
    }
    private void UpdateMass()
    {
        currentBall.GetComponent<Ball>().SetMass(float.Parse(MassInput.text));
        MassInput.text = (currentBall.GetComponent<Ball>().GetMass()).ToString();
    }
    private void UpdateRadius()
    {
        currentBall.GetComponent<Ball>().SetRadius(float.Parse(RadiusInput.text));
        RadiusInput.text = (currentBall.GetComponent<Ball>().GetRadius()).ToString();
    }
    private void UpdateFriction()
    {
        currentBall.GetComponent<Ball>().SetFriction(float.Parse(FrictionInput.text));
        FrictionInput.text = (currentBall.GetComponent<Ball>().GetFriction()).ToString();
    }
    private void UpdateElasticity()
    {
        currentBall.GetComponent<Ball>().SetElasticity(float.Parse(ElasticityInput.text));
        if (currentBall.GetComponent<Ball>().GetElasticity() > 1){
            currentBall.GetComponent<Ball>().SetElasticity(1);
        }else if (currentBall.GetComponent<Ball>().GetElasticity() < 0){
            currentBall.GetComponent<Ball>().SetElasticity(0);
        }
        ElasticityInput.text = (currentBall.GetComponent<Ball>().GetElasticity()).ToString();
    }
    private void UpdatePosition()
    {
          currentBall.GetComponent<Ball>().SetPosition(float.Parse(XInput.text),float.Parse(YInput.text));
        XInput.text = (currentBall.GetComponent<Ball>().GetX()).ToString();
         YInput.text = (currentBall.GetComponent<Ball>().GetY()).ToString();
    }
    private void UpdateForce()
    {

    }
    void ChangeColor()
    {
        isColorShown = !isColorShown;
        HideUIColorPanel(isColorShown);
        ColorPicker.GetComponent<ColorPicker>().CurrentColor = currentBall.GetComponent<Ball>().GetColor();
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void UpdateColor()
    {
        
        currentBall.GetComponent<Ball>().SetColor(ColorPicker.GetComponent<ColorPicker>().CurrentColor);
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
