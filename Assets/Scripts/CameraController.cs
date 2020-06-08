using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // CameraRed
    // position -0.115 3.97 12.355
    //rotation 40 0 0

    // CameraBlue
    // position -0.115 3.97 18.49
    // rotation 40 180 180



    public Transform[] views;
	public float transitionSpeed;
	Transform currentView;
    // cambool false = red
    // cambool true = blue
    public static bool camBool = true;

    
    

    private void Start()
    {
        currentView = views[0];
        SwipeScript.cameraDelegate += SwitchCamera;

    }

    private void OnDestroy()
    {
        SwipeScript.cameraDelegate -= SwitchCamera;
    }


    //https://www.youtube.com/watch?v=EhNzQyGDnHk
    // camera tutorial

    private void Update()
    {

        // for debuging
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentView = views[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentView = views[1];
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentView = views[2];
        }


    }

    public void SwitchCamera(bool camera)
    {
        // if false = CameraRed else true = CameraBlue
        if (StartMenu.player)
        {
            switch (camera)
            {
                case false:
                    //currentView = views[0];
                    StartCoroutine(RedCup());
                    break;
                case true:
                    //currentView = views[2];
                    StartCoroutine(BlueCup());
                    break;
            }
        }

        if (BlueRerack.nextRack)
        {
            BlueRerack.rackEnabled = true;
        }
        if (RedRerack.nextRack)
        {
            RedRerack.rackEnabled = true;
        }
        // for debugging disabled camera switching
        camBool = !camBool;
    }

    IEnumerator RedCup()
    {
        yield return new WaitForSeconds(1);
        currentView = views[0];
    }

    IEnumerator BlueCup()
    {
        yield return new WaitForSeconds(1);
        currentView = views[2];
    }

    private void LateUpdate()
    {
        // Lerp position
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentView.rotation, Time.deltaTime * transitionSpeed);

        //Vector3 currentAngle = new Vector3(
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
        //    );

        //transform.eulerAngles = currentAngle;
        
    }




}
