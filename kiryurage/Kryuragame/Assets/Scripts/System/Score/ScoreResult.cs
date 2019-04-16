using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private int number;
    private int score;
	

	void Update () {
        if(number==1)
        {
            OneScre();
        }
        if(number==2)
        {
            TwoScre();
        }
        if(number==3)
        {
            ThreeScre();
        }
        if (number == 4)
        {
            ForthScre();
        }
        if(number==5)
        {
            FithScore();
        }
        else
        {
            return;
        }
    }

    void OneScre()
    {
        score = Score.GetHiScore();
        scoreText.text = "一位:" + score;
    }

    void TwoScre()
    {
        score = Score.GetSecondScore();
        scoreText.text = "二位:" + score;
    }

    void ThreeScre()
    {
        score = Score.GetThirdScore();
        scoreText.text = "三位:" + score;
    }

    void ForthScre()
    {
        score = Score.GetForthScore();
        scoreText.text = "四位:" + score;
    }

    void FithScore()
    {
        score = Score.GetFithScore();
        scoreText.text = "五位" + score;
    }
}
