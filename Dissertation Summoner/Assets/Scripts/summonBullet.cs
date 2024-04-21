using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class summonBullet : MonoBehaviour
{

    public GameObject target;
    public float damage;
    
    private float speed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

        
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update() //when created then fly at enemy, if enemy is dead (null) then despawn
    {
        if (target != null)
        {
            var dir = (target.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other) //if hit a bad guy then do dmg to them
    {
        if (other.tag == "BADGUY")
        {
            print("hit");
            other.GetComponent<badMan>().health -= damage;
            Destroy(gameObject);
                
        }
    }
}
