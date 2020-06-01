using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipeScript : MonoBehaviour {


    Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

    // Z axis is the blue, goes forwards
    // X and Y is up and sideways

    [SerializeField]
    float throwForceInXandY = 1f; // to control throw force in X and Y directions

    [SerializeField]
    float throwForceInZ = 50f; // to control throw force in Z direction

    public Transform spawnPos;
    public GameObject spawnee;

    Rigidbody rb;
    SphereCollider sc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

            //rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY,  throwForceInZ / timeInterval);
            rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, -direction.y / 125 * throwForceInZ);

            // Destroy ball in 4 seconds
            //Destroy(gameObject, 3f);

        }
    }

    string cupNumber;
    string color;

    public void OnTriggerEnter(Collider other)
    {
        sc = GetComponent<SphereCollider>();
        // gets rid of cup
        // https://stackoverflow.com/questions/52338632/make-an-object-disappear-from-another-object-in-unity-c-sharp

        if (other.gameObject.tag == "Kill")
        {
            //Destroy(this.gameObject);
            //Spawner newBall = new Spawner();
            //newBall.Spawn();

            //DestroyFunction();

            StartCoroutine(DestroyBall());



            Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            //------------------ have ball be unusable and insivislbe for a few seconds after spawn
            StartCoroutine(DestroyBall());
            //Debug.Log("Ball missed " + this.name);
            
        }


        var triggerName = other.gameObject.name;
        color = triggerName.Split(new[] { ' ' }, 2)[1];
        cupNumber = triggerName.Split(new[] { ' ' }, 2)[0];

        if (other.gameObject.tag == "Goal")
        {
            //GameObject ball;
            Destroy(other.gameObject);
            StartCoroutine(Coroutine());
            Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            //sc.material.bounciness = 0.8f;
            //StartCoroutine(Spawnball());
            Debug.Log("Ball sucken " + other.name);
            
            //Destroy(other.gameObject.GetComponent<MeshCollider>());
        }

        sc.material.bounciness = 0.15f;


    }

    IEnumerator Coroutine()
    {
        GameObject objectToDisappear = GameObject.Find(color + "Cup" + cupNumber);
        yield return new WaitForSeconds(1);
        Destroy(objectToDisappear);
        Destroy(this.gameObject);
    }

    IEnumerator DestroyBall()
    {
        // for some reason not spawning instantly allows for the new object spawn to not interfere with past information
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        sc.material.bounciness = 0.8f;
        //Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }
    IEnumerator DelayView()
    {
        yield return new WaitForSeconds(2);

    }

}
