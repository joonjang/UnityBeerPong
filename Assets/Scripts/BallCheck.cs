using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour
{

    GameObject[] balls;
    //int numberOfBalls = 0;

    public Transform spawnPos1;
    public Transform spawnPos2;
    public GameObject spawnee;

    Transform spawnSide;

    // Update is called once per frame
    void Update()
    {
        SpawnSide();   

        CheckNumberOfBall();
    }

    void SpawnSide()
    {
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

    void CheckNumberOfBall()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball") ;
        //foreach (var ball in balls)
        //{
        //    var tmp = ball.GetComponent<Rigidbody>();
        //    if (!tmp.useGravity)
        //    {
        //        numberOfBalls++;
        //    }

        //}
        if (balls.Length > 1)
        {
            Destroy(balls[balls.Length - 1]);
            //numberOfBalls = numberOfBalls - 2;

        }
        //else
        //{
        //    numberOfBalls = 0;
        //}

        //if (balls.Length == 0)
        //{
        //    var spawnBall = Instantiate(spawnee, spawnSide.position, spawnSide.rotation);
        //    spawnBall.tag = "Ball";
        //    var tmp = spawnBall.GetComponent<Rigidbody>();
        //    tmp.useGravity = false;
        //    tmp.mass = 1;
        //}
    }
}
