using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score_handler : MonoBehaviour
{
    public static float score, highScore;

    public Text highScoreText;
    public Text scoreText;
    private int lvNum;
    private string getHighScoreforLvl;



    // Start is called before the first frame update
    void Start()
    {
        lvNum = SceneManager.GetActiveScene().buildIndex;
        getHighScoreforLvl = "highScore" + lvNum;
        highScore = PlayerPrefs.GetFloat(getHighScoreforLvl, 0f);
        if (highScore > 0f)
        {
            float t = highScore;
            string min = ((int)t / 60).ToString();
            int sec = ((int)(t % 60));
            string milSec = ((int)(((t % 60) - sec) * 100)).ToString();
            highScoreText.text ="Best Time: " + min + ":" + sec + ":" + milSec;
        }
        else {
            highScoreText.text = "Best Time: -";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("safeNextLv"))
        {
            score = Player_Timer.timeFloat;
            if (score < highScore || highScore == 0f) {
                PlayerPrefs.SetFloat(getHighScoreforLvl, score);

            }


        }

    }
}
