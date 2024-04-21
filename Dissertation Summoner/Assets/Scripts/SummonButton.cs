using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonButton : MonoBehaviour
{
    public GameObject player;
    public string element = "NONE";
    public bool selected = false;
    public GameObject selectedString;
    public List<GameObject> buttons = new List<GameObject>();
    public int summonNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            selectedFunc();

        }
        else
        {
            notselectedFunc();
        }
        
    }

    public void onFirePress() //if fire is selected then make the summon fire
    {
        element = "FIRE";
        selected = true;
        print("fire");
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().element = element;
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().elementLogic();

    }
    public void onEarthpress () //if earth is pressed make the summon earth
    {
        element = "EARTH";
        selected = true;
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().element = element;
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().elementLogic();

    }

    public void onWindPress() //if wind is pressed make summon wind
    {
        element = "WIND";
        selected = true;
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().element = element;
        player.GetComponent<playerCommands>().summons[summonNum].GetComponent<Summon>().elementLogic();

    }

    private void selectedFunc() //if something has been slected then just display element selected
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);

        }
        selectedString.SetActive(true);
        selectedString.GetComponent<TextMeshProUGUI>().text = element;
        
    }

    private void notselectedFunc() // if not something selected then make the buttons active
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);

        }
        selectedString.SetActive(false);

    }


}
