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

    private void Awake()
    {
        skillTree.SetActive(false);
        playerUi.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown("r")) {
            Defend();
        }
        if (Input.GetKeyDown("q")) {
            passiveAggressiveText.GetComponent<TextMeshProUGUI>().text = "Aggressive";
            PassiveAggressive = "AGGRESSIVE";
            print("aggressive");
        }
        if (Input.GetKeyDown ("e")) {
            passiveAggressiveText.GetComponent<TextMeshProUGUI>().text = "Passive";
            PassiveAggressive = "PASSIVE";
            print("passive");
            foreach (GameObject gameObject in summons)
            {
                gameObject.GetComponentInChildren<aggroRange2>().target = null;
            }
        }
        if (Input.GetKeyDown("i"))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                
                Cursor.lockState = CursorLockMode.Locked;
                print("deactive");
                skillTree.SetActive(false);
                playerUi.SetActive(true);
            }
            else
            {
                
                Cursor.lockState = CursorLockMode.None;
                print("active");
                skillTree.SetActive(true);
                playerUi.SetActive(false);

            }


        }
        
        
    }

    private void Defend()
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

    private void Attack() { 
        
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
