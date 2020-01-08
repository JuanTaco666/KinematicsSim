using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSettings : MonoBehaviour
{
    public Camera mainCamera;
    public Camera otherCamera;

    private double cameraHeight;
    private double cameraWidth;
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = mainCamera.orthographicSize * 2;
        cameraWidth = mainCamera.aspect * cameraHeight;
        mainCamera.enabled = true;
        otherCamera.enabled = false;
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
