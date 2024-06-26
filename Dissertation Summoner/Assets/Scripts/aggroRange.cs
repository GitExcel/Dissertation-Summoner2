using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class aggroRange : MonoBehaviour
{
    public GameObject target;
    public List<GameObject> badGuyInRange = new List<GameObject>();
    public GameObject player;
    private float dist = 10000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //this script is used by the defend command, gets all enemies close to the player then outs them in a list
    {
        foreach (GameObject obj in badGuyInRange)
        {
            if (obj == null)
            {
                badGuyInRange.Remove(obj);
            }
            var d = (obj.transform.position - player.transform.position).sqrMagnitude;
            if (d < dist)
            {
                target = obj;
                

            }
        }
        badGuyInRange.RemoveAll(x => !x);
        
       
        
    }

    private void OnTriggerEnter(Collider other) //if a badguy enters add it to the list
    {
        if (other.gameObject.tag == "BADGUY")
        {
            
            
            badGuyInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) //if a bad guy leaves remove from list
    {
        if (other.gameObject.tag == "BADGUY")
        {


            badGuyInRange.Remove(other.gameObject);
        }

    }

}
