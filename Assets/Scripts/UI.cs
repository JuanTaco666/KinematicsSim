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
    private List<GameObject> balls;
    private bool needBall;
    private bool isPaused;
    private bool isColorShown;
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
        isColorShown = false;
       


        Button btnB = ballButton.GetComponent<Button>();
        btnB.onClick.AddListener(creatingBalls);
        Button btnP = pauseButton.GetComponent<Button>();
        btnP.onClick.AddListener(Pause);
        Button btnR = resetButton.GetComponent<Button>();
        btnR.onClick.AddListener(Reset);
        Button btnC = ColorButton.GetComponent<Button>();
        btnC.onClick.AddListener(changeColor);

        YGravSlider.onValueChanged.AddListener(delegate { updateYGravText(); });
        YGravText.onEndEdit.AddListener(delegate { updateYGravSlider(); });
        updateYGravText();
        XGravSlider.onValueChanged.AddListener(delegate { updateXGravText(); });
        XGravText.onEndEdit.AddListener(delegate { updateXGravSlider(); });
        updateXGravText();

        MassInput.onEndEdit.AddListener(delegate { updateMass(); });
        RadiusInput.onEndEdit.AddListener(delegate { updateRadius(); });
        FrictionInput.onEndEdit.AddListener(delegate { updateFriction(); });
        ElasticityInput.onEndEdit.AddListener(delegate { updateElasticity(); });
        XInput.onEndEdit.AddListener(delegate { updatePosition(); });
        YInput.onEndEdit.AddListener(delegate { updatePosition(); });
        ForceXInput.onEndEdit.AddListener(delegate { updateForce(); });
        ForceYInput.onEndEdit.AddListener(delegate { updateForce(); });

        Pause();
        hideUIPanel(false);

        

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
        if (isColorShown)
        {
            updateColor();
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
        EventSystem.current.SetSelectedGameObject(null);
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
        updateYGravText();
        XGravSlider.value = XGravValue;
        updateXGravText();
        Pause();
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
        EventSystem.current.SetSelectedGameObject(null);
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
    public void openUIPanel(Ball Ball)
    {
        if (!needBall)
        {
            hideUIPanel(true);
            hideUIColorPanel(false);
            currentBall = Ball;
            MassInput.text = (currentBall.GetComponent<Ball>().getMass()).ToString();
            RadiusInput.text = (currentBall.GetComponent<Ball>().getRadius()).ToString();
            FrictionInput.text = (currentBall.GetComponent<Ball>().getFriction()).ToString();
        }
    }
    
    public void hideUIPanel(bool isShown)
    { 
        ballPanel.SetActive(isShown);
        isColorShown = false;
        hideUIColorPanel(isShown);
    }
    private void hideUIColorPanel(bool isShown)
    {
        ColorPicker.SetActive(isShown);
    }
    private void updateMass()
    {
        currentBall.GetComponent<Ball>().setMass(float.Parse(MassInput.text));
        MassInput.text = (currentBall.GetComponent<Ball>().getMass()).ToString();
    }
    private void updateRadius()
    {
        currentBall.GetComponent<Ball>().setRadius(float.Parse(RadiusInput.text));
        RadiusInput.text = (currentBall.GetComponent<Ball>().getRadius()).ToString();
    }
    private void updateFriction()
    {
        currentBall.GetComponent<Ball>().setFriction(float.Parse(FrictionInput.text));
        FrictionInput.text = (currentBall.GetComponent<Ball>().getFriction()).ToString();
    }
    private void updateElasticity()
    {

    }
    private void updatePosition()
    {

    }
    private void updateForce()
    {

    }
    void changeColor()
    {
        isColorShown = !isColorShown;
        hideUIColorPanel(isColorShown);

        EventSystem.current.SetSelectedGameObject(null);
    }
    private void updateColor()
    {
        currentBall.GetComponent<Ball>().setColor(ColorPicker.GetComponent<ColorPicker>().CurrentColor);
    }
    public Ball getCurrentBall()
    {
        return (currentBall);
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
            ball.GetComponent<Ball>().updateGrav(XGravValue,YGravValue);
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
            ball.GetComponent<Ball>().updateGrav(XGravValue, YGravValue);
        }
    }
    //-----------------------------------------------
    private GameObject createBall(float x,float y)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.SetParent(camera.transform);
        ball.transform.position = new Vector3(x, y, -9);
        ball.GetComponent<Ball>().camera = camera;
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
