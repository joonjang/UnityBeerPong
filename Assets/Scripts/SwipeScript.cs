﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = System.Random;

public class SwipeScript : MonoBehaviour {


    Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction


    // Z axis is the blue, goes forwards
    // X and Y is up and sideways

    [SerializeField]
    float throwForceInXandY = 1f; // to control throw force in X and Y directions

    [SerializeField]
    float throwForceInZ = 50f; // to control throw force in Z direction

    public Transform spawnPos1;
    public Transform spawnPos2;
    public GameObject spawnee;

    public static bool touchEnabled = true;
    bool computerTurn = false;


    Rigidbody rb;
    SphereCollider sc;
    Stopwatch sw;
    public delegate void CameraView(bool cam);
    public static event CameraView cameraDelegate;
    public static bool GameStart = false;

    bool timerStarted = false;
    TimeSpan elapsedTime;

    GameObject ball;



    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        sw = new Stopwatch();

    }


    

    // Update is called once per frame
    void Update()
    {
        //ball = GameObject.FindGameObjectWithTag("Ball");
        //if (ball != null)
        //{
        //    rb = ball.GetComponent<Rigidbody>();
        //}
        

        if (!rb.useGravity && !computerTurn && GameStart)
        {
            touchEnabled = true;
        }

        if (StartMenu.menuOver)
        {
            touchEnabled = false;
            StartCoroutine(StartDelay());
            //StartMenu.menuOver = true;
        }

        // if ball speed is low for set period of time destroy
        if (Math.Abs(rb.velocity.z) < 4 && !timerStarted && rb.useGravity)
        {

            sw.Start();
            timerStarted = true;

        }
        if (timerStarted)
        {
            elapsedTime = sw.Elapsed;
            if (elapsedTime.TotalSeconds > 2 && rb.useGravity)
            {
                rb.AddForce(200, 0, 0);
                sw.Stop();
                sw.Reset();
                timerStarted = false;
            }

        }

        sc = GetComponent<SphereCollider>();
        // disables touch input when ui is open
        if (!BlueRerack.rerackInProgress && !RedRerack.rerackInProgress && touchEnabled)
        {

            // ------------------- for debugging
            if (StartMenu.player)
            {
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    rb.useGravity = true;
                    rb.AddForce(0, 120, 320);

                    //cameraDelegate(CameraController.camBool);

                }

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    rb.useGravity = true;
                    rb.AddForce(0, 120, 120);

                    //cameraDelegate(CameraController.camBool);

                }

                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    rb.useGravity = true;
                    rb.AddForce(0, 120, -300);
                    //cameraDelegate(CameraController.camBool);

                }
            }
            else /*if (!CameraController.camBool)*/
            {

                // robot code
                if (!CameraController.camBool && !computerTurn)
                {
                                
                    StartCoroutine(ComputerThrow());
                    computerTurn = !computerTurn;
                    touchEnabled = false;
                }

                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    rb.useGravity = true;
                    rb.AddForce(0, 120, 325);
                    computerTurn = false;
                    //cameraDelegate(CameraController.camBool);

                }

                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    rb.useGravity = true;
                    rb.AddForce(20, 120, 325);
                    computerTurn = false;
                    //cameraDelegate(CameraController.camBool);

                }

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    rb.useGravity = true;
                    rb.AddForce(0, 120, 295);
                    computerTurn = false;
                    //cameraDelegate(CameraController.camBool);

                }


            }
            // -----------------------------------------


            // if you touch the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // getting touch position and marking time when you touch the screen
                touchTimeStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            // if you release your finger
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                rb.useGravity = true;
                // marking time when you release it
                touchTimeFinish = Time.time;

                // calculate swipe time interval 
                timeInterval = touchTimeFinish - touchTimeStart;

                // getting release finger position
                endPos = Input.GetTouch(0).position;

                // calculating swipe direction in 2D space
                direction = startPos - endPos;

                // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
                rb.isKinematic = false;

                // if the camera is flipped upside down
                //if (CameraController.camBool)
                //{
                //    // red
                //     y = -direction.y;
                //}
                //else
                //{
                //    // blue
                //    y = direction.y;
                //}

                // reverses the z for when on blue side


                if (StartMenu.player)
                {
                    Single z;
                    if (CameraController.camBool)
                    {
                        // red
                        z = throwForceInZ;
                    }
                    else
                    {
                        // blue
                        z = -throwForceInZ;
                    }

                    // reverses the x for when on blue side
                    Single x;
                    if (CameraController.camBool)
                    {
                        // red
                        x = -direction.x;
                    }
                    else
                    {
                        // blue
                        x = direction.x;
                    }

                    //rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY,  throwForceInZ / timeInterval);
                    rb.AddForce(x * throwForceInXandY, -direction.y * throwForceInXandY, -direction.y / 125 * z);
                }
                else
                {
                    if (!CameraController.camBool && !computerTurn)
                    {
                        
                        StartCoroutine(ComputerThrow());
                        computerTurn = !computerTurn;
                        touchEnabled = false;
                    }
                    computerTurn = false;
                    rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, -direction.y / 125 * throwForceInZ);
                }

                // Destroy ball in 4 seconds
                //Destroy(gameObject, 3f);
                touchEnabled = false;
            }


            sc.material.bounciness = 0.8f;
        }

    }

    string cupNumber;
    string color;
    Transform spawnSide;

    void SwitchCamera()
    {

        // camera switcher

            cameraDelegate(CameraController.camBool);


        if (CameraController.camBool)
        {
            // red
            spawnSide = spawnPos1;

        }
        else
        {
            // blue
            spawnSide = spawnPos2;

        }
    }

    public void OnTriggerEnter(Collider other)
    {

        sc = GetComponent<SphereCollider>();
        

        this.gameObject.tag = "DeadBall";
        var deadBall = GameObject.FindGameObjectWithTag("DeadBall");
        var tmp = deadBall.GetComponent<Rigidbody>();
        tmp.mass = 100;




        SwitchCamera();
        //StartCoroutine(DelayView());

        


        // gets rid of cup
        // https://stackoverflow.com/questions/52338632/make-an-object-disappear-from-another-object-in-unity-c-sharp


        if (other.gameObject.tag == "Kill")
        {

            StartCoroutine(DestroyBall());


        }


        var triggerName = other.gameObject.name;
        color = triggerName.Split(new[] { ' ' }, 2)[1];
        cupNumber = triggerName.Split(new[] { ' ' }, 2)[0];

        if (other.gameObject.tag == color + "Goal")
        {
            //StartCoroutine(TouchDelay());
            //GameObject ball;
            Destroy(other.gameObject);
            StartCoroutine(Coroutine());
            //StartCoroutine(SpawnBall());
            //StartCoroutine(TouchDelay());


            //StartCoroutine(Spawnball());
            UnityEngine.Debug.Log("Ball sucken " + other.name);
            
            //Destroy(other.gameObject.GetComponent<MeshCollider>());
        }

        StartCoroutine(SpawnBall());
        
        sc.material.bounciness = 0.15f;
       

    }

    IEnumerator Coroutine()
    {
        var deadBall = GameObject.FindGameObjectWithTag("DeadBall");
        string objectName = color + "Cup" + cupNumber;
        GameObject objectToDisappear = GameObject.Find(objectName);
        yield return new WaitForSeconds(2);
        Destroy(objectToDisappear);
        Destroy(deadBall);
        UnityEngine.Debug.Log("Coroutine destroy initated: " + objectToDisappear + " and " + this.gameObject);
    }

    IEnumerator ComputerThrow()
    {
        yield return new WaitForSeconds(2);
        Random randNumber = new Random();
        //rb.useGravity = true;
        //rb.AddForce(randNumber.Next(-20,20), randNumber.Next(130,140), randNumber.Next(-310, -280));

        var ballComp = GameObject.FindGameObjectWithTag("Ball");
        if (ballComp != null)
        {
            var tmpComp = ballComp.GetComponent<Rigidbody>();
        
        tmpComp.useGravity = true;
        tmpComp.AddForce(randNumber.Next(-20, 20), randNumber.Next(130, 140), randNumber.Next(-310, -280));
        }
    }
    IEnumerator DestroyBall()
    {
        var deadBall = GameObject.FindGameObjectWithTag("DeadBall");
        // for some reason not spawning instantly allows for the new object spawn to not interfere with past information
        yield return new WaitForSeconds(2);
        Destroy(deadBall);
        sc.material.bounciness = 0.8f;
        //Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }

    IEnumerator TouchDelay()
    {
        // for some reason not spawning instantly allows for the new object spawn to not interfere with past information
        yield return new WaitForSeconds(2);
        touchEnabled = true;

    }

    IEnumerator StartDelay()
    {
        // for some reason not spawning instantly allows for the new object spawn to not interfere with past information
        yield return new WaitForSeconds(2);
        StartMenu.menuOver = false;
        GameStart = true;
    }

    IEnumerator SpawnBall()
    {
        // for some reason not spawning instantly allows for the new object spawn to not interfere with past information
        yield return new WaitForSeconds(1);
        var spawnBall = Instantiate(spawnee, spawnSide.position, spawnSide.rotation);
        spawnBall.tag = "Ball";
        var tmp = spawnBall.GetComponent<Rigidbody>();
        tmp.useGravity = false;
        tmp.mass = 1;
        //rb = spawnBall.GetComponent<Rigidbody>();
        //rb.useGravity = false;
    }



}
