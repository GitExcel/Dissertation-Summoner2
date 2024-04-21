using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class summonControl : MonoBehaviour
{

    public List<GameObject> SummonBoxes = new List<GameObject>();
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()  
    {
        foreach (var box in SummonBoxes)
        {
            box.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update() //this sets the correct boxes to be active in the menu
    {
        
        for (int i = 0; i < player.GetComponent<playerCommands>().summons.Count; i++ )
        {
            SummonBoxes[i].SetActive(true);

        }

        


    }


    public void resetButton() //when reset is pressed then do reset logic in the summon
    {
        foreach( var summon in player.GetComponent<playerCommands>().summons)
        {
            summon.GetComponent<Summon>().resetElement();
        }
        foreach (var box in SummonBoxes)
        {
            box.GetComponent<SummonButton>().element = "NONE";
            box.GetComponent<SummonButton>().selected = false;
        }


    }
}
