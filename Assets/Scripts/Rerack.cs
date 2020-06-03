using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rerack : MonoBehaviour
{
    public Transform Dcup1;
    public Transform Dcup2;
    public Transform Dcup3;

    public Transform Lcup1;
    public Transform Lcup2;
    public Transform Lcup3;

    public GameObject cupSpawn;


    public GameObject killSpawn;


    GameObject[] cups;
    GameObject[] killBox;
    GameObject[] uiButtons;

    bool rerack2bool = true;
    bool rerack3bool = true;
    public static bool rerackInProgress = false;


    [SerializeField]
    private Canvas[] UI;

    string cupColor;
    // Start is called before the first frame update
    void Start()
    {
        cupColor = "Blue";
        

        //Instantiate(cupSpawn, cup1.position, cup1.rotation);
    }



    // Update is called once per frame
    void Update()
    {
        cups = GameObject.FindGameObjectsWithTag(cupColor + "Cup");
        killBox = GameObject.FindGameObjectsWithTag(cupColor + "Goal");
        uiButtons = GameObject.FindGameObjectsWithTag("UIRerack");


        if (cups.Length == 3 && rerack3bool)
        {
            rerackInProgress = true;

            // option to rerack

            ShowUI(true);

            rerack3bool = false;

            
        }

        if(cups.Length == 2 && rerack2bool)
        {
            Rerack2();
            rerack2bool = false;
        }


    }

    public void Rerack2()
    {

        DestroyCups();
        // rerack when 2
        foreach (var tmp in killBox)
            {
                Destroy(tmp);
            }

            var newCup1 = Instantiate(cupSpawn, Lcup2.position, Lcup2.rotation);
            var newCup2 = Instantiate(cupSpawn, Lcup3.position, Lcup3.rotation);
            var newGoal1 = Instantiate(killSpawn, Lcup2.position, Lcup2.rotation);
            var newGoal2 = Instantiate(killSpawn, Lcup3.position, Lcup3.rotation);

            // rename spawned tags to old tags

            newCup1.tag = cupColor + "Cup";
            newCup2.tag = cupColor + "Cup";
            newCup1.name = cupColor + "Cup7";
            newCup2.name = cupColor + "Cup2";

            newGoal1.tag = cupColor + "Goal";
            newGoal2.tag = cupColor + "Goal";
            newGoal1.name = "7 " + cupColor;
            newGoal2.name = "2 " + cupColor;
    
    }

    public void RerackDiamond3()
    {
        ShowUI(false);
        DestroyCups();

        Instantiate(cupSpawn, Dcup1.position, Dcup1.rotation);
        Instantiate(cupSpawn, Dcup2.position, Dcup2.rotation);
        Instantiate(cupSpawn, Dcup3.position, Dcup3.rotation);

        rerackInProgress = false;
    }

    public void RerackLine3()
    {

        ShowUI(false);
        DestroyCups();
        

        Instantiate(cupSpawn, Lcup1.position, Lcup1.rotation);
        Instantiate(cupSpawn, Lcup2.position, Lcup2.rotation);
        Instantiate(cupSpawn, Lcup3.position, Lcup3.rotation);

        rerackInProgress = false;
    }

    public void RerackNone()
    {
        ShowUI(false);

        rerackInProgress = false;
    }

    void DestroyCups()
    {
        foreach (var tmp in cups)
        {
            Destroy(tmp);
        }
    }

    void ShowUI(bool show)
    {
        foreach (var tmp in UI)
        {
            tmp.enabled = show;
        }
    }

}
