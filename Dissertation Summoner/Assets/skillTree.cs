using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class skillTree : MonoBehaviour
{
    private int skillPoints = 2;
    private int skillPointsSpent = 0;
    public GameObject skillpointtext;
    public GameObject summon;
    public List<GameObject> tier1Buttons = new List<GameObject>();
    public List<GameObject> tier2Buttons = new List<GameObject>();
    private bool tier1Bought = false;
    private bool tier2Bought = false;
    public GameObject player;
    private int extraSummons = 0;
    private int extraDmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        foreach (GameObject i in tier2Buttons)
        {
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        skillpointtext.GetComponent<TextMeshProUGUI>().text = "Skill Points: " + skillPoints;

        
    }


    public void TierOneFirst() //MAKE EXTRA SUMMON
    {
        if (skillPoints >= 1)
        {
            if (!tier1Bought)
            {
                foreach (GameObject i in tier1Buttons)
                {
                    i.SetActive(false);
                }
                foreach (GameObject i in tier2Buttons)
                {
                    i.SetActive(true);
                }
                var extraSum =Instantiate(summon, summon.transform.position, summon.transform.rotation);
                extraSum.GetComponent<Summon>().inZone = false;
                extraSum.GetComponent<Summon>().damage += extraDmg;
                extraSummons++;
                skillPoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier1Bought = true;
            }
            
        }
        
    }



        public void Tier1Second() //SUMMONS DEAL MORE DMG
    {
        if (skillPoints >= 1)
        {
            if (!tier1Bought)
            {
                foreach (GameObject i in tier1Buttons)
                {
                    i.SetActive(false);
                }
                foreach (GameObject i in tier2Buttons)
                {
                    i.SetActive(true);
                }
                skillPoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier1Bought = true;
                extraDmg += 25;
                foreach (GameObject i in player.GetComponent<playerCommands>().summons)
                {
                    i.GetComponent<Summon>().damage += extraDmg;
                }

            }

        }

    }

    public void TiertwoFirst() //MAKE EXTRA SUMMON 2 electric boogaloo
    {
        if (skillPoints >= 1)
        {
            if (!tier2Bought)
            {
                foreach (GameObject i in tier2Buttons)
                {
                    i.SetActive(false);
                }
                var extraSum = Instantiate(summon, summon.transform.position, summon.transform.rotation);
                extraSum.GetComponent<Summon>().inZone = false;
                extraSum.GetComponent<Summon>().damage += extraDmg;
                extraSummons++;
                skillPoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier2Bought = true;
            }

        }

    }

    public void Reset()
    {
        foreach (GameObject i in player.GetComponent<playerCommands>().summons)
        {
            i.gameObject.GetComponent<Summon>().element = i.gameObject.GetComponent<Summon>().baseElement;
            i.gameObject.GetComponent<Summon>().speed = i.gameObject.GetComponent<Summon>().baseSpeed;
            i.gameObject.GetComponent<Summon>().damage = i.gameObject.GetComponent<Summon>().baseDamage;
        }
        foreach (GameObject i in tier1Buttons)
        {
            i.SetActive(true);
        }
        skillPoints += skillPointsSpent;
        skillPointsSpent = 0;
        tier1Bought = false;
        tier2Bought = false;
        extraDmg = 0;
        for (int i = extraSummons; i > 0; i--)
        {
            Destroy(player.GetComponent<playerCommands>().summons[player.GetComponent<playerCommands>().summons.Count - 1]);
            player.GetComponent<playerCommands>().summons.RemoveAt(player.GetComponent<playerCommands>().summons.Count - 1);
            extraSummons--;



        }
        //player.GetComponent<playerCommands>().summons.Clear();
        //var extraSum = Instantiate(summon, summon.transform.position, summon.transform.rotation);
       // extraSum.GetComponent<Summon>().inZone = false;

    }
}
