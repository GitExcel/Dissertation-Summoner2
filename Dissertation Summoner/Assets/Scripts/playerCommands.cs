using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerCommands : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject sendTargetHere;
    public List<GameObject> summons = new List<GameObject>();
    public GameObject aggroCube;
    public GameObject player;
    private float dist = 10000;
    public string PassiveAggressive = "AGGRESSIVE";
    public GameObject skillTree;
    public GameObject passiveAggressiveText;
    public GameObject playerUi;
    public GameObject summonControl;
    public GameObject startermenu;
    public GameObject gameover;
    private bool summonControlBool = false;
    private bool skillTreeBool = false;
    private bool startMenuBool = true;
    public bool elementsUnlocked = false;
    private bool dead = false;
    private bool notdead = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        skillTree.SetActive(false);
        summonControl.SetActive(false);
        playerUi.SetActive(false);
        startermenu.SetActive(true);
        
    }

    void Update()
    {

        if (transform.gameObject.GetComponent<playerhealth>().health <= 0 && !dead) //this is logic for the gameover, if health is 0 open gameover screen
        {
            playerUi.SetActive(false) ;
            skillTree.SetActive(false) ;
            startermenu.SetActive(false ) ;
            summonControl.SetActive(false );
            gameover.SetActive(true) ;
            skillTreeBool = true;
            startMenuBool = true;

            Cursor.lockState = CursorLockMode.None;
            dead = true;
        }
        if (gameover.GetComponent<gameover>().healthUnlimited && !notdead) //if the unlimited health button is pressed on the gameover screen, then do that
        {
            print("nohealth mode");
            skillTreeBool = false;
            startMenuBool = false;
            gameover.SetActive(false);
            playerUi.SetActive(true);
            transform.gameObject.GetComponent<playerhealth>().health = 10000;
            Cursor.lockState = CursorLockMode.Locked;
            notdead = true;

        }
        if (Input.GetMouseButtonDown(0)) //if left click is pressed then do attack logic
        {
            Attack();
        }
        if (Input.GetKeyDown("r")) //if r is pressed do defend logic
        {
            
            Defend();
        }
        if (Input.GetKeyDown("backspace")) //backspace kills the player
        {
            transform.gameObject.GetComponent<playerhealth>().health = 0;
            gameover.GetComponent<gameover>().healthUnlimited = false;


            notdead = false;
            dead = false;
        }
        if (Input.GetKeyDown("q")) //q changes stance to aggressive
        {
            passiveAggressiveText.GetComponent<TextMeshProUGUI>().text = "Aggressive";
            PassiveAggressive = "AGGRESSIVE";
            print("aggressive");
        }
        if (Input.GetKeyDown("e")) //e changes stance to passive
        {
            passiveAggressiveText.GetComponent<TextMeshProUGUI>().text = "Passive";
            PassiveAggressive = "PASSIVE";
            print("passive");
            foreach (GameObject gameObject in summons)
            {
                gameObject.GetComponentInChildren<aggroRange2>().target = null;
            }
        }
        if (Input.GetKeyDown("i") && !summonControlBool && !startMenuBool) //i opens and closes the skill tree menu
        {
            if (Cursor.lockState == CursorLockMode.None)
            {

                Cursor.lockState = CursorLockMode.Locked;
                print("deactive");
                skillTree.SetActive(false);
                skillTreeBool = false;
                playerUi.SetActive(true);
            }
            else
            {

                Cursor.lockState = CursorLockMode.None;
                print("active");
                skillTree.SetActive(true);
                skillTreeBool = true;
                playerUi.SetActive(false);

            }


        }
        if (Input.GetKeyDown("o") && !skillTreeBool && elementsUnlocked && !startMenuBool) //o opens the element control menu if it is unlocked
        {
            if (Cursor.lockState == CursorLockMode.None)
            {

                Cursor.lockState = CursorLockMode.Locked;
                print("deactive");
                summonControlBool = false;
                summonControl.SetActive(false);
                playerUi.SetActive(true);
            }
            else
            {

                Cursor.lockState = CursorLockMode.None;
                print("active");
                summonControlBool= true;
                summonControl.SetActive(true);
                playerUi.SetActive(false);

            }
        }
        if (Input.GetKeyDown("h") && !summonControlBool && !skillTreeBool) //h opens the beginning help menu 
        {
            if (startMenuBool)
            {
                startMenuBool = false;
                startermenu.SetActive(false);
                playerUi.SetActive(true);
            }
            else
            {
                startMenuBool= true;
                startermenu.SetActive(true);
                playerUi.SetActive(false);
            }
        }
    }

    private void Defend() //gets the closest enemy to the player then tells all summons to attack them
    {
        print("defending!");
        foreach (GameObject obj in aggroCube.GetComponent<aggroRange>().badGuyInRange)
        {
            if (obj == null)
            {
                aggroCube.GetComponent<aggroRange>().badGuyInRange.Remove(obj);
            }
            var d = (obj.transform.position - this.gameObject.transform.position).sqrMagnitude;
            if (d < dist)
            {
                print("defedning target found!");
                d = dist;
                foreach (GameObject gameObject in summons)
                {
                    gameObject.GetComponentInChildren<aggroRange2>().target = obj;
                }


            }
        }
        dist = 10000;

    }

    private void Attack() { //this makes a raycast to the mousepos then if an enemy is on that, makes all summons target them
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            ///print(hit.collider.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag == "BADGUY")
            {
                foreach (GameObject gameObject in summons)
                {
                    gameObject.GetComponentInChildren<aggroRange2>().target = hit.collider.gameObject;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                print(hit.collider.name);
            }

        }
    }
}
