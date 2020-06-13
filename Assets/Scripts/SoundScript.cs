using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public static bool sound = true;

    // Update is called once per frame
    public void TurnOn()
    {
        sound = true;
        var soundOnObject = GameObject.Find("SoundOn");
        var soundOnCanvas = soundOnObject.GetComponent<Canvas>();
        soundOnCanvas.enabled = true;
        var soundOffObject = GameObject.Find("SoundOff");
        var soundOffCanvas = soundOffObject.GetComponent<Canvas>();
        soundOffCanvas.enabled = false;
    }
    public void TurnOff()
    {
        sound = false;
        var soundOnObject = GameObject.Find("SoundOn");
        var soundOnCanvas = soundOnObject.GetComponent<Canvas>();
        soundOnCanvas.enabled = false;
        var soundOffObject = GameObject.Find("SoundOff");
        var soundOffCanvas = soundOffObject.GetComponent<Canvas>();
        soundOffCanvas.enabled = true;
    }
}
