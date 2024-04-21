using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonAttackZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) //for the skeleton, if the target comes in attack range then allow them to attack
    {
        if (other.gameObject == transform.parent.gameObject.GetComponent<skeleton>().target)
        {
            transform.parent.gameObject.GetComponent<skeleton>().inRange = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other) //if they leave then they cant attack
    {
        if(other.gameObject == transform.parent.gameObject.GetComponent<skeleton>().target)
        {
            transform.parent.gameObject.GetComponent<skeleton>().inRange = false;
        }

    }
}
