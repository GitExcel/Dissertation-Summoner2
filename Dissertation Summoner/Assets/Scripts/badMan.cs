using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badMan : MonoBehaviour
{
    public float health = 100;
    public GameObject skillPointStorage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        skillPointStorage = GameObject.FindWithTag("SkillPointStorage");
        
    }

    // Update is called once per frame
    void Update() //this is the bad guy health logic script, vry simple if health is 0 then destory self and add 1 to enemies killed
    {
        if (health <= 0)
        {
           skillPointStorage.GetComponent<skillPointStorage>().enemieskilled +=1;
           Destroy(gameObject);
        }
        
    }
    
}
