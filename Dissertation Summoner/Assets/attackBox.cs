using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class attackBox : MonoBehaviour
{
    public GameObject summon;
    public GameObject aggroZone2;
    public bool targetIn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        summon = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

   

    private void OnTriggerStay(Collider other)
    {
        //targetIn = false;
        
        if (aggroZone2.gameObject.GetComponent<aggroRange2>().target == other.gameObject)
        {
            
            
            targetIn = true;
        }

    }
}
