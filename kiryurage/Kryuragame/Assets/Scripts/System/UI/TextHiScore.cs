using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHiScore : MonoBehaviour {
    [SerializeField]
    private Text hiScoretext;
    private int hiScore = 0;

    void Update()
    {
        hiScore = Score.GetHiScore();
        hiScoretext.text = "HiScore:" + hiScore;
    }
}
