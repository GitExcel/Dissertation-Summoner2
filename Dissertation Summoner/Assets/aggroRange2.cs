using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aggroRange2 : MonoBehaviour
{

    public GameObject target = null;
    public GameObject commands;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    private void Awake()
    {
        commands = GameObject.Find("PlayerArmature").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (commands.GetComponent<playerCommands>().PassiveAggressive == "AGGRESSIVE")
        {
            if (other.gameObject.tag == "BADGUY")
            {
                if (target == null)
                {
                    target = other.gameObject;
                    print("TARGET AQUIRRED");
                }
            }



        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (commands.GetComponent<playerCommands>().PassiveAggressive == "AGGRESSIVE")
        {
            if (other.gameObject.tag == "BADGUY")
            {

                if (target == null)
                {
                    target = other.gameObject;
                    print("TARGET AQUIRRED");
                }
            }
        }

    }

}

