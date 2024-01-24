using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Summon : MonoBehaviour
{
    private string behaviourMode = string.Empty;
    public GameObject playerZone;
    public bool inZone = false;
    public GameObject player;
    private Rigidbody r;
    public float speed;
    private CharacterController c;
    private float gravity = -9.81f;
    private Vector3 playerVelocity;
    public Transform facing;
    public Transform lookAtPoint;
    public GameObject point;
    public int pointNum;
    public GameObject aggroZone;
    // Start is called before the first frame update
    void Start()
    {
        behaviourMode = "FOLLOW";
        c = GetComponent<CharacterController>();
        
    }
    private void Awake()
    {
        pointNum = player.gameObject.GetComponent<pointScript>().amountOfPoints;
        point = player.gameObject.GetComponent<pointScript>().points[player.gameObject.GetComponent<pointScript>().amountOfPoints];
        player.gameObject.GetComponent<pointScript>().amountOfPoints += 1;
    }
    private void OnDestroy()
    {
        player.gameObject.GetComponent<pointScript>().amountOfPoints -= 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (behaviourMode == "FOLLOW")
        {
            summonFollow();
        }
        else if (behaviourMode == "ATTACK") 
        {
            summonAttack();
        }

        if (c.isGrounded)
        {
            playerVelocity.y = 0f;
        }

        if (aggroZone.gameObject.GetComponent<aggroRange>().badGuyInRange.Count >= 1)
        {
            behaviourMode = "ATTACK";
        }


        playerVelocity.y += gravity * Time.deltaTime;
        c.Move(playerVelocity);
        
        
        
    }

    private void summonAttack()
    {
        if (aggroZone.gameObject.GetComponent<aggroRange>().badGuyInRange.Count == 0)
        {
            behaviourMode = "FOLLOW";
            
        }
        else
        {
            if (aggroZone.gameObject.GetComponent<aggroRange>().target == null)
            {
                behaviourMode = "FOLLOW";
            }
            print("attacking");
            Vector3 direction = aggroZone.gameObject.GetComponent<aggroRange>().target.transform.position - transform.position;
            c.Move (direction.normalized * (speed * Time.deltaTime));
            

        }
        
    }

    private void summonFollow()
    {
        if (inZone)
        {
            transform.LookAt(new Vector3(lookAtPoint.position.x, transform.position.y, lookAtPoint.position.z));
            print("hello");


        }
        else
        {
            
            
            Vector3 direction = point.transform.position - transform.position;
            
            Vector3 lookDirection = new Vector3 (0f, direction.y, 0f);
            transform.LookAt(new Vector3(point.transform.position.x, transform.position.y, point.transform.position.z));
            c.Move(direction.normalized * (speed * Time.deltaTime));
            
            

        }

        

    }
}
