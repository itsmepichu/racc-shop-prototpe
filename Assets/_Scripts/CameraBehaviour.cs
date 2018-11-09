using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Camera main_camera;
    public float acept_ratio;

    public int hor_resolution;
    public float changeFactor = 47.5f;

    // Use this for initialization
    void Start () {
        main_camera = Camera.main;
        acept_ratio = main_camera.aspect;
        hor_resolution = 500;
        //hor_resolution = Screen.currentResolution.width;

        // Initially game was made on 16:9 aspect ratio enabling this only for 4:3 and other lower aspect ratios
        if(main_camera.aspect < 1.4)
        {
            changeCameraSize_V2();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void changeCameraSize_V2()
    {
        //Debug.Log("screenWidth: " + Screen.width);
        float currentAspect = main_camera.aspect;
        //Debug.Log("currentAspect: " + currentAspect.ToString("F1"));
        if(currentAspect >= 1.6f)
        {
            changeFactor = 41.0f;
        } else
        {
            changeFactor = 47.5f;
        }
        Camera.main.orthographicSize = hor_resolution / currentAspect / changeFactor;
        //Debug.Log("cameraSize: " + Camera.main.orthographicSize.ToString("F1"));
    }
}
