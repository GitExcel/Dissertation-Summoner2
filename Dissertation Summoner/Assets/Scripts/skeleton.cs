using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class skeleton : MonoBehaviour
{
    public GameObject target;
    public bool inRange;
    private bool attackOnCooldown = false;
    private NavMeshAgent agent;

    private Animator a;
    // Start is called before the first frame update
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        a = GetComponent<Animator>();
    }

    IEnumerator attackTimer() //attack cooldown timer, so they dont throw 50 attacks a second
    {
        
        yield return new WaitForSeconds(3);
        
        attackOnCooldown = false;
        
    }

    // Update is called once per frame
    void Update() //if the target is not null then attack that taregt, if no target then go idle
    {

        if (target !=null)
        {
            attack();

        }
        else
        {
            a.SetBool("Mooving", false);

        }
        
    }

    private void attack() //attack logic, use a nav agent to walk towards the target then swing at them
    {
        
        a.SetBool("Mooving", true);

        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));

        if (inRange)
        {
            //transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.y));
            a.SetBool("Mooving", false);
            if (!attackOnCooldown)
            {
                a.SetTrigger("TriggerAttack");
                attackOnCooldown = true;
                StartCoroutine(attackTimer());
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, target.transform.position.y));


            }
            else
            {
                
            }

        }
        else
        {
            agent.SetDestination(target.transform.position);
            transform.LookAt(new Vector3(agent.nextPosition.x, transform.position.y, agent.nextPosition.z));
            a.SetBool("Mooving", true);
            

        }


        

    }

    public void MeleeHit() //called on attack animation event, deal dmg to the guy they hit. Summon health doesnt do anything
    {
        
        if (target.CompareTag("Summon")) {
            target.GetComponent<Summon>().health -= 20;
            print("damage dealt to summon");
        }
        else if (target.CompareTag("Player")) {
            target.GetComponent<playerhealth>().health -= 20;

        }
        inRange = false;



    }
}
