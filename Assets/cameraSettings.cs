using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSettings : MonoBehaviour
{
    public Camera mainCamera;
    public Camera otherCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        otherCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
