using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    public GameObject healthtext;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //this is a container for the health, this shows it in hud
    {
        healthtext.GetComponent<TextMeshProUGUI>().text = health.ToString();


    }
}
