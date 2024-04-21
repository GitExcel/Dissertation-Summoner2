using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonRange : MonoBehaviour
{
    private bool someoneEntered = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //if a player or summon come in range and they dont already have a target, attack the player
    {
        
        if(other.tag == ("Player") || other.tag == ("Summon") && transform.parent.gameObject.GetComponent<skeleton>().target == null)
        {
            if (transform.parent.gameObject.GetComponent<skeleton>().target == null)
            {


                someoneEntered = true;
                transform.parent.gameObject.GetComponent<skeleton>().target = player;
                print("player/summon entered");
            }
    
        }
        
    }

    
}
