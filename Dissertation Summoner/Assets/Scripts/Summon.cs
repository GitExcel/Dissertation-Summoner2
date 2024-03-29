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
    public GameObject aggroZone2;
    public GameObject playerCommands;
    public float damage = 10f;
    public string element = "NONE";
    public GameObject attackBox;
    private bool attackOnCooldown = false;

    public float baseDamage;
    public float baseSpeed;
    public string baseElement;


    Animator animator;
    private string animatorState;

    // Start is called before the first frame update
    void Start()
    {
        behaviourMode = "FOLLOW";
        c = GetComponent<CharacterController>();
        baseElement = element;
        baseDamage = damage;
        baseSpeed = speed;
        
    }
    private void Awake()
    {
        player = GameObject.Find("PlayerArmature").gameObject;
        lookAtPoint = player.transform.Find("LookatPoint").transform;
        aggroZone = GameObject.Find("aggroCube").gameObject;
        animator = GetComponent<Animator>();
        pointNum = player.gameObject.GetComponent<pointScript>().amountOfPoints;
        point = player.gameObject.GetComponent<pointScript>().points[player.gameObject.GetComponent<pointScript>().amountOfPoints];
        player.gameObject.GetComponent<pointScript>().amountOfPoints += 1;
        player.gameObject.GetComponent<playerCommands>().summons.Add(this.gameObject);
        
    }
    private void OnDestroy()
    {
        player.gameObject.GetComponent<pointScript>().amountOfPoints -= 1;

    }


    IEnumerator attackTimer()
    {
        print("it be starting");
        yield return new WaitForSeconds(3f);
        attackBox.GetComponent<attackBox>().targetIn = false;
        attackOnCooldown = false;
        print("it be done");
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
            animator.SetBool("isIdle", false);
            summonAttack();
        }

        if (c.isGrounded)
        {
            playerVelocity.y = 0f;
        }

        if (aggroZone2.gameObject.GetComponent<aggroRange2>().target != null)
        {
            animatorState = "ATTACK";
            behaviourMode = "ATTACK";
        }


        playerVelocity.y += gravity * Time.deltaTime;
        c.Move(playerVelocity);
        
        
        
        
    }

    private void summonAttack()
    {
        if (aggroZone2.gameObject.GetComponent<aggroRange2>().target == null)
        {
            animatorState = "FOLLOW";
            behaviourMode = "FOLLOW";
            
        }
        else
        {
            if (aggroZone2.gameObject.GetComponent<aggroRange2>().target == null)
            {
                behaviourMode = "FOLLOW";
            }
            
            if (attackBox.GetComponent<attackBox>().targetIn && attackOnCooldown == false )
            {
                print("attacked");
                animator.SetTrigger("triggerAttack");
                attackOnCooldown = true;
                aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<badMan>().health -= damage;
                print(aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<badMan>().health);
                StartCoroutine(attackTimer());


                


            }
            else
            {
                transform.LookAt(new Vector3(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.x, transform.position.y, aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.z));
                Vector3 direction = aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position - transform.position;
                c.Move(direction.normalized * (speed * Time.deltaTime));

            }
            

        }
        
    }

    private void summonFollow()
    {
        if (inZone)
        {
            animator.SetBool("isIdle", true);
            transform.LookAt(new Vector3(lookAtPoint.position.x, transform.position.y, lookAtPoint.position.z));
            


        }
        else
        {
            animator.SetBool("isIdle", false);


            Vector3 direction = point.transform.position - transform.position;
            
            Vector3 lookDirection = new Vector3 (0f, direction.y, 0f);
            transform.LookAt(new Vector3(point.transform.position.x, transform.position.y, point.transform.position.z));
            c.Move(direction.normalized * (speed * Time.deltaTime));
            
            

        }

        

    }
}
