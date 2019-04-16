using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    public static int score = 0;
    public static int hiScore = 0;
    //2位以下のハイスコア表示用
    public static int secondScore = 0;
    public static int thirdScore = 0;
    public static int forthScore = 0;
    public static int fithScore = 0;
    void Start () {
		
	}
	
	void Update () {
        if (score >= fithScore) {
            if (score >= forthScore)
            {
                if(score>=thirdScore)
                {
                    if(score<=secondScore)
                    {
                        if (score >= hiScore) { hiScore = score; }
                        else { secondScore = score; }
                    }

                    else
                    {
                        thirdScore = score;
                    }
                }

                else
                {
                    forthScore = score;
                }
            }

            else
            {
                fithScore = score;
            }
        }
        
        //ゲームオーバーになるとスコアがなくなる
		if(Application.loadedLevelName=="GameOver")
        {
            score = 0;
        }
	}

    //スコアを渡す
    public static int GetScore()
    {
        return score;
    }
    public static int GetHiScore()
    {
        return hiScore;
    }

    public static int GetSecondScore()
    {
        return secondScore;
    }
    public static int GetThirdScore()
    {
        return thirdScore;
    }
    public static int GetForthScore()
    {
        return forthScore;
    }
    public static int GetFithScore()
    {
        return fithScore;
    }


    //スコア加点
    public static void ItemScore1()
    {
        score += 5;
    }
    public static void ItemScore2()
    {
        score += 7;
    }
    public static void OneScore()
    {
        score+=10;
    }

    public static void FiveScore()
    {
        score += 50;
    }

    public static void HundScore()
    {
        score += 100;
    }
}
