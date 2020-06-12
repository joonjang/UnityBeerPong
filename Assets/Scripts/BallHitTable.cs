using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitTable : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource ballHitCupSound;
    void Start()
    {
        ballHitCupSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballHitCupSound.Play();
    }
}
