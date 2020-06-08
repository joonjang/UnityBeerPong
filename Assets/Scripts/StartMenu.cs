using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // player1 = false
    // player2 = true
    public static bool player;
    void Start()
    {
        SwipeScript.touchEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1()
    {
        player = false;
        SwipeScript.touchEnabled = true;
        CloseMenu();
        //StartCoroutine(StartTouch());
    }
    public void Player2()
    {
        player = true;
        SwipeScript.touchEnabled = true;
        CloseMenu();
        //StartCoroutine(StartTouch());
    }

    void CloseMenu()
    {
        GameObject[] menu = GameObject.FindGameObjectsWithTag("StartMenu");
        foreach(var tmp in menu)
        {
            Destroy(tmp);
        }
        
    }

    IEnumerator StartTouch()
    {
        yield return new WaitForSeconds(1);
        SwipeScript.touchEnabled = true;
    }
}
