using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraSettings : MonoBehaviour
{
    public Camera camera;

    private double cameraHeight;
    private double cameraWidth;
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = camera.aspect * cameraHeight;
        camera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

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
}
