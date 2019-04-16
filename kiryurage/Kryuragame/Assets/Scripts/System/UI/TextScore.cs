using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour {
    [SerializeField]
    private Text scoretext;
    private int score = 0;

    void Update()
    {
        score = Score.GetScore();
        scoretext.text = "Score:" + score;
    }
}
