using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDropBall : MonoBehaviour
{
    public Rigidbody[] balls;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var ball in balls)
        {
            ball.useGravity = false;
        }
    }

    public void DropBalls()
    {
        balls[i].useGravity = true;
        i++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
