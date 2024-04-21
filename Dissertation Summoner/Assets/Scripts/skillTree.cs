using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class skillTree : MonoBehaviour
{
    public int skillPoints = 1;
    private int skillPointsSpent = 0;
    public GameObject skillpointtext;
    public GameObject skillpointtext2;
    public GameObject summon;
    public List<GameObject> tier1Buttons = new List<GameObject>();
    public List<GameObject> tier2Buttons = new List<GameObject>();
    public List<GameObject> tier3Buttons = new List<GameObject>();
    public List<GameObject> tier4Buttons = new List<GameObject>();
    private bool tier1Bought = false;
    private bool tier2Bought = false;
    private bool tier3Bought = false;
    private bool tier4Bought = false;
    public GameObject player;
    private int extraSummons = 0;
    private int extraDmg;
    private int extraSpeed;
    private float extraAttackSpeed;
    public GameObject summonpoint;
    public GameObject summonControl;
    public GameObject SkillPointCounter;
   
    

    
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
        foreach (GameObject i in tier3Buttons)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in tier4Buttons)
        {
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() //update text to display amount of skill points
    {
        skillpointtext.GetComponent<TextMeshProUGUI>().text = "Skill Points: " + SkillPointCounter.GetComponent<skillPointStorage>().skillpoints;
        

    }


    public void TierOneFirst() //MAKE EXTRA SUMMON
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
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
                createSummon();
                
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier1Bought = true;
            }
            
        }
        
    }



        public void Tier1Second() //SUMMONS DEAL MORE DMG
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
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
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
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

    public void TiertwoFirst() //MAKE EXTRA SUMMON TIER 2
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
        {
            if (!tier2Bought)
            {
                foreach (GameObject i in tier2Buttons)
                {
                    i.SetActive(false);
                }
                foreach (GameObject i in tier3Buttons)
                {
                    i.SetActive(true);
                }
                createSummon();
              
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier2Bought = true;
            }

        }

    }

    public void Tier2Second() //SUMMONS GO FASTER
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
        {
            if (!tier2Bought)
            {
                
                foreach (GameObject i in tier2Buttons)
                {
                    i.SetActive(false);
                }

                foreach (GameObject i in tier3Buttons)
                {
                    i.SetActive(true);
                }
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier2Bought = true;
                extraSpeed += 2;
                foreach (GameObject i in player.GetComponent<playerCommands>().summons)
                {
                    i.GetComponent<Summon>().speed += extraSpeed;
                }

            }

        }

    }

    public void Tier3First() //EXTRA SUMM
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
        {
            if (!tier3Bought)
            {

                foreach (GameObject i in tier3Buttons)
                {
                    i.SetActive(false);
                }
                foreach (GameObject i in tier4Buttons)
                {
                    i.SetActive(true);
                }
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier3Bought = true;
                createSummon();
                

            }

        }

    }

    public void Tier3Second() //SUMMONS ATTACK FASTER
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
        {
            if (!tier3Bought)
            {

                foreach (GameObject i in tier3Buttons)
                {
                    i.SetActive(false);
                }
                foreach (GameObject i in tier4Buttons)
                {
                    i.SetActive(true);
                }
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier3Bought = true;
                extraAttackSpeed += 1.5f;
                foreach (GameObject i in player.GetComponent<playerCommands>().summons)
                {
                    i.GetComponent<Summon>().attackSpeed -= extraAttackSpeed;
                }

            }

        }

    }

    public void Tier4Elements() //UNLOCK ELEMENTS
    {
        if (SkillPointCounter.GetComponent<skillPointStorage>().skillpoints >= 1)
        {
            if (!tier4Bought)
            {

                foreach (GameObject i in tier4Buttons)
                {
                    i.SetActive(false);
                }
                
                SkillPointCounter.GetComponent<skillPointStorage>().skillpoints--;
                skillPointsSpent++;
                print("skill point spent");
                tier4Bought = true;
                player.GetComponent<playerCommands>().elementsUnlocked = true;
                
                

            }

        }

    }



    private void createSummon() //logic for creating a new summon
    {
        var extraSum = Instantiate(summon, summonpoint.transform.position, summonpoint.transform.rotation);
        extraSum.GetComponent<Summon>().inZone = false;
        extraSum.GetComponent<Summon>().damage += extraDmg;
        extraSum.GetComponent<Summon>().speed += extraSpeed;
        extraSum.GetComponent<Summon>().attackSpeed -= extraAttackSpeed;
        extraSummons++;

    }

    public void Reset() //when the rest button is pressed account for everything then reset it back to normal
    {
        player.GetComponent<playerCommands>().elementsUnlocked = false;
        summonControl.GetComponent<summonControl>().resetButton();
        
        foreach (GameObject i in player.GetComponent<playerCommands>().summons)
        {
            i.gameObject.GetComponent<Summon>().element = i.gameObject.GetComponent<Summon>().baseElement;
            i.gameObject.GetComponent<Summon>().speed = i.gameObject.GetComponent<Summon>().baseSpeed;
            i.gameObject.GetComponent<Summon>().damage = i.gameObject.GetComponent<Summon>().baseDamage;
            i.gameObject.GetComponent<Summon>().attackSpeed = i.gameObject.GetComponent<Summon>().baseAttackSpeed;
        }
        foreach (GameObject i in tier1Buttons)
        {
            i.SetActive(true);
        }
        foreach (GameObject i in tier2Buttons)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in tier3Buttons)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in tier4Buttons)
        {
            i.SetActive(false);
        }
        SkillPointCounter.GetComponent<skillPointStorage>().skillpoints += skillPointsSpent;
        skillPointsSpent = 0;
        tier1Bought = false;
        tier2Bought = false;
        tier3Bought = false;
        tier4Bought = false;
        extraDmg = 0;
        extraSpeed = 0;
        extraAttackSpeed = 0;
        for (int i = extraSummons; i > 0; i--)
        {
            Destroy(player.GetComponent<playerCommands>().summons[player.GetComponent<playerCommands>().summons.Count - 1]);
            player.GetComponent<playerCommands>().summons.RemoveAt(player.GetComponent<playerCommands>().summons.Count - 1);
            extraSummons--;



        }


        

    }
}
