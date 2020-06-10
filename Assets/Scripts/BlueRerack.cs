using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueRerack : MonoBehaviour
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


    bool rerack2bool = true;
    bool rerack3bool = true;
    bool newGame = true;
    public static bool rerackInProgress = false;

    // makes the next turn show the UI
    public static bool rackEnabled = false;

    public static bool nextRack = false;


    [SerializeField]
    private Canvas[] UI;
    [SerializeField]
    private Canvas[] WinUI;

    public GameObject panel;
    Animator animator;

    string cupColor;
    // Start is called before the first frame update
    void Start()
    {
        cupColor = "Blue";

        ShowUI(false);
        //Instantiate(cupSpawn, cup1.position, cup1.rotation);
    }

    enum RackOptions
    {
        ShowUIOptions,
        Rack2,
        NoSelection,
        Winner
    }

    RackOptions rackChoice;

    // Update is called once per frame
    void Update()
    {
        cups = GameObject.FindGameObjectsWithTag(cupColor + "Cup");
        killBox = GameObject.FindGameObjectsWithTag(cupColor + "Goal");

        if (rackEnabled)
        {
            GoThroughRack();
        }

        // todo: make next turn enable the rerack option
        if (cups.Length == 3 && rerack3bool)
        {
            nextRack = true;
            rackChoice = RackOptions.ShowUIOptions;

        }

        if (cups.Length == 2 && rerack2bool)
        {
            nextRack = true;
            rackChoice = RackOptions.Rack2;
        }

        if (cups.Length == 0 && newGame)
        {
            GoThroughRack();
            rackChoice = RackOptions.Winner;
            
        }

    }

    public void GoThroughRack()
    {
        switch (rackChoice)
        {
            case RackOptions.ShowUIOptions:
                rerackInProgress = true;
                StartCoroutine(CorutineUI());
                rerack3bool = false;
                nextRack = false;
                rackEnabled = false;
                rackChoice = RackOptions.NoSelection;
                break;
            case RackOptions.Rack2:
                Rerack2();
                rerack2bool = false;
                nextRack = false;
                rackEnabled = false;
                rackChoice = RackOptions.NoSelection;
                break;
            case RackOptions.Winner:
                BlueWin();
                newGame = false;
                rackEnabled = false;
                nextRack = false;
                rackChoice = RackOptions.NoSelection;
                break;
        }

    }

    void BlueWin()
    {
        //foreach (var tmp in WinUI)
        //{
        //    tmp.enabled = true;
        //}
        animator = panel.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("RedWins", true);
        }
    }

    public void Rerack2()
    {

        DestroyCups();
        // rerack when 2


        var newCup1 = Instantiate(cupSpawn, Lcup2.position, Lcup2.rotation);
        var newCup2 = Instantiate(cupSpawn, Lcup3.position, Lcup3.rotation);
        var newGoal1 = Instantiate(killSpawn, Lcup2.position, Lcup2.rotation);
        var newGoal2 = Instantiate(killSpawn, Lcup3.position, Lcup3.rotation);

        // rename spawned tags to old tags

        newCup1.tag = cupColor + "Cup";
        newCup2.tag = cupColor + "Cup";
        newCup1.name = cupColor + "Cup1";
        newCup2.name = cupColor + "Cup2";

        newGoal1.tag = cupColor + "Goal";
        newGoal2.tag = cupColor + "Goal";
        newGoal1.name = "1 " + cupColor;
        newGoal2.name = "2 " + cupColor;

    }

    public void RerackDiamond3()
    {
        ShowUI(false);
        DestroyCups();

        var newCup1 = Instantiate(cupSpawn, Dcup1.position, Dcup1.rotation);
        var newCup2 = Instantiate(cupSpawn, Dcup2.position, Dcup2.rotation);
        var newCup3 = Instantiate(cupSpawn, Dcup3.position, Dcup3.rotation);
        var newGoal1 = Instantiate(killSpawn, Dcup1.position, Dcup1.rotation);
        var newGoal2 = Instantiate(killSpawn, Dcup2.position, Dcup2.rotation);
        var newGoal3 = Instantiate(killSpawn, Dcup3.position, Dcup3.rotation);

        newCup1.tag = cupColor + "Cup";
        newCup2.tag = cupColor + "Cup";
        newCup3.tag = cupColor + "Cup";
        newCup1.name = cupColor + "Cup1";
        newCup2.name = cupColor + "Cup2";
        newCup3.name = cupColor + "Cup3";

        newGoal1.tag = cupColor + "Goal";
        newGoal2.tag = cupColor + "Goal";
        newGoal3.tag = cupColor + "Goal";
        newGoal1.name = "1 " + cupColor;
        newGoal2.name = "2 " + cupColor;
        newGoal3.name = "3 " + cupColor;

        StartCoroutine(Corutine());
    }

    public void RerackLine3()
    {

        ShowUI(false);
        DestroyCups();


        var newCup1 =  Instantiate(cupSpawn, Lcup1.position, Lcup1.rotation);
        var newCup2 =  Instantiate(cupSpawn, Lcup2.position, Lcup2.rotation);
        var newCup3 =  Instantiate(cupSpawn, Lcup3.position, Lcup3.rotation);
        var newGoal1 = Instantiate(killSpawn, Lcup1.position, Lcup1.rotation);
        var newGoal2 = Instantiate(killSpawn, Lcup2.position, Lcup2.rotation);
        var newGoal3 = Instantiate(killSpawn, Lcup3.position, Lcup3.rotation);

        newCup1.tag = cupColor + "Cup";
        newCup2.tag = cupColor + "Cup";
        newCup3.tag = cupColor + "Cup";
        newCup1.name = cupColor + "Cup1";
        newCup2.name = cupColor + "Cup2";
        newCup3.name = cupColor + "Cup3";

        newGoal1.tag = cupColor + "Goal";
        newGoal2.tag = cupColor + "Goal";
        newGoal3.tag = cupColor + "Goal";
        newGoal1.name = "1 " + cupColor;
        newGoal2.name = "2 " + cupColor;
        newGoal3.name = "3 " + cupColor;

        StartCoroutine(Corutine());
    }

    public void RerackNone()
    {
        ShowUI(false);
        StartCoroutine(Corutine());
    }

    IEnumerator Corutine()
    {
        yield return new WaitForSeconds(1);
        rerackInProgress = false;
    }
    IEnumerator CorutineUI()
    {
        yield return new WaitForSeconds(1);
        ShowUI(true);

    }

    void DestroyCups()
    {
        foreach (var tmp in cups)
        {
            Destroy(tmp);
        }
        foreach (var tmp in killBox)
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
