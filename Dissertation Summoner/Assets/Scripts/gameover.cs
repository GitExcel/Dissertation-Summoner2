using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public bool healthUnlimited = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()  //if restart button is clicked on game over then restart the scene
    {
        SceneManager.LoadScene("MainScene");
        
    }

    public void quitthegame () //if quit is pressed close the game
    {
        Application.Quit();
    }

    public void continuewithnohealth () //if pressed continue the game but the player has a bajillion health
    {
        print("unlimited health");
        healthUnlimited = true;


    }
}
