using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class skillPointStorage : MonoBehaviour
{
    public int skillpoints = 1;
    public int enemieskilled = 0;
    public GameObject skillpointtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //some logic to do with skillpoints and where they are contained for easy access, if 3 enemies are killed award a skill point
    {
        skillpointtext.GetComponent<TextMeshProUGUI>().text = skillpoints.ToString();
        if (enemieskilled >= 3)
        {
            skillpoints++;
            enemieskilled = 0;
        }
        
    }


    public void skillpointgain ()
    {
        skillpoints++;
    }
}
