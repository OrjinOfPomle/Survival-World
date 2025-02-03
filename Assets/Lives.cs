using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    int lives;
    public static int retrys = 4;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {   
        int lastlvl = PlayerPrefs.GetInt("lastlvl", retrys);
        int currentlvl = SceneManager.GetActiveScene().buildIndex;
        retrys = currentlvl*3 + 4;
        if (currentlvl == 0)
        {
            PlayerPrefs.SetInt("lastlvl", currentlvl);
            livesText.text = "Lives: 9000+";
        }
        else {
            lives = PlayerPrefs.GetInt("lives", retrys);
            if (lastlvl != currentlvl)
            {
                PlayerPrefs.SetInt("lastlvl", currentlvl);
                PlayerPrefs.SetInt("lives", retrys);
                lives = retrys;
            }
            else
            {
                PlayerPrefs.SetInt("lives", lives - 1);
                lives -= 1;
            }
            
            livesText.text = "Lives: " + lives;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
