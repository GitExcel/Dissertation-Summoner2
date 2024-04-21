using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

using UnityEngine;

using UnityEngine.AI;


public class Summon : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
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
    public float health = 100f;
    public float baseHealth;
    public string element = "NONE";
    public GameObject attackBox;
    private bool attackOnCooldown = false;
    public float attackSpeed = 3f;
    private bool needElementReset = false;


    public float baseDamage;
    public float baseSpeed;
    public string baseElement;
    public float baseAttackSpeed;
    public NavMeshAgent agent;
    private bool ranged = false;
    public Material ownMat;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject windBullet;


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
        baseAttackSpeed = attackSpeed;
        baseHealth = health;
        
    }
    private void Awake() //as it needs to be a prefab, needs to get everything itself rather than through editor
    {
        agent = GetComponent<NavMeshAgent>();
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


    IEnumerator attackTimer() //cooldown timer activated when attack goes off, so the guy doesnt just throw 50 attacks instantly
    {
        
        yield return new WaitForSeconds(attackSpeed);
        attackBox.GetComponent<attackBox>().targetIn = false;
        attackOnCooldown = false;
        
    }

    // Update is called once per frame
    void Update() //behaviour, follow or attack
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

        if (aggroZone2.gameObject.GetComponent<aggroRange2>().target != null)
        {
            animatorState = "ATTACK";
            behaviourMode = "ATTACK";
        }


        playerVelocity.y += gravity * Time.deltaTime;
        
        
        
        
        
    }

    private void summonAttack() //the attack logic, decides wether or not to go back to follow or chooses either melee or ranged attack
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

            if (ranged) //if it is ranged, do ranged attack logic
            {
                rangedAttack();
                
            }
            else //if not ranged its melee so do melee logic
            {
                
                meleeAttack();
                

            }

        }
        
    }

    public void EndAttack() //when the attack actually hits, this is linked to an animation event. 
    {
        
        aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<badMan>().health -= damage;  //does the dmg
        if (element == "EARTH") //if the summon is earth, take aggro
        {
            if (aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<skeleton>().target.CompareTag("Summon")) 
            {
                if (aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<skeleton>().target.GetComponent<Summon>().element != "EARTH")
                {
                print("CHANGE TARGET TO EARTH ELEMENT");
                aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<skeleton>().target = transform.gameObject;

                }

            }
            else
            {
                aggroZone2.gameObject.GetComponent<aggroRange2>().target.gameObject.GetComponent<skeleton>().target = transform.gameObject;

            }
            
             
             

        }
        
    }

    private void summonFollow() //follow logic, it follows its "point" around the player
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
            transform.LookAt(new Vector3(agent.nextPosition.x, agent.nextPosition.y, agent.nextPosition.z));
            agent.SetDestination(point.transform.position);
            
            
            

        }

        

    }


    private void rangedAttack() //if they enemy is in clear view, done by a raycast, then instantiate a bullet that flies at the enemy
    {
        ray = Camera.main.ScreenPointToRay(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position);
        Vector3 rayDir = aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position - transform.position;
        
        if (Physics.Raycast(transform.position, rayDir, out hit))
        {
            if (hit.collider.tag == "BADGUY")
            {
                animator.SetBool("isIdle", true);

                agent.isStopped = true;
                agent.ResetPath();
                transform.LookAt(new Vector3(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.x,
                        transform.position.y, aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.z));
                if (attackOnCooldown == false)
                {
                   
                    
                    
                    animator.SetTrigger("triggerRangedAttack");
                    attackOnCooldown = true;
                    StartCoroutine(attackTimer());
                    



                }
                

            }
            else
            {
                animator.SetBool("isIdle", false);
                agent.isStopped = false;
                transform.LookAt(new Vector3(agent.nextPosition.x, agent.nextPosition.y, agent.nextPosition.z));
                agent.SetDestination(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position);
                
            }
        }

    }

    public void fireRangedAttack() //linked to animation frame, when the bullet is actually fired
    {
        print("fireranged");
        var windBulletInstance = Instantiate(windBullet, transform.position, transform.rotation);
        windBulletInstance.GetComponent<summonBullet>().damage = damage;
        windBulletInstance.GetComponent<summonBullet>().target = aggroZone2.gameObject.GetComponent<aggroRange2>().target;

    }

    private void meleeAttack() //melee attack logic. Stuff like making sure they are close
    {
        if (attackBox.GetComponent<attackBox>().targetIn)
        {
            if (attackOnCooldown == false)
            {

                animator.SetTrigger("triggerAttack");
                attackOnCooldown = true;
                StartCoroutine(attackTimer());
            }
            else
            {
                transform.LookAt(new Vector3(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.x,
                transform.position.y, aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.z));

            }

                
        }
        else
        {
            transform.LookAt(new Vector3(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.x,
                transform.position.y, aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position.z));

            Vector3 direction = aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position - transform.position;
            //c.Move(direction.normalized * (speed * Time.deltaTime));
            agent.SetDestination(aggroZone2.gameObject.GetComponent<aggroRange2>().target.transform.position);

        }

    }

    public void elementLogic() //this is called in the element selection screen, applies the logic when a element is selected
    {
        print("element logic");
        if (element == "FIRE")
        {
            damage += 50;
            health -= 50;
            skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(1, 0, 0, 1));

        }
        else if (element == "EARTH")
        {
            health += 100;
            damage -= 25;
            skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(1, 0.74f, 0));

        }
        else if (element == "WIND")
        {
            ranged = true;
            skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(0, 1, 0.49f, 1));
        }
        else if (element == "NONE")
        {
            print("none element");
            skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(1, 1, 1, 1));
        }

        
        
    }

    public void resetElement() //when element is reset remove the bonus stats
    {
        skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(1, 1, 1, 1));
        needElementReset = true;
        if (element == "FIRE")
        {
            damage -= 50;
            health += 50;


        }
        if (element == "EARTH")
        {
            health -= 100;
            damage += 25;


        }
        if (element == "WIND")
        {
            ranged = false;

        }


    }
}
