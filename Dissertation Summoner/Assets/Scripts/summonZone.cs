using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonZone : MonoBehaviour
{
    public int pointNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Summon")
        {
            
            if (pointNum == other.gameObject.GetComponent<Summon>().pointNum)
            {
                
                other.gameObject.GetComponent<Summon>().inZone = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Summon")
        {
            if (pointNum == other.gameObject.GetComponent<Summon>().pointNum)
            {
            
            other.gameObject.GetComponent<Summon>().inZone = false;
            }
            
        }

    }
}


