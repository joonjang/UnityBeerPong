using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // player1 = false
    // player2 = true
    public static bool player;
    public static bool menuOver;

    public GameObject panel;
    Animator animator;

    // animator tut https://www.youtube.com/watch?v=mz9xfDQ4FCk
    void Start()
    {
        SwipeScript.touchEnabled = false;
        menuOver = false;

        animator = panel.GetComponent<Animator>();
    }


    public void Player1()
    {
        player = false;
        menuOver = true;
        if (animator != null)
        {
            animator.SetBool("Open", true);
        }
        //CloseMenu();
    }
    public void Player2()
    {
        player = true;
        menuOver = true;
        if (animator != null)
        {
            animator.SetBool("Open", true);
        }
        //CloseMenu();
    }

    void CloseMenu()
    {
        GameObject[] menu = GameObject.FindGameObjectsWithTag("StartMenu");
        foreach(var tmp in menu)
        {
            Destroy(tmp);
        }
        
    }

}
